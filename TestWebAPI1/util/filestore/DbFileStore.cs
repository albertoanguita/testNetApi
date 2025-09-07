namespace TestWebAPI1.util.filestore;

public class DbFileStore
{
    private class DirInfo
    {
        public string Name { get; set; }
        public DirInfo Parent { get; set; }
        public string AbsolutePath { get; set; }
        public long CreatedAt { get; set; }
    }
    
    private class FileInfo : DirInfo
    {
        public long Size { get; set; }
        public byte[] Data { get; set; }
        public long ModifiedAt { get; set; }
    }
    
    private const char PathSeparator = '/';
    
    private readonly string _table;
    
    private readonly DirInfo _root;

    public DbFileStore(string table)
    {
        _table = table;
        // todo ensure root dir is created
        _root = new DirInfo { Name = _table };
    }

    public void Create(string environment, string path, byte[] data, bool overwrite = false)
    {
        var (dirs, fileName) = ParsePath(path);
        
        var dir = EnsureDirsCreated(environment, dirs);
    }

    private DirInfo EnsureDirsCreated(string environment, List<string> dirs)
    {
        if (dirs.Count == 0)
            return _root;
        
        var parent = _root;
        var currentDir = dirs.First();
        var absolutePath = PathSeparator + currentDir;
        
        var dirInfo = GetDir(environment, absolutePath);
        
        if (dirInfo == null)
            dirInfo = CreateDir(environment, parent, currentDir, absolutePath);

        foreach (var dir in dirs.Skip(1))
        {
            parent = dirInfo;
            currentDir = dir;
            absolutePath = absolutePath + PathSeparator + currentDir;
        }

        (DirInfo, string parent, string currentDir, string absolutePath) GetNextDir(string path)
        {
            
        }
        
    }

    private DirInfo CreateDir(string environment, DirInfo parent, string currentDir, string absolutePath)
    {
        // todo add to db
        return new DirInfo
        {
            Parent = parent,
            AbsolutePath = absolutePath,
            CreatedAt = DateTime.UtcNow.Millisecond,
            Name = currentDir
        };
    }

    private DirInfo? GetDir(string environment, string absolutePath)
    {
        return null;
    }

    private static (List<string> dirs, string fileName) ParsePath(string path)
    {
        if (path.EndsWith(PathSeparator))
            throw new ArgumentException($"File path cannot end with {PathSeparator}, received {path}");
        
        var items = path.Split(PathSeparator);
        
        if (items.Length == 0)
            throw new ArgumentException($"Invalid file path received: {path}");
        
        var fileName = items[^1];

        var dirs = items.ToList();
        dirs.RemoveAt(dirs.Count - 1);
        
        return (dirs, fileName);
    }
}