using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class Pos
{
    public int x;
    public int y;

    public static float Distance(Pos p1, Pos p2)
    {
        return Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2);
    }
}

//僵尸信息
public class ZombieInfo
{
    public int id;//唯一ID
    public int cardId;//SCV表里ID
    public ZombieController zc;
    public string cardName;
    public string profile;
    public int gold;
    public float maxHp;
    public float hp;
    public int atk;
    public int level;
    public float levelCoefficient = 2;//升级系数
    public float atkSpeed;
    public int atkRange;
    public Pos pos = new Pos();//准备阶段格子x y
    public Pos battlePos = new Pos();//战斗移动坐标
    public float moveSpeed = 0.5f;
    public float strengthValue = 3;//体力值 一轮所能移动的格子数
    public eSkillType skill;//技能
    bool isDizzy=false;//是否眩晕
    float DamageReduction=1;//减伤系数
    /// <summary>
    /// 朝向 true 右 rigth 左
    /// </summary>
    public bool face;
    /// <summary>
    /// 临时坐标
    /// </summary>
    int y;
    public ZombieInfo()
    {
        this.hp = maxHp;
        battlePos = pos;
    }

    public void SetZombieController(ZombieController zc)
    {
        this.zc = zc;
    }
    /// <summary>
    /// 战前准备坐标设置
    /// </summary>
    /// <param name="ground"></param>
    public void SetPos(Button ground)
    {
        pos.x = Intercept.getMe().GetPosForGroundName(ground.name, out y);
        pos.y = y;
        battlePos = pos;
    }
    /// <summary>
    /// 战争中设置坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetBattlePos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }
    /// <summary>
    /// 升级属性
    /// </summary>
    /// <param name="level"></param>
    public void LevelUp(int level)
    {
        level = level + level;
        maxHp = maxHp * Mathf.Pow(levelCoefficient, level);
        hp = hp * Mathf.Pow(levelCoefficient, level);
        atk = (int)(atk * Mathf.Pow(levelCoefficient, level));
    }
    /// <summary>
    /// 受伤
    /// </summary>
    public void Hurt(float val)
    {
        hp -= val*DamageReduction;
        hp = hp < 0 ? 0 : hp;
    }
    /// <summary>
    /// 治疗
    /// </summary>
    public void Treatment(float val)
    {
        hp += val;
        hp = hp >= maxHp ? maxHp : hp;
    }
    /// <summary>
    /// 设置减防系数
    /// </summary>
    /// <param name="t"></param>
    public void SetDamageReduction(float t)
    {
        DamageReduction = t;
    }
    /// <summary>
    /// 设置是否眩晕
    /// </summary>
    /// <param name="isDizzy"></param>
    public void SetDizzy(bool isDizzy)
    {
        this.isDizzy = isDizzy;
    }
}
