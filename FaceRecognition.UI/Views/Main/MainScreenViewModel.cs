using Caliburn.Micro;
using FaceRecognition.UI.Constants.ApplicationConfiguration;
using FaceRecognition.UI.CustomControls.PersonImagePresenter;
using FaceRecognition.UI.Framework.PerRequestViewBase;
using FaceRecognition.UI.Framework.SingletonViewBase;
using FaceRecognition.UI.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Views
{
    public class MainScreenViewModel : PerRequestBase, IMainScreenViewModel 
    {
        public Uri VdoFileName
        {
            get
            {
                return this._vdoFileName;
            }
            set
            {
                if (value != _vdoFileName)
                {
                    this._vdoFileName = value;
                    NotifyOfPropertyChange(() => VdoFileName);
                }
            }
        }
        private Uri _vdoFileName;

        public int Interval
        {
            get
            {
                return this._interval;
            }
            set
            {
                if (value != _interval)
                {
                    this._interval = value;
                    NotifyOfPropertyChange(() => Interval);
                }
            }
        }
        private int _interval;

        public ObservableCollection<PersonImagePresenterModel> PersonImage
        {
            get
            {
                return this._personImage;
            }
            set
            {
                if (value != _personImage)
                {
                    this._personImage = value;
                    NotifyOfPropertyChange(() => PersonImage);
                }
            }
        }
        private ObservableCollection<PersonImagePresenterModel> _personImage;

        public Guid TabGuid { get; private set; }

        public string VdoPath
        {
            get
            {
                return this._vdoPath;
            }
            set
            {
                if (value != _vdoPath)
                {
                    this._vdoPath = value;
                    NotifyOfPropertyChange(() => VdoPath);
                }
            }
        }
        private string _vdoPath;

        public MainScreenViewModel() : base()
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            TabGuid = new Guid("d101a42e-9a67-49a4-903c-93c51198f169");
            
            //Guid.NewGuid();
            Interval = 3000;
        }

        public void BrowseVdo()
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    VdoPath = dialog.FileName;
                }
            }
        }

        public void PreviewVdo()
        {
            VdoFileName = new Uri(VdoPath);
        }

        public void StopVdo()
        {
            VdoFileName = null;
        }

        public void Proceed()
        {
            PersonImage = new ObservableCollection<PersonImagePresenterModel>();
            if (!string.IsNullOrWhiteSpace(VdoPath))
            {
                var fullPath = VdoPath;

                var coreExePath = IoC.Get<IApplicationConfiguration>().GetCoreExePath();
                //ProcessUtils.ExecuteCommand(coreExePath, new string[] 
                //                                { 
                //                                    this.VdoFileName.OriginalString, 
                //                                    "SVM", 
                //                                    Interval.ToString(),
                //                                    IoC.Get<IApplicationConfiguration>().ApplicationGuid.ToString(),
                //                                    this.TabGuid.ToString()
                //                                });

                var tabDirectory = IoC.Get<IApplicationConfiguration>().GetTabDataDirectory(this.TabGuid);
                var tabDataDirectory = new DirectoryInfo(tabDirectory);
                var result = tabDataDirectory.GetDirectories();
                foreach (var person in result)
                {
                    PersonImage.Add(new PersonImagePresenterModel
                    {
                        PersonName = person.Name,
                        ImagePaths = new ObservableCollection<string>(Directory.GetFiles(person.FullName).ToList()),
                        HeaderMode = 0
                    });
                }
            }
        }
    }
}
