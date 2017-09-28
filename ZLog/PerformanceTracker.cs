using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ZLog
{
    public class PerformanceTracker
    {
        private readonly Stopwatch _sw;
        private readonly LogDetail _logDetail;
        private readonly PerformanceLogger _perfLogger = new PerformanceLogger();

        public PerformanceTracker(string name, string userId, string userName,
            string location, string product, string layer)
        {
            var beginTime = DateTime.Now;
            _sw = new Stopwatch();
            _sw.Start();
            _logDetail = new LogDetail
            {
                Message = name,
                UserId = userId,
                UserName = userName,
                Timestamp = DateTime.Now,
                Product = product,
                Layer = layer,
                Location = location,
                Hostname = Environment.MachineName,
                AdditionalInfo = new Dictionary<string, object>
                {
                    {"Started", beginTime.ToString("HH:mm:ss.fff")}
                }
            };

        }

        public PerformanceTracker(string name, string userId, string userName,
            string location, string product, string layer, Dictionary<string, object> perfParms)
            : this(name, userId, userName, location, product, layer)
        {
            foreach (var item in perfParms)
                _logDetail.AdditionalInfo.Add("input-" + item.Key, item.Value);
        }

        public void Stop()
        {
            _sw.Stop();
            var endTime = DateTime.Now;
            _logDetail.AdditionalInfo.Add("Stopped", endTime.ToString("HH:mm:ss.fff"));
            _logDetail.ElapsedMilliseconds = _sw.ElapsedMilliseconds;
            _perfLogger.WriteLog(_logDetail);
        }
    }
}