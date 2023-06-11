namespace Backups.Exceptions;

public class StorageException : Exception
{
    public StorageException(string name)
        : base(name)
    {
    }
}