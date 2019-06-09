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



    public class msgRoomCreateObject : baseUserMsg
    {
        static msgRoomCreateObject g_msglogin;
        public static msgRoomCreateObject getMe()
        {
            if (msgRoomCreateObject.g_msglogin == null)
            {
                msgRoomCreateObject.g_msglogin = new msgRoomCreateObject();
            }
            return msgRoomCreateObject.g_msglogin;
        }

        public msgRoomCreateObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_CreateObject;
        }

 
        //返回唯一id
        public int createObjectId( )
        {
            byte[] bytes =     Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt32(bytes, 0);
        }

        //收到
        public override void handle(JObject jo)
        {
          

            //收到别人的信息
            int srcid = Convert.ToInt32((string)jo["srcid"]);
            int objid = Convert.ToInt32((string)jo["objid"]);

            if (syncObjectMgr.getMe().getObject(objid) != null)
            {
                return;
            }

            double x = Convert.ToDouble((string)jo["x"]);
            double y = Convert.ToDouble((string)jo["y"]);
            double z = Convert.ToDouble((string)jo["z"]);


            double qx = Convert.ToDouble((string)jo["qx"]);
            double qy = Convert.ToDouble((string)jo["qy"]);
            double qz = Convert.ToDouble((string)jo["qz"]);
            double qw = Convert.ToDouble((string)jo["qw"]);

            objType _objType = (objType) Convert.ToInt32((string)jo["objType"]);
            Vector3 pos = new Vector3((float)x, (float)y, (float)z);
            Quaternion qua = new Quaternion((float)qx, (float)qy, (float)qz, (float)qw);


 


            double sx = Convert.ToDouble((string)jo["sx"]);
            double sy = Convert.ToDouble((string)jo["sy"]);
            double sz = Convert.ToDouble((string)jo["sz"]);
            Vector3 scale = new Vector3((float)sx, (float)sy, (float)sz);


          

            logMgr.log("myid" + tyConofig.g_roomid + "srcid" + srcid + "objid" + objid);

            switch(_objType)
            {
                case objType.e_sceneobj:
                    {

                        GameObject tongbu = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/scene/Sphere"));
                        tongbu.GetComponent<Rigidbody>().position = pos;
                        tongbu.GetComponent<Rigidbody>().rotation = qua;
                        baseSyncObjectPhyObj _baseSyncObject = tongbu.GetComponent<baseSyncObjectPhyObj>();
                        _baseSyncObject.init(objid);
                    }
                    break;
                case objType.e_player:
                    {

                        //
                    }
                    break;
                case objType.e_otherplayer:
                {
                        //
                }
                    break;
                case objType.e_scenebox:
                    {
               


                    }
                    

                    break;
            }

        }

  

    }

}

