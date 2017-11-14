using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using Quartz;
using Quartz.Impl;
using QuartzTest.Job;

namespace QuartzTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get the singleton scheduler from the factory (Java sends its regards)
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            //Start it
            scheduler.Start();
            /**
             *  Step (1) Define the job
             *      Create a new job and define it using our custom job class
             *      This custom class would contain our job logic.
             */
            IJobDetail job = JobBuilder.Create<LogJob>()
                .WithIdentity("ConsoleLogging", "Logging")
                .Build();
            //DateTimeOffset jobTime = new DateTimeOffset(2017, 11, 13, 22, 00, 00, TimeSpan.Zero);
            /**
             * Step (2) Create a trigger that will specify when a job will run 
             */
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ConsoleTrigger", "Logging")
                //.StartAt(jobTime)
                .StartNow() 
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                .Build();
            /**
             * Step (3) Tell the scheduler to run the job using the correct trigger
             */
            scheduler.ScheduleJob(job, trigger);
            Thread.Sleep(TimeSpan.FromSeconds(15));
            //Shut it down
            scheduler.Shutdown();

            //Query DB for records created
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();
                const string sql = @"SELECT * FROM Logging.Logs;";

                SqlCommand command = new SqlCommand(sql, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID:{reader["logID"]} " +
                                          $"-- Date:{reader["logDate"]} " +
                                          $"-- Msg:{reader["logMsg"]}");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
