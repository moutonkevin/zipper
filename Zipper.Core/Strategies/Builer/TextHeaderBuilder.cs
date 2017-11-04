using System.IO;
using System.Text;
using Zipper.Core.Interfaces;
using Zipper.Core.Models;

namespace Zipper.Core.Strategies.Builer
{
    public class TextHeaderBuilder : IBuilder
    {
        private const string HeaderBegin = "-----------HEADER BEGIN---------";
        private const string HeaderEnd = "-----------HEADER END-----------";
        private const string TokenTemplate = "[{0}]:[{1}]:[{2}]";

        public Stream Build(ContentBase content)
        {
            var header = content as TextCompressorHeader;
            var finalContent = new StringBuilder();

            finalContent.AppendLine(HeaderBegin);

            foreach (var token in header.HeaderTokens)
                finalContent.AppendLine(
                    string.Format(TokenTemplate,
                        token.Value.Id,
                        token.Key,
                        string.Join(",", token.Value.OccurenceIndexes)));

            finalContent.AppendLine(HeaderEnd);

            var byteArray = Encoding.ASCII.GetBytes(finalContent.ToString());
            var stream = new MemoryStream(byteArray);

            return stream;
        }
    }
}