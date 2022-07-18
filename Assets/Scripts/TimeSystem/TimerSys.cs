using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 计时系统
/// </summary>
public class TimerSys : MonoBehaviour
{
    public static TimerSys Instance;//单例
    /// <summary>
    /// 多线程操作所用到的线程锁
    /// </summary>
    private static readonly string obj = "lock";
    /// <summary>
    /// 全局id
    /// </summary>
    private int tid;
    /// <summary>
    /// 全局id列表，用于保存id
    /// </summary>
    private List<int> tidList = new List<int>();
    /// <summary>
    /// 需要移除的tid缓存列表
    /// </summary>
    private List<int> recTidList = new List<int>();
    /// <summary>
    /// 临时缓存列表
    /// </summary>
    private List<PETimeTask> tmpTimeList = new List<PETimeTask>();
    /// <summary>
    /// 定时任务列表
    /// </summary>
    private List<PETimeTask> timeTaskList = new List<PETimeTask>();

    /// <summary>
    /// 帧数计数器
    /// </summary>
    private int frameCounter;
    /// <summary>
    /// 临时缓存列表(帧定时)
    /// </summary>
    private List<PEFrameTask> tmpFrameList = new List<PEFrameTask>();
    /// <summary>
    /// 定时任务列表（帧定时）
    /// </summary>
    private List<PEFrameTask> frameTaskList = new List<PEFrameTask>();

    /// <summary>
    /// 初始化方法
    /// </summary>
    public void InitSys()
    {
        Instance = this;
        Debug.Log("TimerSys Init Done.");
    }
    private void Update()
    {
        CheckTimeTask();
        CheckFrameTask();
        //如果有需要回收的tid
        if (recTidList.Count>0)
        {
            RecycleTid();
        }
    }
    /// <summary>
    /// 检测定时任务是否满足执行条件并执行方法
    /// </summary>
    private void CheckTimeTask()
    {
        //将缓存列表中的定时任务添加到定时任务列表
        for (int tmpIndex = 0; tmpIndex < tmpTimeList.Count; tmpIndex++)
        {
            timeTaskList.Add(tmpTimeList[tmpIndex]);
        }
        //清空缓存列表
        tmpTimeList.Clear();

        //遍历检测任务是否满足条件
        for (int index = 0; index < timeTaskList.Count; index++)
        {
            PETimeTask task = timeTaskList[index];
            //如果当前游戏时间小于目标时间
            if (Time.realtimeSinceStartup * 1000 < task.destTime)
            {
                continue;//继续
            }
            else//如果当前游戏时间大于等于目标时间（达到预设定时时间）
            {
                //获取对应的委托
                Action cb = task.callback;
                try
                {
                    if (cb != null)//安全验证此委托不为空
                    {
                        cb();//执行委托
                    }
                }
                catch (Exception e)
                {

                    throw new Exception(string.Format("需要执行的定时任务为空，异常码：{0}", e));
                }
                if (task.count == 1)//如果需要执行一次
                {
                    //移除已经完成的任务
                    timeTaskList.RemoveAt(index);
                    index--;//因为委托移除，需要把当前index减一，方便后续循环
                    recTidList.Add(task.tid);//将任务的tid添加到要移除的tid列表
                }
                else
                {
                    if (task.count != 0)//如果需要执行的次数不为无限
                    {
                        task.count -= 1;//减少一次执行次数
                    }
                    task.destTime += task.delay;//在原目标时间基础上加上延迟
                    //剩余一种情况，在次数设为0的时候，不会移除任务，目标时间增加，实现无限次数循环
                }
            }
        }
    }
    /// <summary>
    /// 检测帧定时任务是否满足执行条件并执行方法
    /// </summary>
    private void CheckFrameTask()
    {
        //将缓存列表中的帧定时任务添加到帧定时任务列表
        for (int tmpIndex = 0; tmpIndex < tmpFrameList.Count; tmpIndex++)
        {
            frameTaskList.Add(tmpFrameList[tmpIndex]);
        }
        //清空缓存列表
        tmpFrameList.Clear();

        frameCounter += 1;//每一帧记录一次，帧计数器+1

        //遍历检测任务是否满足条件
        for (int index = 0; index < frameTaskList.Count; index++)
        {
            PEFrameTask task = frameTaskList[index];
            //如果当前游戏帧数小于目标帧数
            if (frameCounter < task.destFrame)
            {
                continue;//继续
            }
            else//如果当前游戏帧数大于等于目标帧数（达到预设定时帧数）
            {
                //获取对应的委托
                Action cb = task.callback;
                try
                {
                    if (cb != null)//安全验证此委托不为空
                    {
                        cb();//执行委托
                    }
                }
                catch (Exception e)
                {

                    throw new Exception(string.Format("需要执行的帧定时任务为空，异常码：{0}", e));
                }
                if (task.count == 1)//如果需要执行一次
                {
                    //移除已经完成的任务
                    frameTaskList.RemoveAt(index);
                    index--;//因为委托移除，需要把当前index减一，方便后续循环
                    recTidList.Add(task.tid);//将任务的tid添加到要移除的tid列表
                }
                else
                {
                    if (task.count != 0)//如果需要执行的次数不为无限
                    {
                        task.count -= 1;//减少一次执行次数
                    }
                    task.destFrame += task.delay;//在原目标帧数基础上加上延迟帧
                    //剩余一种情况，在次数设为0的时候，不会移除任务，目标帧数增加，实现无限次数循环
                }
            }
        }
    }

    #region TimeTask 定时任务方法
    /// <summary>
    /// 增加定时任务
    /// </summary>
    /// <param name="callback">需要执行的委托</param>
    /// <param name="delay">延迟时间</param>
    /// <param name="timeUint">时间单位（默认毫秒）</param>
    /// <param name="count">执行次数（默认1次，0代表无限次）</param>
    /// <returns>该任务全局id</returns>
    public int AddTimeTask(Action callback, float delay, PETimeUint timeUint = PETimeUint.Millsecound, int count = 1)
    {
        //判断 如果传入的时间单位不是毫秒
        if (timeUint != PETimeUint.Millsecound)
        {
            switch (timeUint)
            {
                case PETimeUint.Secound:
                    delay = delay * 1000;
                    break;
                case PETimeUint.Minute:
                    delay = delay * 1000 * 60;
                    break;
                case PETimeUint.Hour:
                    delay = delay * 1000 * 360;
                    break;
                case PETimeUint.Day:
                    delay = delay * 1000 * 360 * 24;
                    break;
                default:
                    Debug.Log("Add Task TimeUnit Type Error..");
                    break;
            }
        }
        int tid = GetTid();
        //目标执行时间=游戏开始运行的时间+需要延迟的时间
        float destTime = Time.realtimeSinceStartup * 1000 + delay;
        //新建一个定时任务类，加入缓存列表（为了跟实际执行任务错开一帧）
        tmpTimeList.Add(new PETimeTask(tid, callback, destTime, delay, count));
        //将该任务的全局id存入id列表
        tidList.Add(tid);
        return tid;//返回该任务id
    }
    /// <summary> 
    /// 删除定时任务
    /// </summary>
    /// <param name="tid">要移除定时任务的全局id</param>
    /// <returns>该任务是否存在并成功移除</returns>
    public bool DeleteTimeTask(int tid)
    {
        bool exist = false;//默认该任务不存在
        for (int i = 0; i < timeTaskList.Count; i++)//遍历任务列表
        {
            PETimeTask task = timeTaskList[i];
            if (task.tid == tid)//如果要移除的任务id存在
            {
                timeTaskList.RemoveAt(i);//移除该任务
                for (int j = 0; j < tidList.Count; j++)
                {
                    if (tidList[j] == tid)
                    {
                        tidList.RemoveAt(j);//在全局id列表中移除该任务的tid
                        break;
                    }
                }
                exist = true;
                break;
            }
        }
        if (!exist)//如果要移除的任务不在任务列表中
        {
            for (int i = 0; i < tmpTimeList.Count; i++)//遍历缓存列表寻找(同上)
            {
                PETimeTask task = tmpTimeList[i];
                if (task.tid == tid)
                {
                    tmpTimeList.RemoveAt(i);
                    for (int j = 0; j < tidList.Count; j++)
                    {
                        if (tidList[j] == tid)
                        {
                            tidList.RemoveAt(j);//在全局id列表中移除该任务的tid
                            break;
                        }
                    }
                    exist = true;
                    break;
                }
            }
        }
        return exist;//返回结果，该任务存在并成功移除
    }
    /// <summary>
    /// 替换定时任务
    /// </summary>
    /// <param name="tid">要替换定时任务的全局id</param>
    /// <param name="callback">需要替换的委托</param>
    /// <param name="delay">延迟时间</param>
    /// <param name="timeUint">时间单位（默认毫秒）</param>
    /// <param name="count">执行次数（默认1次，0代表无限次）</param>
    /// <returns>该任务是否存在并成功替换</returns>
    public bool ReplaceTimeTask(int tid, Action callback, float delay, PETimeUint timeUint = PETimeUint.Millsecound, int count = 1)
    {
        //判断 如果传入的时间单位不是毫秒
        if (timeUint != PETimeUint.Millsecound)
        {
            switch (timeUint)
            {
                case PETimeUint.Secound:
                    delay = delay * 1000;
                    break;
                case PETimeUint.Minute:
                    delay = delay * 1000 * 60;
                    break;
                case PETimeUint.Hour:
                    delay = delay * 1000 * 360;
                    break;
                case PETimeUint.Day:
                    delay = delay * 1000 * 360 * 24;
                    break;
                default:
                    Debug.Log("Add Task TimeUnit Type Error..");
                    break;
            }
        }
        //目标执行时间=游戏开始运行的时间+需要延迟的时间
        float destTime = Time.realtimeSinceStartup * 1000 + delay;
        //新建一个定时任务类
        PETimeTask newTask = new PETimeTask(tid, callback, destTime, delay, count);
        //默认要替换的任务不存在，替换失败
        bool isRep = false;
        for (int i = 0; i < timeTaskList.Count; i++)
        {
            if (timeTaskList[i].tid == tid)//如果该任务在任务列表中存在
            {
                timeTaskList[i] = newTask;//将该任务替换成新任务
                isRep = true;//标志替换成功
                break;
            }
        }
        if (!isRep)//如果替换失败
        {
            for (int i = 0; i < tmpTimeList.Count; i++)//遍历缓存列表寻找
            {
                if (tmpTimeList[i].tid == tid)//如果找到
                {
                    tmpTimeList[i] = newTask;//替换该任务
                    isRep = true;//标志替换成功
                    break;
                }
            }
        }
        return isRep;//返回结果，是否替换成功
    }
    #endregion

    #region Frame Task 帧定时任务方法
    /// <summary>
    /// 增加帧定时任务
    /// </summary>
    /// <param name="callback">需要执行的委托</param>
    /// <param name="delay">延迟帧数</param>
    /// <param name="count">执行次数（默认1次，0代表无限次）</param>
    /// <returns>该任务全局id</returns>
    public int AddFrameTask(Action callback, int delay, int count = 1)
    {
        int tid = GetTid();
        //新建一个定时任务类，加入缓存列表（为了跟实际执行任务错开一帧）
        tmpFrameList.Add(new PEFrameTask(tid, callback, frameCounter+delay, delay, count));
        //将该任务的全局id存入id列表
        tidList.Add(tid);
        return tid;//返回该任务id
    }
    /// <summary> 
    /// 删除帧定时任务
    /// </summary>
    /// <param name="tid">要移除定时任务的全局id</param>
    /// <returns>该任务是否存在并成功移除</returns>
    public bool DeleteFrameTask(int tid)
    {
        bool exist = false;//默认该任务不存在
        for (int i = 0; i < frameTaskList.Count; i++)//遍历任务列表
        {
            PEFrameTask task = frameTaskList[i];
            if (task.tid == tid)//如果要移除的任务id存在
            {
                frameTaskList.RemoveAt(i);//移除该任务
                for (int j = 0; j < tidList.Count; j++)
                {
                    if (tidList[j] == tid)
                    {
                        tidList.RemoveAt(j);//在全局id列表中移除该任务的tid
                        break;
                    }
                }
                exist = true;
                break;
            }
        }
        if (!exist)//如果要移除的任务不在任务列表中
        {
            for (int i = 0; i < tmpFrameList.Count; i++)//遍历缓存列表寻找(同上)
            {
                PEFrameTask task = tmpFrameList[i];
                if (task.tid == tid)
                {
                    tmpFrameList.RemoveAt(i);
                    for (int j = 0; j < tidList.Count; j++)
                    {
                        if (tidList[j] == tid)
                        {
                            tidList.RemoveAt(j);//在全局id列表中移除该任务的tid
                            break;
                        }
                    }
                    exist = true;
                    break;
                }
            }
        }
        return exist;//返回结果，该任务存在并成功移除
    }
    /// <summary>
    /// 替换帧定时任务
    /// </summary>
    /// <param name="tid">要替换帧定时任务的全局id</param>
    /// <param name="callback">需要替换的委托</param>
    /// <param name="delay">延迟帧数</param>
    /// <param name="count">执行次数（默认1次，0代表无限次）</param>
    /// <returns>该任务是否存在并成功替换</returns>
    public bool ReplaceFrameTask(int tid, Action callback, int delay, int count = 1)
    {
        //新建一个定时任务类
        PEFrameTask newTask = new PEFrameTask(tid, callback, frameCounter+delay, delay, count);
        //默认要替换的任务不存在，替换失败
        bool isRep = false;
        for (int i = 0; i < frameTaskList.Count; i++)
        {
            if (frameTaskList[i].tid == tid)//如果该任务在任务列表中存在
            {
                frameTaskList[i] = newTask;//将该任务替换成新任务
                isRep = true;//标志替换成功
                break;
            }
        }
        if (!isRep)//如果替换失败
        {
            for (int i = 0; i < tmpFrameList.Count; i++)//遍历缓存列表寻找
            {
                if (tmpFrameList[i].tid == tid)//如果找到
                {
                    tmpFrameList[i] = newTask;//替换该任务
                    isRep = true;//标志替换成功
                    break;
                }
            }
        }
        return isRep;//返回结果，是否替换成功
    }
    #endregion

    #region Tool Methods 工具方法
    /// <summary>
    /// 为每个定时任务生成唯一id方法
    /// </summary>
    /// <returns></returns>
    private int GetTid()
    {
        lock (obj) //因为多线程进行，为保证数据安全，在线程锁内执行
        {
            tid += 1;//增加一次id

            //安全代码，防止int数据达到最大值
            while (true)
            {
                if (tid == int.MaxValue)//如果全局id达到int最大值，将tid清0重新开始
                {
                    tid = 0;
                }
                bool used = false;
                for (int i = 0; i < tidList.Count; i++)
                {
                    if (tid == tidList[i])//如果重新开始的tid与之前保存使用的id相同，发生冲突
                    {
                        used = true;//标志id被占用
                        break;//跳出循环
                    }
                }
                if (!used)//如果当前id未被其他任务占用
                {
                    break;//跳出循环使用该id
                }
                else
                {
                    tid += 1;//如果当前id已被其他任务占用，id+1
                }
            }
        };
        return tid;
    }
    /// <summary>
    /// 回收完成任务的Tid方法
    /// </summary>
    private void RecycleTid()
    {
        for (int i = 0; i < recTidList.Count; i++)//遍历需要回收的tid列表
        {
            int tid = recTidList[i];//需要回收的tid
            for (int j = 0; j < tidList.Count; j++)//遍历tid列表
            {
                if (tidList[j] == tid)//找到需要回收的tid
                {
                    tidList.RemoveAt(j);//在tid列表中移除此tid
                    break;
                }
            }
        }
        //回收完成，清空回收列表
        recTidList.Clear();
    } 
    #endregion
}
