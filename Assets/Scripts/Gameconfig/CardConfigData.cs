using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//技能数据
public class CardConfigData : Singleton<CardConfigData>
{
    List<CardConfig> cardConfigDatas;//技能数据   
                                                              
    public void Init()
    {
        cardConfigDatas = GameConfigDataBase.GetConfigDatas<CardConfig>();
    }
    /// <summary>
    /// 根据id获得一个技能的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CardConfig GetCard(int id)
    {
        for (int i = 0; i < cardConfigDatas.Count; i++)
        {
            if (cardConfigDatas[i].id == id)
            {
                return cardConfigDatas[i];
            }
        }
        return null;
    }
   
    public List<CardConfig> GetAllCard()
    {
        return cardConfigDatas;
    }
}
