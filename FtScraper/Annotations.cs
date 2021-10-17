using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtScraper
{
    public enum Source
    {
        [SourceDetails("BRCGS", "BrcgsId")]
        Brcgs,
        [SourceDetails("Example Source", "EgId")]
        ExampleSource,
    }

    class SourceDetailsAttribute : Attribute
    {
        public string DatabaseIdName { get; private set; }
        public string DisplayName { get; private set; }

        public SourceDetailsAttribute(string displayName, string databaseIdName)
        {
            DisplayName = displayName;
            DatabaseIdName = databaseIdName;
        }

        public static SourceDetailsAttribute GetFromEnum(Source source)
        {
            string name = Enum.GetName(typeof(Source), source);
            return typeof(Source).GetField(name).GetCustomAttributes(false).OfType<SourceDetailsAttribute>().SingleOrDefault();
        }
    }
}
