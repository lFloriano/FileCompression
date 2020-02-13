using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace POC_console
{
    class Program
    {
        const string path = "C:\\TesteFiles";

        static void Main(string[] args)
        {
            //usando fileStream      
            //SaveZip(ZipFiles(OpenFiles(path)));

            //usando FileInfo
            Library.SaveZip(Library.ZipFiles(Library.OpenFiles(path)));

            Console.ReadLine();
        }

        static IEnumerable<FileStream> OpenFiles(string path)
        {
            List<FileStream> fStream = new List<FileStream>();
            string[] files = Directory.GetFiles(path);

            foreach(string f in files)
            {
                fStream.Add(File.Open(f,FileMode.Open));
            }

            return fStream;
        }

        static Stream ZipFiles(IEnumerable<FileStream> fStream)
        {
            //cria a stream
            MemoryStream ms = new MemoryStream();

            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
            {
                foreach(var f in fStream)
                {
                    ZipArchiveEntry e = zip.CreateEntry(fStream.ToList().IndexOf(f) + ".txt");

                    using(var entryStream = e.Open())
                    {
                        f.CopyTo(entryStream);
                    }
                }
            }

            return ms;
        }

        static void SaveZip(Stream stream)
        {
            using (var fileStream = new FileStream(String.Format(path + "\\test_{0}{1}.zip", DateTime.Now.Minute, DateTime.Now.Second), FileMode.Create))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }
    }
}
