using Backups.Entities;

namespace Backups.Exceptions
{
    public class EntityExistInOSException : Exception
    {
        public EntityExistInOSException(BackupObjectComponent backupObjectComponent)
            : base(string.Format("Entity {0} exist in OS", backupObjectComponent.GetPath()))
        {
        }
    }
}