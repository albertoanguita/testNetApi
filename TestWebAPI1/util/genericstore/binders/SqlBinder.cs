using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace TestWebAPI1.util.genericstore.binders;

/*
 *
 * CREATE TABLE test.GenericStore (
	Id BIGINT UNSIGNED auto_increment NOT NULL,
	Group_ varchar(1024) NOT NULL,
	Name varchar(1024) NOT NULL,
	IntValue INT NULL,
	LongValue BIGINT NULL,
	FloatValue FLOAT NULL,
	DoubleValue DOUBLE NULL,
	BoolValue BOOL NULL,
	StringValue TEXT NULL,
	CONSTRAINT GenericStore_PK PRIMARY KEY (Id),
	CONSTRAINT GenericStore_GroupName_UNIQUE UNIQUE KEY (Group_,Name)
	CREATE INDEX GenericStore_Group__IDX USING BTREE ON test.GenericStore (Group_);
CREATE INDEX GenericStore_Name_IDX USING BTREE ON test.GenericStore (Name);

ALTER TABLE test.GenericStore ADD `Type` TINYINT DEFAULT 5 NOT NULL;
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
AUTO_INCREMENT=1;

 */

public class SqlStoreEntity
{
    public long Id { get; set; }
    public required string Group_ { get; set; }
    public required string Name { get; set; }
    public required GenericStoreType Type { get; set; }
    public int? IntValue { get; set; }
    public long? LongValue { get; set; }
    public float? FloatValue { get; set; }
    public double? DoubleValue { get; set; }
    public bool? BoolValue { get; set; }
    public string? StringValue { get; set; }
}

public class SqlBinder : GenericStoreBinder
{
    private string _connectionString;
    private string _table;
    
    private const string _id = nameof(binders.SqlStoreEntity.Id);
    private const string _group = nameof(binders.SqlStoreEntity.Group_);
    private const string _name = nameof(binders.SqlStoreEntity.Name);
    private const string _type = nameof(binders.SqlStoreEntity.Type);

    public SqlBinder(string connectionString, string table)
    {
        _connectionString = connectionString;
        _table = table;
    }

    private SqlStoreEntity? SqlStoreEntity(string group, string name)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var sql = $"SELECT * FROM {_table} WHERE Group_ = @group AND Name = @name";
        var variable = con.QueryFirstOrDefault<SqlStoreEntity>(sql, new { group, name });
        return variable;
    }

    private void SetValue(string group, string name, GenericStoreType type, string column, string value)
    {
        var variable = SqlStoreEntity(group, name);
        
        using var con = new MySqlConnection(_connectionString);
        con.Open();

        if (variable != null)
        {
            // variable already exists
            if (variable.Type == type)
            {
                var sql = $"UPDATE {_table} SET {column} = @value WHERE {_id} = @id";
                con.Execute(sql, new { value, id = variable.Id });
            }
            else
            {
                // todo exc
            }
        }
        else
        {
            // variable does not exist
            con.Execute($"INSERT INTO {_table}({_group}, {_name}, {_type}, {column}) values (@group, @name, @value)", 
                new { group, name, type, value });
        }
    }

    public int? GetIntValue(string group, string name)
    {
        var variable = SqlStoreEntity(group, name);
        return variable?.IntValue;
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
        SetValue(group, name, nameof(binders.SqlStoreEntity.IntValue), GenericStoreType.Int, value.ToString());
    }

    public void SetValue(string group, string name, long value)
    {
        SetValue(group, name, nameof(binders.SqlStoreEntity.LongValue), GenericStoreType.Long, value.ToString());
    }

    public void SetValue(string group, string name, float value)
    {
        SetValue(group, name, nameof(binders.SqlStoreEntity.FloatValue), GenericStoreType.Float, value.ToString(CultureInfo.InvariantCulture));
    }

    public void SetValue(string group, string name, double value)
    {
        SetValue(group, name, nameof(binders.SqlStoreEntity.DoubleValue), GenericStoreType.Double, value.ToString(CultureInfo.InvariantCulture));
    }

    public void SetValue(string group, string name, bool value)
    {
        SetValue(group, name, nameof(binders.SqlStoreEntity.BoolValue), GenericStoreType.Bool, value.ToString());
    }

    public void SetValue(string group, string name, GenericStoreType type, string value)
    {
        SetValue(group, name, nameof(binders.SqlStoreEntity.StringValue), type, value);
    }

    public void ClearGroup(string group)
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();
        var sql = $"DELETE FROM {_table} WHERE {_group} = @group";		
        con.Execute(sql, new { group });
    }
}