using Dapper;
using Microsoft.Data.SqlClient;

namespace TestWebAPI1.util.genericstore.binders;

public class SqlStoreEntity
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
    private string _table;
    
    public int? GetIntValue(string group, string name)
    {
        var variable = SqlStoreEntity(group, name);
        return variable?.IntValue;
    }

    private SqlStoreEntity? SqlStoreEntity(string group, string name)
    {
        var sql = "SELECT IntValue FROM @table WHERE Group = @group AND Name = @name";

        using var con = new SqlConnection("MyConnectionString");
        con.Open();
        string query = "SELECT * FROM tblFriends WHERE FriendID = @Id";
        var variable = con.QueryFirstOrDefault<SqlStoreEntity>(query, new { table = _table, group = group, name = name });
        return variable;
    }


    public long? GetLongValue(string group, string name)
    {
        var variable = SqlStoreEntity(group, name);
        return variable?.LongValue;
    }

    public float? GetFloatValue(string group, string name)
    {
        var variable = SqlStoreEntity(group, name);
        return variable?.FloatValue;
    }

    public double? GetDoubleValue(string group, string name)
    {
        var variable = SqlStoreEntity(group, name);
        return variable?.DoubleValue;
    }

    public bool? GetBoolValue(string group, string name)
    {
        var variable = SqlStoreEntity(group, name);
        return variable?.BoolValue;
    }

    public string? GetStringValue(string group, string name)
    {
        var variable = SqlStoreEntity(group, name);
        return variable?.StringValue;
    }

    public void SetValue(string group, string name, int value)
    {
        throw new NotImplementedException();
    }

    public void SetValue(string group, string name, long value)
    {
        throw new NotImplementedException();
    }

    public void SetValue(string group, string name, float value)
    {
        throw new NotImplementedException();
    }

    public void SetValue(string group, string name, double value)
    {
        throw new NotImplementedException();
    }

    public void SetValue(string group, string name, bool value)
    {
        throw new NotImplementedException();
    }

    public void SetValue(string group, string name, string value)
    {
        throw new NotImplementedException();
    }

    public void ClearGroup(string group)
    {
        throw new NotImplementedException();
    }

    public int? GetValue(string group, string name)
    {
        throw new NotImplementedException();
    }
}