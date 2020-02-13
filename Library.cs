using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace POC_console
{
    public static class Library
    {
        const string _path = "C:\\TesteFiles";
        public static FileInfo[] OpenFiles(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();

            return files;
        }

        public static Stream ZipFiles(FileInfo[] fStream)
        {
            MemoryStream ms = new MemoryStream();

            using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
            {
                foreach (FileInfo f in fStream)
                {
                    ZipArchiveEntry entry = zip.CreateEntry(f.Name);

                    using (var entryStream = entry.Open())
                    {
                        f.OpenRead().CopyTo(entryStream);
                    }
                }
            }

            return ms;
        }

        public static void SaveZip(Stream stream)
        {
            using (var fileStream = new FileStream(String.Format(_path + "\\lib_{0}{1}.zip", DateTime.Now.Minute, DateTime.Now.Second), FileMode.Create))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }
    }
}
