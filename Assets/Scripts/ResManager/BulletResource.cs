using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//无用
public class BulletResource :Singleton<BulletResource>
{
    GameObject []bullets;

    public void Init()
    {
        bullets = GameResourcesManager.GetAllBullet();
    }

    public GameObject GetBullet(string name)
    {      
        for (int i = 0; i < bullets.Length; i++)
        {
            if (name == bullets[i].name)
            {
                return bullets[i];
            }
        }
        return null;
    }
}

