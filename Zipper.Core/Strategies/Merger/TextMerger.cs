using System.IO;
using System.Linq;
using Zipper.Core.Interfaces;

namespace Zipper.Core.Strategies.Merger
{
    public class TextMerger : StreamBase, IMerger
    {
        public Stream Merge(params Stream[] streams)
        {
            var finalStream = new MemoryStream((int) streams.Sum(s => s.Length));

            foreach (var stream in streams)
            {
                var currentStreamBytes = GetBytesFromStream(stream);
                var currentStreamLength = currentStreamBytes.Length;

                SeekEnd(finalStream);
                finalStream.Write(currentStreamBytes, 0, currentStreamLength);
            }
            return finalStream;
        }
    }
}