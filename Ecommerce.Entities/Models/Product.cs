using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Ecommerce.Entities.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string  Name { get; set; }
    public string  Description { get; set; }
    [ValidateNever]
    public string Image { get; set; }
    [Required]
    public string  Price { get; set; }
    [Required]
    [ForeignKey("category")]
    public int CategoryId { get; set; }
    [ValidateNever]
    public Category category { get; set; }
}
