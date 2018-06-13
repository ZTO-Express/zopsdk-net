using System;

namespace zopsdk_net
{
    public class ZopProperties
    {
        public string companyid {get;}
        public string key {get;}

        public ZopProperties(string companyid, string key )
        {
            this.companyid = companyid;
            this.key = key;
        }
    }
}