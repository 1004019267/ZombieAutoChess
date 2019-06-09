using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Test4 : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    void Start()
    {
        var go = BulletMgr.Instance.CreateBullet(eBulletType.SkillSplashBullet);
        go.GetComponent<SkillSplashBullet>().Init(pos1, pos2);
        //go.GetComponent<SkillBullet>().func = SecondSplash;
    }
    void SecondSplash()
    {
        logMgr.log(2);
    }
}
