using System.Collections.Generic;

namespace Zipper.Core.Models
{
    public class TextCompressorBody : BodyBase
    {
        public string Content { get; set; }
        public Dictionary<string, Token> HeaderTokens { get; set; }
    }
}