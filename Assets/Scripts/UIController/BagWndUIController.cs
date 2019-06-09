using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
///UISource File Create Data:3/8/2019 12:52:25 PM
public partial class BagWndUIController : BaseUI
{

    public static string ui_name = "BagWnd";

    Transform card;
    Transform btnExit;
    Transform content;
    Transform cardShow;

    Text textName;
    Text textProfile;
    Transform hpContent;
    Transform atkContent;
    Transform star;
    Image zombieShow;


    public static void ShowOrHide(bool flag, Transform parent = null)
    {

        BaseUI.ShowOrHide(BagWndUIController.ui_name, flag, parent);

    }

    public static BagWndUIController getMe()
    {
        return uiMgr.getMe().FindUI(BagWndUIController.ui_name).GetComponent<BagWndUIController>();
    }


    //public override bool onBack()
    //{
    //    uiMgr.getMe().closeTopUI();//删除自己
    //    return true;
    //}



    public override bool onEnter()
    {
        m_key = new BuyChildKey();
        m_key.init(this);
        var _key = m_key as BuyChildKey;

        card = this.myCard.transform.Find("Scroll View/Viewport/Card");
        cardShow = this.myCard.transform.Find("card");
        btnExit = this.myCard.transform.Find("Scroll View/Viewport/btnExit");
        content = this.myCard.transform.Find("Scroll View/Viewport/Content");

        textName = cardShow.transform.Find("name").GetComponent<Text>();
        textProfile = cardShow.transform.Find("profile").GetComponent<Text>();
        hpContent = cardShow.transform.Find("hpScroll View/Viewport/Content");
        atkContent= cardShow.transform.Find("atkScroll View/Viewport/Content");
        star = cardShow.transform.Find("star");
        zombieShow = cardShow.transform.Find("portrait").GetComponent<Image>();

        

        Notify();
        //logMgr.log(BattleWndUIController.getMe().getCurButton().name);
        return true;
    }

    public override bool onBack()
    {
        cardShow.gameObject.SetActive(false);
        uiMgr.Instance.setTopUI(BattleWndUIController.ui_name);
        return true;
    }
    /// <summary>
    /// 刷新背包
    /// </summary>
    public override void Notify()
    {
        //Debug.Log("1");
        ClearBagBtn();
        var cards = BattleManager.Instance.leftPlayer._ZombieMgr.GetAllZombie();
        for (int i = 0; i < cards.Count; i++)
        {
            var go = GameObject.Instantiate(card);
            go.SetParent(content, false);
            go.name = "Card" + "_" + cards[i].zombie.cardId;
            go.transform.Find("name").GetComponent<Text>().text = cards[i].zombie.cardName;
            go.transform.Find("count").GetComponent<Text>().text = cards[i].count.ToString();
            go.gameObject.SetActive(true);
            var btn = go.GetComponent<Button>();

            var sprite = ZombieCardResource.Instance.GetCardSprite(cards[i].zombie.cardId, cards[i].zombie.level);
            var x = sprite.textureRect.size.x * 0.2f;
            var y = sprite.textureRect.size.y * 0.2f;
            var zombieSprite = go.transform.Find("BG/sprite").GetComponent<Image>();
            zombieSprite.sprite = sprite;
            zombieSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
            var shadowSprite = go.transform.Find("BG/shadowSprite").GetComponent<Image>();
            shadowSprite.sprite = sprite;
            shadowSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);

            //第一个变为选中效果
            if (i==0)
            {
                zombieSprite.color = Color.white;
            }

            m_key.registerCallBack(btn, btnCardsOnClick);
            m_key.RegisterOnKeyChangedEvent(btn, btnCardsOnEnter,btnCardsOnExit);
        }      

        //var first = content.GetChild(0);
        //if (first!=null)
        //{
        //    first.GetComponent<Image>().color = Color.white;
        //}       
        //var exit = GameObject.Instantiate(btnExit);
        //exit.SetParent(content, false);
        //exit.gameObject.SetActive(true);
        //m_key.registerCallBack(exit.GetComponent<Button>(), btnExitOnClick);
    }

    //private void btnExitOnClick()
    //{
    //    onBack();
    //          
    //}
    /// <summary>
    /// 使用僵尸卡
    /// </summary>
    /// <param name="btn"></param>
    private void btnCardsOnClick(Button btn)
    {
        var battleSelectBtn = BattleWndUIController.getMe().getCurButton();
        PlayerManager.Instance.getLeftPlayer()._ZombieMgr.CreatCard(battleSelectBtn, btn.name, 1);
        //BattleWndUIController.getMe().CreatCard(battleSelectBtn, btn.name, level);    
        cardShow.gameObject.SetActive(false);
        uiMgr.Instance.setTopUI(BattleWndUIController.ui_name);
        Notify();
    }
    /// <summary>
    /// 鼠标悬停显示
    /// </summary>
    /// <param name="btn"></param>
    private void btnCardsOnEnter(Button btn)
    {
        btn.transform.Find("BG/sprite").GetComponent<Image>().color = Color.white;
        var thisCard = PlayerManager.Instance.getLeftPlayer()._ZombieMgr.GetZombie((Intercept.Instance.GetIdForBagName(btn.name)));
        SetCardShow(thisCard.zombie);
    }

    void btnCardsOnExit(Button btn)
    {
        btn.transform.Find("BG/sprite").GetComponent<Image>().color = Color.grey;
    }
    /// <summary>
    /// 设置第一个准心 重置属性
    /// </summary>
    public void SetFirstBtnOnNode()
    {
        m_key.setCurKey(content.GetChild(0).GetComponent<Button>());

        var btns = content.GetComponentsInChildren<Button>();
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].GetComponent<Image>().color = Color.grey;
        }
        btns[0].GetComponent<Image>().color = Color.white;
    }
    /// <summary>
    /// 设置卡牌显示
    /// </summary>
    /// <param name="thisCard"></param>
    public void SetCardShow(ZombieInfo thisCard)
    {
        BuyWndUIController.getMe().ClearStar(hpContent);
        BuyWndUIController.getMe().ClearStar(atkContent);
        CardConfig cardData = CardConfigData.Instance.GetCard(thisCard.cardId);
        SkillConfig skillData = SkillConfigData.Instance.GetSkill(cardData.skill);
        //if (cardData == null)
        //    return;
        textName.text = cardData.cardName;
        textProfile.text = skillData.profile;

        var sprite = ZombieCardResource.Instance.GetCardSprite(thisCard.cardId, thisCard.level);
        zombieShow.sprite = sprite;

        BuyWndUIController.getMe().CreateStar(cardData.hpStar, cardData.hpHalfStar, star, hpContent);
        BuyWndUIController.getMe().CreateStar(cardData.atkStar, cardData.atkHalfStar, star, atkContent);
        cardShow.gameObject.SetActive(true);
    }
    /// <summary>
    /// 清空包里btn
    /// </summary>
    public void ClearBagBtn()
    {
        var btns = content.GetComponentsInChildren<Button>();
        for (int i = 0; i < btns.Length; i++)
        {
            m_key.unregBut(btns[i]);//从btn队列去除
            Destroy(btns[i].gameObject);
        }
    }
}
