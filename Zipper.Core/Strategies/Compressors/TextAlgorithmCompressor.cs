using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zipper.Core.Interfaces;
using Zipper.Core.Models;

namespace Zipper.Core.Strategies.Compressors
{
    public class TextAlgorithmCompressor : StreamBase, ICompressor
    {
        private readonly ITokenizer _tokenizer;
        private readonly IZipBuilder _zipbuilder;

        public TextAlgorithmCompressor(ITokenizer tokenizer, IZipBuilder zipbuilder)
        {
            _tokenizer = tokenizer;
            _zipbuilder = zipbuilder;
        }

        public Stream Compress(Stream stream)
        {
            SeekBegin(stream);

            var contentString = GetStringFromStream(stream);

            var tokenDictionary = GetTokensFromContent(contentString);

            tokenDictionary = ReOrderDictionary(tokenDictionary);

            tokenDictionary = GetStats(tokenDictionary);

            Console.WriteLine("------------Tokens------------");

            DisplayTokenDictionnary(tokenDictionary);

            var keyTokenDictionary = CleanupTokens(tokenDictionary);

            AssignKeyNumbersIdentifier(keyTokenDictionary);

            Console.WriteLine("------------Cleaned Tokens------------");

            DisplayTokenDictionnary(keyTokenDictionary);

            var header = GetHeader(keyTokenDictionary);
            var body = GetBody(contentString, keyTokenDictionary);

            return _zipbuilder.Build(header, body);
        }

        private Dictionary<string, Token> GetTokensFromContent(string content)
        {
            var tokenDictionnary = new Dictionary<string, Token>();
            const int maxTokenLength = 5;
            var id = 1;

            for (var mainLoopCounter = 0; mainLoopCounter < content.Length; mainLoopCounter++)
            {
                var currentLenght = 1;

                for (var tokenizedLoopCounter = 0;
                    tokenizedLoopCounter < maxTokenLength;
                    tokenizedLoopCounter++, currentLenght++)
                {
                    var token = _tokenizer.Tokenize(content, mainLoopCounter, currentLenght);

                    if (string.IsNullOrEmpty(token))
                        continue;

                    if (tokenDictionnary.ContainsKey(token))
                    {
                        tokenDictionnary[token].OccurenceIndexes.Add(mainLoopCounter);
                    }
                    else
                    {
                        tokenDictionnary.Add(token,
                            new Token {OccurenceIndexes = new List<int> {mainLoopCounter}, Id = id});
                        id++;
                    }
                }
            }
            return tokenDictionnary;
        }

        private bool IsSubPartiallyContainedInMain(IList<Tuple<int, int>> main, IList<Tuple<int, int>> sub)
        {
            return main.Any(mainRange => sub.Any(s => s.Item1 >= mainRange.Item1 && s.Item1 <= mainRange.Item2));
        }

        private Dictionary<string, Token> CleanupTokens(Dictionary<string, Token> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var mainToken = tokens.Skip(i).Take(1).FirstOrDefault();

                for (var j = i + 1; j < tokens.Count; j++)
                {
                    var subToken = tokens.Skip(j).Take(1).FirstOrDefault();

                    if (!mainToken.Value.ToBeDeleted && !subToken.Value.ToBeDeleted &&
                        IsSubPartiallyContainedInMain(mainToken.Value.OccurenceRanges, subToken.Value.OccurenceRanges)
                    )
                        subToken.Value.ToBeDeleted = true;
                }
            }

            tokens = tokens.Where(token => !token.Value.ToBeDeleted).ToDictionary(k => k.Key, v => v.Value);

            return tokens;
        }

        private Dictionary<string, Token> ReOrderDictionary(Dictionary<string, Token> tokenDictionary)
        {
            return tokenDictionary.OrderByDescending(d => d.Key.Length)
                .ThenByDescending(d => d.Value.OccurenceIndexes.Count).ToDictionary(k => k.Key, v => v.Value);
        }

        private void DisplayTokenDictionnary(Dictionary<string, Token> tokenDictionary)
        {
            foreach (var keyValuePair in tokenDictionary)
                Console.WriteLine($"[{keyValuePair.Value.Id}] " +
                                  $"Token[{keyValuePair.Key}] " +
                                  $"Occurences[{string.Join(",", keyValuePair.Value.OccurenceIndexes)}] " +
                                  $"Score[{keyValuePair.Value.Score}] " +
                                  $"Ranges[{string.Join(",", keyValuePair.Value.OccurenceRanges)}] " +
                                  $"TBD[{keyValuePair.Value.ToBeDeleted}]"
                );
        }

        private Dictionary<string, Token> GetStats(Dictionary<string, Token> tokens)
        {
            foreach (var keyValuePair in tokens)
            {
                keyValuePair.Value.Score = keyValuePair.Value.OccurenceIndexes.Count * keyValuePair.Key.Length;
                keyValuePair.Value.OccurenceRanges = keyValuePair.Value.OccurenceIndexes
                    .Select(o => new Tuple<int, int>(o, o + keyValuePair.Key.Length)).ToList();
            }

            return tokens;
        }

        private TextCompressorHeader GetHeader(Dictionary<string, Token> tokens)
        {
            return new TextCompressorHeader
            {
                HeaderTokens = tokens
            };
        }

        private TextCompressorBody GetBody(string content, Dictionary<string, Token> keyTokens)
        {
            return new TextCompressorBody
            {
                Content = content,
                HeaderTokens = keyTokens
            };
        }

        private void AssignKeyNumbersIdentifier(Dictionary<string, Token> tokens)
        {
            var identifier = 0;

            foreach (var token in tokens)
            {
                token.Value.Id = identifier;
                identifier++;
            }
        }
    }
}