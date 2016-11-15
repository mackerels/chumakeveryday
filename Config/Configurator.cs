using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoreSandbox.Config
{
    public static class Configurator
    {
        public static Config Config => JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
    }
}
