namespace EnviroSense.API.Models.Core;

public class BaseSuccess
{
    public string Code { get; set; }

    public string Message { get; set; }

    public IList<Entry> Context { get; set; } = new List<Entry>();

    protected BaseSuccess(string code, string message, IList<Entry>? context = null)
    {
        Code = code;
        Message = message;
        Context = context ?? new List<Entry>();
    }

    public class Entry
    {
        public Entry(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
