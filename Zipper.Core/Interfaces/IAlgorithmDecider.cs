using System.IO;

namespace Zipper.Core.Interfaces
{
    public interface IAlgorithmDecider
    {
        IAlgorithmDecided GetAlgorithm(Stream content);
    }
}