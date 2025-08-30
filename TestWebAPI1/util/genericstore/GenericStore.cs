namespace TestWebAPI1.util.genericstore;

public class GenericStore
{
    // todo add autobackup methods, print state method...
    private const string Separator = "/@-@/";
    
    private GenericStoreBinder _binder;

    public int GetValue(string group, string name, int defaultValue)
    {
        var value = _binder.GetIntValue(group, name);
        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, int value)
    {
        _binder.SetValue(group, name, value);
    }

    public long GetValue(string group, string name, long defaultValue)
    {
        var value = _binder.GetIntValue(group, name);
        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, long value)
    {
        _binder.SetValue(group, name, value);
    }

    public int[] GetValue(string group, string name, int[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
            return defaultValue;
        
        var values = listStr.Split(Separator);
        return values.Select(int.Parse).ToArray();
    }

    public void SetValue(string group, string name, int[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
    }

    public bool[] GetValue(string group, string name, bool[] defaultValue)
    {
        var listStr = _binder.GetStringValue(group, name);
        if (listStr == null)
            return defaultValue;
        
        var values = listStr.Split(Separator);
        return values.Select(bool.Parse).ToArray();
    }

    public void SetValue(string group, string name, bool[] value)
    {
        var strValue = GenerateStringList(value.Select(v =>  v.ToString()).ToList());
    }

    private static string[] ParseList(string list)
    {
        return list.Split(Separator);
    }

    private static string GenerateStringList(List<string> list)
    {
        // todo test
        return string.Join(Separator, list);
    }
    
    

    
    public string GetValue(string group, string name, string defaultValue)
    {
        var value = _binder.GetStringValue(group, name);
        return (value ?? defaultValue)!;
    }
    
    public void SetValue(string group, string name, string value)
    {
        _binder.SetValue(group, name, value);
    }

    public void ClearGroup(string group)
    {
        
    }
}