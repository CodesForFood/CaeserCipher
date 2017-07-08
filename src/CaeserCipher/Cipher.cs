using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//can encrypt and decrypt


namespace CaeserCipher
{
    class Cipher
    {
        
        private static string FileContent { get; set; }//Entire content
        private static string[] FileLines { get; set; }
        private static string  CurFile { get; set; }

        public Cipher(FileInfo inFile)
        {
            if(inFile.Exists)
            {
                CurFile = inFile.FullName;
                using (var reader = new StreamReader(CurFile))//using auto closes StreamReader
                {
                    FileContent = reader.ReadToEnd();                
                    FileLines = FileContent.Split(new char[] { '\n','\r' }, StringSplitOptions.RemoveEmptyEntries);//Splits file content by new line escapes
                }

            }

        }




        //takes a line as string and changes each char 
        //change char first
        //change each char in line
        //line by line
        // chars to decimal


        //
        public void Encrypt()
        {
            string changedLine;
            using (var writer = new StreamWriter("encrypted.txt", false, Encoding.Unicode))
            { 
                foreach (var line in FileLines)
                {
                    changedLine = EncryptLine(line);
                    //Console.WriteLine(changedLine);//shows to console
                    writer.WriteLine(changedLine);
                    //Im here trying to write the encryption to the txt file 
                }
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

        //transforms char into decimal,changes decimal, then back out as char
        private static void EncryptChar(char inChar, out char outChar)
        {
            decimal decOfChar = inChar;
            char changedChar = new char();
            decOfChar -= 3;         

            try
            {
                 changedChar = (char)decOfChar;
            }
            catch (OverflowException)
            {
                decOfChar += 6;
                changedChar = (char)decOfChar;                
            }
            finally
            {
                outChar = changedChar;
            }         
        }


        public void ReadFile()
        {         
            foreach (var line in FileLines)
            {
                Console.WriteLine(line);
            }
        }




    }
}
