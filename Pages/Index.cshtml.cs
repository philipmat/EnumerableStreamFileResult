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

            return new EnumerableFileStreamer<LineNumber>(lines, "application/vnd.ms-excel") {
                FileDownloadName = $"Lines {recordCount}.csv"
            };
        }

        private class LineNumber : IStreamWriteable
        {
            public LineNumber(int lineNumber)
                => Position = lineNumber;

            public int Position { get; }

            public async Task WriteAsync(Stream stream)
            {
                byte[] line = Encoding.UTF8.GetBytes($"{Position},Line-{Position:n0}{Environment.NewLine}");
                await stream.WriteAsync(line, 0, line.Length);
            }

            public Task WriteFooterAsync(Stream stream) => Task.CompletedTask;

            public async Task WriteHeaderAsync(Stream stream)
            {
                byte[] line = Encoding.UTF8.GetBytes($"Line Number, Line Text{Environment.NewLine}");
                await stream.WriteAsync(line, 0, line.Length);
            }
        }
    }
}
