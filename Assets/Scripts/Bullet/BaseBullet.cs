using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void BulletExit(ZombieController zc);

//子弹类
public class BaseBullet : MonoBehaviour
{
    public float speed = 0.01f;
    public float hight = 100;
    public bool isMove = false;
    float       t;
    List<Vector3> path;

    // Vector3 _EndPos;

    public int m_npcId = -1;
    public int m_playerId = -1;
    /// <summary>
    /// 贝赛尔曲线初始化
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    public virtual void Init(Transform startPoint, Transform endPoint)
    {
        // _EndPos = endPoint.position;
        var controlPoint = BezierUtils.GetControlPoint(startPoint.position, endPoint.position, hight);
        path = BezierUtils.GetBeizerList(startPoint.position, controlPoint, endPoint.position);
        isMove = true;
    }

    /// <summary>
    /// 寻路模拟
    /// </summary>
    /// <param name="path"></param>
    public virtual void FindPath(List<Vector3> path)
    {
        t += Time.deltaTime;
        if (t >= speed)
        {
            t = 0;
            if (path != null && path.Count > 0)
            {
                var startPos = path[0];
                transform.position = startPos;
                path.Remove(startPos);
                if (path.Count == 0)
                {
                    OnExit();
                }
            }
        }
    }

    public BulletExit func;
    public ZombieController myZc;
    public virtual void OnExit()
    {
        isMove = false;
        if (func != null)
        {
            func(myZc);
            func = null;
        }

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isMove == true)
        {
            FindPath(path);
        }
    }
}
