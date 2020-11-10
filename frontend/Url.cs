using System;
using System.Collections.Generic;
using System.Text;

namespace frontend
{
    static class Url
    {
        public readonly static string Header = "https://csharp.nas.huhaorui.com:8888";
        //public readonly static string Header = "http://localhost:5000";
        public readonly static string LoginUrl = "/api/user/login";
        public readonly static string RegisterUrl = "/api/user/register";
        public readonly static string GetDeskListUrl = "/api/desk/list";
        public readonly static string EnterDesk = "/api/desk/enter";
        public readonly static string status = "/api/game/status";
        public readonly static string press = "/api/game/press";
        public readonly static string ready = "/api/game/ready";
    }
}
