using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace ABPPermission.Entities.Authors;

public class Author : AuditedAggregateRoot<Guid>
{
    public string FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Biography { get; set; }

    // Navigation property - One Author has Many Books
    public virtual ICollection<Books.Book> Books { get; set; }

    public Author()
    {
        Books = new List<Books.Book>();
    }
}
