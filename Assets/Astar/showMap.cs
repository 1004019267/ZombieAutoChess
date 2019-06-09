using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLayersData
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// mapLayersData
    /// 
    public MapLayersData()
    {
        zudang = 0;
    }
    public int x { get; set; }
    public int y { get; set; }
    public int zudang { get; set; }  
}


public class MapData
{
    /// <summary>
    /// 
    /// </summary>
    public string des { get; set; }
    public string gezih { get; set; }
    public string geziw { get; set; }


    public float getgezih()
    { return (float) Convert.ToDouble(gezih);  }

    public float getgeziw()
    { return (float)Convert.ToDouble(geziw); }


    public int gezihnum { get; set; }
    public int geziwnum { get; set; }

    public List<MapLayersData> layers { get; set; }
}

public class showMap : MonoBehaviour
{
    void Start()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/" + "CubeUi");
        //读取json
        MapData _mapData = GetJsonInfo();
        GameObject _goout;
 
        float geziw = (float) Convert.ToDouble(m_mapData.geziw);
        float gezih = (float)Convert.ToDouble(m_mapData.gezih);
       
            for ( int y = 0; y < _mapData.gezihnum; ++y )
            for (int i = 0; i < _mapData.geziwnum; ++i)
            {
                _goout = UnityEngine.Object.Instantiate(go,this.transform);
                _goout.name = "" + y + "_" + i;
               
                // -204 - 118;
                _goout.transform.position =  new Vector3(  geziw / 2f + i * geziw , gezih / 2f + gezih * y, 0);


               // _goout.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(geziw / 2f + i * geziw, gezih / 2f + gezih * y, 0));

               // _goout.transform.SetParent(this.transform); 



        if (getzudang(y,i) == 0)
                {
                    _goout.SetActive(false);
                }
        }
    }

    MapData m_mapData;
     public   MapData GetJsonInfo()
    {
        string Path = Application.streamingAssetsPath + "/JsonData/mapaxing.json"; ;// Application.dataPath + "/Resources/JSON/mapaxing.json";//读取数据，
        string text = IOHelper.LoadJson(Path);
        m_mapData   = JsonMapper.ToObject<MapData>(text);//读取
        return m_mapData;
    }


    //1 是阻挡 0 不是
    public  int getzudang( int x,int y )
    {
        foreach(  var data in m_mapData.layers )
        {
            if(data.x == x && data.y == y)
            {
                return data.zudang;
            }
        }
        return 1;
    }



    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) //存档地图
        //{
        //    Save();
        //}

        //if (Input.GetKeyDown(KeyCode.T)) //存档地图
        //{
        //    List<Vector2> _ret = MapMgr.GetPoints(new Vector2(-84, -58), new Vector2(36, 122));
        //}

      

    }


    void Save()
    {
        m_mapData.layers.Clear();
        for (int y = 0; y < m_mapData.gezihnum; ++y)
            for (int i = 0; i < m_mapData.geziwnum; ++i)
      
        {
           string name = "" + y + "_" + i;
           Transform _Transform=   this.transform.Find(""+y+"_"+i);

            if(_Transform.gameObject.activeInHierarchy == false)
            {
                List<string> list = new List<string>(name.Split('_'));//或
                MapLayersData _mapLayersData = new MapLayersData();
                _mapLayersData.x = Convert.ToInt32(list[0]) ;
                _mapLayersData.y = Convert.ToInt32(list[1]);
                _mapLayersData.zudang = 0;
                m_mapData.layers.Add(_mapLayersData);
            }
        }
        string Path = Application.streamingAssetsPath + "/JsonData/mapaxing.json";  //Application.dataPath + "/Resources/JSON/mapaxing.json";//读取数据，
        IOHelper.SetData(Path, m_mapData);
        logMgr.log("A*地图保存完成");
    }



}

