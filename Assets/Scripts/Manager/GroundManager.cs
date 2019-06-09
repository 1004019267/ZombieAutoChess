using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class Ground
{
    Button btn;
    bool isShelter;//是否有人
}

//存储所有地板信息

class GroundManager : Singleton<GroundManager>
{
    Button[] allGroundBtns;
    /// <summary>
    /// 二维数组对应编号对应地板名字
    /// </summary>
    public Button[,] btns = new Button[10, 5];
    /// <summary>
    /// 左地板
    /// </summary>
    public List<Button> leftBtns = new List<Button>();
    /// <summary>
    /// 右地板
    /// </summary>
    public List<Button> rightBtns = new List<Button>();
    /// <summary>
    /// 一维数组转二维递增转换参数
    /// </summary>
    int num = 0;
    /// <summary>
    /// 临时坐标
    /// </summary>
    public void Init(Button[] allGroundBtn)
    {
        this.allGroundBtns = allGroundBtn;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                btns[x, y] = allGroundBtn[num];
                num++;
                if (x < 5)
                {
                    if (!leftBtns.Contains(btns[x, y]))
                    {
                        leftBtns.Add(btns[x, y]);
                    }
                }
                else
                {
                    if (!rightBtns.Contains(btns[x, y]))
                    {
                        btns[x, y].interactable = false;
                        rightBtns.Add(btns[x, y]);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 从Pos获取地板
    /// </summary>
    /// <param name="name"></param>
    public Button GetGround(Pos pos)//获取地板信息
    {
        return btns[pos.x, pos.y];
    }

    public void ClearNum()

    {
        num = 0;
    }
    /// <summary>
    /// 激活所有btn
    /// </summary>
    public void ResetGround()
    {
        for (int i = 0; i < leftBtns.Count; i++)
        {
            leftBtns[i].interactable = true;
        }
    }
}

