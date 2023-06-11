using Backups.Entities;

namespace Backups.Services
{
    public interface IBackupTask
    {
        Backup GetBackup { get; }
        void AddBackup(BackupObjectComponent backupObjectComponent);
        void AddBackupsFromRepository();
        void DeleteBackup(BackupObjectComponent backupObjectComponent);
        void Backup();
        void BackupInMemory();
    }
}