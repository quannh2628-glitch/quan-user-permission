using System;
using Volo.Abp.Domain.Entities.Auditing;
using ABPPermission.Entities.Authors;

namespace ABPPermission.Entities.Books;

public class Book : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public float Price { get; set; }

    // Foreign Key
    public Guid AuthorId { get; set; }

    // Navigation property - Many Books belong to One Author
    public virtual Author Author { get; set; }
}