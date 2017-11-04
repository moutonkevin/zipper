using System.IO;
using Autofac;
using Zipper.Core.Interfaces;
using Zipper.Core.Utils;

namespace Zipper.Core.Strategies.AlgorithmDecider
{
    public class AlgorithmDecider : StreamBase, IAlgorithmDecider
    {
        public IAlgorithmDecided GetAlgorithm(Stream content)
        {
            SeekBegin(content);

            if (!FileTypeChecker.IsFileBinary(content))
                return IocContainer.Container.ResolveKeyed<IAlgorithmDecided>("textAlgorithm");
            return IocContainer.Container.ResolveKeyed<IAlgorithmDecided>("binaryAlgorithm");
        }
    }
}