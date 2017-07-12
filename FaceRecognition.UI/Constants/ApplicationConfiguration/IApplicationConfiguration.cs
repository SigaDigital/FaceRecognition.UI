using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Constants.ApplicationConfiguration
{
    public interface IApplicationConfiguration
    {
        Guid ApplicationGuid { get; }

        string ApplicationDataDirectory { get; }

        string ApplicationName { get; }

        string GetUnknownPeopleDirectory();

        string GetTabDataDirectory(Guid tabGuid);

        string GetCoreExePath();
    }
}
