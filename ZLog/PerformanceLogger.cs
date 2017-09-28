namespace ZLog
{
    /// <inheritdoc cref="" />
    /// <summary>
    /// Logs information to the "PerformanceLogger" file.
    /// This Class will create default NLog Configuration entries that will
    /// enabled logging to a file with a name that starts with "PerformanceLogger".
    /// It will use a target with a name = "PerformanceLogger"
    /// <target name="PerformanceLogger" type="file" layout="${message}" fileName="PerformanceLogger + -${shortdate}.log" />
    /// It will use a rule with a name = "PerformanceLogger"
    /// <logger name="PerformanceLogger" minlevel="Trace" writeTo="PerformanceLogger" />
    /// </summary>
    public class PerformanceLogger : BaseLogger, IPerformanceLogger
    {
    }
}
