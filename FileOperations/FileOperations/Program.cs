using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Open or Create ReadFile using FileStream");
            OpenorCreateReadFile();

            Console.WriteLine("DirectoryOperations"); 
            DirectoryOperations();

            Console.WriteLine("File Class");
            FileOperations();

            Console.WriteLine("Write To File");
            WriteBytesToFile();


            Console.WriteLine("Write to Text File");
            WiteToTextFile();

            Console.WriteLine("Read Text File");
            ReadTextFile();

            Console.ReadLine();
        }

        private static void WiteToTextFile()
        {
            using (var writeFile = new StreamWriter("streamtest.txt"))
            {
                writeFile.WriteLine("csharp.net-informations.com");
                writeFile.Flush();
            }
        }

        private static void ReadTextFile()
        {
            using (var readFile = new StreamReader("streamtest.txt"))
            {
                while (true)
                {
                    var line = readFile.ReadLine();
                    if (line != null)   
                        Console.WriteLine(line); 
                }
            }
        }

        private static void WriteBytesToFile()
        {
            var byteData = Encoding.ASCII.GetBytes("FileStream Test");
            using (var wFile = new FileStream("streamtest.txt", FileMode.Append))
            { 
                wFile.Write(byteData, 0, byteData.Length);
                wFile.Close();
            }
        }

        private static void FileOperations()
        {
            using (File.Create("ketan.txt", 1000, FileOptions.Encrypted)) { }

            File.Copy("ketan.txt", "ketan-copys.txt", true);
            FileInfo _file = new FileInfo("ketan-copy.txt");
            _file.Attributes = System.IO.FileAttributes.ReadOnly;
            _file.Attributes = System.IO.FileAttributes.Hidden; 
			
            //File.Delete("ketan-copy.txt");
        }

        private static void DirectoryOperations()
        {
            if (!Directory.Exists("testDir1"))
            {
                Directory.CreateDirectory("testDir1");
            }
            else
            {
                if (Directory.Exists("testDir1") && (!Directory.Exists("testDir2")))
                    Directory.Move("testDir1", "testDir2");
            }
            if (Directory.Exists("testDir"))
                Directory.Delete("testDir");
        }

        private static void OpenorCreateReadFile()
        {
            FileStream F = new FileStream("sample.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            for (int i = 1; i <= 20; i++)
            {
                F.WriteByte((byte)i);
            }
            F.Position = 0;
            for (int i = 0; i <= 20; i++)
            {
                Console.Write(F.ReadByte() + " ");
            }
            F.Close();
        }
    }
}
