using System;
using System.Collections.Generic;
using System.Text;

namespace frontend
{
    static class Url
    {
        public static string Header = "https://csharp.nas.huhaorui.com:8888";
        //public static string Header = "http://localhost:5000";
        public static string LoginUrl = "/api/user/login";
        public static string GetDeskListUrl = "/api/desk/list";
        public static string EnterDesk = "/api/desk/enter";
        public static string status = "/api/game/status";
        public static string press = "/api/game/press";
        public static string ready = "/api/game/ready";
    }
}
