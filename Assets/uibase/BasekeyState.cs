using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using LitJson;
using System;
public class BasekeyState //: MonoBehaviour
{
    public NodeInfo m_key = null; // 指向 node 的焦点
    protected List<NodeInfo> m_buttonlist = null;
    protected bool m_move = false;
    //protected ButtonAudio m_Audio;
    protected BaseUI m_gameobject = null; 
    public const KeyCode DPAD_CENTER = (KeyCode)10;     //定义确定键(适应于小米盒子遥控器)  
    public const int ok = 350;
    public const int ok1 = 10;
    public const int ok2 = 330;
    bool isok2;
    bool isok1;
    bool isok;
    bool isenter;



  static  public bool getEnterKeyDown()
    {
        bool isok2 = Input.GetKeyDown((KeyCode)ok2);
        bool isok1 = Input.GetKeyDown((KeyCode)ok1);
        bool isok = Input.GetKeyDown((KeyCode)ok);
        bool isenter = Input.GetKeyDown(KeyCode.Return);
        if (isok1 || isok2 || isok || isenter)
        {
            return true;
        }
        return false;
    }
    static public bool getEnterKeyUp()
    {
        bool isok2 = Input.GetKeyUp((KeyCode)ok2);
        bool isok1 = Input.GetKeyUp((KeyCode)ok1);
        bool isok = Input.GetKeyUp((KeyCode)ok);
        bool isenter = Input.GetKeyUp(KeyCode.Return);
        if (isok1 || isok2 || isok || isenter)
        {
            return true;
        }
        return false;
    }
    static public bool getEnterKey()
    {
        bool isok2 = Input.GetKey((KeyCode)ok2);
        bool isok1 = Input.GetKey((KeyCode)ok1);
        bool isok = Input.GetKey((KeyCode)ok);
        bool isenter = Input.GetKey(KeyCode.Return);
        if (isok1 || isok2 || isok || isenter)
        {
            return true;
        }
        return false;
    }


    public virtual void init(BaseUI gameObject)
    {
        if (m_buttonlist != null)
            return;
        m_gameobject = gameObject;
        m_gameobject.name = m_gameobject.name.Replace("(Clone)", "");
      
        m_buttonlist = new List<NodeInfo>();
       // m_Audio = gameObject.AddComponent<ButtonAudio>();
        //for (int i = 0; i < gameObject.GetComponentsInChildren<Button>().Length; i++)
        //{
            
        //    NodeInfo node = new NodeInfo();
        //    node.node = gameObject.GetComponentsInChildren<Button>()[i];
        //    if (node.node.transform.Find("ui_texiao"))
        //    {
        //        GameObject img = node.node.transform.Find("ui_texiao").gameObject;
        //        if (img != null)
        //        {
        //            img.SetActive(false);
        //        }
        //    }
        //    node.id = i;
        //    node.type = 0;
        //    node.m_normal = node.node.image.sprite;
        //    node.m_hight = node.node.spriteState.highlightedSprite;
        //    node.m_press = node.node.spriteState.pressedSprite;
        //    m_buttonlist.Add(node);
        //}
        //if (m_buttonlist.Count > 0)
        //{
        //    m_key = m_buttonlist[0];
        //    m_key.node.image.sprite = m_key.m_hight;
        //    if (m_key.node.transform.Find("ui_texiao"))
        //    {
        //        GameObject imag = m_key.node.transform.Find("ui_texiao").gameObject;
        //        if (imag != null)
        //        {
        //            imag.SetActive(true);
        //        }
        //    }
        //}
    }

    /// <summary>
    /// 查找所有btn
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public bool AddAllBtn(GameObject root)
    {
        if (m_buttonlist == null)
        {
            m_buttonlist = new List<NodeInfo>();
        }
        FindAllBtn(root,  this.m_buttonlist);
        return true;
    }
    
      void FindAllBtn(GameObject root , List<NodeInfo> buffmap)
    {
        if (!root)
        {
            return  ;
        }
        Button btn = root.GetComponent<Button>();
        if ( btn)
        {
            NodeInfo node = new NodeInfo();
            node.node = btn;
            if (node.node.transform.Find("ui_texiao"))
            {
                GameObject img = node.node.transform.Find("ui_texiao").gameObject;
                if (img != null)
                {
                    img.SetActive(false);
                }
            }
            node.id = buffmap.Count+1;
            node.type = 0;
            node.m_normal = node.node.image.sprite;
            node.m_hight = node.node.spriteState.highlightedSprite;
            node.m_press = node.node.spriteState.pressedSprite;
            buffmap.Add(node);
        }
        for (int i = 0; i < root.transform.GetChildCount(); i++)
        {
            GameObject child = root.transform.GetChild(i).gameObject;
            if (child)
            {
                FindAllBtn(child, buffmap);
            }
        }
    }
    public delegate void inputEvent();
    public delegate void inputEvent1(Button obj);


    /// <summary>
    /// 绑定Button事件
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="fun"></param>
    public void registerCallBack(Button obj, inputEvent fun)
    {

        if(obj == null)
        {
            logMgr.log("registerCallBack error");
            return;
        }

        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (obj.name == m_buttonlist[i].getName())
            {
                    m_buttonlist[i].func = fun;
                logMgr.log("name 重复：registerCallBack error" + obj.name);
                return;
            }
        }


         

        NodeInfo node = new NodeInfo();
        node.node = obj;
        node.func = fun;
        if (node.node.transform.Find("ui_texiao"))
        {
            GameObject img = node.node.transform.Find("ui_texiao").gameObject;
            if (img != null)
            {
                img.SetActive(false);
            }
        }
        node.id = m_buttonlist.Count + 10;
        node.type = 0;
        node.m_normal = node.node.image.sprite;
        node.m_hight = node.node.spriteState.highlightedSprite;
        node.m_press = node.node.spriteState.pressedSprite;
        m_buttonlist.Add(node);
    
        if (m_buttonlist.Count > 0)
        {
            m_key = m_buttonlist[0];
            //m_key.node.image.sprite = m_key.m_hight;
            //if (m_key.node.transform.Find("ui_texiao"))
            //{
            //    GameObject imag = m_key.node.transform.Find("ui_texiao").gameObject;
            //    if (imag != null)
            //    {
            //        imag.SetActive(true);
            //    }
            //}
        }


   


    }

 

    /// <summary>
    /// 销毁button
    /// </summary>
    /// <param name="obj"></param>
    public void unregBut(Button obj) {


       
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (obj.name == m_buttonlist[i].getName())
            {
                m_buttonlist.RemoveAt(i);
                return;
            }
        }


    }

   
    public bool registerCallBack(Button obj, inputEvent1 fun)
    {
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (obj.name == m_buttonlist[i].getName())
            {
                    m_buttonlist[i].func1 = fun;
                    return false;
            }
        }


        NodeInfo node = new NodeInfo();
        node.node = obj;
        node.func1 = fun;

        if (node.node.transform.Find("ui_texiao"))
        {
            GameObject img = node.node.transform.Find("ui_texiao").gameObject;
            if (img != null)
            {
                img.SetActive(false);
            }
        }
        node.id = m_buttonlist.Count + 10;
        node.type = 0;
        node.m_normal = node.node.image.sprite;
        node.m_hight = node.node.spriteState.highlightedSprite;
        node.m_press = node.node.spriteState.pressedSprite;
        m_buttonlist.Add(node);

        if (m_buttonlist.Count > 0)
        {
            m_key = m_buttonlist[0];
            //m_key.node.image.sprite = m_key.m_hight;
            //if (m_key.node.transform.Find("ui_texiao"))
            //{
            //    GameObject imag = m_key.node.transform.Find("ui_texiao").gameObject;
            //    if (imag != null)
            //    {
            //        imag.SetActive(true);
            //    }
            //}
        }

        return true;
    }





    ///
    public void RegisterOnKeyChangedEvent(Button btn, NodeInfo.OnKeyChanged enterEvent = null, NodeInfo.OnKeyChanged exitEvent = null)
    {
       

        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (btn.name == m_buttonlist[i].getName())
            {
                if (enterEvent != null)
                {
                    m_buttonlist[i].OnEnterKey += enterEvent;
                }
                if (exitEvent != null)
                {
                    m_buttonlist[i].OnExitKey += exitEvent;
                }
            }
        }
    }
    /// <summary>
    /// 设置当前Button为焦点
    /// </summary>
    /// <param name="node"></param>
    public void setCurKey(Button node)
    {
        setCurKey(node,true);
    }

    public void setCurKey(Button node, bool flag = true)
    {
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (node == m_buttonlist[i].node)
            {
                m_key = m_buttonlist[i];
                m_move = flag;
            }
        }
    }


    public void setCurKey(string name)
    {
        setCurKey(name, true);
    }

    public void setCurKey(string name, bool flag = true)
    {
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (name == m_buttonlist[i].getName())
            {
                m_key = m_buttonlist[i];
                m_move = true;
            }
        }
    }

    protected virtual bool left()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
            if (m_key.node.transform.position.x < m_buttonlist[i].node.transform.position.x)
            {
                continue;
            }
            if (m_buttonlist[i].node.interactable == false)
            {
                continue;
            }
            Vector2 initpos = (m_key.node.transform.position - m_buttonlist[i].node.transform.position);
            float angle = Vector2.Angle(initpos, Vector2.left);
            if (angle > 140 && angle <= 180)

            {
                float dis = Vector2.Distance(m_buttonlist[i].node.transform.position, m_key.node.transform.position);
                if (!arr.ContainsKey(dis))
                {
                    arr.Add(dis, m_buttonlist[i]);
                }
            }
        }
        if (arr.Count <= 0)
        {
            return false;
        }
        m_key.ExitKey(m_key.node);
        float first_dis = arr.Keys.First();
        m_key = arr[first_dis];
        foreach (float dis in arr.Keys)
        {
            if (first_dis > dis)
            {
                first_dis = dis;
                m_key = arr[dis];
            }
        }
        m_key.EnterKey(m_key.node);
        return true;
    }

    protected virtual bool right()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
            if (m_key.node.transform.position.x > m_buttonlist[i].node.transform.position.x)
            {
                continue;
            }
            if (m_buttonlist[i].node.interactable==false)
            {
                continue;
            }
            float angle = Vector2.Angle((m_key.node.transform.position - m_buttonlist[i].node.transform.position), Vector2.right);
            if (angle > 140 && angle <= 180)
            {
                float dis = Vector2.Distance(m_buttonlist[i].node.transform.position, m_key.node.transform.position);
                if (!arr.ContainsKey(dis))
                {
                    arr.Add(dis, m_buttonlist[i]);
                }
            }
        }
        if (arr.Count <= 0)
        {
            return false;
        }
        m_key.ExitKey(m_key.node);
        float first_dis = arr.Keys.First();
        m_key = arr[first_dis];
        foreach (float dis in arr.Keys)
        {
            if (first_dis > dis)
            {
                first_dis = dis;
                m_key = arr[dis];
            }
        }
        m_key.EnterKey(m_key.node);
        return true;
    }

    protected virtual bool up()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
            if (m_key.node.transform.position.y > m_buttonlist[i].node.transform.position.y)
            {
                continue;
            }
            if (m_buttonlist[i].node.interactable == false)
            {
                continue;
            }
            float angle = Vector2.Angle((m_key.node.transform.position - m_buttonlist[i].node.transform.position), Vector2.up);
            if (angle > 140 && angle <= 180)
            {
                float dis = Vector2.Distance(m_buttonlist[i].node.transform.position, m_key.node.transform.position);
                if (!arr.ContainsKey(dis))
                {
                    arr.Add(dis, m_buttonlist[i]);
                }
            }
        }
        if (arr.Count <= 0)
        {
            return false;
        }
        m_key.ExitKey(m_key.node);
        float first_dis = arr.Keys.First();
        m_key = arr[first_dis];
        foreach (float dis in arr.Keys)
        {
            if (first_dis > dis)
            {
                first_dis = dis;
                m_key = arr[dis];
            }
        }

        m_key.EnterKey(m_key.node);
        return true;
    }


    protected virtual bool down()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
            if (m_key.node.transform.position.y < m_buttonlist[i].node.transform.position.y)
            {
                continue;
            }
            if (m_buttonlist[i].node.interactable == false)
            {
                continue;
            }
            float angle = Vector2.Angle((m_key.node.transform.position - m_buttonlist[i].node.transform.position), Vector2.down);
            if (angle > 140 && angle <= 180)
            {
                float dis = Vector2.Distance(m_buttonlist[i].node.transform.position, m_key.node.transform.position);
                if (!arr.ContainsKey(dis))
                {
                    arr.Add(dis, m_buttonlist[i]);
                }
            }
        }
        if (arr.Count <= 0)
        {
            return false;
        }
        m_key.ExitKey(m_key.node);
        float first_dis = arr.Keys.First();
        m_key = arr[first_dis];
        foreach (float dis in arr.Keys)
        {
            if (first_dis > dis)
            {
                first_dis = dis;
                m_key = arr[dis];
            }
        }
        m_key.EnterKey(m_key.node);
        return true;
    }

    protected virtual void loop()
    {
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_key.node == m_buttonlist[i].node && m_key.node != null && m_buttonlist[i] != null)
            {
                m_key.node.image.sprite = m_key.m_hight;
                if (m_key.node.transform.Find("ui_texiao"))
                {
                    GameObject img = m_key.node.transform.Find("ui_texiao").gameObject;
                    if (img != null)
                    {
                        img.SetActive(true);
                    }
                }
            }
            else
            {
                m_buttonlist[i].node.image.sprite = m_buttonlist[i].m_normal;
                if (m_buttonlist[i].node.transform.Find("ui_texiao"))
                {
                    GameObject img = m_buttonlist[i].node.transform.Find("ui_texiao").gameObject;
                    if (img != null)
                    {
                        img.SetActive(false);
                    }
                }
            }
        }




    }
    public virtual void update()
    {


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //  
            m_gameobject.onBack();
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            m_move = true;
            //   m_Audio.OnkeyUp();

            m_gameobject.Left();
            up();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_move = true;
            //   m_Audio.OnkeyUp();
            m_gameobject.Down();
            down();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_move = true;
            //    m_Audio.OnkeyUp();
            m_gameobject.Left();
            left();

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_move = true;
            //   m_Audio.OnkeyUp();
            m_gameobject.Up();
            right();
        }
        isok2 = Input.GetKeyDown((KeyCode)ok2);
        isok1 = Input.GetKeyDown((KeyCode)ok1);
        isok = Input.GetKeyDown((KeyCode)ok);
        isenter = Input.GetKeyDown(KeyCode.Return);

        if (isok1 || isok2 || isok || isenter)
        {
            if (m_key.func != null)
                m_key.func();
            if (m_key.func1 != null)
                m_key.func1(m_key.node);
            // m_Audio.OnkeyUp();
            m_gameobject.Ok();
        }
        if (!m_move)
        {
            return;
        }
        loop();

    }


    public class NodeInfo
    {
        public int id = 0;
        public Button node = null;
        public Vector2 pianchapos;
        public Sprite m_normal;
        public Sprite m_hight;
        public Sprite m_press;


        public int type = 0;
        public int Movetype = 0;
        public int left_pos = 0;
        public int right_pos = 0;
        public int up_pos = 0;
        public int down_pos = 0;
        public BasekeyState.inputEvent func;
        public BasekeyState.inputEvent1 func1;
        public delegate void OnKeyChanged(Button btn);

        public event OnKeyChanged OnEnterKey;
        public event OnKeyChanged OnExitKey;



        public NodeInfo()
        {

        }

        public void EnterKey(Button btn)
        {
            if (OnEnterKey != null)
            {
                OnEnterKey(btn);
            }
        }
        public void ExitKey(Button btn)
        {
            if (OnExitKey != null)
            {
                OnExitKey(btn);
            }
        }
        public Button getButton()
        {
            if (type == 0)
            {
                return node;
            }
            return null;

        }

        public string getName()
        {
            return node.name;
        }

        public Button getChexButton()
        {
            if (type == 1)
            {
                return node;
            }
            return null;
        }

        public Button getNode()
        {
            return node;
        }


    }

}


public class BasekeyState_config : BasekeyState
{

    public JsonData key_config;
    public override void init(BaseUI gameobj)
    {
        if( m_buttonlist != null )
        {
            return;
        }

        m_gameobject = gameobj;
        m_gameobject.name = m_gameobject.name.Replace("(Clone)", "");
        key_config = new JsonData();
        m_buttonlist = new List<NodeInfo>();
        string json1 = IOHelper.LoadJson(Application.streamingAssetsPath +  "/JsonData/key_config.json");
        if (json1 != null)
        {
            key_config = JsonMapper.ToObject(json1);
        }
        else
        {
            Debug.Log("没有key_config.json");
        }
    }



    protected override bool left()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
        
            var left = key_config[m_gameobject.name][m_key.node.name]["left"];
            if (left != null)
            {

                if (key_config[m_gameobject.name][m_key.node.name]["left"].Equals(m_buttonlist[i].node.name))
                {
                    m_key = m_buttonlist[i];
                }
            }
        }
        return true;
    }

    protected override bool right()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
            var right = key_config[m_gameobject.name][m_key.node.name]["right"];
            if (right != null)
            {
                if (key_config[m_gameobject.name][m_key.node.name]["right"].Equals(m_buttonlist[i].node.name))
                {
                    m_key = m_buttonlist[i];
                }
            }
        }
        return true;
    }

    protected override bool up()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
            var up = key_config[m_gameobject.name][m_key.node.name]["up"];
            if (up != null)
            {
                if (key_config[m_gameobject.name][m_key.node.name]["up"].Equals(m_buttonlist[i].node.name))
                {
                    m_key = m_buttonlist[i];
                }
            }
        }
        return true;
    }
    protected override bool down()
    {
        Dictionary<float, NodeInfo> arr = new Dictionary<float, NodeInfo>();
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_buttonlist[i] == m_key)
            {
                continue;
            }
            if (m_buttonlist[i].node.IsActive() == false)
            {
                continue;
            }
            var down = key_config[m_gameobject.name][m_key.node.name]["down"];
            if (down != null)
            {
                if (key_config[m_gameobject.name][m_key.node.name]["down"].Equals(m_buttonlist[i].node.name))
                {
                    m_key = m_buttonlist[i];
                }
            }
        }
        return true;
    }

}

