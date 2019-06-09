using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ty;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class uiMgr : Singleton<uiMgr>
{
    //UI实例缓存列表
    private readonly gameMap<string, BaseUI> m_UiMap = new gameMap<string, BaseUI>();
    //非唯一性ui实例化集合
    //private readonly Stack<BaseUINoOnly> m_tipUIList = new Stack<BaseUINoOnly>();

    //ui名称
    private readonly Stack<string> top_baseUi = new Stack<string>();
    
    //public const bool ischuangtou = false;//是否可以事件穿透
    private BaseUI _topUiBase = null;//当前UI
    
    // 
    public Stack<string> Top_baseUiList
    {
        get { return top_baseUi; }
    }



    private Transform _canvas;  //画布的Transform组件
    public   void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //Debug.Log(GetType() + "/Awake()");
        GameObject l_canvas = GameObject.Find("Canvas");
        if (l_canvas == null)
        {
            //_canvas = new GameObject("Canvas");
            l_canvas = Resources.Load<GameObject>("Prefabs/Canvas");
            l_canvas = Instantiate<GameObject>(l_canvas);
            l_canvas.name = "Canvas";
            _canvas = l_canvas.transform;

        }
    }


    //当前最高层界面
    public BaseUI getTopUi()
    {
        return _topUiBase;
    }

    /// <summary>
    /// 关闭所有UI界面
    /// </summary>
    public void shundownAllUI()
    {
        top_baseUi.Clear();
        for (int i = 0; i < m_UiMap.Count; i++)
        {
            BaseUI ui_value = m_UiMap.getDataByIndex(i);
            Destroy(ui_value.gameObject);
        }
        m_UiMap.Clear();
    }

  
    
    /// <summary>
    /// 添加UI
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="_parent"></param>
    /// <returns></returns>
   public  BaseUI AddUI(string uiName,Transform _parent = null)
    {
        BaseUI _BaseUI = null;
        if (!m_UiMap.ContainsKey(uiName))
        {
            GameObject ui_obj = SpwanUI(uiName,_parent);
            _BaseUI = ui_obj.GetComponent<BaseUI>();
            m_UiMap.AddOrReplace(uiName, _BaseUI);
            //只有界面被创建的时候才初始化
          
        }
        else
        {
            _BaseUI = m_UiMap.getDataByKey(uiName);
        }
        if (!top_baseUi.Contains(uiName))
        {
            top_baseUi.Push(uiName);
        }
        updateBaseUi_OTT_key();
        return _BaseUI;
    }

    //设置ui最高级
    public void setTopUI( string name )
    {
        BaseUI _BaseUI =this.m_UiMap.getDataByKey(name);

        if (_BaseUI == null)
            return;

        BaseUI _BaseUIgetTopUi = this.getTopUi();
        if (_BaseUIgetTopUi == null)
            return;

        if (_BaseUIgetTopUi == _BaseUI)
            return;

        _BaseUI.Depth = _BaseUIgetTopUi.Depth+1;

        updateBaseUi_OTT_key();


    }
    /// <summary>
    /// 设置最小层级
    /// </summary>
    /// <param name="name"></param>
    public void setDownUI(string name)
    {

        BaseUI _BaseUI = this.m_UiMap.getDataByKey(name);
        if (_BaseUI == null)
            return;
        BaseUI buttomUI = this.m_UiMap.getDataByIndex(0);
        for (int i = 0; i < this.m_UiMap.Count; i++)
        {

            BaseUI _BaseUIchild = this.m_UiMap.getDataByIndex(i);
            if (buttomUI.Depth > _BaseUIchild.Depth)
            {
                buttomUI = _BaseUIchild;
            }
            _BaseUIchild.Depth++;
        }
        _BaseUI.Depth = buttomUI.Depth - 1;

        updateBaseUi_OTT_key();
    }


    void updateBaseUi_OTT_key() {
       int toplay = -100000;
       this._topUiBase = null;
       if (this.m_UiMap.Count == 0) {
           return;
       }
       if (this.m_UiMap.Count == 1 ) {
           BaseUI it = this.m_UiMap.getDataByIndex(0);
           if (it != null) {
               this._topUiBase = it.GetComponent<BaseUI>();
           }
       }
       for (int i = 0; i < this.m_UiMap.Count; ++i) {
           BaseUI l_uibase = this.m_UiMap.getDataByIndex(i).GetComponent<BaseUI>();
           if (l_uibase.Depth > toplay) {
               toplay = l_uibase.Depth;
               this._topUiBase = l_uibase;
           }
       }
    }
    /// <summary>
    /// 生成UI
    /// </summary>
    /// <param name="ui_name"></param>
    /// <returns></returns>
     public static GameObject SpwanUI(string uiname,Transform parent)
    {
        GameObject pre_obj = GameResourcesManager.GetUIPrefab(uiname);
        GameObject clone = GameObject.Instantiate(pre_obj, parent);
        return clone;
    }
    

    /// <summary>
    /// 隐藏最顶层ui
    /// </summary>
   public  void HideUI()
    {
        if (top_baseUi.Count <= 0)
        {
            return;
        }
        string ui_name = top_baseUi.Peek();
        BaseUI obj = m_UiMap.getDataByKey(ui_name);
        if (obj)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void closeTopUI()
    {
        if (top_baseUi.Count > 1)
        {
            //string name = top_baseUi.Pop();
            //BaseUI _baseUi = FindUI(name);
            //if (_baseUi)
            //{
            //    Destroy(FindUI(name).gameObject);
            //    m_UiMap.Remove(name);
            //}
            //if (top_baseUi.Count > 0)
            //{
            //  BaseUI obj =  AddUI(top_baseUi.Peek(),_canvas);
            //}

            BaseUI _baseUi = this.getTopUi();
            if (_baseUi)
            {
                //string name = _baseUi.ui_name;
                //m_UiMap.Remove(name);
                //Destroy(_baseUi.gameObject);
                BaseUI.ShowOrHide(_baseUi.ui_name,false,null);

            }
            updateBaseUi_OTT_key();
        }
    }
    

    /// <summary>
    /// 隐藏所有UI
    /// </summary>
    public void HideAllUI()
    {
        for (int i = 0; i < m_UiMap.Count; i++)
        {
            BaseUI ui_value = m_UiMap.getDataByIndex(i);
            ui_value.gameObject.SetActive(false);
        }
    }

    public BaseUI FindUI(string _name)
    {
        if (m_UiMap.ContainsKey(_name))
        {
            return m_UiMap.getDataByKey(_name);
        }
        return null;
    }


    public void CloseUI(string ui_name)
    {
        if (m_UiMap.ContainsKey(ui_name))
        {
             m_UiMap.Remove(ui_name);
        }
        if (top_baseUi.Contains(ui_name))
        {
          List<string> list = top_baseUi.ToList();
          list.Remove(ui_name);
        }
        updateBaseUi_OTT_key();
    }
    
    /// <summary>
    /// 发送消息通知UI刷新
    /// </summary>
    /// <param name="uiname"></param>
    //public void sendMsgByName(string uiname)
    //{
    //    if (m_UiMap.ContainsKey(uiname))
    //    {
    //        BaseUI _ui = m_UiMap.getDataByKey(uiname).GetComponent<BaseUI>();
    //        _ui.Notify();
    //    }
    //}
    
    
    
    
    public bool init()
    {

        //Debug.Log(GetType() + "/init()");
        return true;
    }

  


 
    public Transform Canvas
    {
        get
        {
            if (_canvas == null)
            {
                if (GameObject.Find("Canvas") == null)
                {
                    gameObject.transform.SetParent(null);
                    gameObject.SetActive(true);
                    gameObject.name = "Canvas";

                }
                _canvas = GameObject.Find("Canvas").transform;
            }
            return _canvas;
        }
        set
        {
            _canvas = value;
        }
    }


    
    void Update()
    {
       
        //if (!ischuangtou)
        //{
        //    //updateBaseUi_OTT_key();
        //}
        //else
        //{
            BaseUI _BaseUI = this.getTopUi();
            if(_BaseUI)
            {
                _BaseUI.GetBasekeyState().update();
            }

            //for (int i = 0; i < m_UiMap.Count; i++)
            //{
            //    BasekeyState _key = m_UiMap.getDataByIndex(i).GetBasekeyState();
            //    if (_key != null)
            //    {
            //        _key.update();
            //    }
            //}
        //}
    }
}
