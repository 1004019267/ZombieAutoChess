using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Intercept : Singleton<Intercept>
{
    /// <summary>
    /// 从购买界面btn名字截取id
    /// </summary>
    public int GetIdForBuyName(string name)
    {
        string[] strArray = name.Split('_');
        if (strArray.Length < 2)
        {
            logMgr.log("格式不对");
            return -1;
        }

        return int.Parse(strArray[1]);
    }
    /// <summary>
    /// 从背包界面btn名字获取CardId
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetCardIdForBagName(string name)
    {
        string[] strArray = name.Split('_');
        if (strArray.Length < 2)
        {
            logMgr.log("格式不对");
            return -1;
        }

        return int.Parse(strArray[1]);
    }
    /// <summary>
    /// 从背包界面btn名字获取Id
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetIdForBagName(string name)
    {
        string[] strArray = name.Split('_');
        if (strArray.Length < 2)
        {
            logMgr.log("格式不对");
            return -1;
        }

        return int.Parse(strArray[1]);
    }
    //public string GetIdForSprite(string name)
    //{
    //    var str = name.Substring(name.Length - 3, 3);
    //    return str;
    //}
    /// <summary>
    /// 从地板名字获得二维数组中x,y
    /// </summary>
    /// <param name="name"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int GetPosForGroundName(string name,out int y)
    {
        string[] strArray = name.Split('_');
        if (strArray.Length < 2)
        {
            logMgr.log("格式不对");
            y = -1;
            return -1;
        }
        y = int.Parse(strArray[1]);
        return int.Parse(strArray[0]);
    }
    /// <summary>
    /// 从僵尸名字获得二维数组中x,y的string
    /// </summary>
    /// <returns></returns>
    //public string GetPosForCardName(string name)
    //{
    //    return name.Substring(7);
    //}
}

