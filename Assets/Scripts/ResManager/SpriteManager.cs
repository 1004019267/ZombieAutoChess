using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class SpriteManager : Singleton<SpriteManager>
{
    public List<Sprite> sprites = new List<Sprite>();
    public void Init()
    {

    }

    public Sprite GetSprite(string name)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if (sprites[i].name == name)
            {
                return sprites[i];
            }
        }
        var sp = GameResourcesManager.GetSprite(name);
        sprites.Add(sp);
        return sp;
    }
}

