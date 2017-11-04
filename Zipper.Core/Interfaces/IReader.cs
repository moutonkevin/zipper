using System.IO;

namespace Zipper.Core.Interfaces
{
    public interface IReader
    {
        Stream GetAll(string key);
    }
}