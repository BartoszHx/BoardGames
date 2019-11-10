using System.Collections.Generic;

namespace BoardGameDatabase.Interfaces
{
    internal interface IMessage
    {
       string CultureInfo { get; }
       string GetMessage(string key, string parameter = null);
       string GetMessage(string key, IEnumerable<string> parameterList);

    }
}
