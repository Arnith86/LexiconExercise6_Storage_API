namespace StorageApi.DTOs;

public record ProductStatsDto(
	int TotalProducts, 
	int TotalInventoryValue,
	double AveragePrice 
);