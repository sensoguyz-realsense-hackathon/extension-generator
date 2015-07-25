using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using RealSense.Commands;

namespace RealSense.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields

        private bool _scrollEnabled;
        private bool _swipeEnabled;
        private bool _likeEnabled;

        #endregion

        #region Public Fields

        public bool ScrollEnabled
        {
            get { return _scrollEnabled; }
            set
            {
                _scrollEnabled = value;
                OnProprtyChanged("ScrollEnabled");
            }
        }
        public bool SwipeEnabled
        {
            get { return _swipeEnabled; }
            set
            {
                _swipeEnabled = value;
                OnProprtyChanged("SwipeEnabled");
            }
        }
        public bool LikeEnabled
        {
            get { return _likeEnabled; }
            set
            {
                _likeEnabled = value;
                OnProprtyChanged("LikeEnabled");
            }
        }

        public ICommand GenerateCommand;

        #endregion

        #region Ctors

        public MainWindowViewModel()
        {
            GenerateCommand = new RelayCommand(GenerateCommandExecute, GenerateCommandCanExecute);
        }

        #endregion

        #region Private Methods

        bool GenerateCommandCanExecute()
        {
            return true;
        }

        void GenerateCommandExecute()
        {
            
        }


        #endregion
    }
}
