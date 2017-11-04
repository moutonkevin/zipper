using System.IO;
using System.Text;

namespace Zipper.Core.Strategies
{
    public abstract class StreamBase
    {
        protected void SeekBegin(Stream content)
        {
            if (content.CanSeek)
                content.Seek(0, SeekOrigin.Begin);
        }

        protected void SeekEnd(Stream content)
        {
            if (content.CanSeek)
                content.Seek(0, SeekOrigin.End);
        }

        protected string GetStringFromStream(Stream content)
        {
            var buffer = new byte[content.Length];

            content.Read(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(buffer);
        }

        protected byte[] GetBytesFromStream(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}