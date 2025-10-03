using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ABPPermission.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using ABPPermission.Entities.Customers;
using ABPPermission.Services.Dtos.Customers;

namespace ABPPermission.Services.Customers;

[Authorize(ABPPermissionPermissions.Customers.Default)]
public class CustomerAppService : ApplicationService, ICustomerAppService
{
    private readonly IRepository<AppCustomer, Guid> _repository;

    public CustomerAppService(IRepository<AppCustomer, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        
        // Apply filters
        if (!string.IsNullOrWhiteSpace(input.Filter))
        {
            queryable = queryable.Where(c => 
                c.FullName.Contains(input.Filter) ||
                c.PhoneNumber.Contains(input.Filter) ||
                c.Address.Contains(input.Filter) ||
                c.Email.Contains(input.Filter) ||
                c.Notes.Contains(input.Filter));
        }

        if (!string.IsNullOrWhiteSpace(input.FullName))
        {
            queryable = queryable.Where(c => c.FullName.Contains(input.FullName));
        }

        if (!string.IsNullOrWhiteSpace(input.PhoneNumber))
        {
            queryable = queryable.Where(c => c.PhoneNumber.Contains(input.PhoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(input.Address))
        {
            queryable = queryable.Where(c => c.Address.Contains(input.Address));
        }

        if (!string.IsNullOrWhiteSpace(input.Email))
        {
            queryable = queryable.Where(c => c.Email.Contains(input.Email));
        }

        if (!string.IsNullOrWhiteSpace(input.Notes))
        {
            queryable = queryable.Where(c => c.Notes.Contains(input.Notes));
        }

        if (input.DateOfBirthFrom.HasValue)
        {
            queryable = queryable.Where(c => c.DateOfBirth >= input.DateOfBirthFrom.Value);
        }

        if (input.DateOfBirthTo.HasValue)
        {
            queryable = queryable.Where(c => c.DateOfBirth <= input.DateOfBirthTo.Value);
        }

        if (input.RankingMin.HasValue)
        {
            queryable = queryable.Where(c => c.Ranking >= input.RankingMin.Value);
        }

        if (input.RankingMax.HasValue)
        {
            queryable = queryable.Where(c => c.Ranking <= input.RankingMax.Value);
        }

        // Apply sorting
        var query = queryable.OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "FullName" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var customers = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<CustomerDto>(
            totalCount,
            ObjectMapper.Map<List<AppCustomer>, List<CustomerDto>>(customers)
        );
    }

    public async Task<CustomerDto> GetAsync(Guid id)
    {
        var customer = await _repository.GetAsync(id);
        return ObjectMapper.Map<AppCustomer, CustomerDto>(customer);
    }

    [Authorize(ABPPermissionPermissions.Customers.Create)]
    public async Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
    {
        var customer = ObjectMapper.Map<CreateUpdateCustomerDto, AppCustomer>(input);
        await _repository.InsertAsync(customer);
        return ObjectMapper.Map<AppCustomer, CustomerDto>(customer);
    }

    [Authorize(ABPPermissionPermissions.Customers.Edit)]
    public async Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
    {
        var customer = await _repository.GetAsync(id);
        ObjectMapper.Map(input, customer);
        await _repository.UpdateAsync(customer);
        return ObjectMapper.Map<AppCustomer, CustomerDto>(customer);
    }

    [Authorize(ABPPermissionPermissions.Customers.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    [Authorize(ABPPermissionPermissions.Customers.InitData)]
    public async Task<InitCustomersResultDto> InitializeCustomersAsync(InitializeCustomersDto input)
    {
        const int batchSize = 1000;
        var random = new Random();
        var created = 0;

        // Parse input strings to arrays
        var firstNames = input.FirstNames.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(name => name.Trim()).ToArray();
        var lastNames = input.LastNames.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(name => name.Trim()).ToArray();
        var cities = input.Cities.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(city => city.Trim()).ToArray();
        var domains = input.EmailDomains.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(domain => domain.Trim()).ToArray();

        // Validate input arrays
        if (firstNames.Length == 0)
            throw new ArgumentException("Danh sách tên không được để trống");
        if (lastNames.Length == 0)
            throw new ArgumentException("Danh sách họ không được để trống");
        if (cities.Length == 0)
            throw new ArgumentException("Danh sách thành phố không được để trống");
        if (domains.Length == 0)
            throw new ArgumentException("Danh sách domain email không được để trống");

        Logger.LogInformation($"Bắt đầu tạo {input.Count} khách hàng với {firstNames.Length} tên và {lastNames.Length} họ");

        for (int batch = 0; batch < input.Count; batch += batchSize)
        {
            var customers = new List<AppCustomer>();
            var currentBatchSize = Math.Min(batchSize, input.Count - batch);

            for (int i = 0; i < currentBatchSize; i++)
            {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)];
                var fullName = $"{lastName} {firstName}"; // Vietnamese format: LastName FirstName
                
                // Generate unique email
                var email = $"{lastName}{firstName}{random.Next(1000, 9999)}@{domains[random.Next(domains.Length)]}";
                
                // Generate Vietnamese phone number
                var phoneNumber = $"0{random.Next(100000000, 999999999)}";
                
                var city = cities[random.Next(cities.Length)];
                var address = $"{random.Next(1, 999)} Đường {GetRandomStreetName()}, {city}";
                
                var birthYear = random.Next(1950, 2005);
                var birthMonth = random.Next(1, 13);
                var birthDay = random.Next(1, 29);
                var dateOfBirth = new DateTime(birthYear, birthMonth, birthDay);
                
                var ranking = random.Next(0, 101);
                var notes = $"Khách hàng từ {random.Next(2010, 2025)}";

                var customer = new AppCustomer
                {
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    DateOfBirth = dateOfBirth,
                    Ranking = ranking,
                    Email = email,
                    Notes = notes
                };

                customers.Add(customer);
            }

            await _repository.InsertManyAsync(customers);
            created += customers.Count;

            // Log progress every 1000 records
            if (created % 1000 == 0)
            {
                Logger.LogInformation($"Đã tạo {created}/{input.Count} khách hàng...");
            }
        }

        Logger.LogInformation($"Hoàn thành tạo {created} khách hàng");

        return new InitCustomersResultDto
        {
            Created = created,
            Message = $"Đã tạo thành công {created} khách hàng"
        };
    }

    private string RemoveVietnameseAccents(string text)
    {
        var vietnameseChars = "àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ";
        var englishChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeeiiiiioooooooooooooooouuuuuuuuuuuyyyyyd";
        
        for (int i = 0; i < vietnameseChars.Length; i++)
        {
            text = text.Replace(vietnameseChars[i], englishChars[i]);
        }
        
        return text;
    }

    private string GetRandomStreetName()
    {
        var streetNames = new[] { "Lê Lợi", "Nguyễn Huệ", "Trần Hưng Đạo", "Lý Thường Kiệt", "Hai Bà Trưng", 
            "Lê Duẩn", "Võ Văn Tần", "Điện Biên Phủ", "Cách Mạng Tháng 8", "Nguyễn Thị Minh Khai" };
        return streetNames[new Random().Next(streetNames.Length)];
    }
}
