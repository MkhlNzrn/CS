namespace Backups.Entities
{
    public class Composite : Component
    {
        private List<Component> children = new List<Component>();

        public Composite(BackupObjectComponent backupObjectComponent)
            : base(backupObjectComponent)
        {
        }

        public override void Add(Component c)
        {
            children.Add(c);
        }

        public override void Remove(Component c)
        {
            children.Remove(c);
        }

        public override List<Component> GetChildren()
        {
            return children;
        }
    }
}