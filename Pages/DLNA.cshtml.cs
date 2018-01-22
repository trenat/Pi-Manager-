using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PiManagment.Models;
using PiManagment.Helpers;
using System.Text;
using System.Runtime.InteropServices;

namespace PiManagment.Pages
{
    public class DLNAModel : PageModel
    {
        private bool isWind = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        private static DLNA _data;
        private static List<Pair> _toDelete = new List<Pair>();
        private static List<string> _toAdd = new List<string>();
        private static List<Pair> _oldLines;

        [BindProperty]
        public DLNA Data { get; set; }

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

            _data = new DLNA(Data);
        }



        public IActionResult OnPostADelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _toDelete.Add(_data.AudioPaths.First(x => x.ID == id));
            _data.AudioPaths.Remove(_data.AudioPaths.First(x => x.ID == id));
            _data.AudioPaths.Last().Value = "";


            Data = new DLNA(_data);
            return Page();
        }
        public IActionResult OnPostVDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _toDelete.Add(_data.VideoPaths.First(x => x.ID == id));
            _data.VideoPaths.Remove(_data.VideoPaths.First(x => x.ID == id));
            _data.VideoPaths.Last().Value = "";


            Data = new DLNA(_data);
            return Page();
        }
        public IActionResult OnPostPDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _toDelete.Add(_data.PhotosPaths.First(x => x.ID == id));
            _data.PhotosPaths.Remove(_data.PhotosPaths.First(x => x.ID == id));
            _data.PhotosPaths.Last().Value = "";


            Data = new DLNA(_data);
            return Page();
        }

        public IActionResult OnPostAddA()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!String.IsNullOrEmpty(Data.AudioPaths.Last().Value))
            {
                _data.AudioPaths.Last().ID = Data.AudioPaths.Count;
                _data.AudioPaths.Last().Value = Data.AudioPaths.Last().Value;
                _data.AudioPaths.Add(new Pair());
                _toAdd.Add($"mkdir = A,{_data.AudioPaths.Last()}");

            }

            Data = new DLNA(_data);
            return Page();
        }
        public IActionResult OnPostAddP()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!String.IsNullOrEmpty(Data.VideoPaths.Last().Value))
            {
                _data.PhotosPaths.Last().ID = Data.PhotosPaths.Count;
                _data.PhotosPaths.Last().Value = Data.PhotosPaths.Last().Value;
                _data.PhotosPaths.Add(new Pair());
                _toAdd.Add($"mkdir = A,{_data.PhotosPaths.Last()}");
            }

            Data = new DLNA(_data);
            return Page();
        }
        public IActionResult OnPostAddV()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!String.IsNullOrEmpty(Data.VideoPaths.Last().Value))
            {
                _data.VideoPaths.Last().ID = Data.VideoPaths.Count;
                _data.VideoPaths.Last().Value = Data.VideoPaths.Last().Value;
                _data.VideoPaths.Add(new Pair());
                _toAdd.Add($"mkdir = A,{_data.VideoPaths.Last()}");
                
            }

            Data = new DLNA(_data);
            return Page();
        }
        
        public IActionResult OnPostSaveDLNAConf()
        {
            if (!isWind)
            {
                _oldLines.RemoveAll(x => _toDelete.Any(y => y.ID == x.ID));

                var clearLines = _oldLines.Select(x => x.Value).ToList();
                clearLines.AddRange(_toAdd);
                var builder = new StringBuilder();
                builder.AppendJoin("\r\n", clearLines);
                builder.ToString();
                $"echo {builder.ToString()} > /etc/minidlna.conf".Bash();

                "sudo minidlna -R".Bash();
                "sudo service minidlnad restart".Bash();

                _toAdd.Clear();
                _toDelete.Clear();
            }


            Data = new DLNA(_data);
            return Page();
        }



    }
}