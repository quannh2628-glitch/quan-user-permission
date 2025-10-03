using System;
using Volo.Abp.Application.Dtos;

namespace ABPPermission.Services.Dtos.Customers;

public class GetCustomerListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? DateOfBirthFrom { get; set; }

    public DateTime? DateOfBirthTo { get; set; }

    public int? RankingMin { get; set; }

    public int? RankingMax { get; set; }

    public string? Email { get; set; }

    public string? Notes { get; set; }
}
