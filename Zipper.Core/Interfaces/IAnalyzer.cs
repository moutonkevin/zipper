using System.IO;

namespace Zipper.Core.Interfaces
{
    public interface IAnalyzer
    {
        IAnalyzed Analyze(Stream content);
    }
}