using MofidApp.SharedKernel.DAL.Abstractions.EntityFramework;

namespace Celestial.ParvazBolourin.Core.DAL.Model.CurrencyRates;

public class Currency : DbBaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}