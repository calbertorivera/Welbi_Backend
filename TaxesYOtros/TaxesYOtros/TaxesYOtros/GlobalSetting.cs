using System;
using System.Collections.Generic;
using System.Text;

namespace TaxesYOtros
{
    public class GlobalSetting
    {
        public const string ProductionEndpoint = "https://www.taxesyotros.com/API";
        public const string TestingEndonit = "https://www.taxesyotros.com/API";
        private string _baseEndpoint;
        public int AsyncTimeoutMillisecond;
        private GlobalSetting()
        {
            BaseEndpoint = ProductionEndpoint;
#if DEBUG
            BaseEndpoint = TestingEndonit;
#endif

            this.AsyncTimeoutMillisecond = (int)TimeSpan.FromSeconds(60).TotalMilliseconds;

        }

      


        private static readonly GlobalSetting _instance = new GlobalSetting();
        public static GlobalSetting Instance
        {
            get { return _instance; }
        }
        #region Endpoints
        public string BaseEndpoint
        {
            get { return _baseEndpoint; }
            set
            {
                _baseEndpoint = value;
                UpdateEndpoint(_baseEndpoint);
            }
        }

        public string LoginEndpoint { get; set; }
        private void UpdateEndpoint(string baseEndpoint)
        {
            LoginEndpoint = $"{baseEndpoint}/login.php";
        }

            #endregion
        }


}
