using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;
 
public class LayersItem
{
    /// <summary>
    /// 
    /// </summary>
    public List<int> data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int height { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int opacity { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool visible { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int width { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int x { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int y { get; set; }
}

public class TilesetsItem
{
    /// <summary>
    /// 
    /// </summary>
    public int firstgid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string source { get; set; }
}

public class Root
{
    /// <summary>
    /// 
    /// </summary>
    public int height { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool infinite { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<LayersItem> layers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int nextlayerid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int nextobjectid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string orientation { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string renderorder { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tiledversion { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tileheight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<TilesetsItem> tilesets { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int tilewidth { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double version { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int width { get; set; }
}
public class JsonTest
{
    static JsonTest g_msglogin;
    public static JsonTest getMe()
    {
        if (JsonTest.g_msglogin == null)
        {
            JsonTest.g_msglogin = new JsonTest();
        }
        return JsonTest.g_msglogin;
    }
    public JsonTest()
    {
        initdata();
    }
    //public static int[,] GetJsonInfo()
    //{
    //    string Path = Application.dataPath + "/Resources/JSON/Map.json";//读取数据，
    //    string text = IOHelper.LoadJson(Path);
    //    Root r = JsonMapper.ToObject<Root>(text);//读取
    //    int[,] data = new int[r.height , r.width  ];
    //    int num = 0;
    //    for (int i = 0; i < r.height ; i++)
    //    {
    //        for (int j = 0; j < r.width; j++)
    //        {
    //            int tmp = r.layers[0].data[num];
    //            data[i, j] = tmp;
    //            num++;
    //        }
    //    }

    //    return data;
    //}


    public MapData m_mapData;
    public void initdata()
    {
        string Path = Application.streamingAssetsPath + "/JsonData/mapaxing.json";// Application.dataPath + "/Resources/JSON/mapaxing.json";//读取数据，
        string text = IOHelper.LoadJson(Path);
        m_mapData = JsonMapper.ToObject<MapData>(text);//读取




    }

    public int GetStopPoint(int x, int y)
    {
        foreach (var data in m_mapData.layers)
        {
            if (data.x == x && data.y == y)
            {
                return 1;//
            }
        }
        return 260;//阻挡
    }

    public void SetStopPoint(int x, int y,bool zudang)
    {

        if(zudang == true)
        {
            foreach (var data in m_mapData.layers)
            {
                if (data.x == x && data.y == y)
                {
                    return;//
                }
            }

            MapLayersData _mapLayersData = new MapLayersData();
            _mapLayersData.x = x;
            _mapLayersData.y = y;
            _mapLayersData.zudang = 0;
            m_mapData.layers.Add(_mapLayersData);
        }
        else
        {
            foreach (var data in m_mapData.layers)
            {
                if (data.x == x && data.y == y)
                {
                    m_mapData.layers.Remove(data);
                    return;//
                }
            }
        }
  

    }



    


}


