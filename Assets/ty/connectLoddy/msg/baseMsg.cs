using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ty
{

    enum e_baseMsg
    {
        e_basemsg_none = -1,
        e_basemsg_login = 0,
        e_basemsg_jointRoom = 2,
        e_basemsg_heart = 13, //
        e_createPlayId  = 14,  //
    }


    class baseMsg
    {

        public e_baseMsg m_e_type = e_baseMsg.e_basemsg_none;
        public baseMsg()
        {
        }

        public virtual void handle(JObject jo)
        {
        }

        public virtual void loop()
        {
        }

        public virtual void sendData(string data)
        {
            tyConnectLoddy.getMe().send(data);
        }



        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter(); formatter.Serialize(ms, obj); return ms.GetBuffer();
            }
        }

        /// <summary> 
        /// 将一个序列化后的byte[]数组还原         
        /// </summary>
        /// <param name="Bytes"></param>         
        /// <returns></returns> 
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter(); return formatter.Deserialize(ms);
            }
        }


    }

}