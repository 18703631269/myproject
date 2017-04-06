using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class configxml
    {
        public int imgsize { get; set; }
        public int videosize { get; set; }
        public string fileextension { get; set; }
        public string videoextension { get; set; }

        public configxml()
        {
            imgsize=10240;
            videosize = 102400;
            fileextension = "gif,jpg,png,bmp,rar,zip,doc,xls,txt";
            videoextension = "flv,mp3,mp4,avi";
        }
    }
}
