using System.Json;

namespace BoardGameDatabase.Operations
{
    internal static class JsonOperation
    {
        public static bool IsJsonFormat(string json)
        {
            //https://stackoverflow.com/questions/14977848/how-to-make-sure-that-string-is-valid-json-using-json-net
            try
            {
                JsonValue.Parse(json);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
