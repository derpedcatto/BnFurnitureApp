﻿namespace BnFurniture.Application.Controllers.ExampleController.DTO;

public class ExampleEntityResponseDTO
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF { get; set; }
    public string? Summary { get; set; }
}
