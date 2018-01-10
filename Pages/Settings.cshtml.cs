using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PiManagment.Models;

namespace PiManagment.Pages
{
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
            Data = new PiData()
            {
                IP = "192.168.1.125",
                Mask = "255.255.255.0",
                Gate = "192.168.1.100"
            };
        }
        
        [BindProperty]
        public PiData Data { get; set; }

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