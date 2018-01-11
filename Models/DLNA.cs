using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiManagment.Models
{
    public class DLNA
    {
        public int AudioCount { set; get; }
        public int VideoCount { set; get; }
        public int PhotosCount { set; get; }

        public List<(int, string)> AudioPaths { set; get; }
        public List<(int, string)> VideoPaths { set; get; }
        public List<(int,string)> PhotosPaths { set; get; }
    }
}
