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

        public DLNA()
        {
            ;
        }
        public DLNA(int A, int V, int P,
                    List<Pair> AP, List<Pair> AV, List<Pair> AA)
        {
            AudioCount = A;
            VideoCount = V;
            PhotosCount = P;
            AudioPaths = new List<Pair>(AA);
            VideoPaths = new List<Pair>(AV);
            PhotosPaths = new List<Pair>(AP);
        }

        public DLNA(DLNA data)
        {
            AudioCount = data.AudioCount;
            VideoCount = data.VideoCount;
            PhotosCount = data.PhotosCount;
            AudioPaths = new List<Pair>(data.AudioPaths);
            VideoPaths = new List<Pair>(data.VideoPaths);
            PhotosPaths = new List<Pair>(data.PhotosPaths);
        }

    }
}
