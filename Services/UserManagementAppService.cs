using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using ABPPermission.Services.Dtos;
using System.Collections.Generic;

namespace ABPPermission.Services;

public class UserManagementAppService : ApplicationService, IUserManagementAppService
{
	private readonly IdentityUserManager _userManager;
	private readonly IIdentityUserRepository _userRepository;

	public UserManagementAppService(IdentityUserManager userManager, IIdentityUserRepository userRepository)
	{
		_userManager = userManager;
		_userRepository = userRepository;
	}

	public async Task<InitUsersResultDto> InitializeHundredUsersAsync()
	{
		const string defaultPassword = "Aa12345@@"; // có chữ thường để đạt yêu cầu chính sách
		int created = 0, reset = 0;

		for (int i = 1; i <= 100; i++)
		{
			var username = $"username{i}";
			var email = $"{username}@local.test";

			// dùng UserManager để tìm theo tên, tránh Users IQueryable
			var existing = await _userManager.FindByNameAsync(username);
			if (existing != null)
			{
				var resetToken = await _userManager.GeneratePasswordResetTokenAsync(existing);
				var resetRes = await _userManager.ResetPasswordAsync(existing, resetToken, defaultPassword);
				if (!resetRes.Succeeded)
				{
					var err = string.Join("; ", resetRes.Errors.Select(e => $"{e.Code}:{e.Description}"));
					throw new BusinessException("User.PasswordResetFailed").WithData("UserName", username).WithData("Errors", err);
				}
				reset++;
				continue;
			}

			var user = new IdentityUser(GuidGenerator.Create(), username, email)
			{
				Name = username,
				Surname = "auto"
			};

			var createRes = await _userManager.CreateAsync(user, defaultPassword);
			if (!createRes.Succeeded)
			{
				var err = string.Join("; ", createRes.Errors.Select(e => $"{e.Code}:{e.Description}"));
				throw new BusinessException("User.CreateFailed").WithData("UserName", username).WithData("Errors", err);
			}
			created++;
		}

		return new InitUsersResultDto { Created = created, PasswordReset = reset };
	}

	public async Task<FillDobResultDto> FillDateOfBirthForNullAsync()
	{
		var rng = new Random();
		// lấy tất cả users qua repository
		List<IdentityUser> users = await _userRepository.GetListAsync();
		int updated = 0;

		foreach (var user in users)
		{
			object? val = null;
			var hasDob = user.ExtraProperties != null && user.ExtraProperties.TryGetValue("DateOfBirth", out val) && val != null;
			if (hasDob)
			{
				continue;
			}

			var year = rng.Next(1988, 2006);
			var month = rng.Next(1, 13);
			var day = rng.Next(1, 29);
			var dob = new DateTime(year, month, day);

			user.ExtraProperties["DateOfBirth"] = dob;

			var res = await _userManager.UpdateAsync(user);
			if (!res.Succeeded)
			{
				var err = string.Join("; ", res.Errors.Select(e => $"{e.Code}:{e.Description}"));
				throw new BusinessException("User.UpdateFailed").WithData("UserId", user.Id).WithData("Errors", err);
			}
			updated++;
		}

		return new FillDobResultDto { Updated = updated };
	}
}
