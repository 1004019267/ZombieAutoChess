using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//
class BattleManager : Singleton<BattleManager>
{
    public Player leftPlayer; //左边的是玩家操作
    public AiPlayer rightPlayer; // 右边的是怪物
    /// <summary>
    /// 确保唯一每个怪物ID
    /// </summary>
    int type = 1;
    public float maxBagZombie = 8;//背包最大僵尸数
    public float maxBattleZombie = 6;//场上最大僵尸数
    
    public int GetId()
    {
        type++;
        return type;
    }
}

