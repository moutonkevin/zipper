using System.IO;
using Zipper.Core.Models;

namespace Zipper.Core.Interfaces
{
    public interface IBuilder
    {
        Stream Build(ContentBase content);
    }
}