using System.IO;

namespace Zipper.Core.Interfaces
{
    public interface IMerger
    {
        Stream Merge(params Stream[] streams);
    }
}