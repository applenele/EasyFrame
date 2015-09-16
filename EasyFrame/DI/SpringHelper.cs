using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public class SpringHelper
    {
        /// <summary>
        /// Spring 上下文
        /// </summary>
        public static IApplicationContext SpringContext
        {
            get
            {
                return ContextRegistry.GetContext();
            }
        }


        /// <summary>
        /// 获取配置文件的 配置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public static T GetObject<T>(string objectName) where T : class
        {
            return (T)SpringContext.GetObject(objectName);
        }
    }
}
