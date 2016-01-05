using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

            Console.WriteLine("Binary Writer ");
            BinaryWriter();

            Console.WriteLine("Binary Reader");
            BinaryReader();

            Console.WriteLine("Get File Properties using path class \n");
            GetFileProperties();

            Console.WriteLine("Get Directory Info \n");
            AllDirectoryOperations();

            Console.WriteLine("Write to Text File");
            WiteToTextFile();

            Console.WriteLine("Read Text File");
            ReadTextFile();

            Console.ReadLine();
        }

        private static void AllDirectoryOperations()
        {
            Console.WriteLine("Get Logical Drives \n");

            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                Console.WriteLine(drive);
            }

            Console.WriteLine(" Get Directories \n");

            string[] dirs = Directory.GetDirectories("d:");
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                string[] files = Directory.GetFiles(dir);
                foreach (string i in files)
                {
                    Console.WriteLine(i);
                }
            }

            //Get All files of directory
            DirectoryInfo dirinfo = new DirectoryInfo(@"D:\MyGit\FileOperations\CsharpFileOperations\FileOperations\FileOperations\bin\Debug");
            //get all the files in the directory and their name and size
            FileInfo[] filelist = dirinfo.GetFiles();
            foreach (FileInfo file in filelist)
                Console.WriteLine(" File Name: {0}  Size:  {1} bytes", file.Name, file.Length);

        }

        private static void GetFileProperties()
        {
            var tmpPath = "csharp.net-informations.txt";
            string fileExtension = Path.GetExtension(tmpPath);
            string filename = Path.GetFileName(tmpPath);
            string filenameWithoutExtension = Path.GetFileNameWithoutExtension(tmpPath);
            string rootPath = Path.GetPathRoot(tmpPath);
            string directory = Path.GetDirectoryName(tmpPath);
            string fullPath = Path.GetFullPath(tmpPath);
            Console.WriteLine("\n -------- File Properties  --------- \n");
            Console.WriteLine("\n fileExtension:" + fileExtension + "\n filename:"
                + filename + "\n filenameWithoutExtension:" + filenameWithoutExtension + "\n rootPath:"
                + rootPath + "\n directory:" + directory + "\n fullPath:" + fullPath);
            Console.WriteLine("\n ----------------- \n");

            FileInfo FileProps = new FileInfo(tmpPath);

            Console.WriteLine("\n File Name = " + FileProps.FullName + "\n Creation Time = " + FileProps.CreationTime + "\n Last Access Time = " + FileProps.LastAccessTime + "\n Last Write TIme = " + FileProps.LastWriteTime + "\n Size = " + FileProps.Length);

        }

        private static void BinaryReader()
        {
            using (var readStream = new FileStream("csharp.net-informations.txt", FileMode.Open))
            {
                BinaryReader readBinary = new BinaryReader(readStream);
                var msg = readBinary.ReadString();
                Console.WriteLine("\nBinaryReader Demo:\n*******\n" + msg);
                readStream.Close();
            }
        }

        private static void BinaryWriter()
        {
            using (var writeStream = new FileStream("csharp.net-informations.txt", FileMode.Create))
            {
                var writeBinay = new BinaryWriter(writeStream);
                writeBinay.Write("CSharp.net-informations.com binary writer test");
                writeBinay.Close();
            }
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

            string[] stringArray = new string[]
            {
                "cat",
                "dog",
                "arrow"
            };
            File.WriteAllLines("file.txt", stringArray);

            int lineCount = File.ReadAllLines("file.txt").Length;

            Console.WriteLine("\n -------- \nWrite & Read a string array to a file.\n ------- \n");
            var lines = File.ReadAllLines("file.txt");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            //WriteAllText Demo
            Console.WriteLine("Enter data to File");
            string newContent = Console.ReadLine();
            File.WriteAllText("perls.txt", newContent);

            //AppendAllText Demo
            File.AppendAllText("perls.txt", "\n Dot Net Perls AppendAllText");

            //  Compress Demo
            byte[] compress = Compress();
            File.WriteAllBytes("compress.gz", compress);
            var decompress = Decompress(compress);
            File.WriteAllBytes("de-compress.txt", compress);
            //File.Delete("ketan-copy.txt");
        }

        public static byte[] Compress()
        {

            byte[] raw = Encoding.ASCII.GetBytes(new string('X', 10000));
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        public static byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
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
