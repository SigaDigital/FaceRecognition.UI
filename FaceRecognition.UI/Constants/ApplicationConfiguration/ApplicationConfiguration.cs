using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Constants.ApplicationConfiguration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public Guid ApplicationGuid { get; private set; }

        public string ApplicationDataDirectory { get; private set; }

        public string ApplicationName { get; private set; }

        public string ApplicationPath { get; private set; }

        private const string INSTANCE_DIRECTORY_NAME = "Instance";

        private const string CORE_DIRECTORY = "Core";

        private const string CORE_EXE_NAME = "test.exe";

        private const string UNKNWON_PEOPLE_DIRECTORY_NAME = "Unknowns";

        private const string COMMON_DATA_DIRECTORY = "Data";

        public ApplicationConfiguration()
        {
            ApplicationName = "VideoTagging";
            ApplicationGuid = new Guid("0a132ffd-c7c0-468c-8970-90f564d65ed7");
            //Guid.NewGuid();
            ApplicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public string GetTabDataDirectory(Guid tabGuid)
        {
            return Path.Combine(ApplicationDataDirectory, INSTANCE_DIRECTORY_NAME, ApplicationGuid.ToString(), tabGuid.ToString());
        }

        public string GetCoreExePath()
        {
            return Path.Combine(ApplicationPath, CORE_DIRECTORY, CORE_EXE_NAME);
        }

        public string GetUnknownPeopleDirectory()
        {
            return Path.Combine(ApplicationDataDirectory, COMMON_DATA_DIRECTORY, UNKNWON_PEOPLE_DIRECTORY_NAME);
        }
    }
}
