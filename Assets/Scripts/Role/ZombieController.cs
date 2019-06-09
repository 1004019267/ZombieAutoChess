using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

//僵尸类
public class ZombieController : MonoBehaviour
{
    public ZombieInfo zombie;//僵尸信息
   // public Player _player_ower;//拥有者
    public Player player;//拥有者
    public Button _Button;//身上btn
    public Transform target;//敌方目标
    public BaseSpine baseSpine;//spine
    public SkillManager skillManager;//技能
    bool isMove = true;
    List<Vector2> points; // 自动寻路的路径点
    float t;
    Vector3 pos;

    void Start()
    {
        skillManager = new SkillManager(this);
    }
    /// <summary>
    /// 寻路移动
    /// </summary>   
    void BattleMove()
    {
        if (points != null && points.Count > 1)
        {
            var startPos = points[points.Count - 1];
            transform.GetComponent<RectTransform>().localPosition = startPos;
            points.Remove(startPos);
        }
    }

    void Update()
    {
        // points = MapMgr.GetPoints(transform.GetComponent<RectTransform>().localPosition, new Vector3(36, 122, 0));


        // points = MapMgr.GetPoints(new Vector2(-84, -58), new Vector2(36, 122));

        //t += Time.deltaTime;
        //if (t >= zombie.moveSpeed)
        //{
        //    t = 0;
        //    BattleMove();
        //}
    }


    /// <summary>
    /// 设置目标
    /// </summary>
    /// <param name="zc"></param>
    public void SetTarget(ZombieController zc)
    {
        target = zc.transform;
    }
    /// <summary>
    /// 升级
    /// </summary>
    /// <param name="level"></param>
    public void LevelUp(int level)                                     
    {
        zombie.LevelUp(level);
        transform.GetComponent<BaseSpine>().setSkin("goblin");
    }
}

