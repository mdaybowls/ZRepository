using System;
using System.Data.SqlClient;

namespace ZLog
{
    /// <summary>
    /// Logs information to the "ErrorLogger" file.
    /// This Class will create default NLog Configuration entries that will
    /// enabled logging to a file with a name that starts with "ErrorLogger".
    /// It will use a target with a name = "ErrorLogger"
    /// <target name="ErrorLogger" type="file" layout="${message}" fileName="ErrorLogger + -${shortdate}.log" />
    /// It will use a rule with a name = "PerformanceLogger"
    /// <logger name="ErrorLogger" minlevel="Trace" writeTo="ErrorLogger" />
    /// </summary>
    public class ErrorLogger : BaseLogger
    {
        /// <summary>
        /// Write Information to the Log.
        /// </summary>
        /// <param name="logDetail"></param>
        public override void WriteLog(LogDetail logDetail)
        {
            if (logDetail.Exception != null)
            {
                var procName = FindProcName(logDetail.Exception);
                logDetail.Location = string.IsNullOrEmpty(procName) ? logDetail.Location : procName;
                logDetail.Message = GetMessageFromException(logDetail.Exception);
            }

            base.WriteLog(logDetail);
        }

        private string GetMessageFromException(Exception ex)
        {
            if (ex.InnerException != null)
                return GetMessageFromException(ex.InnerException);

            return ex.Message;
        }

        private static string FindProcName(Exception ex)
        {
            if (ex is SqlException sqlEx)
            {
                var procName = sqlEx.Procedure;
                if (!string.IsNullOrEmpty(procName))
                    return procName;
            }

            if (!string.IsNullOrEmpty((string)ex.Data["Procedure"]))
            {
                return (string)ex.Data["Procedure"];
            }

            if (ex.InnerException != null)
                return FindProcName(ex.InnerException);

            return null;
        }

    }
}
