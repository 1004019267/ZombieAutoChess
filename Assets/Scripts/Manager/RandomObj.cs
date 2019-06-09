using System.Collections.Generic;
using UnityEngine;


//
public class RandomObj
{
    private static RandomObj instance;
    public static RandomObj getMe()
    {
        if (instance == null)
        {
            instance = new RandomObj();
        }
        return instance;
    }
    //随机创建几个对象
    public List<int> RandintList(int _rand) {
        List<int> arr = new List<int>();
        for (int i = 0; i < _rand; i++)
        {
            arr.Add(Random.Range(0, _rand));
        }
        return arr;
    }
    /// <summary>
    /// 随机去一个数
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public int Randint(int min,int max)
    {
         return Random.Range(min, max);
         
    }





}
