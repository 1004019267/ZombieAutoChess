using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void BulletSplashExit(ZombieController zc,ZombieController zc1,ZombieController zc2,SkillSplashBullet sb);
public class SkillSplashBullet : BaseBullet
{
    public BulletSplashExit splashExit;
    public ZombieController enemy1;
    public ZombieController enemy2;
    public SkillSplashBullet sb;
    public override void OnExit()
    {
        isMove = false;
        if (splashExit != null)
        {
            splashExit(myZc,enemy1,enemy2,sb);
            splashExit = null;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
