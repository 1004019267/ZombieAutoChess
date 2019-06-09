using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XyData
{
    public int x;
    public int y;

    public XyData(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Astar
{
    public static TILEDMAP_TYPE[][] mRoadPointArr;
    public static APoint mStartPos;//开始点
    public static APoint mEndPos;//结束点
    //寻路
    public static List<APoint> SearchRoad(APoint startPos, APoint endPos, TILEDMAP_TYPE[][] roadMap)
    {
        Astar.mStartPos = null;
        Astar.mEndPos = null;
        Astar.mRoadPointArr = null;
        Astar.mStartPos = new APoint(startPos);
        Astar.mEndPos = new APoint(endPos);

        



        Astar.mRoadPointArr = roadMap;
        List<APoint> _ret = Astar.Search();
        List<APoint> _ret_new = new List<APoint>();
        _ret_new.Add(endPos);
        foreach ( var data in _ret)
        {
            _ret_new.Add( data );
        }

         

        return _ret_new;
    }

    /**
     * 打印地图
     */
    public static void PrintMap()
    {
        for (int i = 0; i < MapMgr.width; i++) {
            for (int j = 0; j < MapMgr.height; j++)
            {
                if (mStartPos.equals(new APoint(i, j)))
                {
                    Astar.mRoadPointArr[i][j] = TILEDMAP_TYPE.START;
                }
                if (mEndPos.equals(new APoint(i, j)))
                {
                    Astar.mRoadPointArr[i][j] = TILEDMAP_TYPE.END;
                }
            }
        }

        for (int j = 0; j < MapMgr.height; j++) {
            for (int i = 0; i < MapMgr.width; i++)
            {
                if(Astar.mRoadPointArr[i][j] == TILEDMAP_TYPE.END) Console.Write("E");
                if (Astar.mRoadPointArr[i][j] == TILEDMAP_TYPE.ON_PATH) Console.Write("-");
                if (Astar.mRoadPointArr[i][j] == TILEDMAP_TYPE.SPACE) Console.Write(" ");
                if (Astar.mRoadPointArr[i][j] == TILEDMAP_TYPE.START) Console.Write("S");
                if (Astar.mRoadPointArr[i][j] == TILEDMAP_TYPE.VISITED) Console.Write(" ");
                if (Astar.mRoadPointArr[i][j] == TILEDMAP_TYPE.WALL) Console.Write("W");
            }
            //Debug.Log(Astar.mRoadPointArr[j] + "");
        Console.WriteLine();
        }
    }

    internal static List<APoint> SearchRoad(Vector3 position, Vector2 dest, object mRoadPointArr)
    {
        throw new NotImplementedException();
    }

    internal static List<APoint> SearchRoad(Vector3 position, Vector2 dest)
    {
        throw new NotImplementedException();
    }

    /**
     * 搜索算法
     */
    public static List<APoint> Search()
    {
        MinHeap heap = new MinHeap(); // 用最小堆来记录扩展的点
        XyData[] directs = new XyData[4]; // 可以扩展的四个方向
        //[{ x: 1, y: 0}, { x: 0, y: 1}, { x: -1, y: 0}, { x: 0, y: -1}];
        directs[0] = new XyData(1, 0);
        directs[1] = new XyData(0, 1);
        directs[2] = new XyData(-1, 0);
        directs[3] = new XyData(0, -1);

        heap.add(new PointData(Astar.mStartPos, 0, 0, null)); // 把起始点放入堆
        PointData lastData = null; // 找到的最后一个点的数据,用来反推路径

        for (bool finish = false; !finish && !heap.isEmpty();)
        {
            PointData data = heap.getAndRemoveMin(); // 取出f值最小的点
            APoint point = data.point;


            if( MapMgr.width  <= point.x)
            {
                continue;
            }

            if (Astar.mRoadPointArr[point.x] == null)
            {
                continue;
            }

            if (Astar.mRoadPointArr[point.x][point.y] == TILEDMAP_TYPE.SPACE) // 将取出的点标识为已访问点
            {
                Astar.mRoadPointArr[point.x][point.y] = TILEDMAP_TYPE.VISITED;
            }

            for (int i = 0; i < directs.Length; ++i) // 遍历四个方向的点
            {
                APoint newPnt = new APoint(point.x + directs[i].x, point.y + directs[i].y);
                if (newPnt.x >= 0 && newPnt.x < MapMgr.width && newPnt.y >= 0
                    && newPnt.y < MapMgr.height)
                {
                    TILEDMAP_TYPE e = Astar.mRoadPointArr[newPnt.x][newPnt.y];
                    if (Astar.mEndPos.equals(newPnt)) // 如果是终点,则跳出循环,不用再找
                    {
                        lastData = data;
                        finish = true;
                        break;
                    }
                    if (e != TILEDMAP_TYPE.SPACE) // 如果不是空地,就不需要再扩展
                    {
                        continue;
                    }

                    PointData inQueueData = heap.find(newPnt);
                    if (inQueueData != null) // 如果在堆里,则更新g值
                    {
                        if (inQueueData.g > data.g + 1)
                        {
                            inQueueData.g = data.g + 1;
                            inQueueData.parent = data;
                        }
                    }
                    else // 如果不在堆里,则放入堆中
                    {
                        float h = Astar.h(newPnt);
                        PointData newData = new PointData(newPnt, data.g + 1, h, data);
                        heap.add(newData);
                    }
                }
            }
        }

        List<APoint> arr = new List<APoint>();
        // 反向找出路径
        for (PointData pathData = lastData; pathData != null;)
        {
            APoint pnt = pathData.point;
            if (Astar.mRoadPointArr[pnt.x][pnt.y] == TILEDMAP_TYPE.VISITED)
            {
                Astar.mRoadPointArr[pnt.x][pnt.y] = TILEDMAP_TYPE.ON_PATH;
                arr.Add(pnt);
            }
            pathData = pathData.parent;
        }
        return arr;
    }

    /**
     * h函数
     */
    static float h(APoint pnt)
    {
        // return hBFS(pnt);
        return Astar.hEuclidianDistance(pnt);
        // return hPowEuclidianDistance(pnt);
        // return hManhattanDistance(pnt);
    }


 
 

    /**
     * 曼哈顿距离,小于等于实际值
     */
    static float hManhattanDistance(APoint pnt)
    {
        return Math.Abs(pnt.x - Astar.mEndPos.x) + Math.Abs(pnt.y - Astar.mEndPos.y);
    }

    /**
     * 欧式距离,小于等于实际值
     */
    static float hEuclidianDistance(APoint pnt)
    {
        return (float)(Math.Sqrt(Math.Pow(pnt.x - Astar.mEndPos.x, 2)
            + Math.Pow(pnt.y - Astar.mEndPos.y, 2)));
    }

    /**
     * 欧式距离平方,大于等于实际值
     */
    static float hPowEuclidianDistance(APoint pnt)
    {
        return (float)(Math.Pow(pnt.x - Astar.mEndPos.x, 2) + Math.Pow(pnt.y - Astar.mEndPos.y, 2));
    }

    /**
     * BFS的h值,恒为0
     */
    // hBFS(pnt) {
    //     return 0;
    // }

}
