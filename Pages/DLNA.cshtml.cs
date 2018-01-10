using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PiManagment.Models;

namespace PiManagment.Pages
{
    public class DLNAModel : PageModel
    {
        public void OnGet()
        {
            Data = new DLNA()
            {
                AudioCount = 10,
                VideoCount = 15,
                PhotosCount = 5
            };
        }

        [BindProperty]
        public DLNA Data { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            return RedirectToPage("./Reset");
        }

    }
}