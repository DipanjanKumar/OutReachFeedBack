using System.Threading.Tasks;
using Quartz;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using OutReachFeedBack.Models;

namespace OutReachFeedBack.Scheduler
{
    public class Scheduler
    {
        public static async void StartJobAsync()
        {
            try
            {
                IScheduler scheduler = await Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<LoggingJob>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInHours(1)
                            .RepeatForever())
                            .StartNow()
                        .Build();

               await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                ExceptionDTO logger = new ExceptionDTO()
                {
                    ControllerName = "Scheduler",
                    ActionrName = "StartJob",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "api/ProcessExcel/AddExceptionToDB";
                using (var client = new HttpClient())
                {
                    var responseTask = client.PostAsJsonAsync(requestURI, logger);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                    }
                }
            }
        }
        public class LoggingJob : IJob
        {
            string directory = "";
            public Task Execute(IJobExecutionContext context)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["FilePath"]);
                FileInfo[] fileInfoArray = directoryInfo.GetFiles("*.xlsx");
                Dictionary<int, string> fileDictionary = new Dictionary<int, string>();
                List<string> filepaths = new List<string>();
                foreach (FileInfo file in fileInfoArray)
                {
                    switch (file.Name)
                    {
                        case "OutReach Event Information.xlsx":
                            fileDictionary.Add(2, file.FullName);
                            break;
                        case "Outreach Events Summary.xlsx":
                            fileDictionary.Add(1, file.FullName);
                            break;
                        case "Volunteer_Enrollment Details_Not_Attend.xlsx":
                            fileDictionary.Add(3, file.FullName);
                            break;
                        case "Volunteer_Enrollment Details_Unregistered.xlsx":
                            fileDictionary.Add(4, file.FullName);
                            break;
                    }
                }
                foreach (KeyValuePair<int, string> author in fileDictionary.OrderBy(key => key.Key))
                {
                    filepaths.Add(author.Value);
                }
                if (filepaths.Count > 0)
                {
                    using (var client = new HttpClient())
                    {
                        string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "api/ProcessExcel/AddExcelDataToDB";
                        var responseTask = client.PostAsJsonAsync(requestURI, filepaths);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            directory = ConfigurationManager.AppSettings["ArchiveFilePath"] + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                            if (Directory.Exists(directory))
                            {
                                MoveFiles();
                            }
                            else
                            {
                                Directory.CreateDirectory(directory);
                                MoveFiles();
                            }
                        }
                        else
                        {

                        }
                    }
                }

                return null;
            }
            public void MoveFiles()
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["FilePath"]);
                FileInfo[] fileInfoArray = directoryInfo.GetFiles("*.xlsx");
                foreach (FileInfo file in fileInfoArray)
                {
                    File.Copy(file.FullName, directory + "\\" + file.Name, true);
                    File.Delete(file.FullName);
                }
            }
        }        
    }
}