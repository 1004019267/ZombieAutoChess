using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;
///UISource File Create Data:3/7/2019 1:51:58 PM
public partial class BuyWndUIController : BaseUI
{
    List<CardConfig> appointCardDatas = new List<CardConfig>();//指定金币卡牌
    Button[] btnCards = new Button[3];
    Button btnExit;

    System.Random r = new System.Random();

    Text textName;
    Text textProfile;
    Transform hpContent;
    Transform atkContent;
    Transform star;
    Image zombieShow;
    /// <summary>
    /// 确保唯一每个怪物ID
    /// </summary>
    // int type = 1;
    public static void ShowOrHide(bool flag, Transform parent = null)
    {

        BaseUI.ShowOrHide(BuyWndUIController.ui_name, flag, parent);

    }

    public static BuyWndUIController getMe()
    {

        return uiMgr.getMe().FindUI(BuyWndUIController.ui_name).GetComponent<BuyWndUIController>();

    }




    public override bool onEnter()
    {
        m_key = new BuyChildKey();
        m_key.init(this);
        var _key = m_key as BuyChildKey;

        //if (m_key == null)
        //{
        //    m_key = new BasekeyState();
        //}
        //GameObject obj = this.choseCard;
        //m_key.init(obj);

        textName = card.transform.Find("name").GetComponent<Text>();
        textProfile = card.transform.Find("profile").GetComponent<Text>();
        hpContent = card.transform.Find("hpScroll View/Viewport/Content");
        atkContent = card.transform.Find("atkScroll View/Viewport/Content");
        star = card.transform.Find("star");
        zombieShow = card.transform.Find("portrait").GetComponent<Image>();


        for (int i = 0; i < btnCards.Length; i++)
        {
            btnCards[i] = this.choseCard.transform.Find("btnCard" + (i + 1) + "_").GetComponent<Button>();
        }

        ChooseCard(BattleWndUIController.GetRound());

        SetCardShow(Intercept.Instance.GetIdForBuyName(btnCards[0].name));//第一次加载无移动判定显示
        btnCards[0].transform.Find("BG/sprite").GetComponent<Image>().color = Color.white;//第一次加载无法更新光标

        for (int i = 0; i < btnCards.Length; i++)
        {
            //_key.addactive(btnCards[i]);
            m_key.registerCallBack(btnCards[i], btnCardsOnClick);
            m_key.RegisterOnKeyChangedEvent(btnCards[i], btnCardsOnEnter, btnCardsOnExit);
        }
        btnExit = this.choseCard.transform.Find("btnExit").GetComponent<Button>();
        m_key.registerCallBack(btnExit, btnExitOnClick);
        m_key.RegisterOnKeyChangedEvent(btnExit, btnExitOnEnter,btnExitOnExit);
        //Debug.Log("0");
        return true;
    }


    private void btnExitOnClick()
    {
        onBack();
        //BuyWndUIController.ShowOrHide(false);
    }


    void btnExitOnEnter(Button btn)
    {
        btn.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }
    void btnExitOnExit(Button btn)
    {
        btn.transform.Find("Text").GetComponent<Text>().color = Color.grey;
    }
    public override bool onBack()
    {
        uiMgr.getMe().setTopUI(BattleWndUIController.ui_name);
        uiMgr.getMe().setDownUI(BuyWndUIController.ui_name);
        gameObject.SetActive(false);
        return true;
    }
    /// <summary>
    /// 购买僵尸按钮
    /// </summary>
    /// <param name="btn"></param>
    private void btnCardsOnClick(Button btn)
    {
        var p = BattleManager.Instance.leftPlayer;
        if (p._ZombieMgr.GetAllZombie().Count==BattleManager.Instance.maxBagZombie)
        {
            TipsManager.Instance.TipsShow("背包数量已满无法购买");
        }
        if (int.Parse(btn.transform.Find("gold/Text").GetComponent<Text>().text) <= p.GetGold())
        {
            CardConfig card = CardConfigData.Instance.GetCard(Intercept.Instance.GetIdForBuyName(btn.name));
            SkillConfig skill = SkillConfigData.Instance.GetSkill(card.skill);
            ZombieInfo zombie = new ZombieInfo();
            //zombie.id = type;
            //type++;
            zombie.cardId = card.id;
            zombie.cardName = card.cardName;
            zombie.gold = card.gold;
            zombie.profile = skill.profile;
            zombie.atk = card.atk;
            zombie.maxHp = card.hp;
            zombie.atkSpeed = card.atkSpeed;
            zombie.atkRange = card.atkRange;
            zombie.level = card.level;
            p._ZombieMgr.AddZombie(zombie, 1);
            EventManager.getMe().GoldRemove(card.gold); //p.RemoveGold(card.gold);
            BagWndUIController.getMe().Notify();
            // ChooseCard(BattleWndUIController.GetRound());
            Notify();
        }
    }

    private void btnCardsOnEnter(Button btn)
    {
        btn.transform.Find("BG/sprite").GetComponent<Image>().color = Color.white;
        SetCardShow(Intercept.Instance.GetIdForBuyName(btn.name));
    }

    void btnCardsOnExit(Button btn)
    {
        btn.transform.Find("BG/sprite").GetComponent<Image>().color = Color.gray;
    }

    public override bool onExit()
    {
        return true;
    }

    public override void Notify()
    {
        ChooseCard(BattleWndUIController.GetRound());
        SetFirstBtnOnNode();
    }

    /// <summary>
    /// 初始化卡牌购买界面
    /// </summary>
    void ChooseCard(int round)
    {
        if (round <= 2)
        {
            SetChoiceCard(GetAppointGlodCard(round));
        }
        else
        {
            SetChoiceCard(CardConfigData.Instance.GetAllCard());
        }
    }
    /// <summary>
    /// 设置3个随机选择卡牌
    /// </summary>
    void SetChoiceCard(List<CardConfig> card)
    {
        //Debug.Log(card[0].cardName);
        for (int i = 0; i < btnCards.Length; i++)
        {
            var random = r.Next(0, card.Count);
            btnCards[i].transform.Find("name").GetComponent<Text>().text = card[random].cardName;
            btnCards[i].transform.Find("gold/Text").GetComponent<Text>().text = card[random].gold.ToString();
            btnCards[i].name = "btnCard" + (i + 1) + "_" + card[random].id.ToString();

            var sprite = ZombieCardResource.Instance.GetCardSprite(card[random].id, card[random].level);
            btnCards[i].transform.Find("BG/sprite").GetComponent<Image>().sprite = sprite;
            btnCards[i].transform.Find("BG/shadowSprite").GetComponent<Image>().sprite = sprite;
        }
    }

    /// <summary>
    /// 获取小于一定金币的牌组
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public List<CardConfig> GetAppointGlodCard(int gold)
    {
        if (appointCardDatas.Count > 0)
        {
            appointCardDatas.Clear();
        }
        var cards = CardConfigData.Instance.GetAllCard();
        for (int i = 0; i < cards.Count; i++)
        {
            if (gold >= cards[i].gold)
            {
                appointCardDatas.Add(cards[i]);
            }
        }
        return appointCardDatas;
    }
    /// <summary>
    /// 设置展示简介卡牌
    /// </summary>
    /// <param name="name"></param>
    /// <param name="profile"></param>
    /// <param name="atk"></param>
    /// <param name="hp"></param>
    public void SetCardShow(int id)
    {
        ClearStar(hpContent);
        ClearStar(atkContent);
        CardConfig cardData = CardConfigData.Instance.GetCard(id);
        SkillConfig skillData = SkillConfigData.Instance.GetSkill(cardData.skill);
        //if (cardData == null)
        //    return;
        textName.text = cardData.cardName;
        textProfile.text = skillData.profile;

        var sprite = ZombieCardResource.Instance.GetCardSprite(id,1);      
        zombieShow.sprite = sprite;

        CreateStar(cardData.hpStar, cardData.hpHalfStar, star, hpContent);
        CreateStar(cardData.atkStar, cardData.atkHalfStar, star,atkContent);
    }

    /// <summary>
    /// 创建星星
    /// </summary>
    /// <param name="starCount"></param>
    /// <param name="halfStarCount"></param>
    /// <param name="star"></param>
    /// <param name="halfStar"></param>
    /// <param name="content"></param>
    public void CreateStar(int starCount, int halfStarCount, Transform star, Transform content)
    {
        for (int i = 0; i < starCount; i++)
        {
            var go = Instantiate(star);
            go.SetParent(content);
            go.gameObject.SetActive(true);
        }

        for (int i = 0; i < halfStarCount; i++)
        {
            var go = Instantiate(star);
            go.GetComponent<Image>().color = Color.grey;
            go.SetParent(content);
            go.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 清空星星
    /// </summary>
    /// <param name="content"></param>
    public void ClearStar(Transform content)
    {
        var gos = content.GetComponentsInChildren<Image>();
        for (int i = 0; i < gos.Length; i++)
        {
            Destroy(gos[i].gameObject);
        }
    }
    /// <summary>
    /// 设置焦点为第一个btn 重置其他btn
    /// </summary>
    public void SetFirstBtnOnNode()
    {
        m_key.setCurKey(btnCards[0],false);

        for (int i = 0; i < btnCards.Length; i++)
        {
            btnCardsOnExit(btnCards[i]);
        }
        btnCardsOnEnter(btnCards[0]);
        btnExitOnExit(btnExit);
    }

}
