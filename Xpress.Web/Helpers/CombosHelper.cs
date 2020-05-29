using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xpress.Web.Data;

namespace Xpress.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountiesAsync(int id)
        {
            List<SelectListItem> list = await _dataContext.Counties
                .Include(c => c.State)
                .Where(s => s.State.Id == id)
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = $"{g.Id}"
                })
          .OrderBy(g => g.Text)
          .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un cantón]",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> list = await _dataContext.Countries.Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = $"{g.Id}"
            })
                .OrderBy(g => g.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un país]",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboDistrictsAsync(int id)
        {
            List<SelectListItem> list = await _dataContext.Districts
                .Include(s => s.County)
                .Where(s => s.County.Id == id)
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = $"{g.Id}"
                })
                .OrderBy(g => g.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un distrito]",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboFranchisesAsync()
        {
            List<SelectListItem> list = await _dataContext.Franchises.Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = $"{g.Id}"
            })
                .OrderBy(g => g.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una franquicia]",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int id)
        {
            List<SelectListItem> list = await _dataContext.States
                .Include(s => s.Country)
                .Where(s => s.Country.Id == id)
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = $"{g.Id}"
                })
          .OrderBy(g => g.Text)
          .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una provincia]",
                Value = "0"
            });

            return list;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboTownsAsync(int districtId)
        {
            List<SelectListItem> list = await _dataContext.Towns
                .Include(t => t.District)
                .Where(t => t.District.Id == districtId)
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = $"{g.Id}"
                })
                .OrderBy(g => g.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una localidad]",
                Value = "0"
            });

            return list;
        }
    }
}
