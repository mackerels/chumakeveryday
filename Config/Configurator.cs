using System.IO;
using Newtonsoft.Json;

namespace chumakeveryday.Config
{
    public static class Configurator
    {
        public static Config Config => JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
    }
}
