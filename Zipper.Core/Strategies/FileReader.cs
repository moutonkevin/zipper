using System.IO;
using System.Text;
using Zipper.Core.Interfaces;

namespace Zipper.Core.Strategies
{
    public class FileReader : IReader
    {
        public Stream GetAll(string key)
        {
            var ms = new MemoryStream();
            var fileContentString = File.ReadAllText(key);
            var fileContentBytes = Encoding.ASCII.GetBytes(fileContentString);

            ms.WriteAsync(fileContentBytes, 0, fileContentBytes.Length);

            return ms;
        }
    }
}