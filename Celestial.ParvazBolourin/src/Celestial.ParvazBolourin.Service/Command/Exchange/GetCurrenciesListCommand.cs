using Celestial.ParvazBolourin.Core.DAL.Model.Exchange;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Exchange;

public sealed class GetCurrenciesListCommandReq : BaseEntity, IRequest<GetCurrenciesListCommandRes>
{

}

public sealed class GetCurrenciesListCommandRes : ServiceResContextBase
{
    public List<Currency> Items { get; set; }
}

