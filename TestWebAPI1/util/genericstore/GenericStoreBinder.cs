namespace TestWebAPI1.util.genericstore;

public interface GenericStoreBinder
{
    public void SetValue(string group, string name, int value);
    
    public int? GetIntValue(string group, string name);
    
    public void SetValue(string group, string name, string value);
    
    public string? GetStringValue(string group, string name);
}