using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MS_Products.Domain.Entities;
public class Product (string name, decimal price, Guid storeId, string? description, string? image, Guid? categoryId)
{
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime CreationDate { get; private set; } = DateTime.Now;
	public string Name { get; private set; } = name;
	public decimal Price { get; private set; } = price;
	public string? Description { get; private set; } = description;
	public string? Image { get; private set; } = image;
	public Guid StoreId { get; private set; } = storeId;
	public Guid? CategoryId { get; private set; } = categoryId;
	[JsonIgnore]
	public Category? Category { get; private set; }	
}