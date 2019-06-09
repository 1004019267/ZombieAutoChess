using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void BulletWaveExit(ZombieController zc, ZombieController zc1, ZombieController zc2, SkillWaveBullet sb);
public class SkillWaveBullet : BaseBullet
{

    public BulletWaveExit waveExit;
    public ZombieController enemy1;
    public ZombieController enemy2;
    public SkillWaveBullet sb;
    public override void OnExit()
    {
        isMove = false;
        if (waveExit != null)
        {
            waveExit(myZc, enemy1, enemy2, sb);
            waveExit = null;
        }
    }

    public override void Update()
    {
        base.Update();
    }
}
