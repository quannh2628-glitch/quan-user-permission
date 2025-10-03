using System;
using Volo.Abp.Application.Dtos;

namespace ABPPermission.Services.Dtos.Authors;

public class AuthorDto : AuditedEntityDto<Guid>
{
    public string FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Biography { get; set; }
}
