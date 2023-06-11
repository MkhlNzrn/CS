using System.IO.Compression;
using Backups.Entities;

namespace Backups.Models
{
    public class SplitAlgorithm : IAlgorithm
    {
        private readonly string _path;
        private Composite root;
        private ZipArchive _zip;
        private string _storageAlgorithmName;

        public SplitAlgorithm(string path)
        {
            this._path = path;
            this._storageAlgorithmName = "SplitAlgorithm";
        }

        public SplitAlgorithm(Composite root)
        {
            this.root = root;
        }

        public string Path { get; }

        public IEnumerable<Storage> Backup(List<BackupObjectComponent> backupObjectComponents, string path, bool isPhisical)
        {
            List<Storage> storageList = new List<Storage>();
            if (!isPhisical)
            {
                foreach (var entity in backupObjectComponents)
                {
                    Component original = root.GetChildren()
                        .FirstOrDefault(s => s.BackupObjectComponent.GetName() == entity.GetName());
                    if (original != null)
                    {
                        Component component = original.GetChildren().LastOrDefault();
                        if (component != null)
                        {
                            storageList.Add(new Storage(root + "/" + GetFullName(), backupObjectComponents));
                            if (entity.IsFolder())
                            {
                                string temporaryName = string.Format("{0} SplitStorageInMemory {1}", entity.GetName(), Guid.NewGuid().ToString());
                                component.Add(new FolderComponent(new BackupObjectComponent(temporaryName, true)));
                            }
                            else
                            {
                                string temporaryName = string.Format("{0} SplitStorageInMemory {1}", entity.GetName(), Guid.NewGuid().ToString());
                                component.Add(new FileComponent(new BackupObjectComponent(temporaryName, false)));
                            }
                        }
                    }
                }

                return storageList;
            }

            foreach (BackupObjectComponent entity in backupObjectComponents)
            {
                using (_zip = ZipFile.Open(GetFullName(), ZipArchiveMode.Create))
                {
                    storageList.Add(new Storage(root + "/" + GetFullName(), backupObjectComponents));
                    string name = GetEntryName(entity);
                    if (entity.IsFolder())
                    {
                        _zip.CreateEntry(name);
                    }
                    else
                    {
                        _zip.CreateEntryFromFile(entity.GetPath(), name);
                    }
                }
            }

            return storageList;
        }

        protected string GetFullName()
        {
            return string.Format(@"{0}\{1} {2}-{3}.zip", _path, _storageAlgorithmName, DateTime.Now.ToShortDateString(), Guid.NewGuid());
        }

        protected string GetEntryName(BackupObjectComponent backupObjectComponent)
        {
            string name = null;

            if (backupObjectComponent.IsFolder())
            {
                name = string.Format(@"{0} {1}\", backupObjectComponent.GetName(), Guid.NewGuid());
            }
            else
            {
                string[] file = backupObjectComponent.GetName().Split('.');
                name = string.Format(@"{0} {1}.{2}", backupObjectComponent.GetName(), Guid.NewGuid(), file[1]);
            }

            return name;
        }
    }
}