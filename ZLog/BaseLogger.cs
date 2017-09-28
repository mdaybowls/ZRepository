using System;
using System.Configuration;
using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ZLog
{
    /// <summary>
    /// The Base class for a Logger.
    /// </summary>
    public abstract class BaseLogger : ILogger
    {
        private string _logFolder;

        /// <summary>
        /// The Name of the Logger (i.e. PerformanceLogger, UsageLogger, DiagnosticLogger, ErrorLogger)
        /// </summary>
        protected string LoggerName { get; }

        /// <summary>
        /// An instance of the Logger required to write information to the Log.
        /// </summary>
        private Logger _logger;

        /// <summary>
        /// Local flag used to determine if this Logger has been enabled or not.  If 
        /// it is not enabled, nothing will be written to that logger.
        /// </summary>
        private bool? _enabled;

        /// <summary>
        /// Is this Logger Enabled By Default?  If this flag is set to true, this
        /// logger will be enabled if not entries exists in the Web.Config file.
        /// This value defaults to true, so a Logger is Enabled By Default unless 
        /// this value is overridden.
        /// </summary>
        protected bool EnabledByDefault = true;

        /// <summary>
        /// Set this flag to determine of information is written to the Log or not.  If True,
        /// information is written to the Log.  If false, nothing is written to the Log.
        /// </summary>
        /// <remarks>
        /// You can set this in the Config file with the following entry:
        /// <app key="ZLog.{LoggerName}.Enabled" value="true|false" />
        /// </remarks>
        public bool Enabled
        {
            get
            {
                string logEntryName = string.Format("ZLog.{0}.Enabled", LoggerName);
                if (_enabled == null)
                {
                    var value = ConfigurationManager.AppSettings[logEntryName];
                    _enabled = string.IsNullOrEmpty(value) ? EnabledByDefault : Convert.ToBoolean(value);
                }
                return (bool)_enabled;
            }
            set => _enabled = value;
        }

        /// <summary>
        /// The File Path that the Log File will be created/updated in.
        /// </summary>
        /// <remarks>You can set this in the Config file with the following entries:
        /// This will set the log path for an individual Logger.
        /// <app key="ZLog.{LoggerName}.LogFolder" value="{FilePath}" />
        /// This will set the path for all loggers (if an individual path has not been specified)
        /// <app key="ZLog.LogFolder" value="{FilePath}" />
        /// </remarks>
        protected string LogFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_logFolder))
                {
                    var logFolderEntry = string.Format("ZLog.{0}.LogFolder", LoggerName);
                    _logFolder = ConfigurationManager.AppSettings[logFolderEntry];

                    if (string.IsNullOrEmpty(_logFolder))
                        _logFolder = ConfigurationManager.AppSettings["ZLog.LogFolder"];

                    if (!string.IsNullOrEmpty(_logFolder) && !_logFolder.EndsWith(@"\"))
                        _logFolder += @"\";
                }
                return _logFolder;
            }
        }

        /// <summary>
        /// Create an instance of the BaseLogger
        /// </summary>
        protected BaseLogger()
        {
            LoggerName = GetType().Name;
            InitializeLogger();
        }

        /// <summary>
        /// Initialize the Logger
        /// </summary>
        /// <remarks>
        /// The Logger will be initialized with a Target and a Rule that matches the {LoggerName} which
        /// will enabled logging to a File By Default.
        /// </remarks>
        public void InitializeLogger()
        {
            var config = LogManager.Configuration ?? new LoggingConfiguration();

            var target = config.FindTargetByName(LoggerName);
            if (target == null)
            {
                target = new FileTarget(LoggerName)
                {
                    FileName = LogFolder + LoggerName + "-${shortdate}.log",
                    Layout = "${date:format=HH\\:mm\\:ss} ${logger} ${message}"
                };
                config.AddTarget(LoggerName, target);
            }

            if (!config.LoggingRules.Any(r => r.NameMatches(LoggerName)))
            { 
                var rule = new LoggingRule(LoggerName, LogLevel.Trace, target) { Final = true };
                config.LoggingRules.Add(rule);
            }

            LogManager.Configuration = config;
            _logger = LogManager.GetLogger(LoggerName);
        }

        /// <summary>
        /// Write Information to the Log.
        /// </summary>
        public virtual void WriteLog(LogDetail logDetail)
        {
            if (Enabled)
                _logger.Trace(logDetail.GetFormattedMessage());
        }
    }
}