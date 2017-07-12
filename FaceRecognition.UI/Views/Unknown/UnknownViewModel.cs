using Caliburn.Micro;
using FaceRecognition.UI.Constants.ApplicationConfiguration;
using FaceRecognition.UI.CustomControls.PersonImagePresenter;
using FaceRecognition.UI.Framework.SingletonViewBase;
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

        public UnknownViewModel()
        {

        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            PersonImage = new ObservableCollection<PersonImagePresenterModel>();
            var unknownPeopleDirectory = IoC.Get<IApplicationConfiguration>().GetUnknownPeopleDirectory();
            var unknownPeopleDataDirectory = new DirectoryInfo(unknownPeopleDirectory);
            var result = unknownPeopleDataDirectory.GetDirectories();
            foreach (var person in result)
            {
                PersonImage.Add(new PersonImagePresenterModel
                {
                    PersonName = person.Name,
                    ImagePaths = new ObservableCollection<string>(Directory.GetFiles(person.FullName).ToList()),
                    HeaderMode = 1
                });
            }
        }
    }
}
