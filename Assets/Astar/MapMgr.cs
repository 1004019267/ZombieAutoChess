using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapMgr
{
    public static int width = 5;
    public static int height = 10;
    private TILEDMAP_TYPE[][] mRoadPointArr;
    private Dictionary<string, APoint[]> mObjectPointMap;

    private static MapMgr _instance = null;
    public static MapMgr getMe()
    {
        if (_instance == null)
        {
            _instance = new MapMgr();
        }
        return _instance;

    }

    /// <summary>
    /// 设置阻挡 zudang： true 是阻挡 false 可以行走  
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="isStop"></param>
    public void SetStopPoint(Vector2 pos, bool isStop)
    {
        APoint _APoint = GetAPointByPosition(pos);
        JsonTest.getMe().SetStopPoint(_APoint.x, _APoint.x, isStop);
    }

    public static List<Vector2> GetPoints(Vector2 start, Vector2 end)
    {



        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                int id = JsonTest.getMe().GetStopPoint(i, j); // [i, j];

                MapMgr.getMe().mRoadPointArr[i][j] = MapMgr.canThroughByID(id);
            }
        }


        List<APoint> _ret = Astar.SearchRoad(GetAPointByPosition(start, TILEDMAP_TYPE.START), GetAPointByPosition(end, TILEDMAP_TYPE.END), MapMgr.getMe().mRoadPointArr);


        List<Vector2> retex = new List<Vector2>();

        foreach (var data in _ret)
        {
            Vector2 pos = GetVec2ByApoint(data);
            retex.Add(pos);
        }

        return retex;
    }
    //重置地图

    public void reset()
    {
        init();
    }
    public void init()
    {

        mRoadPointArr = new TILEDMAP_TYPE[width][];
        for (int i = 0; i < width; ++i)
        {
            this.mRoadPointArr[i] = new TILEDMAP_TYPE[height];
        }


        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                int id = JsonTest.getMe().GetStopPoint(i, j); // [i, j];

                this.mRoadPointArr[i][j] = MapMgr.canThroughByID(id);

            }
        }


    }



    public void shutdown()
    {
        this.mRoadPointArr = null;
        _instance = null;
    }



    public static TILEDMAP_TYPE canThroughByID(int id)
    {
        if (id != 260)
        {
            return TILEDMAP_TYPE.SPACE;
        }
        return TILEDMAP_TYPE.WALL;
    }






    // 根据地图坐标点获得tiledmap坐标
    public static APoint GetAPointByPosition(Vector2 pos, TILEDMAP_TYPE type = TILEDMAP_TYPE.START)
    {

        float x = (pos.x + 204) / JsonTest.getMe().m_mapData.getgeziw();
        float y = (pos.y + 118) / JsonTest.getMe().m_mapData.getgezih();

        if (x < 0)
        {
            x = 0;
        }
        if (y < 0)
        {
            y = 0;
        }

        return new APoint((int)x, (int)y, type);


    }

    // 根据tiledmap坐标点获得地图坐标
    public static Vector2 GetVec2ByApoint(APoint p)
    {

        float x = p.x * JsonTest.getMe().m_mapData.getgeziw() - 204  ;
        float y = (p.y) * JsonTest.getMe().m_mapData.getgezih() - 118  ;
        return new Vector2(x, y);

    }
}
