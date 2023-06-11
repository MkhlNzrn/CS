using Backups.Entities;
using Backups.Models;

namespace Backups.Services
{
    public class BackupTask : IBackupTask
    {
        private readonly Dictionary<string, BackupObject> _backups;
        private readonly IRepository _entityRepository;
        private readonly Backup _backup;
        private readonly IAlgorithm _storageAlgorithm;

        public BackupTask(IRepository entityRepository, IAlgorithm storageAlgorithm)
        {
            this._entityRepository = entityRepository;
            this._storageAlgorithm = storageAlgorithm;

            _backups = new Dictionary<string, BackupObject>();
            _backup = new Backup();
        }

        public Backup GetBackup
        {
            get { return _backup; }
        }

        public void AddBackup(BackupObjectComponent backupObjectComponent)
        {
            _backups.Add(backupObjectComponent.GetName(), new BackupObject { BackupObjectComponent = backupObjectComponent });
        }

        public void AddBackupsFromRepository()
        {
            foreach (var entity in _entityRepository.Entities)
                AddBackup(entity);
        }

        public void DeleteBackup(BackupObjectComponent backupObjectComponent)
        {
            if (_backups.ContainsKey(backupObjectComponent.GetName()))
            {
                _backups.Remove(backupObjectComponent.GetName());
            }
        }

        public void Backup()
        {
            RestorePoint restorePoint = new RestorePoint();
            _backup.AllBackup.Add(restorePoint);
            List<BackupObject> backupObjects = _backups.Values.ToList();
            restorePoint.BackupObjects.AddRange(backupObjects);

            List<BackupObjectComponent> entities = backupObjects.Select(s => s.BackupObjectComponent).ToList();
            _entityRepository.CopyEntities(entities);
            _storageAlgorithm.Backup(entities, _storageAlgorithm.Path, true);
        }

        public void BackupInMemory()
        {
            RestorePoint restorePoint = new RestorePoint();
            _backup.AllBackup.Add(restorePoint);
            List<BackupObject> backupObjects = _backups.Values.ToList();
            restorePoint.BackupObjects.AddRange(backupObjects);

            List<BackupObjectComponent> entities = backupObjects.Select(s => s.BackupObjectComponent).ToList();
            _entityRepository.CopyEntities(entities);
            _storageAlgorithm.Backup(entities, _storageAlgorithm.Path, false);
        }
    }
}