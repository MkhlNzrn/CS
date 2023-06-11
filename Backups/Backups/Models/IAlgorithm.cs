using Backups.Entities;

namespace Backups.Models
{
    public interface IAlgorithm
    {
        public string Path { get; }

        IEnumerable<Storage> Backup(List<BackupObjectComponent> backupObjectComponents, string path, bool isPhisical);

        // public IEnumerable<Storage> BackupInMemory(List<BackupObjectComponent> backupObjectComponents, string path);
    }
}