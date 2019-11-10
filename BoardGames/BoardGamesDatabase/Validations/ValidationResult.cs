using System.Collections.Generic;
using BoardGameDatabase.Interfaces.Validators;

namespace BoardGameDatabase.Validations
{
    internal class ValidationResult 
    {
        public bool IsSucces { get; set; } = true;

        public Dictionary<string, string> ErrorList { get; private set; } = new Dictionary<string, string>();
    }
}
