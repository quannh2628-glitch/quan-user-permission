using System;
using System.ComponentModel.DataAnnotations;

namespace ABPPermission.Services.Dtos.Authors;

public class CreateUpdateAuthorDto
{
    [Required]
    [StringLength(256)]
    public string FullName { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(500)]
    public string Address { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Biography { get; set; } = string.Empty;
}
