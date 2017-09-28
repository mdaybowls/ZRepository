using System;
using Paycor.PayrollCorp.Data;
using ZRepository;
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
                using (var db = new PayrollCorpContext())
                {
                    var uow = new GenericUnitOfWork(db);
                    var pqRepo = uow.GetRepositoryInstance<Company>();
                    var company = new Company
                    {
                        CompanyID = 1,
                        CompanyName = "MRDTest",
                        Inactive = "Y",
                        UserID = "mday",
                        DateModified = DateTime.Now
                    };

                    pqRepo.Add(company);
                    pqRepo.Delete(new Company {CompanyID = 1});
                    uow.SaveChanges();
                }
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

