﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.CustomControls.PersonImagePresenter
{
    public class RemovePersonEventArgs : EventArgs
    {
        public string ImageDirectory { get; set; }
    }
}
