namespace ZLog
{
    /// <summary>
    /// Logs information to the "UsageLogger" file.
    /// This Class will create default NLog Configuration entries that will
    /// enabled logging to a file with a name that starts with "UsageLogger".
    /// It will use a target with a name = "PerformanceLogger"
    /// <target name="UsageLogger" type="file" layout="${message}" fileName="UsageLogger + -${shortdate}.log" />
    /// It will use a rule with a name = "PerformanceLogger" 
    /// <logger name="UsageLogger" minlevel="Trace" writeTo="UsageLogger" />
    /// </summary>
    public class UsageLogger : BaseLogger, IUsageLogger
    {
    }
}
