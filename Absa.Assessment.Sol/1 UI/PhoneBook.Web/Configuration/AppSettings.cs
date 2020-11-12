using System;

namespace PhoneBook.Web.Configuration {

    public class AppSettings {

        public ConnectionStrings ConnectionStrings { get; set; }

        public Logging Logging { get; set; }

        public ServiceUrls ServiceUrls { get; set; }
    }

    public class ServiceUrls {
        public string PhoneBookUrl { get; set; }
    }

    public class ConnectionStrings {

        public string DefaultConnection { get; set; }
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
}
