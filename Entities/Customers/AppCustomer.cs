using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace ABPPermission.Entities.Customers;

public class AppCustomer : AuditedAggregateRoot<Guid>
{
    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int Ranking { get; set; }

    public string Email { get; set; }

    public string Notes { get; set; }
}
