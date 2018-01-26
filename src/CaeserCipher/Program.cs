using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CaeserCipher.MainFolder;

namespace CaeserCipher
{
    class Program
    {
        //ask for file
        //encrypt and decrypt
        //load de/encrypted files
        //Break each sentence and restruct
                  
        static void Main(string[] args)
        {

            DirFile dirFile = new DirFile();

            Cipher cipher = new Cipher(dirFile.MainFile);

            cipher.ShowMenu();

        }

       


        








    }
}
