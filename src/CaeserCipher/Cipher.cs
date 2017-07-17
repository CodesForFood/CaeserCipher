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
        
        public FileInfo CurrentFile { get; set; }
        public DirectoryInfo CurrentDir { get; set; }


        const string MENU =
                    "<1>Encrypt file\n" +
                    "<2>Decrypt file\n" +
                    "<3>View file\n" +
                    "<4>Change file\n" +
                    "<5>Change Directory\n" +
                    "<99>Quit Application";

        public Cipher()
        {
            ChangeDir();
            ChangeFile();

            if (CurrentFile.Exists)
            {

                ProgramLoop();
            }
            else
            {
                Console.WriteLine("Error checking file exist in constructor");
            }

        }

        void ChangeDir()
        {

            Console.WriteLine("What directory are the files in?");
            string input = Console.ReadLine();
            var dir = new DirectoryInfo(input);

            if (dir.Exists)
            {
                CurrentDir = dir;
            }
            else
            {
                Console.WriteLine("404");
                ChangeDir();
            }


        }

        void ChangeFile()
        {
            string input;

            Console.WriteLine("Looking in: " + this.CurrentDir.FullName);
            Console.WriteLine("What file are you looking for?, No need to add .txt");

            input = Console.ReadLine();

            var fileName = Path.Combine(CurrentDir.FullName, input + ".txt");//get full file name           
            var file = new FileInfo(fileName);

            if (file.Exists)
            {
                CurrentFile = file;
                GetContents(CurrentFile.FullName);
            }
            else
            {
                Console.WriteLine("404");
                ChangeFile();
            }
        }

        void GetContents(string file)
        {
            using (var reader = new StreamReader(file))//using auto closes StreamReader
            {
                FileContent = reader.ReadToEnd();
                FileLines = FileContent.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);//Splits file content by new line escapes
            }


        }


        void ProgramLoop()
        {
          
            Console.WriteLine(MENU);

            int.TryParse(Console.ReadLine(), out int choice);

            if (choice != 0)
            {
                if (choice == 1)
                {
                   
                    this.Encrypt();
                    Console.WriteLine("Success, File \"encrypted.txt\" has been created");
                    ProgramLoop();

                }
                else if (choice == 2)
                {
                    var key = new Key(CurrentFile);
                    key.Decrypt();
                    //key.ToConsole();
                    Console.WriteLine("Success, File \"decrypted.txt\" has been created");
                    ProgramLoop();
                }
                else if (choice == 3)
                {                   
                    ReadFile();
                    ProgramLoop();
                }
                else if (choice == 4)
                {
                    ChangeFile();
                    ProgramLoop();
                }
                else if (choice == 5)
                {
                    ChangeDir();
                    ProgramLoop();
                }
                else if (choice == 99)
                {
                    Console.WriteLine("Exiting...");
                    Console.ReadLine();

                }
                else
                {
                    Console.WriteLine("404");
                    ProgramLoop();
                }
            }
            else
            {
                Console.WriteLine("404");
                ProgramLoop();
            }
        }

 




            //takes a line as string and changes each char 
            //change char first
            //change each char in line
            //line by line
            // chars to decimal
        

            
        public void Encrypt()
        {
            string changedLine;
            using (var writer = new StreamWriter("encrypted.txt",false, Encoding.Unicode))
            { 
                foreach (var line in FileLines)
                {
                    changedLine = EncryptLine(line);
                    //Console.WriteLine(changedLine);//shows to console
                    writer.WriteLine(changedLine);                    
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
