using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CardsApp.Misc
{
    public static class EnumAttributes
    {
        public static List<Attribute> GetAttributes<TEnum>(this TEnum enumValue) where TEnum:Enum
        {
            var intval = Convert.ToInt32(enumValue);
            var type = typeof(TEnum);
            var member = type.GetMember(Enum.GetName(type,intval));
            var attributes = member.First().GetCustomAttributes();
            return attributes.ToList();
        }
    }
}
