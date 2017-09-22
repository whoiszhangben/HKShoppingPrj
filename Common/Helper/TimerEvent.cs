using System;
using System.Timers;

namespace HKShoppingManage.Common
{
    /// <summary>
    /// Title：简易定时事件类
    /// CreateDate：2013年7月3日
    /// </summary>
    public class TimerEvent
    {
        #region 构造函数

        /// <summary>
        /// 定时事件构造函数
        /// [每次到达间隔时间时，先检查是否可执行，然后执行事件]
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="interval">时间检测间隔</param>
        /// <param name="doEvent">待执行事件</param>
        /// <param name="chkFunc">额外检查事件</param>
        public TimerEvent(string eventName, TimeSpan interval, Action doEvent, Func<bool> chkFunc)
        {
            Name = eventName;
            DoEvent = doEvent;
            ChkFunc = chkFunc;

            timer = new Timer(interval.TotalMilliseconds);
            timer.Elapsed += new ElapsedEventHandler(Elapsed);
        }

        #endregion

        /// <summary>
        /// 定时事件名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            timer.Start();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }

        #region Timer相关

        /// <summary>
        /// 内部定时器
        /// </summary>
        private Timer timer;
        /// <summary>
        /// 是否正在进行事件执行
        /// </summary>
        private bool isElapsing;
        /// <summary>
        /// 定时执行事件
        /// </summary>
        private Action DoEvent;
        /// <summary>
        /// 额外检查事件
        /// </summary>
        private Func<bool> ChkFunc;
        /// <summary>
        /// Timer间隔执行事件
        /// </summary>
        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isElapsing) { return; }
            try
            {
                isElapsing = true;
                if (ChkFunc != null && !ChkFunc())
                {
                    return;
                }
                if (DoEvent != null)
                {
                    try
                    {
                        LogHelper.Log.WriteDebug(string.Format("定时任务[{0}]开始", Name));
                        DoEvent();
                        LogHelper.Log.WriteDebug(string.Format("定时任务[{0}]结束", Name));
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("定时任务[{0}]出现异常", Name);
                LogHelper.Log.WriteError(errMsg, ex);
            }
            finally
            {
                isElapsing = false;
            }
        }

        #endregion


    }
}
