using System.ComponentModel.DataAnnotations;

namespace Celestial.Agency.Api.Areas.Admin.Models.Dto;

public class EditCurrencyDto
{
    [Required] public int Id { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    [Required] public decimal ExchangeRate { get; set; }
}

public class AddCurrencyDto
{
    [Required] public string Name { get; set; } = string.Empty;

    [Required] public decimal ExchangeRate { get; set; }
}