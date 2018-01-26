using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CaeserCipher.MainFolder
{
    class DirFile
    {
        ///Handles the directory and files

        public const string BR = "----------------------------------------------------------------------------------";


        DirectoryInfo MainDir { get; set; }
        public FileInfo MainFile { get; set; }


        public DirFile()
        {
            GetDirectory();
        }

        public void GetDirectory()
        {
            Say("Please Enter the Directory Name:");
            var dirName = Console.ReadLine();

            try { MainDir = new DirectoryInfo(dirName); }
            catch (Exception) { Say("Not Valid"); GetDirectory(); }

            if (MainDir.Exists)
            {
                Say("Directory Loaded");
                GetFile();
            }
            else
            {
                Say("404: Directory NOT found" +
                    "\n" + BR);
                GetDirectory();
            }
        }

        public void GetFile()
        {
            Say("Looking in: " + MainDir.FullName);
            Say("Enter the file name to be encrypted/decrypted:");
            var fileName = Console.ReadLine();

            MainFile = new FileInfo(Path.Combine(MainDir.FullName, fileName));

            if(MainFile.Exists)
            {
                Say("File has been found");
            }
            else
            {
               Say("404: File NOT found ");
                GetFile();
            }
        }

        //Not sure if this is lazy
        public static void Say(string text) { Console.WriteLine(text); }

    }
}
