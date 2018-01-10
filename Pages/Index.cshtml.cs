using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PiManagment.Pages
{
    public class IndexModel : PageModel
    {

        public string Message { get; set; }
        public string Head { get; set; }

        public void OnGet()
        {
            Head = "Pi Manager";
            Message = "Strona do zdalnego zarządzania urządzeniem Raspberry Pi";
        }
    }
}
