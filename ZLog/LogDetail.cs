using System;
using System.Collections.Generic;
using System.Text;

namespace ZLog
{
    public class LogDetail
    {
        public LogDetail()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        // WHERE
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }

        // WHO
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        // EVERYTHING ELSE
        public long? ElapsedMilliseconds { get; set; }  // only for performance entries
        public Exception Exception { get; set; }  // the exception for error logging
        public string CorrelationId { get; set; } // exception shielding from server to client
        public Dictionary<string, object> AdditionalInfo { get; set; }  // everything else

        public string GetFormattedMessage()
        {
            var message = new StringBuilder();
            message.Append(FormatPropertyExcludingNull("Message", Message));
            message.AppendFormat("[Timestamp: {0:yyyy/MM/dd HH:mm:ss.fff}]", Timestamp);
            message.Append(FormatPropertyExcludingNull("Product", Product));
            message.Append(FormatPropertyExcludingNull("Layer", Layer));
            message.Append(FormatPropertyExcludingNull("Location", Location));
            message.Append(FormatPropertyExcludingNull("Hostname", Hostname));
            message.Append(FormatPropertyExcludingNull("UserId", UserId));
            message.Append(FormatPropertyExcludingNull("UserName", UserName));

            if (CustomerId != null)
                message.AppendFormat("[CustomerId: {0}]", CustomerId);

            message.Append(FormatPropertyExcludingNull("CustomerName", CustomerName));
            message.Append(FormatPropertyExcludingNull("CorrelationId", CorrelationId));

            if (ElapsedMilliseconds != null)
                message.AppendFormat("[ElapsedMilliseconds: {0}]", ElapsedMilliseconds);

            if (Exception != null)
                message.AppendFormat("[{0}]", Exception.ToBetterString());

            var additionalInformation = new StringBuilder();

            if (AdditionalInfo != null)
            {
                additionalInformation.Append("[Additional-Information]");
                foreach (var entry in AdditionalInfo)
                    additionalInformation.AppendFormat("[{0}: {1}]", entry.Key, entry.Value);
                message.Append(additionalInformation);
            }
            return message.ToString();
        }

        private string FormatPropertyExcludingNull(string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
                return string.Format("[{0}: {1}]", name, value);
            return string.Empty;
        }
    }
}