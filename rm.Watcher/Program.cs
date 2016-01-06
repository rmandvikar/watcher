using System;
using System.Configuration;

namespace rm.Watcher
{
    public class Program
    {
        public static void Main()
        {
            Start();
            Console.ReadKey();
        }

        public static void Start()
        {
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (!key.StartsWith("watch"))
                {
                    continue;
                }
                var value = ConfigurationManager.AppSettings[key];
                var tokens = value.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length != 2)
                {
                    continue;
                }
                var source = tokens[0].Trim();
                var target = tokens[1].Trim();
                new Watcher(source, target).Run();
            }
        }
    }
}
