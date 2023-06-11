namespace Backups.Entities
{
    public abstract class Component
    {
        private readonly BackupObjectComponent _backupObjectComponent;

        public Component(BackupObjectComponent backupObjectComponent)
        {
            this._backupObjectComponent = backupObjectComponent;
        }

        public BackupObjectComponent BackupObjectComponent { get { return _backupObjectComponent; } }
        public abstract void Add(Component c);
        public abstract void Remove(Component c);

        public abstract IReadOnlyCollection<Component> GetChildren();
    }
}