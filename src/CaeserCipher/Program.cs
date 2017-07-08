using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CaeserCipher
{
    class Program
    {
        //ask for file
        //encrypt and decrypt
        //load de/encrypted files
        //Break each sentence and restructer

        

        static public FileInfo CurrentFile{ get; set; }
        static public DirectoryInfo CurrentDir { get; set; }
   


        static string _menu = "<1>Encrypt file" +
                    "\n<2>Decrypt file" +
                    "\n<3>View file" +
                    "\n<4>Change file" +
                    "\n<5>Change Directory" +
                    "\n<99>Quit Application";





        static void Main(string[] args)
        {

            ChangeDir();
            ChangeFile();
            ProgramLoop();

        }

        static void ChangeDir()
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

        static void ChangeFile()
        {
            string input;

            Console.WriteLine("Looking in: " + CurrentDir.FullName);
            Console.WriteLine("What file are you looking for?");
            input = Console.ReadLine();

            var fileName = Path.Combine(CurrentDir.FullName, input + ".txt");//get full file name           
            var file = new FileInfo(fileName);

            if (file.Exists)
            { 
                CurrentFile = file;
            }
            else
            {
                Console.WriteLine("404");
                ChangeFile();
            }
        }

        static void ProgramLoop()
        {
            

            Console.WriteLine(_menu);

           
            int.TryParse(Console.ReadLine(),out int choice);           

            if (choice != 0)
            {
                if (choice == 1)
                {
                    var cCipher = new Cipher(CurrentFile);
                    cCipher.Encrypt();
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
                    var read = new Cipher(CurrentFile);
                    read.ReadFile();
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
                    
                }
                else { Console.WriteLine("404"); }
            }
            else { Console.WriteLine("404"); }


        }

        








    }
}
