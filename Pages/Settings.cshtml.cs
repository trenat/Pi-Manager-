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
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
            int group = 0;

            var output = "ipconfig".Bash();
            var lines = output.Split("\r\n");
            var cards = lines.Select(x => ((!x.StartsWith(" ") && x != "")? ++group : group, x))
                              .Where(b => b.x != "")
                              .GroupBy(v => v.Item1)
                              .ToList();
            var lanCard = cards.FirstOrDefault(y => y.First().x.Contains("Wi-Fi"));
            var lanCardData = lanCard.Select(x => x.x.Split(":"));
            var IP = lanCardData.First(x => x[0].Contains("IPv4"))[1];
            var Mask = lanCardData.First(x => x[0].Contains("Mask"))[1];
            var Gate = lanCardData.First(x => x[0].Contains("Gate"))[1];
            Data = new PiData()
            {
                IP = IP,
                Mask = Mask,
                Gate = Gate
            };
        }
        
        [BindProperty]
        public PiData Data { get; set; }

        public IActionResult OnPost(PiData data)
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }



            //ifconfig eth0 0.0.0.0 0.0.0.0 && dhclient


            //killall dhclient && ifconfig eth0 10.0.1.22 netmask 255.255.255.0


            //$"sudo ifconfig wlan0 {Data.IP} netmask {Data.Mask}".Bash();
            //$"sudo route add default gw {Data.Gate} eth0".Bash();





            return RedirectToPage("./Reset");
        }

    }
}