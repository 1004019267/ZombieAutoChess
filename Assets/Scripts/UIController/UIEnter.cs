using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnter : MonoBehaviour {  
    void Awake()
    {
        CardConfigData.Instance.Init();
        SkillConfigData.Instance.Init();
        ZombieCardResource.Instance.Init();
        ZombieResource.Instance.Init();
        BulletResource.Instance.Init();
        TipsManager.Instance.Init(GameObject.Find("Canvas").transform);

        MapMgr.getMe().init();//
          
    }
	// Use this for initialization
	void Start () {
        Player p = new Player();
        p.id = 1;
        
        PlayerManager.Instance.AddPlayer(p);
        BattleManager.Instance.leftPlayer = p;            
        StartWndUIController.ShowOrHide(true);
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
