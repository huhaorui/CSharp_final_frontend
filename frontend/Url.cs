using System;
using System.Collections.Generic;
using System.Text;

namespace frontend
{
    static class Url
    {
        static public string Header = "https://localhost:5001";
        static public string LoginUrl = "/api/user/login";
        static public string GetDeskListUrl = "/api/desk/list";
        static public string EnterDesk = "/api/desk/enter";
        static public string status = "/api/game/status";
        static public string press = "/api/game/press";
        static public string ready = "/api/game/ready";
    }
}
