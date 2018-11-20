using System;
using System.Collections.Generic;
using System.Linq;
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
            return Content($"Will download {recordCount}.");
        }
    }
}
