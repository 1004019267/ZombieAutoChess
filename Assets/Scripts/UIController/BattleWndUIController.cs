using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;
using DG.Tweening;
///UISource File Create Data:3/6/2019 5:11:00 PM
public partial class BattleWndUIController : BaseUI
{
    static int round = 1;
    // Button[] btn;
    public Transform leftCard;
    Player p;

    Text textGold;

    Material buttonMat;
    /// <summary>
    /// 临时坐标
    /// </summary>
    int y;
    public static void ShowOrHide(bool flag, Transform parent = null)
    {
        BaseUI.ShowOrHide(BattleWndUIController.ui_name, flag, parent);
    }
    public static BattleWndUIController getMe()
    {
        return uiMgr.getMe().FindUI(BattleWndUIController.ui_name).GetComponent<BattleWndUIController>();

    }

    public override void Start()
    {
        onEnter();
    }

    public override bool onEnter()
    {
        m_key = new BuyChildKey();
        m_key.init(this);
        var _key = m_key as BuyChildKey;
        //if (m_key == null)
        //{
        //    m_key = new BasekeyState();
        //    m_key.init(gameObject);
        //    m_key.AddAllBtn(this.Ground.transform.Find("leftGround").gameObject);
        //}
        buttonMat = GameResourcesManager.GetMaterial("SelectButton");

        GroundManager.getMe().Init(this.Ground.transform.Find("AllGround").GetComponentsInChildren<Button>());
        for (int i = 0; i < GroundManager.getMe().leftBtns.Count; i++)
        {
            m_key.registerCallBack(GroundManager.getMe().leftBtns[i], btnOnClick);
            m_key.RegisterOnKeyChangedEvent(GroundManager.getMe().leftBtns[i], btnOnEnter, btnOnExit);
        }
        GroundManager.getMe().leftBtns[0].image.material = buttonMat;

        leftCard = this.Ground.transform.Find("leftCard");

        p = BattleManager.Instance.leftPlayer;
        p.camp = eCamp.Left;

        textGold = this.UpData.transform.Find("NowGoldImage/nowGold").GetComponent<Text>();
        EventManager.getMe().GoldAdd(8);
        //NotificationCenter.DefaultCenter().AddObserver(this, "SetGold");
        return true;
    }

    private void btnOnEnter(Button btn)
    {
        btn.image.material = buttonMat;
    }

    private void btnOnExit(Button btn)
    {
        btn.image.material = null;
    }



    /// <summary>
    /// 注册地图图标
    /// </summary>
    /// <param name="btn"></param>
    private void btnOnClick(Button btn)
    {

        //

        if (uiMgr.getMe().FindUI(MenuWndUIController.ui_name) != null)
        {

            if (MenuWndUIController.getMe().MovePutDown() == true)
            {
                return;
            }
        }


        if (p._ZombieMgr.GetAllZombie().Count < 1)
            return;
        //if (uiMgr.getMe().FindUI(BagWndUIController.ui_name) == null)
        //{
        //    BagWndUIController.ShowOrHide(true);
        //}
        //else
        //{
        uiMgr.Instance.setTopUI(BagWndUIController.ui_name);
        BagWndUIController.getMe().SetFirstBtnOnNode();
        //}
        //Debug.Log("btn");
        //var g = BagWndUIController.getMe().getCurButton().name;


        ZombieInfo thisCard = p._ZombieMgr.GetZombie(Intercept.Instance.GetCardIdForBagName(BagWndUIController.getMe().getCurButton().name)).zombie;
        if (thisCard == null)
            return;
        BagWndUIController.getMe().SetCardShow(thisCard);
    }

    public override bool onExit()
    {

        return true;

    }

    public override void Notify()
    {

    }

    public override bool onBack()
    {
        if (uiMgr.getMe().FindUI(CloseWndUIController.ui_name) != null)
        {
            CloseWndUIController.getMe().gameObject.SetActive(true);
            uiMgr.getMe().setTopUI(CloseWndUIController.ui_name);
            CloseWndUIController.getMe().Notify();
        }
        CloseWndUIController.ShowOrHide(true);
        CloseWndUIController.getMe().MessageBox.transform.Find("Image").GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite("tch_01");
        CloseWndUIController.getMe().messageBoxShowYes = btnExitYes;
        CloseWndUIController.getMe().messageBoxShowNo = btnExitNo;
        return true;
    }

    private void btnExitYes()
    {
        StartWndUIController.ShowOrHide(true);        
        ClearAllZombie();
        p._ZombieMgr.RemoveAllZombie();
        p.ClearGold();
        p.DestroyEvent();
         BagWndUIController.getMe().gameObject.SetActive(false);
    }

    void btnExitNo()
    {
        uiMgr.Instance.setTopUI(BattleWndUIController.ui_name);
    }

    public static int GetRound()
    {
        return round;
    }
    void Update()
    {
        BuffManager.Instance.Update();
    }


    public void SetGold(float gold)
    {
        textGold.text = "X " + gold;
        //textGold.FontTextureChanged();
    }
    /// <summary>
    /// 清除所有僵尸
    /// </summary>
    public void ClearAllZombie()
    {
        foreach (var item in p._ZombieMgr.GetAllZombieController())
        {
            m_key.unregBut(item.Value._Button);
            Destroy(item.Value.gameObject);
        }
        //for (int i = 0; i < zombies.Length; i++)
        //{
        //    m_key.unregBut(zombies[i].transform.Find("Button").GetComponent<Button>());
        //    Destroy(zombies[i].gameObject);
        //}
        GroundManager.getMe().ResetGround();
    }

    /// <summary>
    /// 设置焦点为第一个btn
    /// </summary>
    public void SetFirstBtnOnNode()
    {
        m_key.setCurKey(GroundManager.getMe().leftBtns[0], false);
        for (int i = 0; i < GroundManager.getMe().leftBtns.Count; i++)
        {
            GroundManager.getMe().leftBtns[i].image.material = null;
        }
        GroundManager.getMe().leftBtns[0].image.material = buttonMat;
    }
}
