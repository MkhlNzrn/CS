using Backups.Entities;
using Backups.Exceptions;

namespace Backups.Models;

public class Storage
{
    private List<BackupObjectComponent> _objects;
    public Storage(string path, List<BackupObjectComponent> objects)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new StorageException("Storage is invalid");
        }

        Path = path;
        _objects = objects;
    }

    public string Path { get; }
}