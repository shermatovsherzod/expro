using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expro.Common
{
    public static class AppData
    {
        public static AppConfiguration Configuration;
    }

    public class AppConfiguration
    {
        public string UploadsPath { get; set; }
        public string NoPhotoUrl { get; set; }
        public string SpinnerUrl { get; set; }
        public int WebsiteCommission { get; set; }
        public int PaymentSystemCommission { get; set; }

        /// <summary>
        /// ### ### ### ### ##0
        /// </summary>
        public string NumberViewStringFormat { get; set; }

        /// <summary>
        /// dd.MM.yyyy
        /// </summary>
        public string DateViewStringFormat { get; set; }
        /// <summary>
        /// HH:mm
        /// </summary>
        public string TimeViewStringFormat { get; set; }
        /// <summary>
        /// dd.MM.yyyy HH:mm
        /// </summary>
        public string DateTimeViewStringFormat { get; set; }

        public string DateTextViewStringFormat { get; set; }
        public string DateDbViewStringFormat { get; set; }

        public string TokenValidAudience { get; set; }
        public string TokenValidIssuer { get; set; }
        public string TokenValidAudienceServer { get; set; }
        public string TokenValidIssuerServer { get; set; }
        public string TokenSecurityKey { get; set; }
        public int TokenExpirationPeriodInDays { get; set; }

        public int PriceMin { get; set; }
        public int PriceMax { get; set; }
        public int PriceStep { get; set; }
        public int RatingThresholdForCreatingPaidDocuments { get; set; }

        /// <summary>
        /// "email1;email2;email3;..."
        /// </summary>
        public string AdminEmails { get; set; }

        public int DocumentRejectionDeadlinePeriodInMinutes { get; set; }
        public int QuestionRejectionDeadlinePeriodInMinutes { get; set; }
        public int QuestionCompletionDeadlinePeriodInMinutes { get; set; }

        /// <summary>
        /// From email
        /// </summary>
        public string ExproEmailAddress { get; set; }
        public string ExproEmailUsername { get; set; }
        public string ExproEmailPassword { get; set; }
        public string ExproEmailSmtpClient { get; set; }
        public int ExproEmailSmtpPort { get; set; }
        public bool ExproEmailEnableSsl { get; set; }

        /// <summary>
        /// 5mb
        /// </summary>
        public int FileMaxLengthInKB { get; set; }
    }
}
