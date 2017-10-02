using System;
using ZLog;

namespace ZRepositoryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var perfTrack = new PerformanceTracker("ZRepository", Environment.UserName, Environment.UserName,
                "ZRepositoryConsole.Program.Main", "ZRepository", "Job");

            try
            {
                perfTrack.Stop();
            }
            catch (Exception ex)
            {
                var logDetail = new LogDetail
                {
                    Product = "ZRepository",
                    Layer = "Job",
                    Location = "ZRepositoryConsole.Program.Main",
                    UserName = Environment.UserName,
                    Hostname = Environment.MachineName,
                    Exception = ex
                };
                var logger = new ErrorLogger();
                logger.WriteLog(logDetail);
            }
            finally
            {
                perfTrack.Stop();
                Console.ReadKey();
            }
        }
    }
}

