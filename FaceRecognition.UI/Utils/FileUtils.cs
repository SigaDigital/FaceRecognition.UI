using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Utils
{
    public static class FileUtils
    {
        public static void CopyDirectory(string src, string dest)
        {
            foreach (string dirPath in Directory.GetDirectories(src, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(src, dest));

            foreach (string newPath in Directory.GetFiles(src, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(src, dest), true);
        }
    }
}
