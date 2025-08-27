namespace TestWebAPI1;

// todo use dependency injection where possible to avoid this class
public static class Configuration
{
    private static IConfiguration? _config;
    
    private static SensorsApi? _sensorsApi;

    public static void SetConfig(IConfiguration configuration)
    {
        _config ??= configuration;
        _sensorsApi = new SensorsApi(configuration.GetSection("SensorsApi"));
    }

    public static SensorsApi SensorsApi()
    {
        return _sensorsApi!;
    }
}

public class SensorsApi
{
    private readonly IConfiguration _sensorsApi;

    public SensorsApi(IConfiguration sensorsApi)
    {
        _sensorsApi = sensorsApi;
    }

    public string Url()
    {
        return _sensorsApi.GetValue<string>("Url")!;
    }

    public int Port()
    {
        return _sensorsApi.GetValue<int>("Port");
    }

    public string BasePath()
    {
        return _sensorsApi.GetValue<string>("BasePath")!;
    }

    public string Command()
    {
        return _sensorsApi.GetValue<string>("Command")!;
    }
}