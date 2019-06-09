using System.Collections.Generic;

//
public class Player : BaseRole
{

    public ZombieMgr _ZombieMgr;// ��Ҵ����Ľ�ʬ

    /// <summary>
    /// ��Ӫ
    /// </summary>
    public eCamp camp; 

    public int id;
    /// <summary>
    /// ���
    /// </summary>
    float gold;

   // private SkillSystem _skillSystem;
    private StateSystem _stateSystem;


    //
    //Button  
    //spine 


    /// <summary>
    /// �󶨹۲����¼�
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
    /// ����id���Լ��ֿ��л�ý�ʬ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
   
    /// <summary>
    /// ��ӽ��
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
    /// ���ٽ��
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
    /// ��ȡ��ǰ�����
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