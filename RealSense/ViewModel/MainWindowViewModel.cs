using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using GeneratorLibrary;
using Newtonsoft.Json;
using RealSense.Commands;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace RealSense.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public const string PathToProject = "instahandless";
        public const string RealSenseExtensionString = "RealSense Extension"; 

        #region Private Fields

        private string _configFilePath;
        private string _directoryPath;
        private string _fileName;
        private string _directoryName;
        

        #endregion

        #region Public Fields

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnProprtyChanged("FileName");
            }
        }

        public string DirectoryName
        {
            get { return _directoryName; }
            set
            {
                _directoryName = value;
                OnProprtyChanged("DirectoryName");
            }
        }

        public ICommand ChooseConfigCommand { get; set; }
        public ICommand ChooseDirectoryCommand { get; set; }
        public ICommand CompileCommand { get; set; }

        #endregion

        #region Ctors

        public MainWindowViewModel()
        {
            ChooseConfigCommand = new RelayCommand(ChooseConfigExecute);
            ChooseDirectoryCommand = new RelayCommand(ChooseDirectoryExecute);
            CompileCommand = new RelayCommand(CompileExecute, CompileCanExecute);
        }

        #endregion

        #region Private Methods

        void ChooseConfigExecute()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Json file | *.json";
            if (dialog.ShowDialog() ?? false)
            {
                _configFilePath = dialog.FileName;
                FileName = dialog.SafeFileName;
            }
        }
        void ChooseDirectoryExecute()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                _directoryPath = dialog.SelectedPath;
                DirectoryName = Path.GetFileName(dialog.SelectedPath);
            }
        }
        bool CompileCanExecute()
        {
            return !string.IsNullOrEmpty(FileName) &&
                   !string.IsNullOrEmpty(DirectoryName);
        }
        void CompileExecute()
        {
            try
            {
                var json = File.ReadAllText(_configFilePath);
                dynamic config = JsonConvert.DeserializeObject(json);
                var path = _directoryPath + "/" + config.name + " " + RealSenseExtensionString;
                if (Directory.Exists(path))
                    if (
                        MessageBox.Show("Current extension already generated.\n\nDo you want replace this extension?",
                            "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        Directory.Delete(path, true);
                    else return;
                    FolderManager.CopyDir(new DirectoryInfo(PathToProject),
                    _directoryPath + "/" + config.name + " " + RealSenseExtensionString);
            }
            catch (Exception ex)
            {
//                MessageBox.Show("Something goes wrong");
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Extension generate sucessfully", "Generate complete", MessageBoxButtons.OK);
        }


        #endregion
    }
}
