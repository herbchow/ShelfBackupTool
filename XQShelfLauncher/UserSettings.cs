using System.IO;
using Newtonsoft.Json;

namespace XQShelfLauncher
{
    public class UserSettings
    {
        public SettingsData Data { get; private set; }

        public class SettingsData
        {
            public string ExePath { get; set; }
            public string ContentPath { get; set; }
        }

        private const string settingsFile = "ShelfLauncherSettings.json";

        public void Load()
        {
            string text = "";
            try
            {
                text = File.ReadAllText(settingsFile);
            }
            catch(FileNotFoundException e)
            {
                
            }
            if (!string.IsNullOrEmpty(text))
            {
                Data = JsonConvert.DeserializeObject<SettingsData>(text);
            }
        }

        public void Save(string exePath, string contentPath)
        {
            var text = JsonConvert.SerializeObject(new SettingsData {ExePath = exePath, ContentPath = contentPath});
            File.WriteAllText(settingsFile, text);
        }
    }
}
