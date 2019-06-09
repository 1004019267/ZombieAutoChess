//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class UI_Manager : Singleton<UI_Manager>
//{

//    private readonly Dictionary<string, GameObject> CacheMap = new Dictionary<string, GameObject>();
//    private readonly Stack<GameObject> top_list = new Stack<GameObject>();

//    /// <summary>
//    /// 生成UI对象
//    /// </summary>
//    /// <param name="path"></param>
//    /// <returns></returns>
//    static GameObject SpawnUI<T>(string path) where T : BaseUI
//    {
//        GameObject obj = Resources.Load<GameObject>("Prefabs/" + path);
//        if (obj == null)
//        {
//            throw new Exception(path + "没有");
//        }
//        if (obj.GetComponent<T>() == null)
//        {
//            addBaseUI<T>(obj);
//        }
//        Instance.CacheMap.Add(path, obj);
//        return obj;
//    }

//    public static T addBaseUI<T>(GameObject obj) where T : BaseUI
//    {
//        return obj.AddComponent<T>();
//    }

//    public static void removeBaseUI<T>(GameObject obj) where T : BaseUI
//    {
//        Destroy(obj.GetComponent<T>());
//    }

//    public   void Awake()
//    {
//        //Debug.Log(GetType() + "/Awake()");
//        GameObject _canvas = GameObject.Find("Canvas");
//        if (_canvas == null)
//        {
//            //_canvas = new GameObject("Canvas");
//            _canvas = Resources.Load<GameObject>("Prefabs/Canvas");
//            _canvas = Instantiate<GameObject>(_canvas);
//            _canvas.name = "Canvas";
//        }
//    }


//    public bool init()
//    {

//        //Debug.Log(GetType() + "/init()");
//        return true;
//    }

//    public string top_UI_Name()
//    {
//        if (top_list.Count <= 0)
//        {
//            return "";
//        }
//        return top_list.Peek().name;
//    }

//    public GameObject ShowUI<T>(string path, Transform _parent) where T : BaseUI
//    {
//        GameObject ui_obj = null;
//        if (!CacheMap.ContainsKey(path))
//        {
//            ui_obj = SpawnUI<T>(path);
//        }
//        CacheMap.TryGetValue(path, out ui_obj);
//        GameObject clone_UI = Instantiate(ui_obj, _parent);
//        //clone_UI.transform.parent = _parent;


//        clone_UI.SetActive(true);
//        clone_UI.GetComponent<T>().onEnter();
//        top_list.Push(clone_UI);
//        return clone_UI;
//    }
//    delegate bool callback();
//    public void HideUI<T>() where T : BaseUI
//    {
//        if (top_list.Count <= 0)
//        {
//            return;
//        }
//        GameObject UI_obj = top_list.Pop();
//        callback exitFunc = UI_obj.GetComponent<T>().onExit;
//        //Destroy(GameObject.Find("Canvas/" + UI_obj.name).gameObject);
//        Destroy(UI_obj);
//        exitFunc();
//    }

//    private Transform _canvas;  //画布的Transform组件
//    public Transform Canvas
//    {
//        get
//        {
//            if (_canvas == null)
//            {
//                if (GameObject.Find("Canvas") == null)
//                {
//                    gameObject.transform.SetParent(null);
//                    gameObject.SetActive(true);
//                    gameObject.name = "Canvas";

//                }
//                _canvas = GameObject.Find("Canvas").transform;
//            }
//            return _canvas;
//        }
//        set
//        {
//            _canvas = value;
//        }
//    }

//    void Update()
//    {
//        //DataMgr.getMe().Notify();
//    }
//}
