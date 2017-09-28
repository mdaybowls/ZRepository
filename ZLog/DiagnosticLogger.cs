namespace ZLog
{
    /// <inheritdoc cref="" />
    /// <summary>
    /// Writes information to the "DiagnosticLogger" file.
    /// This Class will create default NLog Configuration entries that will
    /// enabled logging to a file with a name that starts with "DiagnosticLogger".
    /// It will use a target with a name = "DiagnosticLogger"
    /// <target name="DiagnosticLogger" type="file" layout="${message}" fileName="DiagnosticLogger + -${shortdate}.log" />
    /// It will use a rule with a name = "PerformanceLogger"
    /// <logger name="DiagnosticLogger" minlevel="Trace" writeTo="DiagnosticLogger" />
    /// </summary>
    public class DiagnosticLogger : BaseLogger, IDiagnosticLogger
    {
        public DiagnosticLogger()
        {
            EnabledByDefault = false;
        }
    }
}