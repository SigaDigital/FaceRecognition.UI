using FaceRecognition.UI.Utils;
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

        private const string CORE_EXE_NAME = "video-tagging.exe";

        private const string UNKNWON_PEOPLE_DIRECTORY_NAME = "Unknows";

        private const string COMMON_DATA_DIRECTORY = "Data";

        private const string DESCRIPTOR_DIRECTORY = "Descriptor";

        private const string TEMP_TRAIN_DIRECTORY = "Temp";

        public ApplicationConfiguration()
        {
            ApplicationName = "VideoTagging";
            ApplicationGuid = Guid.NewGuid();
            ApplicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            this.ValidateAndCreateDirectory(ApplicationDataDirectory);
            this.ValidateAndCreateDirectory(GetUnknownPeopleDirectory());
            this.ValidateAndCreateDirectory(GetTempTrainPath());
            this.ValidateAndCreateDirectory(Path.Combine(ApplicationDataDirectory, INSTANCE_DIRECTORY_NAME));

            if (!Directory.Exists(Path.Combine(ApplicationDataDirectory, COMMON_DATA_DIRECTORY)))
            {
                FileUtils.CopyDirectory(Path.Combine(ApplicationPath, CORE_DIRECTORY, COMMON_DATA_DIRECTORY), Path.Combine(ApplicationDataDirectory, COMMON_DATA_DIRECTORY));
            }
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

        public string GetDescriptorPath()
        {
            return Path.Combine(ApplicationDataDirectory, COMMON_DATA_DIRECTORY, DESCRIPTOR_DIRECTORY);
        }

        public string GetTempTrainPath()
        {
            return Path.Combine(ApplicationDataDirectory, COMMON_DATA_DIRECTORY, TEMP_TRAIN_DIRECTORY);
        }

        public string GetAppDataPath()
        {
            return ApplicationDataDirectory;
        }

        private void ValidateAndCreateDirectory (string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
