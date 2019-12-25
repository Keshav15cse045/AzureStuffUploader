using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppModelv2_WebApp_OpenIDConnect_DotNet.Controllers.Helper
{
    public class ListEntity
    {
        public List<Entity> Files { get; set; }
    }
    public class Entity
    {
        public string Filenames { get; set; }
        public string Url { get; set; }
    }
}