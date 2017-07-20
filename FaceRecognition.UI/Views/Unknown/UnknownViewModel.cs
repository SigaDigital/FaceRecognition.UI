using Caliburn.Micro;
using FaceRecognition.UI.Constants.ApplicationConfiguration;
using FaceRecognition.UI.CustomControls.PersonImagePresenter;
using FaceRecognition.UI.Framework.SingletonViewBase;
using FaceRecognition.UI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Views
{
    public class UnknownViewModel : SingletonBase, IUnknownViewModel
    {
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

        protected override void OnInitialize()
        {
            UpdateUnknownPeople();
        }

        public void UpdateUnknownPeople()
        {
            PersonImage = new ObservableCollection<PersonImagePresenterModel>();
            var unknownPeopleDirectory = IoC.Get<IApplicationConfiguration>().GetUnknownPeopleDirectory();

            if (!Directory.Exists(unknownPeopleDirectory))
            {
                Directory.CreateDirectory(unknownPeopleDirectory);
            }

            var unknownPeopleDataDirectory = new DirectoryInfo(unknownPeopleDirectory);
            var result = unknownPeopleDataDirectory.GetDirectories();
            foreach (var person in result)
            {
                var files = Directory.GetFiles(person.FullName, "*.jpg").ToList();
                if (files.Count > 0)
                {
                    PersonImage.Add(new PersonImagePresenterModel
                    {
                        PersonName = "",
                        ImagePaths = new ObservableCollection<string>(files),
                        HeaderMode = HeaderMode.EDITABLE
                    });
                }
            }
        }

        public void Clear()
        {
            PersonImage = new ObservableCollection<PersonImagePresenterModel>();
        }

        public void Train()
        {
            var trainList = PersonImage.Where(x => x.HasChanged).ToList();
            var tempTrainPath = IoC.Get<IApplicationConfiguration>().GetTempTrainPath();
            var unknownPeopleDirectory = IoC.Get<IApplicationConfiguration>().GetUnknownPeopleDirectory();
            var tempDirInfo = new DirectoryInfo(tempTrainPath);
            var coreExePath = IoC.Get<IApplicationConfiguration>().GetCoreExePath();
            var appDataPath = IoC.Get<IApplicationConfiguration>().GetAppDataPath();
            this.Clear();

            if (!File.Exists(appDataPath))
            {
                File.Create(Path.Combine(appDataPath, "list.txt")).Dispose();
            }

            using (var tw = new StreamWriter(Path.Combine(appDataPath, "list.txt"), true))
            {
                foreach (var person in trainList)
                {
                    var imgDir = new DirectoryInfo(Path.Combine(unknownPeopleDirectory, Path.GetDirectoryName(person.ImagePaths[0])));

                    foreach (var file in imgDir.GetFiles())
                    {
                        File.Delete(file.FullName);
                    }

                    imgDir.Delete();
                    tw.WriteLine(person.PersonName);
                }

                tw.Close();
            }

            ProcessUtils.ExecuteCommand(@"C:/python27/python.exe", new string[]
                                               {
                                                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\main.py",
                                                    "-f \"" + Path.Combine(appDataPath, "list.txt") + "\"",
                                                    "-t 100",
                                                    "-d \"" + IoC.Get<IApplicationConfiguration>().GetDescriptorPath() + "\"",
                                               });

            File.Delete(Path.Combine(appDataPath, "list.txt"));

            this.UpdateUnknownPeople();
        }
    }
}
