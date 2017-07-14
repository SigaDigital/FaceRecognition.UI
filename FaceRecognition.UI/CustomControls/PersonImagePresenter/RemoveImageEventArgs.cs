using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.CustomControls.PersonImagePresenter
{
    public class RemoveImageEventArgs : EventArgs
    {
        public int ItemIndex { get; set; }

        public string ImagePath { get; set; }
    }
}
