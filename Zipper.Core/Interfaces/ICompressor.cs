using System.IO;

namespace Zipper.Core.Interfaces
{
    public interface ICompressor
    {
        Stream Compress(Stream stream);
    }
}