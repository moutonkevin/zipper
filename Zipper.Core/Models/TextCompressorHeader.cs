using System.Collections.Generic;

namespace Zipper.Core.Models
{
    public class TextCompressorHeader : HeaderBase
    {
        public Dictionary<string, Token> HeaderTokens { get; set; }
    }
}