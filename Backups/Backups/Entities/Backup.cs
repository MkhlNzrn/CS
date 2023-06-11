namespace Backups.Entities
{
    public class Backup
    {
        private readonly IList<RestorePoint> restorePoints;

        public Backup()
        {
            restorePoints = new List<RestorePoint>();
        }

        public IList<RestorePoint> AllBackup { get { return restorePoints; } }
    }
}
