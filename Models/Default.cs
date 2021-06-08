using System;
using System.Collections.Generic;

namespace dotnet_webapi.Models
{
    public class Default
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public List<Endpoints> Endpoints { get; set; }
      // public string[] Endpoints { get; set; }
    }

    public class Endpoints
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Uri Url_Endpoints { get; set; }
        public string Summary { get; set; }
    }



}