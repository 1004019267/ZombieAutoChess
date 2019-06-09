using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//技能管理 挂到僵尸类ZombieController

public class SkillManager
{
    ZombieController _contorl;
    SkillConfig skillData;//当前主动技能缓存
    bool isOK=true;
    bool isStart;//技能是否启动
   
    public SkillManager(ZombieController zc)
    {
        _contorl = zc;
       // skillData = SkillConfigData.Instance.GetSkill((int)zc.zombie.skill);
       
    }

    Random r = new Random();
    /// <summary>
    /// 当前卡牌伤害数据缓存
    /// </summary>
    public void TriggerActiveSkill(ZombieController zc)
    {
        if (r.Next(0, 100) <= skillData.probability)
        {
            switch (zc.zombie.skill)
            {
                case eSkillType.Splash:
                    Splash(zc);
                    break;
                case eSkillType.TauntAll:
                    TauntAll(zc);
                    break;
                case eSkillType.ZombieWave:
                    ZombieWave(zc);
                    break;              
            }
            isOK = false;
        }
    }


    /// <summary>
    /// 僵尸冲击波
    /// </summary>
    /// <param name="zc"></param>
    private void ZombieWave(ZombieController zc)
    {
        var enemy = zc.player._ZombieMgr.GetNearZombie(zc, zc.target.GetComponent<ZombieController>().player, 10);
        var enemySecond = zc.player._ZombieMgr.GetNearZombie(enemy, enemy.player, 10);
        var enemyThird = zc.player._ZombieMgr.GetNearZombie(enemySecond, enemySecond.player,10);

        while (enemy == enemyThird)
        {
            enemyThird = zc.player._ZombieMgr.GetNearZombie(enemySecond, enemySecond.player, 1);
        }
        var go = BulletMgr.Instance.CreateBullet(eBulletType.SkillSplashBullet).GetComponent<SkillSplashBullet>();
    }

    /// <summary>
    /// 全体嘲讽
    /// </summary>    
    private void TauntAll(ZombieController zc)
    {
        var enemy = zc.target.GetComponent<ZombieController>();
        var enemys = enemy.player._ZombieMgr.GetAllZombieController();
        foreach (var item in enemys)
        {
            BuffTaunt taunt = (BuffTaunt)BuffManager.Instance.createBuff(eBuffType.Dizzy);
            taunt.SetTarget(item.Value, zc, skillData.duration);
        }
    }
    /// <summary>
    /// 溅射 不包含普工
    /// </summary>
    /// <param name="zc"></param>    
    void Splash(ZombieController zc)
    {
        var enemy = zc.target.GetComponent<ZombieController>();
        var enemySecond = zc.player._ZombieMgr.GetNearZombie(enemy, enemy.player, 1);
        var enemyThird = zc.player._ZombieMgr.GetNearZombie(enemySecond, enemySecond.player, 1);
        while (enemy == enemyThird)
        {
            enemyThird = zc.player._ZombieMgr.GetNearZombie(enemySecond, enemySecond.player, 1);
        }

        var go = BulletMgr.Instance.CreateBullet(eBulletType.SkillSplashBullet).GetComponent<SkillSplashBullet>();
        go.Init(zc.transform, enemySecond.transform);
        go.myZc = zc;
        go.enemy1 = enemySecond;
        go.enemy2 = enemyThird;
        go.sb = go;
        go.splashExit = SecondSplash;
    }
    //一段
    void SecondSplash(ZombieController zc, ZombieController enemy1, ZombieController enemy2, SkillSplashBullet sb)
    {
        enemy1.zombie.Hurt(zc.zombie.atk * skillData.Percentage1L1);
        sb.Init(zc.transform, enemy2.transform);
        sb.splashExit = ThirdSplash;
        //logMgr.log(2);
    }
    //二段
    void ThirdSplash(ZombieController zc, ZombieController enemy1, ZombieController enemy2, SkillSplashBullet sb)
    {
        enemy2.zombie.Hurt(zc.zombie.atk * skillData.Percentage1L1);
    }


    //通过当前技能获取冷却时间和其他信息
    float getSkillTime(eSkillType _type)
    {
        float time = 0;




        return time;
    }

    void test(string jinengming, Action start, Action end, float time)
    {
        //basespine _spine = new basespine(_contorl.GetComponent<SkeletonUtilityHide>(),start, end);
        //_spine.PlayAnim();
    }


    private float m_time = 0;
    private bool isstart = false;


    public void Loop(float t)
    {
        if (isStart)
        {
            m_time += t;        
            if (m_time >= skillData.cd)
            {
                m_time = 0;
                isOK = true;
            }
        }
    }

}

