using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PiManagment.Models;
using PiManagment.Helpers;

namespace PiManagment.Pages
{
    public class DLNAModel : PageModel
    {
        public void OnGet()
        {
            var lineID = 0;

            //var confFIle = "cat /etc/minidlna.conf".Bash();
            //var lines = confFIle.Split("\r\n").Select(value => (lineID, value));
            //var dictionaries = lines.Where(x => x.value.Contains("media_dir"));
            //var audio = dictionaries.Select(x => x.value.Split("=A,")[1]);
            //var video = dictionaries.Select(x => x.value.Split("=V,")[1]);
            //var photo = dictionaries.Select(x => x.value.Split("=P,")[1]);


            Data = new DLNA()
            {
                AudioCount = 10,
                VideoCount = 15,
                PhotosCount = 5,
                AudioPaths = new List<(int,string)>()
                {
                    (1,"C:/audio"),
                    (2,"D:/audio"),
                    (3,"E:/audio")
                },
               // AudioPaths = new List<string>(audio);
                PhotosPaths = new List<(int, string)>()
                {
                    (1,"C:/photo"),
                    (2,"D:/photo"),
                    (3,"E:/photo")
                },
                VideoPaths = new List<(int, string)>()
                {
                    (1,"C:/video"),
                    (2,"D:/video"),
                    (3,"E:/video")
                }
            };
        }

        [BindProperty]
        public DLNA Data { get; set; }

        public IActionResult OnPost(DLNA data)
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }



            //"sudo minidlna -R".Bash();
            //"sudo service minidlnad restart".Bash()

            return Page();
            //return RedirectToPage("./Reset");
        }

    }
}