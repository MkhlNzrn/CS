namespace Backups.Entities
{
    public class RestorePoint
    {
        private readonly DateTime created;
        private readonly List<BackupObject> backupObjects;
        public RestorePoint()
        {
            this.created = DateTime.Now;
            this.backupObjects = new List<BackupObject>();
        }

        public DateTime Created { get { return created; } }
        public List<BackupObject> BackupObjects { get { return backupObjects; } }
    }
}
