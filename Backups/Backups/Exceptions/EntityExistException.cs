namespace Backups.Exceptions
{
    public class EntityExistException : Exception
    {
        public EntityExistException(string name)
            : base(string.Format("Entity {0} exist in repository", name))
        {
        }
    }
}