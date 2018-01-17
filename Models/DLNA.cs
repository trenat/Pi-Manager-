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

        public List<Pair> AudioPaths { set; get; }
        public List<Pair> VideoPaths { set; get; }
        public List<Pair> PhotosPaths { set; get; }
    }
}
