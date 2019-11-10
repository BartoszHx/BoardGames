using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoardGameDatabase.Enums;
using BoardGameDatabase.Interfaces.Validators;

namespace BoardGameDatabase.Validations
{
    internal class ValidationMessage : IValidationMessage
    {
        public string CultureInfo { get; }

        public ValidationMessage()
        {
            //https://stackoverflow.com/questions/1142802/how-to-use-localization-in-c-sharp  //Hmm
            //Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("pl-PL");
        }

        public string GetMessage(string key, string parameter = null)
        {
            return string.Format(Properties.ValidationMessages.ResourceManager.GetString(key), parameter);
        }

        public string GetMessage(string key, IEnumerable<string> parameterList)
        {
            return parameterList != null ? string.Format(Properties.ValidationMessages.ResourceManager.GetString(key), parameterList.Select(s=> s.ToString()).ToArray())
                       : Properties.ValidationMessages.ResourceManager.GetString(key);
        }
    }
}
