using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace ChipotleNews.Config
{
    public class ConnectConfig
    {
        public static ConnectorClient Connector { get; set; }
        public static Activity CurrentActivity { get; set; }
    }
}