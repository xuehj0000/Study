using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System.Linq;

namespace StudyDemo1
{
    /// <summary>
    /// 调度任务
    /// </summary>
    public class T16_QuartZ
    {
        public async static Task Init()
        {
            #region 创建一个单元（时间轴/载体）

            StdSchedulerFactory factory = new StdSchedulerFactory();     
            IScheduler scheduler = await factory.GetScheduler();        
            await scheduler.Start();

            #endregion

            #region 监听

            scheduler.ListenerManager.AddJobListener(new MyJobListener());
            scheduler.ListenerManager.AddTriggerListener(new MyTriggerListener());

            #endregion

            // Job 生成作业描述   
            IJobDetail jobDetail = JobBuilder.Create<SendMessageJob>()             
                                                .WithIdentity("sendMessageJob", "group1")     
                                                .WithDescription("this is send Message Job")   // 描述
                                                .Build();           //生成描述

            jobDetail.JobDataMap.Add("student1", "张三");// 传递参数
            jobDetail.JobDataMap.Add("student2", "李四");
            jobDetail.JobDataMap.Add("Year", DateTime.Now.Year);

            // Trigger 时间策略
            ITrigger trigger = TriggerBuilder.Create()
                                              .WithIdentity("sendMessageTrigger", "group1")
                                              .StartNow()
                                              .WithSimpleSchedule(w=>w.WithIntervalInSeconds(5).WithRepeatCount(3))
                                              .Build();        // 得到一个标准的时间策略

            trigger.JobDataMap.Add("student3", "王五");
            trigger.JobDataMap.Add("student4", "六六");
            trigger.JobDataMap.Add("Year", DateTime.Now.Year);


            // 把时间策略和作业承载到单元上
            await scheduler.ScheduleJob(jobDetail, trigger);
        }




    }

    #region 任务类

    /// <summary>
    /// 任务类
    /// </summary>
    [PersistJobDataAfterExecution]         // 执行后的保留作业数据
    [DisallowConcurrentExecution]          // 上次没结束，不允许当前执行。保证任务可以一一串联起来
    public class SendMessageJob : IJob     // 无状态的
    {
        public SendMessageJob()
        {
            Console.WriteLine("SendMessageJob 被构造了！");       //每次调用都创建新的实例，无状态的
        }


        /// <summary>
        /// 当前 Task 内部就是作业需要执行的任务,执行的任务
        /// </summary>
        public async Task Execute(IJobExecutionContext context)   // context 是上下文，有了context以后我们就可以获取很多信息
        {
            // 在发消息上课的时候，需要制定名称
            await Task.Run(() =>
            {
                var student1 = context.JobDetail.JobDataMap.GetString("student1");
                var year1 = context.JobDetail.JobDataMap.GetInt("Year");

                var student2 = context.Trigger.JobDataMap.Get("student4");
                var year2 = context.Trigger.JobDataMap.GetInt("Year");


                var year3 = context.MergedJobDataMap.Get("Year");   // 注意，使用MergedJobDataMap有覆盖，去重。后者为准

                Console.WriteLine();
                Console.WriteLine("**********************");
                Console.WriteLine($"{student1}你好！{year1}现在开始上课了！{ DateTime.Now }");
                Console.WriteLine($"{student2}你好！{year2}现在开始上课了！{ DateTime.Now }");
                Console.WriteLine($"{year3}");
                Console.WriteLine("**********************");
                Console.WriteLine();


                context.JobDetail.JobDataMap.Put("Year", year2 + 1);  // 链式传参

            });
        }
    }

    #endregion

    #region 监听

    /// <summary>
    /// 可以做一些日志的记录
    /// </summary>
    public class MyJobListener : IJobListener
    {
        public string Name => throw new NotImplementedException();

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("this is JobExecutionVetoed");
            });
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("this is JobToBeExecuted");
            });
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("this is JobWasExecuted");
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyTriggerListener : ITriggerListener
    {
        public string Name => throw new NotImplementedException();

        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("this is TriggerComplete");
            });
        }

        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("this is TriggerFired");
            });
        }

        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("this is TriggerMisfired");
            });
        }

        /// <summary>
        /// 是否取消，如果返回false, 如果返回true,就会取消了
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("this this ");
            });
            return false;
        }
    }


    public class MySchedulerListener : ISchedulerListener
    {
        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region 日志

    public class MyQuartZLog : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            var logger = new Logger((level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}][{func()}{string.Join(";", parameters.Select(r => r == null ? " " : r.ToString()))} 自定义日志{name}]");
                }
                return false;
            });
            return logger;
        }

        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region 知识点

    // QuartZ 定时任务框架，包含三个角色
    // Ischeduler(某一单元，流程)
    // ITrigger 从某一时刻开始做事
    // Ijob 做什么事儿






    #endregion


}
