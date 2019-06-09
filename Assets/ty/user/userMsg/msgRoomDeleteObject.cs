using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ty
{
    //



    class msgRoomDeleteObject : baseUserMsg
    {
        static msgRoomDeleteObject g_msglogin;
        public static msgRoomDeleteObject getMe()
        {
            if (msgRoomDeleteObject.g_msglogin == null)
            {
                msgRoomDeleteObject.g_msglogin = new msgRoomDeleteObject();
            }
            return msgRoomDeleteObject.g_msglogin;
        }

        public msgRoomDeleteObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_deleteObj;
        }

  
        //收到
        public override void handle(JObject jo)
        {


            //收到别人的信息

            int objid = Convert.ToInt32((string)jo["objid"]);

            baseSyncObject _baseSyncObject = syncObjectMgr.getMe().getObject(objid);
            if(_baseSyncObject)
            {
                GameObject.Destroy(_baseSyncObject.gameObject);
            }
         

 


        }

  

    }

}

