using System.Collections.Generic;

//
public class Player : BaseRole
{

    public ZombieMgr _ZombieMgr;// 玩家创建的僵尸

    /// <summary>
    /// 阵营
    /// </summary>
    public eCamp camp; 

    public int id;
    /// <summary>
    /// 金币
    /// </summary>
    float gold;

   // private SkillSystem _skillSystem;
    private StateSystem _stateSystem;


    //
    //Button  
    //spine 


    /// <summary>
    /// 绑定观察者事件
    /// </summary>
    public void Init()
    {
    
        _ZombieMgr = new ZombieMgr(this);
        

        // NotificationCenter.DefaultCenter().PostNotification(this, "SetGold", this);
        EventManager.getMe().GoldAdd += this.AddGold;
        EventManager.getMe().GoldRemove += this.RemoveGold;
    }

    public void DestroyEvent()
    {
        EventManager.getMe().GoldAdd -= this.AddGold;
        EventManager.getMe().GoldRemove -= this.RemoveGold;
    }
    //public SkillSystem skillSystem
    //{
    //    get { return _skillSystem; }
    //}

    public StateSystem stateSystem
    {
        get { return _stateSystem; }
    }
    /// <summary>
    /// 根据id从自己仓库中获得僵尸
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
   
    /// <summary>
    /// 添加金币
    /// </summary>
    /// <param name="money"></param>
    public void AddGold(float money)
    {
        gold += money;
        //if (uiMgr.getMe().FindUI(StartWndUIController.ui_name) != null)
        //{
            BattleWndUIController.getMe().SetGold(gold);
       // }
    }
    /// <summary>
    /// 减少金币
    /// </summary>
    /// <param name="money"></param>
    public void RemoveGold(float money)
    {
        gold -= money;
        BattleWndUIController.getMe().SetGold(gold);
    }

    public void ClearGold()
    {
        gold = 0;
    }
    /// <summary>
    /// 获取当前金币数
    /// </summary>
    /// <returns></returns>
    public float GetGold()
    {
        return gold;
    }

    protected override void Start()
    {
      //  _skillSystem = new SkillSystem(this.gameObject);
        _stateSystem = new StateSystem(this.gameObject);
        //SkillInstance _instance = _skillSystem.Create(eSkillType.Normal, transform);
        //_skillSystem.Add(_instance);
    }

    protected override void Loop()
    {
        if (_stateSystem != null)
        {
            _stateSystem.Loop();
        }
    }
}