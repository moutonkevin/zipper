using Autofac;
using Zipper.Core.Interfaces;

namespace Zipper.Core.Models
{
    public class BinaryAlgorithmDecision : IAlgorithmDecided
    {
        public ICompressor GetCompressor()
        {
            return IocContainer.Container.ResolveKeyed<ICompressor>("binaryCompressor");
        }
    }
}