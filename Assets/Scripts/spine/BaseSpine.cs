using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BaseSpine :MonoBehaviour
{
 
    
    //public Transform m_run_left;//player的左边
    //public Transform m_run_right;//player的右边

    //public SkeletonUtilityHide _SkeletonUtilityHide;//换装类
    //public SkeletonUtility _SkeletonUtility;
    [HideInInspector]
    public SkeletonGraphic _SkeletonGraphic;
    Action _start;
    Action _end;
    //  public SkeletonGraphic _SkeletonGraphic;
    //public bool AttackSuccees = false;//用于判断攻击是否有效，true才能触发
    //public BaseSpine(SkeletonGraphic _sk, Action start,Action end)
    //{
    //    _SkeletonGraphic = _sk;
    //    _start = start;
    //    _end = end;
    //    Init();
    //}


    void  Start()
    {
      Init();
    }



    public void Init()
    {
        _SkeletonGraphic =transform.Find("Spine").GetComponent<SkeletonGraphic>();
      //  _SkeletonUtility   = GetComponent<SkeletonUtility>();

        //_SkeletonUtilityHide = gameObject.GetOrAddComponent<SkeletonUtilityHide>();
        //_SkeletonUtilityHide.Init();
        _SkeletonGraphic.Skeleton.SetToSetupPose();

        _SkeletonGraphic.AnimationState.Event -= state_Event;
        _SkeletonGraphic.AnimationState.Start -= Start_Event;
        _SkeletonGraphic.AnimationState.End -= End_Event;
        _SkeletonGraphic.AnimationState.Complete -= Complete_Event;
        _SkeletonGraphic.AnimationState.Event += state_Event;
        _SkeletonGraphic.AnimationState.Start += Start_Event;
        _SkeletonGraphic.AnimationState.End += End_Event;
        _SkeletonGraphic.AnimationState.Complete += Complete_Event;
 
    }

   public virtual void Start_Event(TrackEntry trackEntry)
    {
        //trackEntry.Animation.Name
        // m_PlayerStateMgr.state_Event(trackEntry, null, E_spinestate.E_spinestate_start);
        if(_start != null)
        {
            _start();
            _start = null;
        }
    }

    public virtual void End_Event(TrackEntry trackEntry)
    {
        //m_PlayerStateMgr.state_Event(trackEntry, null, E_spinestate.E_spinestate_end);

        if (_end != null)
        {
            _end();
            _end = null;
        }

    }


    public virtual void Complete_Event(TrackEntry trackEntry)
    {
       // m_PlayerStateMgr.state_Event(trackEntry, null, E_spinestate.E_spinestate_complete);
    }


    public virtual void state_Event(TrackEntry trackEntry, Spine.Event e)
    {
        //Debug.Log(e.Data.Name);
        //Debug.Log("Play Note");

       // m_PlayerStateMgr.state_Event(trackEntry, e);
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animName"></param>
    /// <param name="loop"></param>
    /// <param name="speed"></param>
    public void PlayAnim(string animName, bool loop, float speed = 1.0f)
    {
        _SkeletonGraphic.AnimationState.SetAnimation(0, animName, loop);
        _SkeletonGraphic.timeScale = speed;
    }
    /// <summary>
    /// 换装
    /// </summary>
    /// <param name="skin"></param>
    public void setSkin( string skin )
    {
        _SkeletonGraphic.Skeleton.SetSkin( skin );
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{

        //    this.PlayAnim("saotui", true);
        //}


    }




    /// <summary>
    /// 玩家方向
    /// </summary>  
    public void setdir(bool dir)
    {
        _SkeletonGraphic.Skeleton.FlipX = dir;
    }
    /// <summary>
    /// 清除动画
    /// </summary>
    /// <param name="trackIndex"></param>
    public void ClearTrack(int trackIndex)
    {
        _SkeletonGraphic.AnimationState.ClearTrack(0);

    }
    public void ClearTracks()
    {
        _SkeletonGraphic.AnimationState.ClearTracks();

    }
    /// <summary>
    /// 设置混合 切动画
    /// </summary>
    /// <param name="fromName"></param>
    /// <param name="toName"></param>
    /// <param name="duration"></param>
    public void SetMix(string fromName, string toName, float duration)
    {
        _SkeletonGraphic.AnimationState.Data.SetMix(fromName, toName, duration);
    }
    /// <summary>
    /// 获取当前动画时间
    /// </summary>
    /// <returns></returns>
    public float GetAnimTime()
    {
        return _SkeletonGraphic.AnimationState.GetCurrent(0).AnimationTime;

        //return _SkeletonAnimation.skeleton.Time;
    }
 
    public float GetAnimationEnd()
    {
        return _SkeletonGraphic.AnimationState.GetCurrent(0).AnimationEnd;
    }

 


    







}
