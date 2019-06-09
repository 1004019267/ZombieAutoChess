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

 


    class msgRoomSnycAllObj : baseUserMsg
    {
        static msgRoomSnycAllObj g_msglogin;
        public static msgRoomSnycAllObj getMe()
        {
            if (msgRoomSnycAllObj.g_msglogin == null)
            {
                msgRoomSnycAllObj.g_msglogin = new msgRoomSnycAllObj();
            }
            return msgRoomSnycAllObj.g_msglogin;
        }

        public msgRoomSnycAllObj()
        {
            m_e_type = e_baseMsg_user.e_basemsg_SnycAllObj; //
 
        }


       
        public override void handle(JObject jo)
        {

            int size = Convert.ToInt32((string)jo["size"]);
               Dictionary<int, int> _objecidmap = new Dictionary<int, int>();


            for (int i = 0; i < size; ++i)
            {
                int objid = Convert.ToInt32((string)jo["" + i]);
                _objecidmap.Add(objid, objid);
            }

            foreach (var elem in syncObjectMgr.getMe().m_roomObjMap)
            {
                int objid = 0;
                if (_objecidmap.TryGetValue(elem.Value.m_objectId, out objid) == false)
                {
                  //  int objid = Convert.ToInt32((string)jo["" + i]);
                    syncObjectMgr.getMe().DestroyObject(elem.Value.m_objectId);
                    return;
                }
            }






        }

        

    }

}

