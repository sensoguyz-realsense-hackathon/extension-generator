using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using RealSense.Commands;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace RealSense.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public const string PathToProject = "asd";

        #region Private Fields

        private string _configFilePath;
        private string _directoryPath;
        

        #endregion

        #region Public Fields

        public string ConfigFilePath
        {
            get { return _configFilePath; }
            set
            {
                _configFilePath = value;
                OnProprtyChanged("ConfigFilePath");
            }
        }

        public string DirectoryPath
        {
            get { return _directoryPath; }
            set
            {
                _directoryPath = value;
                OnProprtyChanged("DirectoryPath");
            }
        }

        public ICommand ChooseConfigCommand { get; set; }
        public ICommand ChooseDirectoryCommand { get; set; }

        #endregion

        #region Ctors

        public MainWindowViewModel()
        {
            ChooseConfigCommand = new RelayCommand(ChooseConfigExecute);
            ChooseDirectoryCommand = new RelayCommand(ChooseDirectoryExecute);
        }
        #endregion

        #region Private Methods

        void ChooseConfigExecute()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Json file | *.json";
            if (dialog.ShowDialog() ?? false)
            {
                ConfigFilePath = Path.GetFileName(dialog.FileName);
            }
        }
        void ChooseDirectoryExecute()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                DirectoryPath = Path.GetFileName(dialog.SelectedPath);
            }
        }


        #endregion
    }
}
