using System;
using Autofac;
using Zipper.Core;
using Zipper.Core.Interfaces;

namespace Zipper
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            Ioc.Register();
            IocContainer.Container = Ioc.Container;

            var reader = Ioc.Container.Resolve<IReader>();
            var writer = Ioc.Container.Resolve<IWriter>();
            var algorithmDecider = Ioc.Container.Resolve<IAlgorithmDecider>();

            using (var fileContent = reader.GetAll(@"C:\Users\kevin.mouton\Desktop\zipperTest.txt"))
            {
                var algorithm = algorithmDecider.GetAlgorithm(fileContent);
                var compressor = algorithm.GetCompressor();
                var compressContent = compressor.Compress(fileContent);

                writer.Save(compressContent, @"C:\Users\kevin.mouton\Desktop\zipperTest.zip");
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}