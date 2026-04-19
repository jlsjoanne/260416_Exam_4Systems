using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace _260416_Exam_4Systems
{
    public static class SystemMethod
    {
        public static string CombinePath(object folder, object file)
        {
            string folderPath = folder?.ToString() ?? String.Empty;
            string fileName = file?.ToString() ?? String.Empty;

            return Path.Combine(folderPath, fileName);

        }
    }
}