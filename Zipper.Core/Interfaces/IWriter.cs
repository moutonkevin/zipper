using System.IO;

namespace Zipper.Core.Interfaces
{
    public interface IWriter
    {
        void Save(Stream value, string destination);
    }
}