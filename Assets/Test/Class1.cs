using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class Class1:MonoBehaviour
{
    public Button btn;
    public BasekeyState _key;
   private void Start()
    {

        List<test> _test = GameConfigDataBase.GetConfigDatas<test>();
        for (int i = 0; i < _test.Count; i++)
        {
            Debug.Log(_test[i].name);
            Debug.Log(_test[i].age);
            Debug.Log(_test[i].level);
        }
        btn = this.transform.Find("Button").GetComponent<Button>();
        _key = this.GetComponent<BasekeyState>();
        
        _key.registerCallBack(btn,this.test);
    }

    void test(Button obj)
    {
        Debug.Log("test" + obj.name);
    }

}

