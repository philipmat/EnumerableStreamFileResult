using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnumerableStreamFileResult.Pages
{
    public class IndexModel : PageModel
    {
        public const int DefaultMaxSize = 100_000;

        public void OnGet()
        {
        }

        public IActionResult OnPostDownload(int recordCount)
        {
            var lines = Enumerable
                .Range(1, recordCount)
                .Select(i => new LineNumber(i));

            return new EnumerableFileStreamer<LineNumber>(lines, new LineNumberToCsvWriter()) {
                FileDownloadName = $"Lines {recordCount}.csv"
            };
        }

        private class LineNumber
        {
            public LineNumber(int lineNumber)
                => Position = lineNumber;

            public int Position { get; }

        }

        private class LineNumberToCsvWriter : IStreamWritingAdapter<LineNumber>
        {
            public string ContentType => "application/vnd.ms-excel";

            public async Task WriteAsync(LineNumber item, Stream stream)
            {
                byte[] line = Encoding.UTF8.GetBytes($"{item.Position},Line-{item.Position:n0}{Environment.NewLine}");
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
}
