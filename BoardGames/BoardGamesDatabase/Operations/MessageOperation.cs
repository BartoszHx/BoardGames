using BoardGameDatabase.Interfaces.Validators;
using System.Collections.Generic;

using BoardGameDatabase.Enums;

namespace BoardGameDatabase.Operations
{
    internal sealed class MessageOperation
    {
        private static readonly MessageOperation instance = new MessageOperation();
        public static string GetValidationMessage(ValidationKey key) => instance.validationMessage.GetMessage(key.ToString());
        public static string GetValidationMessage(ValidationKey key, string parameter) => instance.validationMessage.GetMessage(key.ToString(), parameter);
        public static string GetValidationMessage(ValidationKey key, IEnumerable<string> parameterList) => instance.validationMessage.GetMessage(key.ToString(), parameterList);

        private IValidationMessage validationMessage;

        static MessageOperation()
        {
            
        }
        private MessageOperation()
        {
            validationMessage = StaticKernel.Get<IValidationMessage>();
        }

    }
}
