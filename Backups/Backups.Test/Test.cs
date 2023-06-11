using Backups.Entities;
using Backups.Models;
using Backups.Services;
using Xunit;

namespace Backups.Test
{
    public class Test
    {
        [Fact]
        public void AddTwoFilesSplitStorage()
        {
            var root = new Composite(new BackupObjectComponent("Root", true));
            IRepository repository = new InMemoryRepository(root);
            BackupObjectComponent obj6 = repository.CreateEntity("File 1.txt", false);
            BackupObjectComponent obj7 = repository.CreateEntity("File 2.txt", false);
            IBackupTask backupTasks = new BackupTask(repository, new SplitAlgorithm(root));
            backupTasks.AddBackupsFromRepository();
            backupTasks.BackupInMemory();
            Backup firstTest = backupTasks.GetBackup;
            backupTasks.DeleteBackup(obj6);
            backupTasks.BackupInMemory();
            Backup secondTest = backupTasks.GetBackup;
            int restorePointsCount = backupTasks.GetBackup.AllBackup.Count;
            Assert.Equal(2, restorePointsCount);
            Assert.Equal(2, backupTasks.GetBackup.AllBackup[0].BackupObjects.Count);
            Assert.Single(backupTasks.GetBackup.AllBackup[1].BackupObjects);
        }
    }
}