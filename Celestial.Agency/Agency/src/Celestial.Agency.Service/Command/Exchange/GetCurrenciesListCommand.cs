using Celestial.Agency.Core.DAL.Model.Exchange;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Exchange;

public sealed class GetCurrenciesListCommandReq : BaseEntity, IRequest<GetCurrenciesListCommandRes>
{
}

public sealed class GetCurrenciesListCommandRes : ServiceResContextBase
{
    public List<Currency> Items { get; set; } = new();
}