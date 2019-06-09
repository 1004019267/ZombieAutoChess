using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
class BuyChildKey:BasekeyState
{

    private List<Button> m_list = new List<Button>();//需要特殊处理的btn
    
    public void addactive( Button btn)
    {
        if (m_list.Contains(btn))
        {
            m_list.Remove(btn);
            m_list.Add(btn);
        }else
        {
            m_list.Add(btn);
        }
    }

    public bool removeactive( Button btn)
    {
        bool flag = false;
        if (m_list.Contains(btn))
        {
            flag = m_list.Remove(btn);
        }
        return flag;
    }




    protected override void loop()
    {
        //ebug.Log(m_move);
       
        for (int i = 0; i < m_buttonlist.Count; i++)
        {
            if (m_key.node == m_buttonlist[i].node && m_key.node != null && m_buttonlist[i] != null)
            {
                //m_key.node.image.sprite = m_key.m_hight;
                for (int j = 0; j < m_list.Count; j++)
                {
                    //当前特殊按钮触发
                    if (m_key.node == m_list[j])
                    {
                        //m_key.node.transform.Find("Text").GetComponent<Text>().color = Color.white;
                        //m_key.node.transform.Find("BG/sprite").GetComponent<Image>().color = Color.white;
                        //BuyWndUIController _baseui = this.m_gameobject as BuyWndUIController;
                        //_baseui.SetCardShow(Intercept.Instance.GetIdForBuyName(m_key.node.name));
                    }
                }

               

            }
            else
            {
                //  m_buttonlist[i].node.image.sprite = m_buttonlist[i].m_normal;
                for (int j = 0; j < m_list.Count; j++)
                {
                    //不是当前按钮 在特殊按钮
                    if (m_buttonlist[i].node == m_list[j] && m_buttonlist[i].node != m_key.node)
                    {
                        //m_list[j].transform.Find("Text").GetComponent<Text>().color = Color.grey;
                        //m_list[j].transform.Find("BG/sprite").GetComponent<Image>().color = Color.gray;
                    }
                }
            }
        }




        // m_move = false;
}

}
