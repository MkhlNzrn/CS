using Backups.Entities;
using Backups.Exceptions;

namespace Backups.Models
{
    public class Repository : IRepository
    {
        private List<BackupObjectComponent> _entities;

        public Repository(string path)
        {
            Path = path;
            _entities = new List<BackupObjectComponent>();
        }

        public string Path { get; }
        public IReadOnlyCollection<BackupObjectComponent> Entities => _entities.AsReadOnly();

        public BackupObjectComponent CreateEntity(string name, bool isFolder)
        {
            if (_entities.Any(s => s.GetName() == name)) throw new EntityExistException(name);

            var backupObjectComponent =
                new BackupObjectComponent(name, isFolder, string.Format("{0}\\{1}", Path, name));
            _entities.Add(backupObjectComponent);

            CreateFileAndDictionary(backupObjectComponent);

            return backupObjectComponent;
        }

        public BackupObjectComponent FindEntity(string name)
        {
            BackupObjectComponent obj = _entities.Find(s => s.GetName() == name);
            if (obj == null) throw new EntityNotExistException(name);
            return obj;
        }

        public void RemoveEntity(string name)
        {
            int index = _entities.FindIndex(s => s.GetName() == name);
            if (index == -1)
                throw new EntityNotExistException(name);

            _entities.RemoveAt(index);
        }

        public void CopyEntities(IList<BackupObjectComponent> entities)
        {
            foreach (BackupObjectComponent entity in entities)
            {
                if (entity.IsFolder())
                {
                    Directory.CreateDirectory(string.Format("{0} {1} {2}", entity.GetPath(), "Copy", Guid.NewGuid().ToString()));
                }
                else
                {
                    string[] file = entity.GetPath().Split('.');
                    using (FileStream fstream =
                           new FileStream(
                               string.Format("{0} {1} {2}.{3}", file[0], "Copy", Guid.NewGuid().ToString(), file[1]), FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
                    {
                    }
                }
            }
        }

        private void CreateFileAndDictionary(BackupObjectComponent backupObjectComponent)
        {
            if (backupObjectComponent.IsFolder())
            {
                if (Directory.Exists(backupObjectComponent.GetPath()))
                    throw new EntityExistInOSException(backupObjectComponent);

                Directory.CreateDirectory(backupObjectComponent.GetPath());
            }
            else
            {
                if (File.Exists(backupObjectComponent.GetPath()))
                    throw new EntityExistInOSException(backupObjectComponent);

                using (FileStream fstream = new FileStream(backupObjectComponent.GetPath(), FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
                {
                }
            }
        }
    }
}