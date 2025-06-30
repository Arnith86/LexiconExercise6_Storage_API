using System.ComponentModel.DataAnnotations;

namespace StorageApi.DTOs;

public class UpdateProductDto
{
	[Key]
	[Required]
	public int Id { get; set; }

	[Required]
	[MaxLength(50, ErrorMessage = "Name can not be longer than 50 characters.")]
	public string Name { get; set; } = string.Empty;

	[Range(1, 100, ErrorMessage = "Price must be within 1-100 range.")]
	public int Price { get; set; }

	[MaxLength(50, ErrorMessage = "Category can not be longer than 50 characters.")]
	public string Category { get; set; } = string.Empty;

	[MaxLength(6, ErrorMessage = "Shelf do not have longer names then 6 characters.")]
	public string Shelf { get; set; } = string.Empty;

	[Required]
	[Range(0, 1000)]
	public int Count { get; set; }

	[MaxLength(250, ErrorMessage = "Description is limited to 250 characters.")]
	public string Description { get; set; } = string.Empty;
}


