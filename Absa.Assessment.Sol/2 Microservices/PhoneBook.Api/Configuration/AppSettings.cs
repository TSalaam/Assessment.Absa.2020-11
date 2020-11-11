using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Configuration {

    /// <summary>
    /// 
    /// </summary>
    public class AppSettings {

        public Logging Logging { get; set; }

        public Settings Settings { get; set; }
    }

    public class Logging {

        public bool IncludeScopes { get; set; }

        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel {

        public string Default { get; set; }

        public string System { get; set; }

        public string Microsoft { get; set; }
    }

    public class Settings {

        public string LoggingDirectory { get; set; }

        public bool PerfMonEnabled { get; set; }
    }
}
