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
        public string ForgotPasswordEndpoint { get; set; }
        public string ConfirmOTPEndpoint { get; set; }
        public string ResetPasswordEndpoint { get;  set; }
        public string RegisterPasswordEndpoint { get;  set; }
        public string TextsEndpoint { get; set; }

        private void UpdateEndpoint(string baseEndpoint)
        {
            LoginEndpoint = $"{baseEndpoint}/login.php";
            ConfirmOTPEndpoint = $"{baseEndpoint}/login2.php";
            ForgotPasswordEndpoint = $"{baseEndpoint}/forgotPassword.php";
            ResetPasswordEndpoint = $"{baseEndpoint}/resetPassword.php";
            RegisterPasswordEndpoint = $"{baseEndpoint}/register.php";
            TextsEndpoint = $"{baseEndpoint}"+"/{0}.json";
        }

            #endregion
        }


}
