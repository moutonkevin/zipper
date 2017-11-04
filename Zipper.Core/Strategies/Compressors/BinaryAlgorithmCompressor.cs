using System.IO;
using Zipper.Core.Interfaces;

namespace Zipper.Core.Strategies.Compressors
{
    public class BinaryAlgorithmCompressor : ICompressor
    {
        public Stream Compress(Stream stream)
        {
            return stream;
        }
    }
}