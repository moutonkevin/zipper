using Zipper.Core.Interfaces;

namespace Zipper.Core.Strategies.Tokenizer
{
    public class TextCompressorStringTokenizer : ITokenizer
    {
        public string Tokenize(string content, int offset, int length)
        {
            if (offset + length > content.Length)
                return string.Empty;

            return content.Substring(offset, length);
        }
    }
}