using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 二次贝塞尔函数工具类
/// </summary>
public class BezierUtils 
{
    /// <summary>
    /// 根据T值，计算贝赛尔曲线上面对应的点
    /// </summary>
    /// <param name="t"></param>T值
    /// <param name="p0"></param>起始点
    /// <param name="p1"></param>控制点
    /// <param name="p2"></param>目标点
    /// <returns></returns>根据T值计算出来的贝赛尔曲线
    /// (1-t)2P0+2(1-t)tP1+t2P2
    static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;//(1-t)2P0
        p += 2 * u * t * p1;//2(1-t)tP1
        p += tt * p2;//t2P2

        return p;
    }
    /// <summary>
    /// 获取一个贝赛尔曲线点的数组
    /// </summary>
    /// <param name="startPoint"></param>起始点
    /// <param name="contiolPoint"></param>控制点
    /// <param name="endPoint"></param>目标点
    /// <param name="segmentNum"></param>采样点的数量 精度
    /// <returns></returns>一个贝赛尔曲线的数组
    public static List<Vector3> GetBeizerList(Vector3 startPoint, Vector3 contiolPoint, Vector3 endPoint)
    {
        //计算所合适的区间数 优化远近差值速度 根据自身情况找规律设置 我的是按2D屏幕坐标左到右 从上到下
        var segmentNum =15+ (int)(Mathf.Abs(endPoint.x - startPoint.x) * 0.02f) +(int)(Mathf.Abs(endPoint.y - startPoint.y) * 0.02f);


        List<Vector3> path = new List<Vector3>();
       // Vector3[] path = new Vector3[segmentNum];
        //若长度为5 采样点为 1/5 2/5 3/5 4/5 5/5k
        for (int i = 1; i <= segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = CalculateCubicBezierPoint(t, startPoint, contiolPoint, endPoint);
            path.Add(pixel);           
           // Debug.Log(path[i - 1]);
        }
        return path;
    }

    /// <summary>
    /// 获得他们中心点向上一定距离的点 只适用2D
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <returns></returns>
   public static Vector3 GetControlPoint(Vector3 startPoint, Vector3 endPoint, float dValue)
    {
        Vector3 p1 = startPoint;
        Vector3 p2 = endPoint;
        //向量p1p2
        Vector3 p1p2 = (p2 - p1);
        Debug.Log(p1p2);
        //求中心开始向量和结束向量的一半向量
        Vector3 halfP1P2 = p1 + p1p2 * 0.5f;    
        //求他们的旋转向量的方法 把向量p2p1以Z轴旋转90度 如果end在start左边就p1p2为负数改变方向
        Vector3 pVertical = Quaternion.AngleAxis(90, Vector3.forward*p1p2.normalized.x) * p1p2;
        //那么中心点坐标为从一半开始的向量 加上pVertical的方向乘上长度
        Vector3 controlPoint = halfP1P2 + pVertical.normalized * dValue;
        return controlPoint;
    }
}
