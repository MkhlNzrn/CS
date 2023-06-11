using Backups.Entities;

namespace Backups.Models
{
    public interface IRepository
    {
        IReadOnlyCollection<BackupObjectComponent> Entities { get; }
        BackupObjectComponent CreateEntity(string name, bool isFolder);
        BackupObjectComponent FindEntity(string name);
        void RemoveEntity(string name);
        void CopyEntities(IList<BackupObjectComponent> entities);
    }
}