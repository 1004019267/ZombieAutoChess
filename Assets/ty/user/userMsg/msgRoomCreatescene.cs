using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ty
{
    //

 


    class msgRoomCreatescene : baseUserMsg
    {
        static msgRoomCreatescene g_msglogin;
        public static msgRoomCreatescene getMe()
        {
            if (msgRoomCreatescene.g_msglogin == null)
            {
                msgRoomCreatescene.g_msglogin = new msgRoomCreatescene();
            }
            return msgRoomCreatescene.g_msglogin;
        }

        public msgRoomCreatescene()
        {
            m_e_type = e_baseMsg_user.e_basemsg_createscene; //
 
        }
        //private AsyncOperation async = null;
        //public IEnumerator  ChangeScene(string name)
        //{
        //    async = SceneManager.LoadSceneAsync(name);
        //    async.allowSceneActivation = false;
        //    while (!async.isDone)
        //    {
        //        if (async.progress >= 0.9f)
        //        {
        //            msgServerCheckScene.getMe().sendto(tyConofig.g_ty_playid, true);
        //            async.allowSceneActivation = true; 
        //        }
        //        yield return null;
        //    }
        //}
        //返回房间信息
        public override void handle(JObject jo)
        {
            //开始创建场景。
            logMgr.log("开始创建场景");
            string scenename = (string)jo["scenename"] ;
            if( tyConofig.isRoomOwer() == true ) //
            {
                //create scene
            }
            else
            {
                //create scene 创建一个空的创建。
                SceneManager.LoadScene("shuangRen");
                msgServerCheckScene.getMe().sendto(tyConofig.g_ty_playid, true);

            }

        }

        public virtual void sendto(string scenename)
        {
            JObject staff = new JObject();
            staff.Add(new JProperty("scenename", scenename));
            this.sendToALL(staff);
        }

    }

}

