using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PiManagment.Helpers;

namespace PiManagment.Pages
{
    public class ResetModel : PageModel
    {
        public void OnGet()
        {
        }
        

        public IActionResult OnPostShutdown()
        {
            var output = "cd".Bash();
            return Page();
        }

        public IActionResult OnPostReset()
        {
            return Page();

        }

    }
}