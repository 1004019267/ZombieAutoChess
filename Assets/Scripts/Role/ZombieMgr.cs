using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class MyZombie
{
    public ZombieInfo zombie;
    public int count;
}
//不是单例
public class ZombieMgr
{
    public Player player;

    Sprite nowSprite;
    public ZombieMgr(Player _Player_in)
    {
        player = _Player_in;
        nowSprite = SpriteManager.Instance.GetSprite("tr");
    }

    /// <summary>
    /// 开始场景上的创建好玩家的怪物
    /// </summary>
    /// <returns></returns>   
    Dictionary<int, ZombieController> myCards_ZC = new Dictionary<int, ZombieController>();
    /// <summary>
    /// 死亡的僵尸
    /// </summary>
    Dictionary<int, ZombieController> myCardsDead_ZC = new Dictionary<int, ZombieController>();
    /// <summary>
    /// 背包僵尸
    /// </summary>
    List<MyZombie> bagZombies = new List<MyZombie>();

    int y;//坐标缓存

  
    /// <summary>
    /// 为战场上创建一个卡牌
    /// </summary>
    public void CreatCard(Button battleBtn, string bagName, int level)
    {
        if (myCards_ZC.Count==BattleManager.Instance.maxBattleZombie)
        {
            TipsManager.Instance.TipsShow("场上怪物已满无法创建");
        }
        var zombie = player._ZombieMgr.GetZombie(Intercept.Instance.GetIdForBagName(bagName)).zombie;
        var card = GameObject.Instantiate(ZombieResource.Instance.GetZombie(zombie.cardId, zombie.level));
        zombie.id = BattleManager.Instance.GetId();
        var cardBtn = card.transform.Find("Button").GetComponent<Button>();
        
        card.transform.SetParent(BattleWndUIController.getMe().leftCard, false);
        card.transform.position = battleBtn.transform.position;
        card.name = "zombie" + "_" + zombie.id + "_" + level;

        var zombieControl = card.gameObject.GetComponent<ZombieController>();
        zombieControl._Button = cardBtn;
        zombieControl._Button.name = "Button" +"_" +card.name;
        zombieControl.zombie = zombie;
        zombieControl.zombie.zc = zombieControl;
        zombieControl.player = player;
        var a = Intercept.getMe().GetPosForGroundName(battleBtn.gameObject.name, out y);
        //zombieControl.zombie.pos.x = Intercept.getMe().GetPosForGroundName(battleBtn.gameObject.name, out y);
        //zombieControl.zombie.pos.y = y;
        zombieControl.zombie.SetPos(battleBtn);
        if (BattleManager.getMe().leftPlayer.camp == eCamp.Left)
        {
            zombieControl.zombie.face = true;
        }
        else
        {
            zombieControl.zombie.face = false;
        }

        BattleWndUIController.getMe().GetBasekeyState().registerCallBack(cardBtn, ZombieCardBtnOnClick);// m_key.registerCallBack(cardBtn, ZombieCardBtnOnClick);
        BattleWndUIController.getMe().GetBasekeyState().setCurKey(cardBtn); //m_key.setCurKey(cardBtn);
        BattleWndUIController.getMe().GetBasekeyState().RegisterOnKeyChangedEvent(cardBtn,ZombieCardBtnOnEnter, ZombieCardBtnOnExit);
        cardBtn.image.sprite = cardBtn.spriteState.highlightedSprite;

        myCards_ZC.Add(zombie.id, zombieControl);
        RemoveZombie(zombie,1);
        battleBtn.interactable = false;
        battleBtn.image.material = null;
    }

    private void ZombieCardBtnOnEnter(Button btn)
    {       
        btn.image.sprite = btn.spriteState.highlightedSprite;
    }

    private void ZombieCardBtnOnExit(Button btn)
    {
        btn.image.sprite = nowSprite;
    }

    /// <summary>
    /// 注册僵尸卡
    /// </summary>
    /// <param name="btn"></param>
    private void ZombieCardBtnOnClick(Button btn)
    {
        if (uiMgr.getMe().FindUI(MenuWndUIController.ui_name) == null)
        {
            MenuWndUIController.ShowOrHide(true);
        }
        else
        {
            MenuWndUIController.getMe().gameObject.SetActive(true);
            MenuWndUIController.getMe().Notify();
            uiMgr.getMe().setTopUI(MenuWndUIController.ui_name);
        }

        MenuWndUIController.getMe().transform.position = btn.transform.position;
        MenuWndUIController.getMe().id = Intercept.Instance.GetIdForBagName(btn.transform.parent.name);
        MenuWndUIController.getMe().zombie = btn.transform.parent.GetComponent<ZombieController>().zombie;
    }

    /// <summary>
    /// 获取背包僵尸
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MyZombie GetZombie(int cardId)
    {
        for (int i = 0; i < bagZombies.Count; i++)
        {
            if (cardId == bagZombies[i].zombie.cardId)
            {
                return bagZombies[i];
            }
        }
        return null;
    }

    public MyZombie GetZombie(ZombieInfo zombie)
    {
        for (int i = 0; i < bagZombies.Count; i++)
        {
            if (zombie.cardId== bagZombies[i].zombie.cardId)
            {
                return bagZombies[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 添加背包指定数量僵尸
    /// </summary>
    /// <param name="card"></param>
    public bool AddZombie(ZombieInfo card,int count)
    {      
        for (int i = 0; i < bagZombies.Count; i++)
        {
            if (bagZombies[i].zombie.cardId==card.cardId)
            {
                bagZombies[i].count+=count;
                return true;
            }
        }
        MyZombie zombie = new MyZombie();
        zombie.zombie = card;
        zombie.count = 1;
        bagZombies.Add(zombie);
        return false;
    }
    /// <summary>
    /// 移除背包指定数量僵尸
    /// </summary>
    /// <param name="card"></param>
    /// <param name="count"></param>
    public bool RemoveZombie(int cardId, int count)
    {
        for (int i = 0; i < bagZombies.Count; i++)
        {
            if (bagZombies[i].zombie.cardId == cardId && bagZombies[i].count >= count)
            {
                bagZombies[i].count -= count;
                if (bagZombies[i].count == 0)
                {
                    bagZombies.Remove(bagZombies[i]);
                }
                return true;
            }
        }
        return false;
    }
   
    public bool RemoveZombie(ZombieInfo card, int count)
    {
        for (int i = 0; i < bagZombies.Count; i++)
        {
            if (bagZombies[i].zombie.cardId == card.cardId && bagZombies[i].count >= count)
            {
                bagZombies[i].count -= count;
                if (bagZombies[i].count == 0)
                {
                    bagZombies.Remove(bagZombies[i]);
                }
                return true;
            }          
        }    
        return false;
    }
    /// <summary>
    /// 背包是否包含僵尸
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public bool ContainsZombie(ZombieInfo card)
    {
        for (int i = 0; i < bagZombies.Count; i++)
        {
            if (bagZombies[i].zombie.cardId==card.cardId)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 清除所有背包僵尸
    /// </summary>
    public void RemoveAllZombie()
    {
        bagZombies.Clear();
    }
    /// <summary>
    /// 获取所有背包僵尸
    /// </summary>
    /// <returns></returns>
    public List<MyZombie> GetAllZombie()
    {
        return bagZombies;
    }
    /// <summary>
    /// 获得准备时场上怪物
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ZombieController GetZombieController(int id)
    {
        return myCards_ZC[id];
    }
    public ZombieController GetZombieController(ZombieInfo zombie)
    {
        foreach (var item in myCards_ZC)
        {
            if (item.Value.zombie==zombie)
            {
                return item.Value;
            }
        }
        return null;
    }
    /// <summary>
    ///  移除准备时场上僵尸
    /// </summary>  
    public void RemoveZombieController(int id)
    {
        myCards_ZC.Remove(id);
    }

    public void RemoveZombieController(ZombieController zc)
    {
        foreach (var item in myCards_ZC)
        {
            if (item.Value == zc)
            {
                RemoveZombieController(item.Key);
                return;
            }
        }
    }
    /// <summary>
    /// 获得所有场上怪物
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, ZombieController> GetAllZombieController()
    {
        return myCards_ZC;
    }
    /// <summary>
    /// 根据x，y获得场上怪物
    /// </summary>
    /// <returns></returns>
    public ZombieController GetZombieController(int x, int y)
    {
        Pos pos = new Pos();
        pos.x = x;
        pos.y = y;
        foreach (var item in myCards_ZC)
        {
            if (item.Value.zombie.pos == pos)
            {
                return item.Value;
            }
        }
        return null;
    }
    /// <summary>
    /// 移除场上僵尸
    /// </summary>
    /// <param name="zombieControl"></param>
    public void DeleteZombie(ZombieController zombieControl)
    {
        //Transform _Transform = zombieControl.transform.Find("Button");
        RemoveZombieController(zombieControl);
        BattleWndUIController.getMe().GetBasekeyState().unregBut(zombieControl._Button);
        GameObject.Destroy(zombieControl.gameObject);
    }

    //查找前方敌人
    public ZombieController GetFrontZombie(ZombieController zc)
    {
        if (zc.zombie.face) // 左 
        {
            return GetZombieController(zc.zombie.pos.x + 1, zc.zombie.pos.y);
        }
        else
        {
            return GetZombieController(zc.zombie.pos.x - 1, zc.zombie.pos.y);
        }
    }
    /// <summary>
    /// 获取周围一定范围特定的人
    /// </summary>
    /// <param name="zc"></param>
    /// <returns></returns>
    public List<ZombieController> GetRoundZombie(ZombieController zc, Player p, int Range)
    {
        List<ZombieController> zcs = new List<ZombieController>();
        for (int i = -Range; i <= Range; i++)
        {
            for (int j = -Range; j < Range; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int x = zc.zombie.pos.x + i;
                int y = zc.zombie.pos.y + j;

                if (x < 10 && x >= 0 && y >= 0 && y < 5)
                {
                    ZombieController enemyZc = p._ZombieMgr.GetZombieController(x, y);
                    if (enemyZc != null)
                    {
                        zcs.Add(enemyZc);
                    }
                }
            }
        }
        return zcs;
    }
    /// <summary>
    /// 获取一定范围最近的某一方僵尸
    /// </summary>
    /// <param name="zc"></param>
    /// <returns></returns>
    public ZombieController GetNearZombie(ZombieController zc, Player p, int Range=5)
    {
        var enemyZombies = GetRoundZombie(zc, p, Range);
        QuickSort(enemyZombies, zc, 0, enemyZombies.Count - 1);
        return enemyZombies[0];
    }
    /// <summary>
    ///  为序列的某个部分进行划分 左小右大
    /// </summary>
    /// <param name="arr">序列</param>
    /// <param name="start">开始位置</param>
    /// <param name="end">结束位置</param>
    public static void QuickSort(List<ZombieController> enemyZombies, ZombieController zc, int start, int end)
    {
        if (start < end)
        {
            int s = start;
            int e = end;
            //临界值位置的标量
            bool isStart = true;
            while (s < e)
            {
                if (Pos.Distance(zc.zombie.battlePos, enemyZombies[s].zombie.battlePos) > Pos.Distance(zc.zombie.battlePos, enemyZombies[e].zombie.battlePos))
                {
                    Swap(enemyZombies, s, e);
                    //标量转换
                    isStart = !isStart;
                }
                //移动索引位置
                if (isStart)
                {
                    e--;
                }
                else
                {
                    s++;
                }
            }
            //将序列划分成了：
            //左边:start→（e-1）
            QuickSort(enemyZombies, zc, start, e - 1);
            //右边：s+1→end
            QuickSort(enemyZombies, zc, s + 1, end);
        }
    }
    /// <summary>
    /// 两两交换
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="j"></param>
    /// <param name="v"></param>
    private static void Swap(List<ZombieController> arr, int j, int v)
    {
        ZombieController temp = arr[j];
        arr[j] = arr[v];
        arr[v] = temp;
    }
}
