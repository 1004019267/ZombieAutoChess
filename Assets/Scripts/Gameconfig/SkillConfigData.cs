using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class SkillConfigData:Singleton<SkillConfigData>
{
    List<SkillConfig> skillConfigDatas = new List<SkillConfig>();

    public void Init()
    {
        skillConfigDatas = GameConfigDataBase.GetConfigDatas<SkillConfig>();
    }
    /// <summary>
    /// 根据id获得一个卡的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SkillConfig GetSkill(int id)
    {
        for (int i = 0; i < skillConfigDatas.Count; i++)
        {
            if (skillConfigDatas[i].id == id)
            {
                return skillConfigDatas[i];
            }
        }
        return null;
    }

    public List<SkillConfig> GetAllSkill()
    {
        return skillConfigDatas;
    }
}

