using System;
using System.ComponentModel.DataAnnotations;

namespace DocRefList.Infrastructure
{
    public class NotLessCurrentDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime dateTime = (DateTime) value;
                
                if (dateTime > DateTime.Now)
                    return true;
                else
                    ErrorMessage = "Установленная дата не может быть меньше текущей";
            }
            return false;
        }
    }
}
