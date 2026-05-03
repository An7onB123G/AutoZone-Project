using AutoZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoZone.Models.ViewModels
{
    public class CarsPageViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public IEnumerable<SelectListItem> BrandModelOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> CarTypeOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> EngineTypeOptions { get; set; } = new List<SelectListItem>();

        public string? Search { get; set; }
        public int? BrandModelId { get; set; }
        public int? CarTypeId { get; set; }
        public int? EngineTypeId { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public int? MileageFrom { get; set; }
        public int? MileageTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public bool? IsAutomatic { get; set; }
        public string? Sort { get; set; }

        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
