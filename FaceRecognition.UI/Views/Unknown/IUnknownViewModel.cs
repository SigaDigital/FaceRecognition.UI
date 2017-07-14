using FaceRecognition.UI.Framework.SingletonViewBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Views
{
    public interface IUnknownViewModel
    {
        void UpdateUnknownPeople();

        void Clear();
    }
}
