﻿using System;
using System.ComponentModel.DataAnnotations;
using ABPPermission.Entities.Books;

namespace ABPPermission.Services.Dtos.Books;

public class CreateUpdateBookDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public BookType Type { get; set; } = BookType.Undefined;

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; } = DateTime.Now;

    [Required]
    public float Price { get; set; }

    [Required]
    public Guid AuthorId { get; set; }
}