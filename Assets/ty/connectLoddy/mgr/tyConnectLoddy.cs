
using System;
using System.Collections.Generic;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Net;

//链接loddy服务器  服务器代码 
namespace ty
{
    public class tyConnectLoddy
    {
        static tyConnectLoddy g_websocketMgr;
        public static tyConnectLoddy getMe()
        {

            if (tyConnectLoddy.g_websocketMgr == null)
            {
                tyConnectLoddy.g_websocketMgr = new tyConnectLoddy();
            }
            return tyConnectLoddy.g_websocketMgr;
        }


        WebSocket m_ws = null;
        public bool m_connent = false;
        public bool m_connenting = false;
 
        public void connent(string loginServerIp, int loginServerPort)
        {
            if (m_connent == true)
            {
                return;
            }
            if (m_connenting == true)
            {
                return;
            }
 
            if (m_ws!= null)
            {
                m_ws.Close();
                m_ws = null;
            }

            string _url = "ws://" + loginServerIp + ":" + loginServerPort;
            logMgr.log("websocket : " + _url);

            using (var ws = new WebSocket(_url))
                m_ws = ws;

            m_connenting = true;
            // To set the WebSocket events.
            m_ws.OnOpen += (sender, e) =>
            {
                this.OnConnect();
            };

            m_ws.OnMessage += (sender, e) =>
            {
                this.OnMessage(e.Data);
            };


            m_ws.OnError += (sender, e) =>
            {
                logMgr.log(e.ToString());
                this.OnError(sender, e);
            };


            m_ws.OnClose += (sender, e) =>
            {
                logMgr.log(e.ToString());
                this.OnClose();
            };

            m_ws.ConnectAsync();
        }

        void OnError(object sender, ErrorEventArgs e)
        {
            logMgr.log(e.Message);

            m_connent = false;

            m_connenting = false;

            logMgr.log("连接大厅失败！");
        }

        void OnConnect()
        {
            m_connent = true;
            m_connenting = false;
            logMgr.log("连接大厅成功！");

            if (tyConofig.g_ty_playid == -1)
            {
                msgCreatePlayId.getMe().getPlayID();
            }
            else
            {
                msgLoddylogin.getMe().login();
            }
        }

        
        public void shutdown()
        {
            OnClose();
        }
        void OnClose()
        {

            m_connent = false;
            m_connenting = false;
            if(m_ws!=null)
            {
                m_ws.CloseAsync();
                m_ws = null;
            }
            tyConnectLoddy.g_websocketMgr = new tyConnectLoddy();
            tyInterface.getMe().onconnectLoddyError("");

        }

        void OnMessage(string data)
        {
            this.addMsg(data);
        }

        Queue<string> m_Queue_msg = new Queue<string>();
        public virtual void addMsg(string msg)
        {
            lock (m_Queue_msg)
            {
                m_Queue_msg.Enqueue(msg);
            }
        }


        public virtual void loop_msg()
        {
            while (m_Queue_msg.Count > 0)
            {
                lock (m_Queue_msg)
                {
                    string msg = m_Queue_msg.Dequeue();
                    tyLoddymsgMgr.getMe().handle(msg);
                }
            }
        }


        float m_time_login = 1;
        public void loop(float t)//场景
        {

            tyLoddymsgMgr.getMe().loop();

            this.loop_msg();

            m_time_login -= t;

            if (m_time_login > 0)
            {
                return;
            }

            m_time_login = 10;

            if (this.m_connent == false)
            {
                if (this.m_connenting == false)
                {
                    this.connent(tyConofig.g_ty_loddyServerIp, tyConofig.g_ty_loddyServerPort);
                }
            }

        }


        public void send(string data)
        {
            if (m_connent == false)
            {
                logMgr.log("tyconnectloddy addMsg error socket 还没有连接！");
                return;
            }

            if (m_ws != null)
            {
                m_ws.SendAsync(data,null);
            }

        }



    }
}