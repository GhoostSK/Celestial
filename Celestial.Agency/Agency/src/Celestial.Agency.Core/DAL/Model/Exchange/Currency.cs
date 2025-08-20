using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.Agency.Core.DAL.Model.Exchange;

public class Currency : DbBaseEntity<int>
{
    public string Name { get; set; } = string.Empty;

    public decimal ExchangeRate { get; set; }
}