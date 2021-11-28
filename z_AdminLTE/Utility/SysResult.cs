using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace z_AdminLTE
{
    public class SysResult
    {
        public SysResult()
        {
            Suscess = true;
        }

        public SysResult(bool success, string msg)
        {
            Suscess = success;
            Msg = msg;
        }

        /// <summary>
        /// 返回结果编码 0：失败 1：成功
        /// </summary>
        public bool Suscess { get; set; }

        /// <summary>
        /// 返回结果内容 成功：Success  失败：异常内容
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回结果 成功：返回T类型数据 失败：默认null
        /// </summary>
        public object ResultData { get; set; }
    }
}
