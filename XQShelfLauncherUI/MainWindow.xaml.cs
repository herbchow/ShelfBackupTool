using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using XQShelfLauncher;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace XQShelfLauncherUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ContentBackup _contentBackup;
        private string _selectedBackup;
        private readonly UserSettings _userSettings;
        public ObservableCollection<string> FoundBackups { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _contentBackup = new ContentBackup();
            FoundBackups = new ObservableCollection<string>();
            SelectionListBox.ItemsSource = FoundBackups;
            _userSettings = new UserSettings();
            _userSettings.Load();
            ApplyPreviousSettings(_userSettings.Data);
        }

        private void ApplyPreviousSettings(UserSettings.SettingsData data)
        {
            if (data != null)
            {
                if (
                    Directory.Exists(data.ContentPath))
                {
                    LabelContentPath.Content = data.ContentPath;
                    RefreshBackupsInPath();
                }
            }
        }

        private void ButtonBrowseSettings_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Debug.WriteLine(string.Format("selected folder name: " + folderBrowser.SelectedPath));
                LabelContentPath.Content = folderBrowser.SelectedPath;
                PopulateBackupsInPath(folderBrowser.SelectedPath);
            }
        }

        private void RefreshBackupsInPath()
        {
            var contentPath = (string) LabelContentPath.Content;
            if (!string.IsNullOrEmpty(contentPath))
            {
                PopulateBackupsInPath(contentPath);
            }
        }

        private void PopulateBackupsInPath(string path)
        {
            _contentBackup.ShelfDataPath = path;
            var foundBackups = _contentBackup.FindBackupsInPath();
            FoundBackups.Clear();
            foreach (var backup in foundBackups)
            {
                FoundBackups.Add(backup);
            }
        }

        private void LoadContent_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedBackup))
            {
                ShowOkMessage("Error: No backup selected", "Please select a backup to load");
            }
            else
            {
                var loadedFolder = _contentBackup.Restore(_selectedBackup);
                ShowOkMessage("Loaded: " + loadedFolder, "Loaded");
            }
        }

        private static void ShowOkMessage(string message, string caption)
        {
            var buttons = MessageBoxButtons.OK;
            var result = MessageBox.Show(message, caption, buttons);
        }

        private void SaveContent_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SaveContentName.Text))
            {
                ShowOkMessage("Please enter name of backup above", "Error: No backup folder name entered");
            }
            else
            {
                var savedFolder = _contentBackup.Save(SaveContentName.Text);
                RefreshBackupsInPath();
                ShowOkMessage("Created backup: " + savedFolder, "Saved");
            }
        }

        private void SelectionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                LoadButton.IsEnabled = true;
                _selectedBackup = (string) e.AddedItems[0];
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Store settings data?
            _userSettings.Save((string) LabelContentPath.Content);
        }
    }
}
