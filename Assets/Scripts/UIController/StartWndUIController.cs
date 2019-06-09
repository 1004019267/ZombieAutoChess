using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
///UISource File Create Data:3/6/2019 2:56:54 PM
public partial class StartWndUIController : BaseUI
{
    public static void ShowOrHide(bool flag, Transform parent = null)
    {
        BaseUI.ShowOrHide(StartWndUIController.ui_name, flag, parent);
    }

    public static StartWndUIController getMe()
    {
        return uiMgr.getMe().FindUI(StartWndUIController.ui_name).GetComponent<StartWndUIController>();

    }
    public override bool onEnter()
    {
        //if (m_key == null)
        //{
        //    m_key = new BasekeyState();
        //}
        //m_key.init(this.gameObject);
        Button btnSingle = this.btnSingle.GetComponent<Button>();
        m_key.registerCallBack(btnSingle, btnSingleOnClick);
        //  m_key.RegisterOnKeyChangedEvent(btnSingle, btnOnEnter, btnOnExit);

        Button btnDouble = this.btnDouble.GetComponent<Button>();

        Button btnExit = this.btnExit.GetComponent<Button>();
        m_key.registerCallBack(btnExit, btnExitOnClick);
        // m_key.RegisterOnKeyChangedEvent(btnExit, btnOnEnter, btnOnExit);
        m_key.setCurKey(btnSingle);
        btnSingle.image.sprite = btnSingle.spriteState.highlightedSprite;
        uiMgr.Instance.setTopUI(StartWndUIController.ui_name);
        return true;
    }
    //private void btnOnEnter(Button btn)
    //{
    //    btn.image.color = Color.white;
    //}

    //private void btnOnExit(Button btn)
    //{
    //    btn.image.color = Color.grey;
    //}

    //单机模式
    private void btnSingleOnClick()
    {
        GroundManager.getMe().ClearNum();
        BattleManager.Instance.leftPlayer.Init();
        if (uiMgr.getMe().FindUI(MenuWndUIController.ui_name) != null)
        {
            MenuWndUIController.getMe().isPingpong = false;
        }

       
        if (uiMgr.getMe().FindUI(BagWndUIController.ui_name) != null)
        {
            BagWndUIController.getMe().gameObject.SetActive(true);
            BagWndUIController.getMe().Notify();
            uiMgr.getMe().setTopUI(BagWndUIController.ui_name);

        }
        BagWndUIController.ShowOrHide(true);
       
        if (uiMgr.getMe().FindUI(BattleWndUIController.ui_name) != null)
        {
            BattleWndUIController.getMe().gameObject.SetActive(true);
            BattleWndUIController.getMe().SetFirstBtnOnNode();
            EventManager.getMe().GoldAdd(8);
        }
        
        BattleWndUIController.ShowOrHide(true);
        
        if (uiMgr.getMe().FindUI(BuyWndUIController.ui_name) != null)
        {
            BuyWndUIController.getMe().gameObject.SetActive(true);
            uiMgr.getMe().setTopUI(BuyWndUIController.ui_name);
            BuyWndUIController.getMe().Notify();
        }
        BuyWndUIController.ShowOrHide(true);
       
        StartWndUIController.ShowOrHide(false);
    }
    //双人按钮
    private void btnDoubleClick()
    {

    }
    //退出按钮
    private void btnExitOnClick()
    {
        //uiMgr.getMe().sendMsgByName(ui_name);
        //Application.Quit();      
        if (uiMgr.getMe().FindUI(CloseWndUIController.ui_name) != null)
        {
            CloseWndUIController.getMe().gameObject.SetActive(true);
            uiMgr.getMe().setTopUI(CloseWndUIController.ui_name);
        }
        CloseWndUIController.ShowOrHide(true);
        CloseWndUIController.getMe().MessageBox.transform.Find("Image").GetComponent<Image>().sprite=SpriteManager.Instance.GetSprite("t_02");
        CloseWndUIController.getMe().messageBoxShowYes = btnExitYes;
        CloseWndUIController.getMe().messageBoxShowNo = btnExitNo;
    }

    public void btnExitYes()
    {
        Application.Quit();
        SoundManager.stopAll();
    }

    public void btnExitNo()
    {
        uiMgr.Instance.setTopUI(StartWndUIController.ui_name);
    }
    public override bool onExit()
    {
        return true;
    }

    public override bool onBack()
    {
        btnExitOnClick();
        return true;
    }

    public override void Notify()
    {
        //Text text = transform.Find("txt").GetComponent<Text>();
        //text.text = "dsadas";
    }

    void Update()
    {

    }

   
}
