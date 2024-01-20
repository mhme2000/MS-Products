using System.Diagnostics.CodeAnalysis;

namespace MS_Products.Domain.Entities;
[ExcludeFromCodeCoverage]
public class Category (string name, Guid storeId, string? description)
{
	public Guid Id { get; private set; } = Guid.NewGuid();
	public Guid StoreId { get; private set; } = storeId;
	public DateTime CreationDate { get; private set; } = DateTime.Now;
	public string Name { get; private set; } = name;
	public string? Description { get; private set; } = description;
}