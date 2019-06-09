using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
//using UnityEditor;
namespace ty
{
    public class serverMgr : Singleton<serverMgr>
    {

        // Use this for initialization
        //
        void Start()
        {
            initMsg();
            //连接大厅
            tyInterface.getMe().connectLoddy( tyConofig.g_ty_gameid , tyConofig.g_ty_loddyServerIp, tyConofig.g_ty_loddyServerPort ); //
            //EditorApplication.playmodeStateChanged += () => {
            //    tyInterface.getMe().outRoom();
            //};
        }

        private void OnDestroy()
        {
            tyInterface.getMe().outRoom();
        }

        void initMsg()
        {
            tyInterface.getMe().regmsg(msgRoomSendDataToOther.getMe());
            tyInterface.getMe().regmsg(msgRoomSendKeyToOther.getMe());
            tyInterface.getMe().regmsg(msgRoomSyncObject.getMe());
            tyInterface.getMe().regmsg(msgRoomSyncObject_ctoc.getMe());
            tyInterface.getMe().regmsg(msgRoomCreateObject.getMe());
            tyInterface.getMe().regmsg(msgRoomStartGame.getMe());
            tyInterface.getMe().regmsg(msgRoomGameOver.getMe());
           tyInterface.getMe().regmsg(msgRoomCreatescene.getMe());
           tyInterface.getMe().regmsg(msgRoomDeleteObject.getMe());
           tyInterface.getMe().regmsg(msgRoomPhysicSyncObject.getMe());
           tyInterface.getMe().regmsg(msgRoomPlayerSyncObject.getMe());
        
           tyInterface.getMe().regmsg(msgRoomSnycAllObj.getMe());
           tyInterface.getMe().regmsg(msgCheckScene.getMe());

            //注册服务器协议
            tyInterface.getMe().regservermsg(msgServerRoomSendDataToOther.getMe());
            tyInterface.getMe().regservermsg(msgServerRoomSendKeyToOther.getMe());
            tyInterface.getMe().regservermsg(msgServerRoomSyncObject.getMe());
            tyInterface.getMe().regservermsg(msgServerRoomSyncObject_ctoc.getMe());
            
            tyInterface.getMe().regservermsg(msgServerRoomCreateObject.getMe());
            tyInterface.getMe().regservermsg(msgServerRoomStartGame.getMe());
            tyInterface.getMe().regservermsg(msgServerRoomGameOver.getMe());
            tyInterface.getMe().regservermsg(msgServerRoomCreatescene.getMe());
            tyInterface.getMe().regservermsg(msgServerRoomDeleteObject.getMe());
            tyInterface.getMe().regservermsg(msgServerCheckScene.getMe());
            tyInterface.getMe().regservermsg(msgServerPhysicSyncObj.getMe());
            tyInterface.getMe().regservermsg(msgServerPlayerSyncObject.getMe());


            tyInterface.getMe().regservermsg(msgServerRoomSnycAllObj.getMe());



        }

        private void FixedUpdate()
        {
            float t = Time.deltaTime;
            tyInterface.getMe().loop(t); //
        }
        // Update is called once per frame
        void Update()
        {
            serverLogic.getMe().loop();
            Update_test();
        }


        //测试代码
        void Update_test()
        {
           
        
            if (Input.GetKey(KeyCode.P))
            {
                tyInterface.getMe().jointRoom();
                return;
            }
            if (Input.GetKey(KeyCode.C))
            {
                GameObject tongbu1 = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/scene/tongbu"));
                tongbu1.transform.SetParent(null);
                baseSyncObject _baseSyncObject = tongbu1.GetComponent<baseSyncObject>();
                _baseSyncObject.init(msgRoomCreateObject.getMe().createObjectId());
                return;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {


                GameObject tongbua = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/scene/Sphere"));
                baseSyncObjectPhyObj _baseSyncObject = tongbua.GetComponent<baseSyncObjectPhyObj>();
                //_baseSyncObject.init(msgRoomCreateObject.getMe().createObjectId());

            }

            if (GameObject.Find("Sphere(Clone)"))
            {
                baseSyncObjectPhyObj tongbu = GameObject.Find("Sphere(Clone)").GetComponent<baseSyncObjectPhyObj>();
                if (Input.GetKey(KeyCode.UpArrow))
                {

                    tongbu._input = Vector2.up;
                }

                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    tongbu._input = Vector2.down;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    tongbu._input = Vector2.left * 100;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    tongbu._input = Vector2.right * 100;
                }
                else
                {
                    tongbu._input = Vector2.zero;
                }
            }



        }


    }
}

