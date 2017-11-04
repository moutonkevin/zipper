using System.IO;
using Zipper.Core.Interfaces;

namespace Zipper.Core.Strategies
{
    public class FileWriter : StreamBase, IWriter
    {
        public void Save(Stream value, string destination)
        {
            using (var fileStream = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                SeekBegin(value);
                value.CopyTo(fileStream);
            }
        }
    }
}