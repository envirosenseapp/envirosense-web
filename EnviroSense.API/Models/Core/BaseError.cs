namespace EnviroSense.API.Models.Core;

public abstract class BaseError
{
    public string Code { get; set; }
    
    public string Message { get; set; }

    public IList<Entry> Context { get; set; } = new List<Entry>();

    protected BaseError(string code, string message, IList<Entry>? context = null)
    {
        Code = code;
        Message = message;
        Context = context ?? new List<Entry>();
    }
    
    public class Entry
    {
        public Entry(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
