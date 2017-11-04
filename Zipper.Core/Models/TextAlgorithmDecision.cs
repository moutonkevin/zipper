using Autofac;
using Zipper.Core.Interfaces;

namespace Zipper.Core.Models
{
    public class TextAlgorithmDecision : IAlgorithmDecided
    {
        public ICompressor GetCompressor()
        {
            return IocContainer.Container.ResolveKeyed<ICompressor>("textCompressor");
        }
    }
}