using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基类buff
public class BaseBuff 
{
    public eBuffType buffType;
    public ZombieController target;
    public ZombieController user;
    public BuffManager _BuffManager;
    // Use this for initialization
    public float lifeTime;
    public bool isBegin = false;

    public virtual void SetTarget(ZombieController target, float t)
    {
        isBegin = true;
        lifeTime = t;
        this.target = target;      
    }
    public virtual void SetTarget(ZombieController target, ZombieController user, float t)
    {
        isBegin = true;
        lifeTime = t;
        this.target = target;
        this.user = user;
    }

    public virtual void SetTarget(ZombieController target, float t, float val)
    {
        lifeTime = t;
        this.target = target;
        isBegin = true;
    }
    public virtual void Init(BuffManager in_BuffManager)
    {
        _BuffManager = in_BuffManager;
    }

    public virtual void Update()
    {
        if (isBegin == false)
            return;

        if (isBegin)
            lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Exit();
        }
    }

    /// <summary>
    /// 属性结束
    /// </summary>
    public virtual void Exit()
    {
        isBegin = false;
    }

}
