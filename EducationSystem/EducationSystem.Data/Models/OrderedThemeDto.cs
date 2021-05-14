using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class OrderedThemeDto : ThemeDto
    {
        public int Order { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is OrderedThemeDto))
                return false;

            var themeObj = (OrderedThemeDto)obj;
            return (themeObj.Name==Name &&
                    themeObj.Id == Id &&
                    themeObj.IsDeleted == IsDeleted &&
                    themeObj.Order==Order);

        }
        public override string ToString()
        {
            return $" {Id}  {Name}  {Order} ";
        }
    }
}
