using System;
using System.Threading.Tasks;
using ABPPermission.Entities.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.PermissionManagement;
using ABPPermission.Permissions;

namespace ABPPermission.Data;

public class PermissionDataSeederContributor
	: IDataSeedContributor, ITransientDependency
{
	private readonly IRepository<Book, Guid> _bookRepository;
	private readonly IPermissionManager _permissionManager;

	public PermissionDataSeederContributor(IRepository<Book, Guid> bookRepository, IPermissionManager permissionManager)
	{
		_bookRepository = bookRepository;
		_permissionManager = permissionManager;
	}

	public async Task SeedAsync(DataSeedContext context)
	{
		// Seed sample books (idempotent)
		if (await _bookRepository.GetCountAsync() <= 0)
		{
			await _bookRepository.InsertAsync(
				new Book
				{
					Name = "1984",
					Type = BookType.Dystopia,
					PublishDate = new DateTime(1949, 6, 8),
					Price = 19.84f
				},
				autoSave: true
			);

			await _bookRepository.InsertAsync(
				new Book
				{
					Name = "The Hitchhiker's Guide to the Galaxy",
					Type = BookType.ScienceFiction,
					PublishDate = new DateTime(1995, 9, 27),
					Price = 42.0f
				},
				autoSave: true
			);
		}

		// Grant permissions to admin role by default
		await _permissionManager.SetForRoleAsync(
			roleName: "admin",
			permissionName: ABPPermissionPermissions.UserManagement.CreateBatch,
			isGranted: true
		);

		await _permissionManager.SetForRoleAsync(
			roleName: "admin",
			permissionName: ABPPermissionPermissions.UserManagement.FillDob,
			isGranted: true
		);
	}
}
