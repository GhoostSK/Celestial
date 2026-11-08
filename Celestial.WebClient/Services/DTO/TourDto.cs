// ---------------------- Requests ----------------------

using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class GetTourListReqDto
{
    public string Language { get; set; } = string.Empty;
    public string? Title { get; set; }
    public List<string> Countries { get; set; } = new();
    public List<string> Cities { get; set; } = new();
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public double FromPrice { get; set; }
    public double ToPrice { get; set; }
    public bool IsActiveTour { get; set; }
    public TourSortBy SortBy { get; set; } = TourSortBy.Newest;
    public int PageSize { get; set; } = 9;
    public int PageNumber { get; set; } = 1;
}

public sealed class GetTourBySlugReqDto
{
    public string Language { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class GetFeaturedTourListReqDto
{
    public string Language { get; set; } = string.Empty;
    public int TopCount { get; set; }
}

public sealed class GetTourEquipmentListReqDto
{
    public string Language { get; set; } = string.Empty;
}

// ---------------------- Responses ----------------------

public sealed class GetTourListResDto : ServiceResContextBase
{
    public List<TourListItemDto> Items { get; set; } = new();
    public TourFiltersDto Filters { get; set; } = new();
    public PaginationMeta Pagination { get; set; } = new();
}

public sealed class GetTourBySlugResDto : ServiceResContextBase
{
    public TourDetailDto Item { get; set; } = new();
}

public sealed class GetFeaturedTourListResDto : ServiceResContextBase
{
    public List<FeaturedTourItemDto> Items { get; set; } = new();
}

public sealed class GetTourEquipmentListResDto : ServiceResContextBase
{
    public List<TourEquipmentItemDto> Items { get; set; } = new();
}

// ---------------------- DTOs ----------------------

public sealed class TourListItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TourText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public List<string> Country { get; set; } = new();
    public int TotalDay { get; set; }
    public int Score { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? RegistrationDeadline { get; set; }
    public int? DaysUntilRegistrationDeadline { get; set; }
    public TourStatus TourStatus { get; set; }
    public int RemainingCapacity { get; set; }
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    public string Slug { get; set; } = string.Empty;
}

public sealed class TourFiltersDto
{
    public List<string> Countries { get; set; } = new();
    public List<Continent> TourContinent { get; set; } = new();
    public List<TourType> TourType { get; set; } = new();
    public List<TourScope> TourScope { get; set; } = new();
    public int FromDay { get; set; }
    public int ToDay { get; set; }
    public double FromPrice { get; set; }
    public double ToPrice { get; set; }
}

public sealed class TourDetailDto
{
    public string Name { get; set; } = string.Empty;
    public string TourMainText { get; set; } = string.Empty;
    public List<string> TourFacilities { get; set; } = new();
    public string TourTips { get; set; } = string.Empty;
    public string PlanShortText { get; set; } = string.Empty;
    public List<TourPlanItemDto> TourPlans { get; set; } = new();
    public List<string> Countries { get; set; } = new();
    public string TourCode { get; set; } = string.Empty;
    public TourStatus TourStatus { get; set; }
    public DateOnly StartDate { get; set; }
    public int PassengerCapacity { get; set; }
    public string ImportantTips { get; set; } = string.Empty;
    public List<TourTransportItemDto> TransPorts { get; set; } = new();
    public List<TourHotelItemDto> Hotels { get; set; } = new();
    public TourPrimaryTransportDto PrimaryTransport { get; set; } = new();
    public DateOnly RegistrationDeadline { get; set; }
    public int DaysUntilRegistrationDeadline { get; set; }
    public List<TourEquipmentItemDto> NecessaryEquipment { get; set; } = new();
    public List<TourEquipmentItemDto> UnNecessaryEquipment { get; set; } = new();
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public int Score { get; set; }
    public List<TopBlogItemDto> SimilarBlog { get; set; } = new();
    public List<FeaturedTourItemDto> SimilarTour { get; set; } = new();
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeywords { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

public sealed class TourPlanItemDto
{
    public long Id { get; set; }
    public string PlanDayText { get; set; } = string.Empty;
    public string PlanText { get; set; } = string.Empty;
    public List<string> PlanPicturePath { get; set; } = new();
}

public sealed class TourTransportItemDto
{
    public long Id { get; set; }
    public TransportVehicleType TransPortType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public sealed class TourPrimaryTransportDto
{
    public string LeadFlightCode { get; set; } = string.Empty;
    public string ReturnFlightCode { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string LeadStartTime { get; set; } = string.Empty;
    public string LeadEndTime { get; set; } = string.Empty;
    public string ReturnStartTime { get; set; } = string.Empty;
    public string ReturnEndTimeTime { get; set; } = string.Empty;
    public string AirLine { get; set; } = string.Empty;
    public string AirLineImagePath { get; set; } = string.Empty;
}

public sealed class TourHotelItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int HotelScore { get; set; }
    public double Price { get; set; }
    public string Slug { get; set; } = string.Empty;
}

public sealed class TourEquipmentItemDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public sealed class FeaturedTourItemDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public List<string> Countries { get; set; } = new();
    public decimal BasePrice { get; set; }
    public decimal? ForeignPrice { get; set; }
    public string? ForeignCurrency { get; set; }
    public int DaysUntilRegistrationDeadline { get; set; }
    public int Score { get; set; }
    public int VoteCount { get; set; }
    public int TotalDay { get; set; }
    public string Slug { get; set; } = string.Empty;
}

public sealed class TopBlogItemDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string HeaderText { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int Likes { get; set; }
    public string Slug { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedByProfilePicture { get; set; } = string.Empty;
}
