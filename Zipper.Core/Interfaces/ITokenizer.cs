namespace Zipper.Core.Interfaces
{
    public interface ITokenizer
    {
        string Tokenize(string content, int offset, int length);
    }
}