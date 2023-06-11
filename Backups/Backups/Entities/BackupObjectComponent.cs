namespace Backups.Entities
{
    public class BackupObjectComponent
    {
        private string _name;
        private bool _isFolder;

        private string _path;

        public BackupObjectComponent(string name, bool isFolder, string path)
        {
            _name = name;
            _isFolder = isFolder;
            _path = path;
        }

        public BackupObjectComponent(string name, bool isFolder)
        {
            _name = name;
            _isFolder = isFolder;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetPath()
        {
            return _path;
        }

        public bool IsFolder()
        {
            return _isFolder;
        }
    }
}