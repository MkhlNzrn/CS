namespace Backups.Exceptions
{
    public class EntityNotExistException : Exception
    {
        public EntityNotExistException(string name)
            : base(string.Format("Entity {0} not exist in repository", name))
        {
        }
    }
}