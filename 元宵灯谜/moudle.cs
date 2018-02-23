using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace yuanxiao
{
    public class LoginMoudle
    {
        public string status { set; get; }
    }

    public class riddleUncompeletedMoudle
    {
        public string RID { set; get; }
        public string PNUM { set; get; }
        public string COSTTIME { set; get; }
    }

    public class riddles
    {
        public string GID { set; get; }
        public string APNAME { set; get; }
    }
    public class riddleGet
    {   
        public string totalpage { set; get; }
        public string lastgid { set; get; }
        public IList<riddles> Riddles{ set; get; } 
    }


    

    public class riddleContentGet
    {
        public string rquestion { set; get; }
        public string rtips { set; get; }
    }

    public class resultModule
    {
        public string result { set; get; }
    }
   
}