using Microsoft.Practices.Unity.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Unity;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace StudyDemo1
{
    /// <summary>
    /// AOP 面向切面编程
    /// </summary>
    public class T15_AOP
    {
        public static void Use()
        {

            //DecoratorAOP.Show();  // 测试装饰器模式

            //ProxyAOP.Show();      // 测试代理模式

            UnityConfigAOP.Show();  // unity 实现aop
        }
    }

    #region AOP静态实现：装饰器模式

    /// <summary>
    /// 装饰器模式实现静态代理
    /// AOP在方法前后增加自定义的方法
    /// </summary>
    public class DecoratorAOP
    {
        public static void Show()
        {
            User user = new User { Name = "名称", Password = "123456" };

            IUserProcessor processor = new UserProcessor();
            processor = new UserProcessorDecorator(processor);
            processor.RegUser(user);
        }
    }

    /// <summary>
    /// 装饰器的模式去提供一个AOP功能
    /// </summary>
    public class UserProcessorDecorator : IUserProcessor
    {
        private IUserProcessor _userProcessor { get; set; }

        public UserProcessorDecorator(IUserProcessor userProcessor)
        {
            _userProcessor = userProcessor;
        }

        public void RegUser(User user)
        {
            BeforeProceed(user);

            this._userProcessor.RegUser(user);

            AfterProceed(user);
        }
        /// <summary>
        /// 业务逻辑之前
        /// </summary>
        private void BeforeProceed(User user)
        {
            Console.WriteLine("逻辑前");
        }
        /// <summary>
        /// 业务逻辑之后
        /// </summary>
        private void AfterProceed(User user)
        {
            Console.WriteLine("逻辑后");
        }
    }

    
    #endregion

    #region AOP静态实现，代理模式
    /// <summary>
    /// 代理模式实现静态代理
    /// AOP 在方法前后增加自定义的方法
    /// </summary>
    public class ProxyAOP
    {
        public static void Show()
        {
            User user = new User() { Name = "名称", Password = "123456" };
            IUserProcessor processor = new ProxyUserProcessor();
            processor.RegUser(user);
        }
    }
    /// <summary>
    /// 代理模式去提供一个AOP功能
    /// </summary>
    public class ProxyUserProcessor : IUserProcessor
    {
        private IUserProcessor _userProcessor = new UserProcessor();

        public void RegUser(User user)
        {
            BeforeProceed(user);
            _userProcessor.RegUser(user);
            AfterProceed(user);
        }

        /// <summary>
        /// 业务逻辑之前
        /// </summary>
        private void BeforeProceed(User user)
        {
            Console.WriteLine("逻辑前");
        }
        /// <summary>
        /// 业务逻辑之后
        /// </summary>
        private void AfterProceed(User user)
        {
            Console.WriteLine("逻辑后");
        }
    }

    #endregion

    #region AOP动态实现，代理模式(已经启用，.net core 中没有该类类库)

    /// <summary>
    /// 使用.Net Remoting/RealProxy实现动态代理
    /// 局限在业务类必须是继承自MarshalByRefObject类型
    /// </summary>
    //public class RealProxyAOP
    //{
    //    public static void Show()
    //    {
    //        User user = new User { Name = "名称", Password = "123456" };

    //        UserProcessor processor = TransparentProxy.Create<UserProcessor>();


    //    }
    //}
    ///// <summary>
    ///// 真实代理
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class MyRealProxy<T>: RealProxy
    //{
    //    private Type type = null;
    //    public InvokeProxy() : this(typeof(T))
    //    {
    //        type = typeof(T);
    //    }

    //    protected InvokeProxy(Type classToProxy) : base(classToProxy)
    //    {
    //    }

    //    //接收本地调用请求，然后转发远程访问

    //    public override IMessage Invoke(IMessage msg)
    //    {
    //        Console.WriteLine("Invoke 远程服务调用！");
    //        ReturnMessage message = new System.Runtime.Remoting.Messaging.ReturnMessage("Test", null, 0, null, (IMethodCallMessage)msg);

    //        return (IMessage)message;
    //    }
    //}

    ///// <summary>
    ///// 透明代理
    ///// </summary>
    //public static class TransparentProxy
    //{
    //    public static T Create<T>()
    //    {
    //        T instance = Activator.CreateInstance<T>();
    //        MyRealProxy<T> realProxy = new MyRealProxy<T>(instance);
    //        T transparentProxy = (T)realProxy.GetTransparentProxy();
    //        return transparentProxy;
    //    }
    //}


    #endregion

    #region 使用 Unity 依赖注入支持AOP扩展(微软提供)   Nuget 查找Unity ,安装6个类库

    /// <summary>
    /// 通过Unity + 配置文件来实现的
    /// 使用EntLib\PIAB Unity 实现动态代理
    /// </summary>
    public class UnityConfigAOP
    {
        public static void Show()
        {
            User user = new User { Name = "名称", Password = "123456" };
            //创建容器
            IUnityContainer container = new UnityContainer();
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            // unity 配置文件路径
            fileMap.ExeConfigFilename = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "\\Unity.Config");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
            configSection.Configure(container, "aopContainer");//容器名称，保持与配置文件一致

            IUserProcessor1 processor = container.Resolve<IUserProcessor1>();
            processor.RegUser(user);
            User userNew1 = processor.GetUser(user);
        }
    }

    #endregion


    #region 公用部分

    public interface IUserProcessor
    {
        void RegUser(User user);
    }

    //public class UserProcessor : IUserProcessor
    public class UserProcessor : MarshalByRefObject, IUserProcessor   // 动态实现，必须继承自MarshalByRefObject。静态的用上方的
    {
        public void RegUser(User user)
        {
            Console.WriteLine("用户功能实现！");
        }
    }


    public interface IUserProcessor1
    {
        void RegUser(User user);
        User GetUser(User user);
    }

    public class UserProcessor1 : IUserProcessor1   
    {
        public void RegUser(User user)
        {
            Console.WriteLine("用户功能实现！");
        }

        public User GetUser(User user)
        {
            return user;
        }
    }



    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    #endregion


    #region Unity AOP扩展类 方法
    /// <summary>
    /// 扩展一： 插入日志
    /// </summary>
    public class LogBeforeBehavior : IInterceptionBehavior
    {
        public bool WillExecute { get { return true; } }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //此处编写需要添加的业务逻辑（写日志逻辑）
            Console.WriteLine("写入日志");
            // 执行实例方法（必须调用），如果先调用实例方法，再执行扩展操作，则需要先调用getNext().Invoke(input, getNext) 方法
            return getNext().Invoke(input, getNext);   
        }
    }

    /// <summary>
    /// 扩展二：执行缓存方法（方法结果缓存）
    /// </summary>
    public class CachingBehavior : IInterceptionBehavior
    {
        private static Dictionary<string, object> CachingBehaviorDictionary = new Dictionary<string, object>();

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("CachingBehavior");
            string key = $"{input.MethodBase.Name}_{JsonConvert.SerializeObject(input.Inputs)}";
            if (CachingBehaviorDictionary.ContainsKey(key))
            {
                return input.CreateMethodReturn(CachingBehaviorDictionary[key]);//缓存存在，直接返回缓存结果
            }
            else
            {
                //缓存不存在，执行方法并保存缓存结果
                IMethodReturn result = getNext().Invoke(input, getNext);
                if (result.ReturnValue != null)
                    CachingBehaviorDictionary.Add(key, result.ReturnValue);
                return result;
            }

        }

        public bool WillExecute
        {
            get { return true; }
        }
    }

    /// <summary>
    /// 扩展三：方法异常处理
    /// </summary>
    public class ExceptionLoggingBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn methodReturn = getNext()(input, getNext);

            Console.WriteLine("ExceptionLoggingBehavior");
            if (methodReturn.Exception == null)
            {
                Console.WriteLine("无异常");
            }
            else
            {
                Console.WriteLine($"异常:{methodReturn.Exception.Message}");
            }
            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }

    /// <summary>
    /// 扩展四：方法执行后操作
    /// </summary>
    public class LogAfterBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn methodReturn = getNext()(input, getNext);//执行后面的全部动作

            Console.WriteLine("LogAfterBehavior");
            Console.WriteLine(input.MethodBase.Name);
            foreach (var item in input.Inputs)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
                //反射&序列化获取更多信息
            }
            Console.WriteLine("LogAfterBehavior" + methodReturn.ReturnValue);
            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }

    /// <summary>
    /// 扩展五：参数检查（敏感词过滤等场景）
    /// </summary>
    public class ParameterCheckBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("ParameterCheckBehavior");
            User user = input.Inputs[0] as User;
            if (user.Password.Length < 10)
            {
                //返回一个异常
                return input.CreateExceptionMethodReturn(new Exception("密码长度不能小于10位"));

            }
            else
            {
                Console.WriteLine("参数检测无误");
                return getNext().Invoke(input, getNext);
            }
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
    #endregion

    #region 知识点

    /// 3.动态代理实现AOP
    /// 4.Unity、MVC中的AOP

    #endregion

}
