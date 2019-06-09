using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Spine;
using Spine.Unity;
class ZombieResource : Singleton<ZombieResource>
{
    GameObject[] zombies;
    //SkeletonDataAsset[] zombieSpines;
    public void Init()
    {
        zombies = GameResourcesManager.GetAllZombiePrefab();
        //zombieSpines = GameResourcesManager.GetAllZombieSpine();
        //var go = GameObject.Find("Spine").GetComponent<SkeletonGraphic>();
        //go.skeletonDataAsset = zombieSpines[0];
       

        //go.AnimationState.SetAnimation(0, "zhanli", false);
        //go.startingAnimation="zhanli";

    }

    public GameObject GetZombie(int id, int level)
    {
        var spriteId = "zombie" + id + "_" + level;
        for (int i = 0; i < zombies.Length; i++)
        {
            if (spriteId == zombies[i].name)
            {
                return zombies[i];
            }
        }
        return null;
    }

    //public SkeletonDataAsset GetZombieSpine(int id, int level)
    //{
    //    var spriteId = "zombieSpine" + id + "_" + level;
    //    for (int i = 0; i < zombieSpines.Length; i++)
    //    {
    //        if (spriteId == zombieSpines[i].name)
    //        {
    //            return zombieSpines[i];
    //        }
    //    }
    //    return null;
    //}
}

