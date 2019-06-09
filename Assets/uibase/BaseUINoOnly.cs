//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ty;
//using UnityEngine;


////非唯一性uipub
//public abstract class  BaseUINoOnly:MonoBehaviour
//{
//   protected BasekeyState m_key;
   
//   /// <summary>
//   /// ui层级深度不可修改
//   /// </summary>
//   [SerializeField]
//   private int m_depth = 0;

//   public int Depth {
//      get
//      {
//         m_depth = transform.GetSiblingIndex();
//         return m_depth;
//      }
//      set
//      {
//         transform.SetSiblingIndex(value);
//         m_depth = value;
//      }
//   }
//   public static void ShowOrHide(string ui_name,bool flag,Transform parent = null)
//   {
//      if (parent == null)
//      {
//         parent = uiMgr.getMe().Canvas;
//      }
//      if (flag)
//      {
//         BaseUINoOnly _instance = uiMgr.getMe().AddNoOnlyUI(ui_name, parent).GetComponent<BaseUINoOnly>();
//         if (_instance)
//         {
//            _instance.onEnter();
//         }
//      }
//      else
//      {
//         BaseUINoOnly ui_obj = uiMgr.getMe().FindTopUI(ui_name);
//         ui_obj.onExit();
//         uiMgr.getMe().CloseOneUI(ui_name);
//      }
//   }
//   /// <summary>
//   /// 界面初始化
//   /// </summary>
//   /// <returns></returns>
//   public abstract bool onEnter();
//   /// <summary>
//   /// 界面离开
//   /// </summary>
//   /// <returns></returns>
//   public abstract bool onExit();




//}
