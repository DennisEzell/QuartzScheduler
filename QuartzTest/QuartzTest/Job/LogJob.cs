using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Quartz;

namespace QuartzTest.Job
{
    public class LogJob : IJob
    {
        private static int jobCounter;

        public void Execute(IJobExecutionContext context)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                string sql = $@"
                              INSERT INTO Logging.Logs(logDate,logMsg)
                              VALUES('{DateTime.Now}', 'Job {jobCounter++} - inserting into DB')";

                SqlCommand command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
        }
    }
}
