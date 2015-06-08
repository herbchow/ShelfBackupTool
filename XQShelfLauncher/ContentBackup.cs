using System;
using System.Collections.Generic;
using System.IO;

namespace XQShelfLauncher
{
    public class ContentBackup
    {
        const string ShelfbackupPrefix = "_shelfbackup_";
        public string ShelfDataPath { get; set; }

        private readonly string[] _foldersToCopy = new[]
            {
                "Config", "ContentDatabase", "Database", "Assets//Virtual Shelf"
            };

        public string Save(string backupNameNoPrefix)
        {
            if(string.IsNullOrEmpty(ShelfDataPath))
                throw new InvalidOperationException("Must set ShelfDataPath");

            var nameWithPrefix = AddPrefix(backupNameNoPrefix);
            foreach (var currentFolder in _foldersToCopy)
            {
                var sourcePath = Path.Combine(ShelfDataPath, currentFolder);
                var destPath = Path.Combine(new[]
                    {
                        ShelfDataPath, nameWithPrefix, currentFolder
                    });
                FileUtils.DirectoryCopy(sourcePath, destPath, true, true);
            }
            return nameWithPrefix;
        }

        private string AddPrefix(string backupNameNoPrefix)
        {
            return ShelfbackupPrefix + backupNameNoPrefix;
        }

        public string Restore(string backupName)
        {
            if (string.IsNullOrEmpty(ShelfDataPath))
                throw new InvalidOperationException("Must set ShelfDataPath");

            string nameWithPrefix = backupName;
            if (!backupName.StartsWith(ShelfbackupPrefix))
            {
                nameWithPrefix = AddPrefix(backupName);   
            }
            
            foreach (var currentFolder in _foldersToCopy)
            {
                var destPath = Path.Combine(ShelfDataPath, currentFolder);
                var sourcePath = Path.Combine(new[]
                    {
                        ShelfDataPath, nameWithPrefix, currentFolder
                    });
                FileUtils.DirectoryCopy(sourcePath, destPath, true, true);
            }
            return nameWithPrefix;
        }

        public IList<string> FindBackupsInPath()
        {
            if (string.IsNullOrEmpty(ShelfDataPath))
                throw new InvalidOperationException("Must set ShelfDataPath");

            var allSubdirs = Directory.GetDirectories(ShelfDataPath);
            var backups = new List<string>();
            foreach (var subdir in allSubdirs)
            {
                var lastDir = Path.GetFileName(subdir);

                if (lastDir.StartsWith(ShelfbackupPrefix))
                {
                    backups.Add(lastDir);
                }
            }
            return backups;
        }
    }
}
