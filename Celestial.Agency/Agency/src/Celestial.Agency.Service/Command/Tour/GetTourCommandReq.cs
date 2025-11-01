using Celestial.Agency.Core.DAL.Model.Tour;
using Celestial.Agency.Core.Enum.Tour;
using Celestial.Agency.Service.Command.ShirdalBlog;
using Celestial.SharedKernel.Common;
using Celestial.SharedKernel.Domain;
using MediatR;

namespace Celestial.Agency.Service.Command.Tour;

public class GetTourCommandReq : BaseEntity, IRequest<GetTourByIdCommandRes>
{
    public string? Slug { get; set; }
}

public sealed class GetTourByIdCommandRes : ServiceResContextBase
{
    public GetTourItemRes Item { get; set; } = new();
    //public List<GetToursListItemRes> Type { get; set; }
}

public sealed class GetTourItemRes
{
    public string Name { get; set; } = string.Empty;
    public string TourMainText { get; set; } = string.Empty;
    public List<string> TourFacilities { get; set; } = new();
    public string TourTips { get; set; } = string.Empty;
    public string PlanShortText { get; set; } = string.Empty;
    public List<GetTourByIdTourPlan> TourPlans { get; set; } = new();
    public List<string> Countries { get; set; } = new();
    public string TourCode { get; set; } = string.Empty;
    public TourStatus TourStatus { get; set; }
    public DateOnly StartDate { get; set; }
    public int PassengerCapacity { get; set; }
    public string ImportantTips { get; set; } = string.Empty;
    public List<TourTransPorts> TransPorts { get; set; } = new();
    public List<int> Hotels { get; set; } = new();
    public TourPrimaryTransport PrimaryTransport { get; set; } = new();
    public DateOnly RegistrationDeadline { get; set; }
    public int DaysUntilRegistrationDeadline { get; set; }
    public List<TourEquipmentDto> UnNecessaryEquipment { get; set; } = new();
    public List<TourEquipmentDto> NecessaryEquipment { get; set; } = new();
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public int Score { get; set; }
    public List<TopBlogDto> SimilarBlog { get; set; } = new();
    public List<GetTourFeaturedItemDto> SimilarTour { get; set; } = new();

    //seo
    public string MetaTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class TourEquipmentDto
{
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public sealed class GetTourByIdTourPlan
{
    public long Id { get; set; }
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
    public List<string> PlanPicturePath { get; set; } = new();
}