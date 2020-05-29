using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xpress.Web.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboCountiesAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();
        Task<IEnumerable<SelectListItem>> GetComboDistrictsAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboFranchisesAsync();
        Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboTownsAsync(int districtId);
    }
}