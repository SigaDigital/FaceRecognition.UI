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
            this.Clear();

            foreach (var person in trainList)
            {
                var individualTrainPath = Path.Combine(tempTrainPath, person.PersonName);
                Directory.CreateDirectory(individualTrainPath);

                foreach(var img in person.ImagePaths)
                {
                    File.Move(img, Path.Combine(individualTrainPath, Path.GetFileName(img)));
                }

                var datFiles = Directory.GetFiles(Path.Combine(unknownPeopleDirectory, Path.GetDirectoryName(person.ImagePaths[0])), "*.dat").ToList();
                File.Move(datFiles[0], Path.Combine(individualTrainPath, string.Format("Descriptor_{0}.dat", person.PersonName)));

                var prevDir = new DirectoryInfo(Path.Combine(unknownPeopleDirectory, Path.GetDirectoryName(person.ImagePaths[0])));
                prevDir.Delete();

                ProcessUtils.ExecuteCommand(coreExePath, new string[]
                                                {
                                                    "train",
                                                    person.PersonName,
                                                    individualTrainPath,
                                                    IoC.Get<IApplicationConfiguration>().GetDescriptorPath()
                                                });
            }

            foreach (FileInfo file in tempDirInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in tempDirInfo.GetDirectories())
            {
                dir.Delete(true);
            }

            this.UpdateUnknownPeople();
        }
    }
}
