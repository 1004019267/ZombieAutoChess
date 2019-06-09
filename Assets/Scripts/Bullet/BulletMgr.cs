using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBulletType
{
    NormalBullet = 0,   //普通子弹
    SkillSplashBullet,  //技能散射子弹
    SkillWaveBullet,    //技能冲击波
}


//子弹管理类
public class BulletMgr : Singleton<BulletMgr>
{
    public GameObject CreateBullet(eBulletType type)
    {
        switch (type)
        {
            case eBulletType.NormalBullet:
                return Instantiate(BulletResource.Instance.GetBullet(eBulletType.NormalBullet.ToString()));
            case eBulletType.SkillSplashBullet:
                return Instantiate(BulletResource.Instance.GetBullet(eBulletType.SkillSplashBullet.ToString()));
            case eBulletType.SkillWaveBullet:
                return Instantiate(BulletResource.Instance.GetBullet(eBulletType.SkillWaveBullet.ToString()));
        }
        return null;
    }

 

}
