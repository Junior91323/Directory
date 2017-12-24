namespace Directory.Web.Utils
{
    using System;
    using System.Configuration;

    public static class ApplicationSettings
    {
        public static int PageSize
        {
            get
            {
                int res = 0;
                if (ConfigurationManager.AppSettings["PageSize"] == null)
                    return 20;

                Int32.TryParse(ConfigurationManager.AppSettings["PageSize"].ToString(), out res);

                return res == 0 ? 20 : res;
            }
        }

    }
}