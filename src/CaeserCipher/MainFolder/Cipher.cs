using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//can encrypt and decrypt


namespace CaeserCipher.MainFolder
{
    class Cipher
    {
        
        private static string FileContent { get; set; }//Entire content
        private static string[] FileLines { get; set; }
        private static FileInfo CurFile { get; set; }

        string menu = "<1>Encrypt file" +
                   "\n<2>Decrypt file" +
                   "\n<3>View file" +
                   "\n<4>Exit Program";

        //takes a line as string and changes each char 
        //change char first
        //change each char in line
        //line by line
        // chars to int

        public Cipher(FileInfo inFile)
        {
            CurFile = inFile;
            SetContent();
        }

        void SetContent()
        {
            using (var reader = new StreamReader(CurFile.FullName))//using auto closes StreamReader
            {
                FileContent = reader.ReadToEnd();
                FileLines = FileContent.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);//Splits file content by new line escapes
            }
        }
       
        
        public void Encrypt()
        {
            string changedLine;
            using (var writer = new StreamWriter(CurFile.FullName, false, Encoding.Unicode))
            { 
                foreach (var line in FileLines)
                {
                    changedLine = EncryptLine(line);
                    //Console.WriteLine(changedLine);
                    writer.WriteLine(changedLine);                   
                }
                DirFile.Say("Encryption Done");
            }                               
        }

        public void Decrypt()
        {
            string changedLine;
            using (var writer = new StreamWriter(CurFile.FullName, false, Encoding.Unicode))
            {
                foreach (var line in FileLines)
                {
                    changedLine = DecryptLine(line);
                    writer.WriteLine(changedLine);
                }
                DirFile.Say("Decryption Done");
            }
        }


        //changes each char of a string and returns the encrypted string 
        private static string  EncryptLine(string line)
        {
            string changedLine;
            List<char> changedCharList = new List<char>();

            foreach (char letter in line)
            {
                EncryptChar(letter, out char changedChar);
                changedCharList.Add(changedChar);
            }
            changedLine = new string(changedCharList.ToArray());
            return changedLine;
        }

        //transforms char into int,changes int, then back out as char
        private static void EncryptChar(char inChar, out char outChar)
        {
            int decOfChar = inChar;
            char changedChar;
            decOfChar -= 3;         

            if(decOfChar > 0) //No neg int has a char
            { 
                changedChar = (char)decOfChar;
            }
            else
            {
                decOfChar += 6;
                changedChar = (char)decOfChar;
            }       
            
            outChar = changedChar;                 
        }     

        //changes each char of a string and returns the encrypted string 
        private static string DecryptLine(string line)
        {
            string changedLine;
            List<char> changedCharList = new List<char>();
            foreach (char letter in line)
            {
                DecryptChar(letter, out char changedChar);
                changedCharList.Add(changedChar);
            }

            //possible bug, extra null terminator added
            changedLine = new string(changedCharList.ToArray());
            return changedLine;
        }

        //transforms char into decimal,changes decimal, then back out as char
        private static void DecryptChar(char inChar, out char outChar)
        {
            int decOfChar = inChar;
            char changedChar;
            decOfChar += 3;

            // according to ASCII typical char stop at 127, except for extended char
            // I wont support for now
            if (decOfChar < 128) 
            { 
                changedChar = (char)decOfChar;
            }
            else
            {
                decOfChar -= 6;
                changedChar = (char)decOfChar;
            }
            
            outChar = changedChar;                                            
        }

        public void ReadFile()
        {
            DirFile.Say(DirFile.BR);
            foreach (var line in FileLines)
            {
               DirFile.Say(line);
            }
        }

        public void ShowMenu()
        {
            DirFile.Say(DirFile.BR);
            DirFile.Say(menu);

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 1) { Encrypt(); SetContent(); ShowMenu(); }///Three inline statements for 1 and 2
                else if (choice == 2) { Decrypt(); SetContent(); ShowMenu(); }
                else if (choice == 3) { ReadFile(); ShowMenu(); }
                else if(choice == 4) { DirFile.Say("Peace Out..."); Console.ReadKey(); }
                else
                {
                    DirFile.Say("Invalid choice");
                    ShowMenu();
                }

            }
            else { DirFile.Say("Invalid choice"); ShowMenu(); }

        }


    }
}
