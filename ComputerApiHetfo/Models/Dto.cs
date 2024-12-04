namespace ComputerApiHetfo.Models
{
    public record CreateOsDto(string? Name);
    public record UpdateOsDto(string? Name);

    public record CreateComputer(string? Brand, string? Type, double? Display, int? Memory, Guid? OsId);
    public record UpdateComputer(string? Brand, string? Type, double? Display, int? Memory);
}
