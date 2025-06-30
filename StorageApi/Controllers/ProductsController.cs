using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageApi.Data;
using StorageApi.DTOs;

namespace StorageApi.Controllers
{
	[Route("api/Products")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly StorageApiContext _context;
		private readonly IMapper _mapper;

		public ProductsController(
			StorageApiContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
		{
			var product = await _context.Product.ToListAsync();

			return Ok(_mapper.Map<IEnumerable<ProductDto>>(product));
		}

		// GET: api/Products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetProduct(int id)
		{
			var product = await _context.Product.FindAsync(id);

			if (product is null) return NotFound();

			return Ok(_mapper.Map<ProductDto>(product));
		}

		// PUT: api/Products/5  // Update
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, UpdateProductDto updateProductDto)
		{
			if (id != updateProductDto.Id) return BadRequest();

			Product product = _mapper.Map<Product>(updateProductDto);

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Products   // Create
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<CreateProductDto>> PostProduct(CreateProductDto createProductDto)
		{
			if (createProductDto is null) return NotFound("Product data is null.");

			Product product = _mapper.Map<Product>(createProductDto);

			_context.Product.Add(product);

			await _context.SaveChangesAsync();

			createProductDto = _mapper.Map<CreateProductDto>(product);

			return CreatedAtAction("GetProduct", new { id = product.Id }, createProductDto);
		}

		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _context.Product.FindAsync(id);

			if (product is null) return NotFound();

			_context.Product.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// GET: api/products/stats
		[HttpGet("stats")]
		public async Task<ActionResult<ProductStatsDto>> GetProductStats()
		{
			IEnumerable<ProductDto> productDto = await _context.Product
				.Select(p => _mapper.Map<ProductDto>(p))
				.ToListAsync();

			int totalProducts = productDto.Sum(p => p.Count);
			int totalInventoryValue = productDto.Sum(p => p.InventoryValue);
			double averagePrice = productDto.Any() ? productDto.Average(p => p.Price) : 0;

			// The return will be a Json object 
			return Ok(
				new ProductStatsDto(
					totalProducts,
					totalInventoryValue,
					averagePrice
				)
			);
		}


		// GET: api/products/bycategory/?category="CategoryName"
		[HttpGet("bycategory")]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByCategory([FromQuery] string? category)
		{
			IQueryable<Product> productQuery = _context.Product;

			if (!string.IsNullOrEmpty(category))
			{
				productQuery = productQuery.Where(p => p.Category == category);
			}
			else return BadRequest("Category cannot be null or empty.");

			var products = await productQuery.ToListAsync();

			return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
		}


		private bool ProductExists(int id)
		{
			return _context.Product.Any(e => e.Id == id);
		}
	}
}
