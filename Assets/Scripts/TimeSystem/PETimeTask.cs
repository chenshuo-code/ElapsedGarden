/// <summary>
/// 定时任务数据类
/// </summary>
using System;
/// <summary>
/// 时间定时任务数据类
/// </summary>
public class PETimeTask
{
    /// <summary>
    /// 定时任务的ID
    /// </summary>
    public int tid;
    /// <summary>
    /// 目标执行时间
    /// </summary>
    public float destTime;
    /// <summary>
    /// 需要延迟的时间
    /// </summary>
    public float delay;
    /// <summary>
    /// 委托
    /// </summary>
    public Action callback;
    /// <summary>
    /// 执行次数
    /// </summary>
    public int count;
    /// <summary>
    /// 定时任务构造函数
    /// </summary>
    public PETimeTask(int tid,Action callback, float destTime,float delay,int count)
    {
        this.tid = tid;
        this.callback = callback;
        this.destTime = destTime;
        this.delay = delay;
        this.count = count;
    }
}
/// <summary>
/// 帧定时任务数据类
/// </summary>
public class PEFrameTask
{
    /// <summary>
    /// 定时任务的ID
    /// </summary>
    public int tid;
    /// <summary>
    /// 目标执行帧数
    /// </summary>
    public int destFrame;
    /// <summary>
    /// 需要延迟的帧数
    /// </summary>
    public int delay;
    /// <summary>
    /// 委托
    /// </summary>
    public Action callback;
    /// <summary>
    /// 执行次数
    /// </summary>
    public int count;
    /// <summary>
    /// 帧定时任务构造函数
    /// </summary>
    public PEFrameTask(int tid, Action callback, int destFrame, int delay, int count)
    {
        this.tid = tid;
        this.callback = callback;
        this.destFrame = destFrame;
        this.delay = delay;
        this.count = count;
    }
}
/// <summary>
/// 时间单位枚举
/// </summary>
public enum PETimeUint
{
    Millsecound,
    Secound,
    Minute,
    Hour,
    Day
}
