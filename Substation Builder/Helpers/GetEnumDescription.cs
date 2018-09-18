using System;
using System.ComponentModel;

namespace Substation_Builder.Helpers
{
    public static class GetEnumDescription
    {
        //used to get the description field of a ENUM
        public static string GetDescription (this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attr.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
    }
}
