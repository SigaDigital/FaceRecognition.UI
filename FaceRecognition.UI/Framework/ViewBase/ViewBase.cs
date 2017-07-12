using Caliburn.Micro;
using FaceRecognition.UI.Framework.PerRequestViewBase;
using FaceRecognition.UI.Framework.SingletonViewBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Framework.ViewBase
{
    public class ViewBase : Screen
    {
        protected virtual void OnViewReady() { }

        protected virtual void OnInitialized() { }

        public ViewBase()
        {
            this.OnInitialize();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.OnInitialized();
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            this.OnViewReady();
        }
    }
}
