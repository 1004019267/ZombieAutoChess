//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

/////UISource File Create Data:4/3/2019 2:49:12 PM
//public partial class CardShowWndUIController : BaseUI
//{

//    Text textName;
//    Text textProfile;
//    Transform hpContent;
//    Transform atkContent;
//    Transform star;
//    Image zombieShow;
//    public static void ShowOrHide(bool flag, Transform parent = null)
//    {

//        BaseUI.ShowOrHide(CardShowWndUIController.ui_name, flag, parent);

//    }

//    public static CardShowWndUIController getMe()
//    {

//        return uiMgr.getMe().FindUI(CardShowWndUIController.ui_name).GetComponent<CardShowWndUIController>();

//    }
  


//    public override bool onEnter()
//    {

//        textName = card.transform.Find("name").GetComponent<Text>();
//        textProfile = card.transform.Find("profile").GetComponent<Text>();
//        hpContent = card.transform.Find("hpScroll View/Viewport/Content");
//        atkContent = card.transform.Find("atkScroll View/Viewport/Content");
//        star = card.transform.Find("star");
//        zombieShow = card.transform.Find("portrait").GetComponent<Image>();


//        return true;

//    }

//    public override bool onExit()
//    {

//        return true;

//    }

//    public override void Notify()
//    {
        
//    }

//    /// <summary>
//    /// …Ë÷√’π æºÚΩÈø®≈∆
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="profile"></param>
//    /// <param name="atk"></param>
//    /// <param name="hp"></param>
//    public void SetCardShow(int id,int level)
//    {      
//        ClearStar(hpContent);
//        ClearStar(atkContent);
//        CardConfig cardData = CardConfigData.Instance.GetCard(id);
//        SkillConfig skillData = SkillConfigData.Instance.GetSkill(cardData.skill);
//        //if (cardData == null)
//        //    return;
//        textName.text = cardData.cardName;
//        textProfile.text = skillData.profile;

//        var sprite = ZombieCardResource.Instance.GetCardSprite(id, level);
//        zombieShow.sprite = sprite;

//        CreateStar(cardData.hpStar, cardData.hpHalfStar, star, hpContent);
//        CreateStar(cardData.atkStar, cardData.atkHalfStar, star, atkContent);
//        card.gameObject.SetActive(true);
//    }

//    ///// <summary>
//    ///// …Ë÷√ø®≈∆œ‘ æ
//    ///// </summary>
//    ///// <param name="thisCard"></param>
//    //public void SetCardShow(ZombieInfo thisCard)
//    //{
//    //    ClearStar(hpContent);
//    //    ClearStar(atkContent);
//    //    CardConfig cardData = CardConfigData.Instance.GetCard(thisCard.cardId);
//    //    SkillConfig skillData = SkillConfigData.Instance.GetSkill(cardData.skill);
//    //    //if (cardData == null)
//    //    //    return;
//    //    textName.text = cardData.cardName;
//    //    textProfile.text = skillData.profile;

//    //    var sprite = ZombieCardResource.Instance.GetCardSprite(thisCard.cardId, thisCard.level);
//    //    zombieShow.sprite = sprite;

//    //    CreateStar(cardData.hpStar, cardData.hpHalfStar, star, hpContent);
//    //    CreateStar(cardData.atkStar, cardData.atkHalfStar, star, atkContent);
//    //    card.gameObject.SetActive(true);
//    //}

//    /// <summary>
//    /// ¥¥Ω®–«–«
//    /// </summary>
//    /// <param name="starCount"></param>
//    /// <param name="halfStarCount"></param>
//    /// <param name="star"></param>
//    /// <param name="halfStar"></param>
//    /// <param name="content"></param>
//    public void CreateStar(int starCount, int halfStarCount, Transform star, Transform content)
//    {
//        for (int i = 0; i < starCount; i++)
//        {
//            var go = Instantiate(star);
//            go.SetParent(content);
//            go.gameObject.SetActive(true);
//        }

//        for (int i = 0; i < halfStarCount; i++)
//        {
//            var go = Instantiate(star);
//            go.GetComponent<Image>().color = Color.grey;
//            go.SetParent(content);
//            go.gameObject.SetActive(true);
//        }
//    }
//    /// <summary>
//    /// «Âø’–«–«
//    /// </summary>
//    /// <param name="content"></param>
//    public void ClearStar(Transform content)
//    {
//        var gos = content.GetComponentsInChildren<Image>();
//        for (int i = 0; i < gos.Length; i++)
//        {
//            Destroy(gos[i].gameObject);
//        }
//    }
//}
