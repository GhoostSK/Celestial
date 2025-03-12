using Celestial.ParvazBolourin.Core.DAL.Model.Tour;
using Celestial.ParvazBolourin.Core.Enum.Tour;
using MediatR;
using MofidApp.SharedKernel.Common.Model;
using MofidApp.SharedKernel.Domain.Services;

namespace Celestial.ParvazBolourin.Service.Command.Tour;

public class GetTourByIdCommandReq : BaseEntity, IRequest<GetTourByIdCommandRes>
{
    public int TourId { get; set; }
    public int LanguageId { get; set; }
}

public sealed class GetTourByIdCommandRes : ServiceResContextBase
{
    public GetTourByIdItemRes Item { get; set; } = new();
}

public sealed class GetTourByIdItemRes
{
    public int LanguageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TourMainText { get; set; } = string.Empty;
    public List<string> TourFacilities { get; set; } = new();
    public string TourTips { get; set; } = string.Empty;
    public string PlanShortText { get; set; } = string.Empty;
    public List<GetTourByIdTourPlan> TourPlans { get; set; } = new();
    public List<string> Countries { get; set; } = new();
    public string HotelCode { get; set; } = string.Empty;
    public TourStatus TourStatus { get; set; }
    public DateTime StartDate { get; set; }
    public int PassengerCapacity { get; set; }
    public string ImportantTips { get; set; } = string.Empty;
    public List<TourTransPorts> TransPorts { get; set; } = new();
    public List<int> Hotels { get; set; } = new();
    public TourPrimaryTransport PrimaryTransport { get; set; } = new();
    public string Price { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Score { get; set; }
}

public sealed class GetTourByIdTourPlan
{
    public long Id { get; set; }
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
    public List<string> PlanPicturePath { get; set; } = new();
}