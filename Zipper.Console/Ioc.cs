using Autofac;
using Zipper.Core.Interfaces;
using Zipper.Core.Models;
using Zipper.Core.Strategies;
using Zipper.Core.Strategies.AlgorithmDecider;
using Zipper.Core.Strategies.Builer;
using Zipper.Core.Strategies.Compressors;
using Zipper.Core.Strategies.Merger;
using Zipper.Core.Strategies.Tokenizer;

namespace Zipper
{
    public static class Ioc
    {
        public static IContainer Container { get; set; }

        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileReader>().As<IReader>();
            builder.RegisterType<FileWriter>().As<IWriter>();
            builder.RegisterType<AlgorithmDecider>().As<IAlgorithmDecider>();
            builder.RegisterType<BinaryAlgorithmCompressor>().Keyed<ICompressor>("binaryCompressor");
            builder.RegisterType<TextAlgorithmCompressor>().Keyed<ICompressor>("textCompressor");
            builder.RegisterType<BinaryAlgorithmDecision>().Keyed<IAlgorithmDecided>("binaryAlgorithm");
            builder.RegisterType<TextAlgorithmDecision>().Keyed<IAlgorithmDecided>("textAlgorithm");
            builder.RegisterType<TextCompressorStringTokenizer>().As<ITokenizer>();
            builder.RegisterType<TextZipBuilder>().As<IZipBuilder>();
            builder.RegisterType<TextHeaderBuilder>().As<IBuilder>();
            builder.RegisterType<TextBodyBuilder>().As<IBuilder>();
            builder.RegisterType<TextMerger>().As<IMerger>();

            Container = builder.Build();
        }
    }
}