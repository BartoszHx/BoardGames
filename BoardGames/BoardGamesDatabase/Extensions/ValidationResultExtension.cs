using BoardGameDatabase.Operations;
using BoardGameDatabase.Validations;
using System;
using System.Collections.Generic;

using BoardGameDatabase.Enums;

namespace BoardGameDatabase.Extensions
{
    internal static class ValidationResultExtension
    {
        private static void AddError(ValidationResult valid, string key, Func<string> getMessage)
        {
            valid.IsSucces = false;
            if (valid.ErrorList.ContainsKey(key))
            {
                valid.ErrorList[key] += ". " + getMessage();
            }
            else
            {
                valid.ErrorList.Add(key, getMessage());
            }
        }

        public static void AddError(this ValidationResult valid, ValidationKey key, string parameter = null)
        {
            AddError(valid, key.ToString(), ()=> MessageOperation.GetValidationMessage(key, parameter));
        }

        public static void AddError(this ValidationResult valid, ValidationKey key, IEnumerable<string> parameterList)
        {
            AddError(valid, key.ToString(), () => MessageOperation.GetValidationMessage(key, parameterList));
        }
    }
}
