using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//基类buff

public enum eBuffType
{
    Dizzy = 0,//眩晕
    Taunt,//嘲讽
    DamageReduction//减伤
}
/// <summary>
/// 单个buff
/// </summary>
[System.Serializable]
public class BuffDizzy : BaseBuff
{

    public override void Init(BuffManager in_BuffManager)
    {
        base.Init(in_BuffManager);
        buffType = eBuffType.Dizzy;    

    }
    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="t"></param>
    public override void SetTarget(ZombieController enemyZC,float t)
    {
        base.SetTarget(enemyZC,t);     
        target.zombie.SetDizzy(true);
    }

    public override void Exit()
    {
        base.Exit();
        target.zombie.SetDizzy(false);
    }

}


public class BuffTaunt : BaseBuff
{
    public override void Init(BuffManager in_BuffManager)
    {
        base.Init(in_BuffManager);
        buffType = eBuffType.Taunt;
    }
    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="t"></param>
    public override void SetTarget(ZombieController enemyZC,ZombieController user, float t)
    {
        base.SetTarget(enemyZC,user ,t);
        target.SetTarget(user);     
    }
    /// <summary>
    /// 属性结束
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        target.SetTarget(target.player._ZombieMgr.GetNearZombie(target, user.player));
    }

}

public class BuffDamageReduction : BaseBuff
{

    public override void Init(BuffManager in_BuffManager)
    {
        base.Init(in_BuffManager);
        buffType = eBuffType.DamageReduction;   
    }
    /// <summary>
    /// 设置属性
    /// </summary>
    /// <param name="t"></param>
    public override void SetTarget(ZombieController target,float t,float val)
    {
        base.SetTarget(target,t, val);
        lifeTime = t;
        target.zombie.SetDamageReduction(val);
        isBegin = true;
    }
    /// <summary>
    /// 属性结束
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        target.zombie.SetDamageReduction(1); 
    }

}

//怪物buff 
public class BuffManager :Singleton<BuffManager>
{
    public eBuffType buffChoose;
    List<BaseBuff> _curBuff = new List<BaseBuff>(); //当前buff
    public void init()
    {

    }
    public void shutdown()
    {

    }
    // Update is called once per frame
    public void Update()
    {  
        for (int i = 0; i < _curBuff.Count; i++)
        {
            _curBuff[i].Update();
        }
    }

 
    public BaseBuff createBuff(eBuffType type)
    {


        switch (type)
        {
            case eBuffType.Dizzy:
                {
                    BuffDizzy _Buff = new BuffDizzy();
                    _Buff.Init(this);
                    _curBuff.Add(_Buff);
                    return _Buff;

                }                
            case eBuffType.Taunt:
                {
                    BuffTaunt _Buff = new BuffTaunt();
                    _Buff.Init(this);
                    _curBuff.Add(_Buff);
                    return _Buff;
                }           
            case eBuffType.DamageReduction:
                {
                    BuffDamageReduction _Buff = new BuffDamageReduction();
                    _Buff.Init(this);
                    _curBuff.Add(_Buff);
                    return _Buff;
                }           
        }

        return null;




    }

    //
    public void testcreateBuff()
    {

        //BuffDizzy _BuffDizzy = (BuffDizzy)createBuff(eBuffType.Dizzy);
        //_BuffDizzy.setTarget(0);

    }



}
