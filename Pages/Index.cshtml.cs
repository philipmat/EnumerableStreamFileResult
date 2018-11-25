using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EnumerableStreamFileResult.Models;

namespace EnumerableStreamFileResult.Pages
{
    public partial class IndexModel : PageModel
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
    }
}
