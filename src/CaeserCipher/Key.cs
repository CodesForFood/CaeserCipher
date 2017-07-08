using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaeserCipher
{
    class Key : Cipher
    {
        static string TextBuffer { get; set; }
        static string[] LineBuffer { get; set; }
        string OpenedFile { get; set; }
        

        public Key(FileInfo inFile) : base(inFile)
        {
            OpenFile(inFile);
        }

        //open the file
        public void OpenFile(FileInfo inFile)
        {
            OpenedFile = inFile.FullName;
            using (var reader = new StreamReader(OpenedFile))
            {
                TextBuffer = reader.ReadToEnd();
                LineBuffer = TextBuffer.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            }

        }            

        public void Decrypt()
        {
            string changedLine;
            using (var writer = new StreamWriter("decrypted.txt", false, Encoding.Unicode))
            {
                foreach (var line in LineBuffer)
                {
                    changedLine = DecryptLine(line);
                    //Console.WriteLine(changedLine);//just shows in console
                    writer.WriteLine(changedLine);                   
                }
            }

           
        }

        public void ToConsole()
        {
           foreach(var line in LineBuffer)
            {
                Console.WriteLine(line);
            }
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
            decimal decOfChar = inChar;
            char changedChar = new char();
            decOfChar += 3;

            try
            {
                changedChar = (char)decOfChar;
            }
            catch (OverflowException)
            {
                decOfChar -= 6;
                changedChar = (char)decOfChar;
            }
            finally
            {
                outChar = changedChar;
            }
        }
    }
}
