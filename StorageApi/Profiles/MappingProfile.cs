using AutoMapper;
using StorageApi.DTOs;

namespace StorageApi.Profiles;
/// <summary>
/// This class defines the mapping configuration for AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Product, ProductDto>();
	}
}
