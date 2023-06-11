namespace Backups.Entities
{
    public class FileComponent : Component
    {
        private List<Component> _children = new List<Component>();

        public FileComponent(BackupObjectComponent backupObjectComponent)
            : base(backupObjectComponent)
        {
        }

        public override void Add(Component c)
        {
            _children.Add(c);
        }

        public override void Remove(Component c)
        {
            _children.Add(c);
        }

        public override List<Component> GetChildren()
        {
            return _children;
        }
    }
}