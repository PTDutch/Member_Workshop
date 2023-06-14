using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ASP_MEMBER_WORKSHOP.Models
{
    public static class ExtensionModel
    {
        // ปรับแต่งค่า Error ของ ModelState ใหม่
        public static string GetErrorModelState(this ModelStateDictionary modelState)
        {
            var modelValue = modelState.Values
                                .Select(value => value.Errors)
                                .Where(value => value.Count() > 0)
                                .FirstOrDefault();
            if (modelValue == null) return null;
            return modelValue[0].ErrorMessage;
        }

        // ปรับแต่ง Error Exception แสดงค่า inner exception ในสุด
        public static Exception GetErrorException(this Exception exceotion)
        {
            if(exceotion.InnerException != null)
                return exceotion.InnerException.GetErrorException();
            return exceotion;
        }
    }
}