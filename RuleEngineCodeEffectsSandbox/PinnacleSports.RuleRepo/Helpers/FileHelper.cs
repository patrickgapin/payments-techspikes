using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PinnacleSports.RuleRepo.Helpers
{
    public class FileHelper
    {
        public static  IList<string> DirectorySearch(string directoryPath)
        {
            var fileList = new List<string>();
            
            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                fileList.AddRange(Directory.GetFiles(directory));
                DirectorySearch(directory);
            }

            return fileList;
        }
    }
}