using System;
using System.Text;

namespace XQShelfLauncher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string ShelfPath = "C:\\GitAlt\\XQ.Shelf\\build\\Win32\\XQ.Shelf.exe";
            const string kShelfDataPath = "C://Users//herbert.chow//AppData//LocalLow//iQmetrix//XQ_Shelf";
            var backup = new ContentBackup{ShelfDataPath = kShelfDataPath};
            var locatedBackups = backup.FindBackupsInPath();
            var builder = new StringBuilder();
            foreach (var item in locatedBackups)
            {
                if (builder.ToString().Length == 0)
                {
                    builder.Append(item);
                }
                else
                {
                    builder.Append(", " + item);
                }
            }
            Console.WriteLine("Found backups: " + builder);
            Console.WriteLine("Restore(1) or Save (2)? ");
            var choice = Console.ReadLine();
            var choiceInt = -1;
            if (int.TryParse(choice, out choiceInt))
            {
                switch (choiceInt)
                {
                    case 1:
                        Console.WriteLine("Enter folder to restore: ");
                        var restoreFolder = Console.ReadLine();
                        backup.Restore(restoreFolder);
                        var processStarter = new ProcessStarter();
                        processStarter.Start(ShelfPath);
                        break;
                    case 2:
                        Console.WriteLine("Enter name of backup (ie: dsw, hitcase, etc.): ");
                        var saveFolder = Console.ReadLine();
                        backup.Save(saveFolder);
                        break;
                }
            }
        }
    }
}
