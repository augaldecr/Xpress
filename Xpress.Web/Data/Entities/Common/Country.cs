using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xpress.Web.Data.Entities.Common
{
    //País
    public class Country : BasicEntity
    {
        [Display(Name = "Provincias")]
        public virtual ICollection<State> States { get; set; }
    }
}