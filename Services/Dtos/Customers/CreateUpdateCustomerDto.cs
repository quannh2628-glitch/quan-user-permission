using System;
using System.ComponentModel.DataAnnotations;

namespace ABPPermission.Services.Dtos.Customers;

public class CreateUpdateCustomerDto
{
    [Required]
    [StringLength(256)]
    public string FullName { get; set; } = string.Empty;

    [StringLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(500)]
    public string Address { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    public int Ranking { get; set; } = 0;

    [StringLength(256)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Notes { get; set; } = string.Empty;
}
