using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class ZombieCardResource : Singleton<ZombieCardResource>
{
    Sprite[] zombieCardSprite;

    public void Init()
    {
        zombieCardSprite = GameResourcesManager.GetAllZombieCardSprite();
    }

    public Sprite GetCardSprite(int id,int level)
    {
        var spriteId = "zombie" + id + "-" + level;
        for (int i = 0; i < zombieCardSprite.Length; i++)
        {
            if (spriteId==zombieCardSprite[i].name)
            {
                return zombieCardSprite[i];
            }
        }
        return null;
    }
}

