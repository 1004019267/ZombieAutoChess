using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 
/// 定时触发类
/// 张雨强
/// 
/// </summary>
/// 
public delegate void TimerHandler();

public delegate void TimerArgsHandler(System.Object[] args);

public class Timer
{
    public TimerHandler m_Handler;  //无参的委托
    public TimerArgsHandler m_ArgsHandler;//带参数的委托
    public bool m_IsComplete = true; //是否完成
    public float m_Frequency; //时间间隔
    public int m_Repeats; //重复次数
    public System.Object[] m_Args;

    public float m_LastTickTime;

    public Timer() { }

    /// <summary>
    /// 创建一个时间事件对象
    /// </summary>
    /// <param name="m_Handler">回调函数</param>
    /// <param name="m_ArgsHandler">带参数的回调函数</param>
    /// <param name="frequency">时间内执行</param>
    /// <param name="repeats">重复次数</param>
    /// <param name="m_Args">参数  可以任意的传不定数量，类型的参数</param>
    public Timer(TimerHandler m_Handler, TimerArgsHandler m_ArgsHandler, float frequency, int repeats, System.Object[] m_Args)
    {
        this.m_Handler = m_Handler;
        this.m_ArgsHandler = m_ArgsHandler;
        this.m_Frequency = frequency;
        this.m_Repeats = repeats == 0 ? 1 : repeats;
        this.m_Args = m_Args;
        this.m_LastTickTime = Time.time;
    }

    public void Notify()
    {
        if (m_Handler != null)
            m_Handler();
        if (m_ArgsHandler != null)
            m_ArgsHandler(m_Args);
    }

    public void CleanUp()
    {
        m_Handler = null;
        m_ArgsHandler = null;
        m_IsComplete = true;
        m_Repeats = 1;
        m_Frequency = 0;
    }

}

/// <summary>
/// 计时器
/// 添加一个计时事件
/// 删除一个计时事件
/// 更新计时事件
/// </summary>
public class TimerMgr : Singleton<TimerMgr>
{
    private List<Timer> _Timers;//时间管理器
    protected override void Awake()
    {
        base.Awake();
        if (_Timers == null)
        {
            _Timers = new List<Timer>();
        }
    }
    /// <summary>
    /// 创建一个简单的计时器
    /// </summary>
    /// <param name="callBack">回调函数</param>
    /// <param name="time">计时器时间</param>
    /// <param name="repeats">回调次数  小于0代表循环 大于0代表repeats次</param>
    public Timer CreateTimer(TimerHandler callBack, float time, int repeats = 1)
    {
        return Create(callBack, null, time, repeats);
    }

    public Timer CreateTimer(TimerArgsHandler callBack, float time, int repeats, params System.Object[] args)
    {
        return Create(null, callBack, time, repeats, args);
    }

    private Timer Create(TimerHandler callBack, TimerArgsHandler callBackArgs, float time, int repeats, params System.Object[] args)
    {
        Timer timer = new Timer(callBack, callBackArgs, time, repeats, args);
        _Timers.Add(timer);
        return timer;
    }

    public void DestroyTimer(Timer timer)
    {
        if (timer != null)
        {
            _Timers.Remove(timer);
            timer.CleanUp();
        }
    }

    /// <summary>
    /// 固定更新检查更新的频率
    /// </summary>
    void FixedUpdate()
    {
        if (_Timers == null)
            return;
        if (_Timers.Count != 0)
        {
            for (int i = _Timers.Count - 1; i >= 0; i--)
            {
                Timer timer = _Timers[i];
                float curTime = Time.time;
                if (timer.m_Frequency + timer.m_LastTickTime > curTime)
                {
                    continue;
                }
                timer.m_LastTickTime = curTime;
                if (timer.m_Repeats-- == 0)
                {//计时完成，可以删除了
                    DestroyTimer(timer);
                }
                else
                {//触发计时
                    timer.Notify();
                }
            }
        }
    }
}
