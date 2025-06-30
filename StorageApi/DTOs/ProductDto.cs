using System.ComponentModel.DataAnnotations;

namespace StorageApi.DTOs;

public class ProductDto
{
	public int Id { get; set; }

	[Required]
	[MaxLength(50)]
	public string Name { get; set; } = string.Empty;

	[Range(1, 100)]
	public int Price { get; set; }

	[Required]
	[Range(0, 1000)]
	public int Count { get; set; }
	
	[MaxLength(250)]
	public string Description { get; set; } = string.Empty;

	public int InventoryValue => Price * Count;
}
