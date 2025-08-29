namespace TestWebAPI1.util.genericstore.binders;

public class GenericStoreEntity
{
    public long Id { get; set; }
    public required string Group { get; set; }
    public required string Name { get; set; }
    public int? IntValue { get; set; }
    public long? LongValue { get; set; }
    public float? FloatValue { get; set; }
    public double? DoubleValue { get; set; }
    public bool? BoolValue { get; set; }
    public string? StringValue { get; set; }
}

public class SqlBinder : GenericStoreBinder
{
    public void SetValue(string group, string name, int value)
    {
        throw new NotImplementedException();
    }

    public int? GetValue(string group, string name)
    {
        throw new NotImplementedException();
    }
}