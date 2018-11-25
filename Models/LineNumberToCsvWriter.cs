using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EnumerableStreamFileResult.Models
{
    internal class LineNumberToCsvWriter : IStreamWritingAdapter<LineNumber>
    {
        public string ContentType => "application/vnd.ms-excel";

        public async Task WriteAsync(LineNumber item, Stream stream)
        {
            byte[] line = Encoding.UTF8.GetBytes($"{item.Position},\"Line {item.Position:n0}\"{Environment.NewLine}");
            await stream.WriteAsync(line, 0, line.Length);
        }

        public Task WriteFooterAsync(Stream stream, int recordCount) => Task.CompletedTask;

        public async Task WriteHeaderAsync(Stream stream)
        {
            byte[] line = Encoding.UTF8.GetBytes($"Line Number, Line Text{Environment.NewLine}");
            await stream.WriteAsync(line, 0, line.Length);
        }
    }
}
