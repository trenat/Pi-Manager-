using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PiManagment.Models;
using PiManagment.Helpers;
using System.Text;

namespace PiManagment.Pages
{
    public class DLNAModel : PageModel
    {
        public void OnGet()
        {
            //var lineID = 0;
            //var confFIle = "cat /etc/minidlna.conf".Bash();
            //var lines = confFIle.Split("\r\n").Select(value => (lineID++, value));
            //var dictionaries = lines.Where(x => x.value.Contains("media_dir"));
            //var audio = dictionaries.Select(x => x.value.Split("=A,")[1]);
            //var video = dictionaries.Select(x => x.value.Split("=V,")[1]);
            //var photo = dictionaries.Select(x => x.value.Split("=P,")[1]);


            Data = new DLNA()
            {
                AudioCount = 10,
                VideoCount = 15,
                PhotosCount = 5,
                // AudioPaths = new List<string>(audio),
                // VideoPaths = new List<string>(video),
                // PhotosPaths = new List<string>(photo)
                AudioPaths = new List<Pair>()
                {
                   new Pair (1,"C:/audio"),
                   new Pair (2,"D:/audio"),
                   new Pair (3,"E:/audio"),
                   new Pair  ()
                },
                PhotosPaths = new List<Pair>()
                {
                   new Pair(1,"C:/photo"),
                   new Pair (2,"D:/photo"),
                   new Pair (3,"E:/photo"),
                   new Pair  ()
                },
                VideoPaths = new List<Pair>()
                {
                   new Pair  (1,"C:/video"),
                   new Pair  (2,"D:/video"),
                   new Pair  (3,"E:/video"),
                   new Pair  ()
                }
            };
        }

        [BindProperty]
        public DLNA Data { get; set; }
        

        public IActionResult OnPostADelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Data.AudioPaths.Remove(Data.AudioPaths.First(x => x.ID == id));
            return Page();
        }

        public IActionResult OnPostVDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Data.VideoPaths.Remove(Data.VideoPaths.First(x => x.ID == id));
            return Page();
        }

        public IActionResult OnPostPDelete(int id, DLNA data)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Data.PhotosPaths.Remove(Data.PhotosPaths.First(x => x.ID == id));
            Data.PhotosPaths.Last().Value = "";
            return Page();
        }

        public IActionResult OnPostAddA(DLNA data)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!String.IsNullOrEmpty(data.AudioPaths.Last().Value))
            {
                Data.AudioPaths.Last().ID = data.AudioPaths.Count;
                Data.AudioPaths.Add(new Pair());
            }
            

            //"sudo minidlna -R".Bash();
            //"sudo service minidlnad restart".Bash()

            return Page();
            //return RedirectToPage("./Reset");
        }

        public IActionResult OnPostAddP(DLNA data)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!String.IsNullOrEmpty(data.PhotosPaths.Last().Value))
            {
                Data.PhotosPaths.Last().ID = data.PhotosPaths.Count;
                Data.PhotosPaths.Add(new Pair());
            }
            


            return Page();
        }


        public IActionResult OnPostAddV(DLNA data)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!String.IsNullOrEmpty(data.VideoPaths.Last().Value))
            {
                Data.VideoPaths.Last().ID = data.VideoPaths.Count;
                Data.VideoPaths.Add(new Pair());
            }

            return Page();
        }

        public IActionResult OnPostSaveDLNAConf()
        {


            //var confFIle = "cat /etc/minidlna.conf".Bash();
            //var lineID = 0;
            //var lines = confFIle.Split("\r\n").Select(value => (++lineID, value));
            //var linesList = lines.ToList();

            //linesList.RemoveAll(x => Data.AudioPaths.Any(y => y.ID == x.Item1) ||
            //                         Data.PhotosPaths.Any(y => y.ID == x.Item1) ||
            //                         Data.VideoPaths.Any(y => y.ID == x.Item1));
            //var clearLines = linesList.Select(x => x.Item1).ToList();
            //var builder = new StringBuilder();
            //builder.AppendJoin("\r\n", clearLines);
            //builder.ToString();
            //$"echo {builder.ToString()} > /etc/minidlna.conf".Bash();

            //"sudo minidlna -R".Bash();
            //"sudo service minidlnad restart".Bash();

            return Page();
        }



    }
}