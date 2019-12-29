using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardsApp.Misc
{
    public static class ExtTableToDropdown
    {
        public static List<SelectListItem> GetDropdownItems<T>(this IEnumerable<T> table, string textField)
        {
            return table.Select(g => new SelectListItem(typeof(T).GetProperty(textField).GetValue(g).ToString(), ((dynamic) g).Id.ToString())).ToList();
            return null;
        }
    }
}
