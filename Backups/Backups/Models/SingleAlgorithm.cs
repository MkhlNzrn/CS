using System.IO.Compression;
using Backups.Entities;

namespace Backups.Models
{
    public class SingleAlgorithm : IAlgorithm
    {
        private readonly string path;
        private ZipArchive zip;
        private string storageAlgorithmName;
        private Composite root;

        public SingleAlgorithm(string path)
        {
            this.path = path;
            this.storageAlgorithmName = nameof(SingleAlgorithm);
        }

        public SingleAlgorithm(Composite root)
        {
            this.root = root;
        }

        public string Path { get; }

        public IEnumerable<Storage> Backup(List<BackupObjectComponent> backupObjectComponents, string path, bool isPhisical)
        {
            List<Storage> storageList = new List<Storage>();
            storageList.Add(new Storage(path + "/" + GetFullName(), backupObjectComponents));
            if (!isPhisical)
            {
                string name = string.Format("SingleStorageInMemory {0}", Guid.NewGuid().ToString());
                root.Add(new FolderComponent(new BackupObjectComponent(name, true)));

                Component component = root.GetChildren().FirstOrDefault(s => s.BackupObjectComponent.GetName() == name);
                foreach (var entity in backupObjectComponents)
                {
                    if (component != null)
                    {
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

                return storageList;
            }

            using (zip = ZipFile.Open(GetFullName(), ZipArchiveMode.Create))
            {
                foreach (BackupObjectComponent entity in backupObjectComponents)
                {
                    string name = GetEntryName(entity);

                    if (entity.IsFolder())
                    {
                        zip.CreateEntry(name);
                    }
                    else
                    {
                        zip.CreateEntryFromFile(entity.GetPath(), name);
                    }
                }
            }

            return storageList;
        }

        protected string GetFullName()
        {
            return string.Format(@"{0}\{1} {2}-{3}.zip", path, storageAlgorithmName, DateTime.Now.ToShortDateString(), Guid.NewGuid());
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
                string[] file = backupObjectComponent.GetPath().Split('.');
                name = string.Format(@"{0} {1}.{2}", backupObjectComponent.GetName(), Guid.NewGuid(), file[1]);
            }

            return name;
        }
    }
}