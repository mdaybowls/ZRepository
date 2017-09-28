namespace ZLog
{
    public interface ILogger
    {
        bool Enabled { get; set; }
        void InitializeLogger();
        void WriteLog(LogDetail logDetail);
    }
}