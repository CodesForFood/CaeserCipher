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
        
        
        static public string PlainText{ get; set; }
        static string menu = "<1>Encrypt file" +
                    "\n<2>Decrypt file" +
                    "\n<3>View file";


        static void Main(string[] args)
        {

            
            string currentDir = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDir);
            int choice = 0;

            Console.WriteLine("What file are you looking for?");
            PlainText = Console.ReadLine();
            var fileName = Path.Combine(directory.FullName, PlainText + ".txt");//get full file name           
            var file = new FileInfo(fileName);
           
          
          
                Console.WriteLine(menu);
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    var cCipher = new Cipher(file);                    
                    cCipher.Encrypt();
                    Console.WriteLine("File has been Encrypted");

                }
                else if(choice == 2)
                {
                    var key = new Key(file);
                    key.Decrypt();
                    key.ToConsole();
                }
                else if(choice == 3)
                {
                    if (file.Exists)
                    {
                        var read = new Cipher(file);
                        read.ReadFile();
                        
                    }

                }
                else { Console.WriteLine("404"); }  





            
           
        }       


    }
}
