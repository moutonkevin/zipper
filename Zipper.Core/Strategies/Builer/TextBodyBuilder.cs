using System.IO;
using System.Linq;
using System.Text;
using Zipper.Core.Interfaces;
using Zipper.Core.Models;

namespace Zipper.Core.Strategies.Builer
{
    public class TextBodyBuilder : IBuilder
    {
        private const string BodyBegin = "-----------BODY BEGIN-----------";
        private const string BodyEnd = "-----------BODY END-------------";
        private const string SpecialKeyFormat = "[{0}]";

        public Stream Build(ContentBase content)
        {
            var body = content as TextCompressorBody;
            var finalContent = new StringBuilder();

            finalContent.AppendLine(BodyBegin);

            for (var position = 0; position < body.Content.Length; position++)
            {
                var headerToken =
                    body.HeaderTokens.FirstOrDefault(h => h.Value.OccurenceIndexes.Any(o => o == position));
                if (string.IsNullOrEmpty(headerToken.Key))
                {
                    var character = body.Content[position];

                    finalContent.Append(character);
                }
                else
                {
                    finalContent.Append(string.Format(SpecialKeyFormat, headerToken.Value.Id));
                    position += headerToken.Key.Length - 1;
                }
            }

            finalContent.AppendLine();
            finalContent.AppendLine(BodyEnd);

            var byteArray = Encoding.ASCII.GetBytes(finalContent.ToString());
            var stream = new MemoryStream(byteArray);

            return stream;
        }
    }
}