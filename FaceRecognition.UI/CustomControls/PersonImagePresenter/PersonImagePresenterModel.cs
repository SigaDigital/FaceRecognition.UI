using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.CustomControls.PersonImagePresenter
{
    public class PersonImagePresenterModel : PropertyChangedBase
    {
        public string PersonName 
        { 
            get
            {
                return this._personName;
            } 
            set
            {
                if (this._personName != value)
                {
                    this._personName = value;
                    NotifyOfPropertyChange(() => PersonName);
                }
            } 
        }
        private string _personName { get; set; }

        public ObservableCollection<string> ImagePaths
        {
            get
            {
                return this._imagePaths;
            }
            set
            {
                if (this._imagePaths != value)
                {
                    this._imagePaths = value;
                    NotifyOfPropertyChange(() => ImagePaths);
                }
            }
        }
        private ObservableCollection<string> _imagePaths { get; set; }

        public HeaderMode HeaderMode
        {
            get
            {
                return this._headerMode;
            }
            set
            {
                if (this._headerMode != value)
                {
                    this._headerMode = value;
                    NotifyOfPropertyChange(() => HeaderMode);
                }
            }
        }
        private HeaderMode _headerMode { get; set; }

        public bool HasChanged
        {
            get
            {
                return this._hasChaged;
            }
            set
            {
                if (this._hasChaged != value)
                {
                    this._hasChaged = value;
                    NotifyOfPropertyChange(() => HasChanged);
                }
            }
        }
        private bool _hasChaged { get; set; }
    }

    public enum HeaderMode
    {
        VIEW = 0,
        EDITABLE = 1
    }
}
