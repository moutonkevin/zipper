using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zipper.Core.Interfaces;
using Zipper.Core.Models;

namespace Zipper.Core.Strategies.Builer
{
    public class TextZipBuilder : IZipBuilder
    {
        private readonly IBuilder _bodyBuilder;
        private readonly IBuilder _headerBuilder;
        private readonly IMerger _merger;

        public TextZipBuilder(IEnumerable<IBuilder> builders, IMerger merger)
        {
            _headerBuilder = builders.FirstOrDefault();
            _bodyBuilder = builders.Skip(1).FirstOrDefault();
            _merger = merger;
        }

        public Stream Build(HeaderBase header, BodyBase body)
        {
            var formattedHeader = _headerBuilder.Build(header);
            var formattedBody = _bodyBuilder.Build(body);

            var finalContent = _merger.Merge(formattedHeader, formattedBody);

            return finalContent;
        }
    }
}