using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PiManagment.Models;
using PiManagment.Helpers;
using System.Runtime.InteropServices;
using System.Text;

namespace PiManagment.Pages
{
    public class SettingsModel : PageModel
    {
        private bool isWind = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public void OnGet()
        {
            int group = 0;

            //var output = "cat /etc/dhcpcd.conf"


            if (isWind)
            {
                var output = "ipconfig".Bash();
                var lines = output.Split("\r\n");
                var cards = lines.Select(x => ((!x.StartsWith(" ") && x != "") ? ++group : group, x))
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
            else
            {
                var output = "cat /etc/dhcpcd.conf".Bash();
                var lines = output.Split("\r\n");
                var groups = lines.Select(x => ((!x.StartsWith(" ") && x != "") ? ++group : group, x))
                                  .Where(b => b.x != "")
                                  .GroupBy(v => v.Item1)
                                  .ToList();
                var lanCard = groups.FirstOrDefault(y => y.First().x.Contains("wlan0"));

                var lanCardData = lanCard.Select(x => x.x.Split("="));
                var IP = lanCardData.First(x => x[0].Contains("ip"))[1].Split("/")[0] ?? "192.168.1.144";
                var Mask = lanCardData.First(x => x[0].Contains("Mask"))[1] ?? "255.255.255.0";
                var Gate = lanCardData.First(x => x[0].Contains("routers"))[1] ?? "192.168.1.1";
                Data = new PiData()
                {
                    UseDHCP = lines.First(x => x.Contains("Interface wlan0"))?.Trim().StartsWith("#") ?? false,
                    IP = IP,
                    Mask = Mask,
                    Gate = Gate
                };

            }



        }

        [BindProperty]
        public PiData Data { get; set; }

        public IActionResult OnPost(PiData data)
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }


            if (!isWind)
            {
                int group = 0;

                //Saving global

                var output = "cat /etc/dhcpcd.conf".Bash();
                var lines = output.Split("\r\n");
                var groups = lines.Select(x => ((!x.StartsWith(" ") && x != "") ? ++group : group, x))
                                  .Where(b => b.x != "")
                                  .GroupBy(v => v.Item1)
                                  .ToList();
                var lanCard = groups.FirstOrDefault(y => y.First().x.Contains("wlan0"));
                var lanCardData = lanCard.Select(x => x.x.Split("=")).ToList();

                var newlines = lines.ToList();

                StringBuilder s = new StringBuilder();
                if (data.UseDHCP)
                {
                    newlines.RemoveAll(x => lanCard.Any(y => y.x == x));

                    //calculate mask: 
                    var mask = data.Mask.Split(".");
                    string _mask;
                    string maskConverted = "";
                    try
                    {
                        foreach (var submask in mask)
                        {
                            maskConverted+= Convert.ToString(Convert.ToInt16(submask), 2);
                        }
                        _mask = "/" + maskConverted.TakeWhile(x => x == '1').Count().ToString();
                    }
                    catch
                    {
                        _mask = "/24";
                    }
                    s.AppendJoin("\r\n", newlines);
                    s.AppendLine("");
                    s.AppendLine("interface wlan0");
                    s.AppendLine($"static ip_adress={data.IP + mask}");
                    s.AppendLine($"static routers={data.Gate}");
                    s.AppendLine($"static domain_name_servers={data.Gate} 8.8.8.8");
                }
                else
                {
                    newlines.Select(x => lanCard.Any(y => y.x == x) ? x.Insert(0, "#") : x);
                    s.AppendJoin("\r\n", newlines);
                }
                



                $"sudo {s.ToString()} > /etc/dhcpcd.conf".Bash();



                //Saving actual 
                "sudo ifconfig eth0 0.0.0.0 0.0.0.0 && sudo dhclient".Bash(); //create dhcliect and nullify adress/mask
                $"sudo killall dhclient && sudo ifconfig wlan0 {Data.IP} netmask {Data.Mask}".Bash(); //assign IP and mask
                $"sudo route add default gw {Data.Gate} eth0".Bash(); //Add router

            }





            return RedirectToPage("./Reset");
        }

    }
}
