using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EducationSystem.Core.CustomExceptions
{
    public class ValidationException : Exception
    {
        public int StatusCode { get; private set; } 
        public string ErrorMessage { get; private set; }

        public ValidationException(ModelStateDictionary modelState)
        {
            StatusCode = (int)HttpStatusCode.Conflict;
            ErrorMessage = ""; // get errors from modelState and combine them into ErrorMessage
            foreach(var state in modelState)
            {
                ErrorMessage += $"Invalid format {state.Key}: {state.Value.Errors[0].ErrorMessage} ";
            }
        }
    }
}
