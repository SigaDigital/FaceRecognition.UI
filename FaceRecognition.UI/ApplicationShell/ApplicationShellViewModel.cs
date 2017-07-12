using Caliburn.Micro;
using FaceRecognition.UI.Framework.SingletonViewBase;
using FaceRecognition.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ApplicationShell
{
    public class ApplicationShellViewModel : Screen, IApplicationShellViewModel
    {
        public IMainScreenViewModel MainScreenView 
        { 
            get { return _mainScreenView; } 
            set
            {
                _mainScreenView = value;
                NotifyOfPropertyChange(() => MainScreenView);
            }
        }
        private IMainScreenViewModel _mainScreenView;

        public IUnknownViewModel UnknowScreenView
        {
            get { return _unknownScreenView; }
            set
            {
                _unknownScreenView = value;
                NotifyOfPropertyChange(() => UnknowScreenView);
            }
        }
        private IUnknownViewModel _unknownScreenView;

        public ApplicationShellViewModel()
        {
            MainScreenView = IoC.Get<IMainScreenViewModel>();
            UnknowScreenView = IoC.Get<IUnknownViewModel>();
        }

        private void Configure() 
        {
            
        }
    }
}
