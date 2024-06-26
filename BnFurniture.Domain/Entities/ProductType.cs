﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductType
{
    [Key]
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    // Nav
    [ForeignKey(nameof(CategoryId))]
    public ProductCategory ProductCategory { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
}
