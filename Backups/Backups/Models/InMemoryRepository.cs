using Backups.Entities;
using Backups.Exceptions;

namespace Backups.Models
{
    public class InMemoryRepository : IRepository
    {
        private List<BackupObjectComponent> entities;
        private Composite root;

        public InMemoryRepository(Composite root)
        {
            this.root = root;
            entities = new List<BackupObjectComponent>();
        }

        public IReadOnlyCollection<BackupObjectComponent> Entities => root.GetChildren().Select(s => s.BackupObjectComponent).ToList();

        public void CopyEntities(IList<BackupObjectComponent> entities)
        {
            foreach (var entity in entities)
            {
                Component component = root.GetChildren().FirstOrDefault(s => s.BackupObjectComponent.GetName() == entity.GetName());
                if (component != null)
                {
                    if (entity.IsFolder())
                    {
                        string temporaryName = string.Format("{0} {1} {2}", entity.GetName(), "Copy Folder ", Guid.NewGuid().ToString());
                        component.Add(new FolderComponent(new BackupObjectComponent(temporaryName, true)));
                    }
                    else
                    {
                        string temporaryName = string.Format("{0} {1} {2}", entity.GetName(), "Copy File ", Guid.NewGuid().ToString());
                        component.Add(new FileComponent(new BackupObjectComponent(temporaryName, true)));
                    }
                }
            }
        }

        public BackupObjectComponent CreateEntity(string name, bool isFolder)
        {
            BackupObjectComponent backupObjectComponent = null;
            if (entities.Any(s => s.GetName() == name))
                    throw new EntityExistException(name);

            backupObjectComponent = new BackupObjectComponent(name, isFolder);
            entities.Add(backupObjectComponent);

            if (backupObjectComponent.IsFolder())
                {
                    root.Add(new FolderComponent(backupObjectComponent));
                }
            else
            {
                root.Add(new FileComponent(backupObjectComponent));
            }

            return backupObjectComponent;
            }

        public BackupObjectComponent FindEntity(string name)
        {
            return root.GetChildren().Find(x => x.BackupObjectComponent.GetName() == name).BackupObjectComponent;
        }

        public void RemoveEntity(string name)
        {
            root.GetChildren().Remove(root.GetChildren().Find(x => x.BackupObjectComponent.GetName() == name));
        }
    }
}