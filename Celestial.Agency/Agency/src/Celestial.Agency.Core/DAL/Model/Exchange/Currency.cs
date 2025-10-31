using Celestial.Dal.Ef.SqlServer.Abstractions;

namespace Celestial.Agency.Core.DAL.Model.Exchange;

public class Currency : DbBaseEntity<int>
{
    public string Name { get; set; } = string.Empty;

    public decimal ExchangeRate { get; set; }
}