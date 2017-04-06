using System;

namespace Model
{
    public class t_user
    {
        public Guid uids { get; set; }
        public int urole { get; set; }
        public string uname { get; set; }
        public string ulog { get; set; }
        public string upwd { get; set; }
        public string utel { get; set; }
        public string ulxfs { get; set; }
        public string usex { get; set; }
        public string ual { get; set; }
        public DateTime udate { get; set; }
        public string ubl { get; set; }
        public string ulock { get; set; }
        public string uemail { get; set; }
        public string uyzm { get; set; }
        public int ustate { get; set; }
        public t_user()
        {
            uids = Guid.NewGuid();
            urole = 0;
            uname = string.Empty;
            ulog = string.Empty;
            upwd = string.Empty;
            utel = string.Empty;
            ulxfs = string.Empty;
            usex = string.Empty;
            ual = string.Empty;
            udate = DateTime.Now;
            ubl = string.Empty;
            ulock = "0";
            uemail = string.Empty;
            uyzm = string.Empty;
            ustate = 0;//默认没有验证
        }
    }
}
