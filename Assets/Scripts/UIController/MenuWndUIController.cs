using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Spine;
using Spine.Unity;
///UISource File Create Data:3/13/2019 2:22:00 PM
public partial class MenuWndUIController : BaseUI
{
    /// <summary>
    /// 当前僵尸id
    /// </summary>
    public int id;

    /// <summary>
    /// 当前僵尸zombie
    /// </summary>
    public ZombieInfo zombie;
    //int count;//计数
    /// <summary>
    /// 可以升级的僵尸
    /// </summary>  
    //public List<ZombieInfo> needZombies = new List<ZombieInfo>();
    /// <summary>
    /// 场上除自己外僵尸
    /// </summary>
   // public List<ZombieController> battleZombie = new List<ZombieController>();
    /// <summary>
    /// 目标玩家
    /// </summary>
    Player p;

    /// <summary>
    /// 闪烁动画
    /// </summary>
    Tween tween;
    /// <summary>
    /// 是否在闪烁
    /// </summary>
    public bool isPingpong;
    // Text text;

    //Color selectColor;
    public static void ShowOrHide(bool flag, Transform parent = null)
    {

        BaseUI.ShowOrHide(MenuWndUIController.ui_name, flag, parent);

    }

    public static MenuWndUIController getMe()
    {

        //if (uiMgr.getMe().FindUI(MenuWndUIController.ui_name) == null)
        //    return null;

        return uiMgr.getMe().FindUI(MenuWndUIController.ui_name).GetComponent<MenuWndUIController>();

    }

    public override bool onEnter()
    {
        m_key = new BuyChildKey();
        m_key.init(this);
        var _key = m_key as BuyChildKey;

        m_key.registerCallBack(this.UpLevel.GetComponent<Button>(), btnOnUpLevelClick);
        m_key.registerCallBack(this.Sold.GetComponent<Button>(), btnOnSoldClick);
        m_key.registerCallBack(this.Recover.GetComponent<Button>(), btnOnRecoverClick);
        m_key.registerCallBack(this.Move.GetComponent<Button>(), btnOnMoveClick);
        UpLevel.transform.Find("Text").GetComponent<Text>().color = ColorTypeChange.Instance.HexColorToColor("f99629");

        m_key.RegisterOnKeyChangedEvent(UpLevel.GetComponent<Button>(), btnOnEnter, btnOnExit);
        m_key.RegisterOnKeyChangedEvent(Sold.GetComponent<Button>(), btnOnEnter, btnOnExit);
        m_key.RegisterOnKeyChangedEvent(Recover.GetComponent<Button>(), btnOnEnter, btnOnExit);
        m_key.RegisterOnKeyChangedEvent(Move.GetComponent<Button>(), btnOnEnter, btnOnExit);
        p = BattleManager.getMe().leftPlayer;

        //ColorUtility.TryParseHtmlString("f99629", out selectColor);
        // text = transform.Find("Text").GetComponent<Text>();
        return true;
    }

    private void btnOnEnter(Button btn)
    {
        btn.transform.Find("Text").GetComponent<Text>().color = ColorTypeChange.Instance.HexColorToColor("f99629");
    }

    private void btnOnExit(Button btn)
    {
        btn.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void btnOnMoveClick()
    {
        //uiMgr.getMe().setDownUI(MenuWndUIController.ui_name);
        onBack(MoveZombie);
    }
    void MoveZombie()
    {
        isPingpong = true;
        Pingpong(p._ZombieMgr.GetZombieController(id).GetComponent<BaseSpine>()._SkeletonGraphic, 0, 1, 1);
        //zombies = p._ZombieMgr.GetAllZombieController();
        foreach (var item in p._ZombieMgr.GetAllZombieController())
        {
            item.Value._Button.interactable = false;
        }
        //for (int i = 0; i < zombies.Count; i++)
        //{
        //    //BattleWndUIController.getMe().GetBasekeyState().unregBut(zombies[i].gameObject.GetComponent<Button>());
        //    zombies[i].gameObject.GetComponent<Button>().interactable = false;
        //    //var a = zombies[i].gameObject.GetComponent<Image>();
        //}

        MoveZombieNodeToGround(zombie);
        transform.position = new Vector3(1000, 1000);
    }

    //移动放下
    public bool MovePutDown()
    {
        if (isPingpong == true)
        {
            isPingpong = false;
            tween.Kill();
            var baseSpine = p._ZombieMgr.GetZombieController(id).GetComponent<BaseSpine>();
            baseSpine._SkeletonGraphic.color = Color.white;
            var nowNode = BattleWndUIController.getMe().GetBasekeyState().m_key.node;
            var zc = p._ZombieMgr.GetZombieController(id);
            zc.transform.position = nowNode.transform.position;

            BattleWndUIController.getMe().GetBasekeyState().setCurKey(zc._Button);
            nowNode.interactable = false;
            nowNode.image.material = null;

            //zombie.pos.x = Intercept.getMe().GetPosForGroundName(nowNode.name, out y);
            //zombie.pos.y = y;
            zombie.SetPos(nowNode);

            foreach (var item in p._ZombieMgr.GetAllZombieController())
            {
                item.Value._Button.interactable = true;
            }
            //for (int i = 0; i < zombies.Length; i++)
            //{
            //    zombies[i].gameObject.GetComponent<Button>().interactable = true;
            //}
            return true;
        }
        return false;
    }

    //public override bool Ok()
    //{

    //    return true;     
    //}

    /// <summary>
    /// 闪烁动画
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="startValue"></param>
    /// <param name="endValue"></param>
    /// <param name="times"></param>
    void Pingpong(SkeletonGraphic spine, float startValue, float endValue, float times)
    {
        tween = spine.DOFade(endValue, times);
        tween.SetAutoKill(false);
        tween.onComplete = () =>
        {
            if (isPingpong)
            {
                Pingpong(spine, endValue, startValue, times);
            }
        };
    }
    /// <summary>
    /// 收回背包
    /// </summary>
    private void btnOnRecoverClick()
    {
        int count = 0;
        switch (zombie.level)
        {
            case 1:
                p._ZombieMgr.AddZombie(zombie, 1);
                count = 1;
                break;
            case 2:
                p._ZombieMgr.AddZombie(zombie, 3);
                count = 3;
                break;
            case 3:
                p._ZombieMgr.AddZombie(zombie, 9);
                count = 9;
                break;
        }

        BagWndUIController.getMe().Notify();

        MoveZombieNodeToGround(zombie);

        p._ZombieMgr.DeleteZombie(p._ZombieMgr.GetZombieController(id));
        TipsManager.Instance.TipsShow("获得" + count + "个" + zombie.cardName);
        onBack();
    }
    /// <summary>
    /// 卖出
    /// </summary>
    public void btnOnSoldClick()
    {
        var money = SoldZombie(zombie);

        MoveZombieNodeToGround(zombie);

        p._ZombieMgr.DeleteZombie(p._ZombieMgr.GetZombieController(id));
        TipsManager.Instance.TipsShow("卖出" + zombie.cardName + "获得" + money+"金币");
        onBack();
    }
    /// <summary>
    /// 升级 优先升到最高级
    /// </summary>
    private void btnOnUpLevelClick()
    {
        var myZombie = p._ZombieMgr.GetZombie(zombie);
        if (myZombie == null)
        {
            TipsManager.Instance.TipsShow("你没有该种类的僵尸!!!");
            //uiTip.ShowOrHide("僵尸数量不够");
            onBack();
            return;
        }

        //Debug.Log(zombie.level);
        if (zombie.level == 3)
        {
            TipsManager.Instance.TipsShow("已为最高级");
            onBack();
            return;
        }
        else if (myZombie.count >= 8 && zombie.level < 2)
        {
            p._ZombieMgr.RemoveZombie(myZombie.zombie, 8);
            myZombie.zombie.zc.LevelUp(2);
        }
        else if (myZombie.count >= 2)
        {
            p._ZombieMgr.RemoveZombie(myZombie.zombie, 2);
            myZombie.zombie.zc.LevelUp(1);
        }
        else
        {
            TipsManager.Instance.TipsShow("僵尸数量不够");
            onBack();
            return;
            //uiTip.ShowOrHide("僵尸数量不够");
        }
        BagWndUIController.getMe().Notify();
        onBack();
        // zombies = p._ZombieMgr.GetAllZombieController();

        //var allBagZombies = BattleManager.getMe().leftPlayer.GetAllZombie();
        //遍历场上zombie
        //foreach (var item in p._ZombieMgr.GetAllZombieController())
        //{
        //    //不能扣自己当前要升级的
        //    if (item.Value.zombie.cardId == zombie.cardId && item.Value.zombie.level == zombie.level && item.Value.zombie.id != zombie.id)
        //    {
        //        needZombies.Add(item.Value.zombie);
        //    }
        //    if (item.Value.zombie.id != zombie.id)
        //    {
        //        battleZombie.Add(item.Value);
        //    }
        //}
        //for (int i = 0; i < zombies.Length; i++)
        //{
        //    //不能扣自己当前要升级的
        //    if (zombies[i].zombie.cardId == zombie.cardId && zombies[i].zombie.level == zombie.level && zombies[i].zombie.id != zombie.id)
        //    {
        //        needZombies.Add(zombies[i].zombie);
        //    }
        //    if (zombies[i].zombie.id != zombie.id)
        //    {
        //        battleZombie.Add(zombies[i]);
        //    }
        //}
        //遍历背包里zombie
        //var zombies = p._ZombieMgr.GetAllZombie();
        //for (int i = 0; i < zombies.Count; i++)
        //{
        //    if (zombies[i].zombie.cardId == zombie.cardId)
        //    {

        //        // needZombies.Add(p._ZombieMgr.GetAllZombie()[i]);
        //    }
        //}
        //如果有2个或以上 去除前两个升级
        //if (needZombies.Count >= 2)
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        if (battleZombie[i].zombie.id == needZombies[i].id)
        //        {
        //            p._ZombieMgr.DeleteZombie(battleZombie[i]);
        //        }
        //        else if (p._ZombieMgr.GetAllZombie().Contains(needZombies[i]))
        //        {
        //            p._ZombieMgr.RemoveZombie(needZombies[i]);
        //            BagWndUIController.getMe().Notify();
        //        }
        //    }

        //zombie.LevelUp();

        //p._ZombieMgr.GetZombieController(id).GetComponent<BaseSpine>().setSkin("goblin");
        // npcMgr.getMe().getNpc("");

        //btn.GetComponent<Image>().overrideSprite = CardResource.Instance.GetCardSprite(zombie.cardId, zombie.level);
        //btn.transform.parent.Find("Spine").GetComponent<basespine>().setSkin("goblin");// = "goblin";
        //var a = btn.GetComponent<Image>().sprite;
        //var b = btn.GetComponent<Image>().overrideSprite;
        //p._ZombieMgr.GetZombieController(id).transform.name = "zombie" + "_" + zombie.id + "_" + zombie.level;
        //onBack();
        //}
        //else
        //{
        // TextShow("抱歉同类型怪物不够三个");
        //}
        //needZombies.Clear();
        //battleZombie.Clear();
    }

    //public override bool Right()
    //{
    //    gameObject.SetActive(false);
    //    return true;
    //}
    //public override bool Left()
    //{
    //    gameObject.SetActive(false);
    //    return true;
    //}

    public override bool onBack()
    {

        uiMgr.getMe().setDownUI(MenuWndUIController.ui_name);
        gameObject.SetActive(false);
        return true;
    }

    public override bool onBack(Action action)
    {
        uiMgr.getMe().setDownUI(MenuWndUIController.ui_name);
        action();
        gameObject.SetActive(false);
        return true;
    }

    public override bool onExit()
    {

        return true;

    }

    public override void Notify()
    {
        SetFirstBtnOnNode();
    }


    /// <summary>
    /// 卖僵尸
    /// </summary>
    /// <param name="zombie"></param>
    float SoldZombie(ZombieInfo zombie)
    {
        float money = 0;
        //根据等级返还金币
        switch (zombie.level)
        {
            case 1:
                money = zombie.gold * 0.5f;
                break;
            case 2:
                money = zombie.gold * 0.5f * 3;
                break;
            case 3:
                money = zombie.gold * 0.5f * 9;
                break;
        }
        EventManager.getMe().GoldAdd(money);
        return money;
    }
    /// <summary>
    /// 把当前僵尸焦点到下一层的地板焦点
    /// </summary>
    void MoveZombieNodeToGround(ZombieInfo zombie)
    {
        //var ground = GroundManager.getMe().GetGround(Intercept.getMe().GetPosForCardName(zombieControl.gameObject.name));
        var ground = GroundManager.getMe().GetGround(zombie.pos);
        BattleWndUIController.getMe().GetBasekeyState().setCurKey(ground);
        ground.interactable = true;
    }

    //void Update()
    //{


    //}
    /// <summary>
    /// 设置焦点为第一个btn
    /// </summary>
    public void SetFirstBtnOnNode()
    {
        m_key.setCurKey(UpLevel.GetComponent<Button>());
        UpLevel.transform.Find("Text").GetComponent<Text>().color = ColorTypeChange.Instance.HexColorToColor("f99629");
        Sold.transform.Find("Text").GetComponent<Text>().color = Color.white;
        Recover.transform.Find("Text").GetComponent<Text>().color = Color.white;
        Move.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }
}
