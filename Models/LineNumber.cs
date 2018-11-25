namespace EnumerableStreamFileResult.Models
{
    internal class LineNumber
    {
        public LineNumber(int lineNumber)
            => Position = lineNumber;

        public int Position { get; }

    }
}
