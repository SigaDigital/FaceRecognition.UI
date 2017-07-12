using Caliburn.Micro;
using FaceRecognition.UI.ApplicationShell;
using FaceRecognition.UI.Constants.ApplicationConfiguration;
using FaceRecognition.UI.Properties;
using FaceRecognition.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FaceRecognition.UI
{
    public class AppBootstrapper : BootstrapperBase
    {
        SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<IApplicationShellViewModel, ApplicationShellViewModel>();
            container.Singleton<IApplicationConfiguration, ApplicationConfiguration>();
            container.Singleton<IUnknownViewModel, UnknownViewModel>();

            container.PerRequest<IMainScreenViewModel, MainScreenViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var settings = new Dictionary<string, object>
                           {
                               { "SizeToContent", SizeToContent.Manual },
                               { "Height" , 640  },
                               { "Width"  , 1024 },
                           };

            DisplayRootViewFor<IApplicationShellViewModel>(settings);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {
                Assembly.GetExecutingAssembly()
            };
        }
    }
}
