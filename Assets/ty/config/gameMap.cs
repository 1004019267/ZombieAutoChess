using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// 主要优化foreach

namespace ty
{
    public class gameMap<TKey, TValue>
    {

        private Dictionary<TKey, TValue> m_Map = new Dictionary<TKey, TValue>();
        public gameMap()
        {
        }
        List<TKey> m_base_key = null;
        List<TValue> m_base_TValue = null;
        public int Count { set; get; }

        public void Add(TKey key, TValue value)
        {
            m_Map.Add(key, value);
            m_base_key = new List<TKey>(m_Map.Keys);
            m_base_TValue = new List<TValue>(m_Map.Values);
            Count = m_Map.Count;
        }


        public void AddOrReplace(TKey key, TValue value)
        {
            m_Map.Remove(key);
            m_Map.Add(key, value);
            m_base_key = new List<TKey>(m_Map.Keys);
            m_base_TValue = new List<TValue>(m_Map.Values);
            Count = m_Map.Count;
        }
        public void TryAdd(TKey key, TValue value)
        {
            if (getDataByKey(key) != null)
                return;


            m_Map.Add(key, value);
            m_base_key = new List<TKey>(m_Map.Keys);
            m_base_TValue = new List<TValue>(m_Map.Values);
            Count = m_Map.Count;
        }


        public bool Remove(TKey key)
        {
            bool ret = m_Map.Remove(key);
            m_base_key = new List<TKey>(m_Map.Keys);
            m_base_TValue = new List<TValue>(m_Map.Values);
            Count = m_Map.Count;
            return ret;
        }


        public TValue getDataByIndex(int index)
        {
            return m_base_TValue[index];
        }


        public TKey getKeyByIndex(int index)
        {
            return m_base_key[index];
        }
        public void Clear()
        {
            m_Map.Clear();
            if (m_base_key != null)
            {
                m_base_key.Clear();
            }
            if (m_base_TValue != null)
            {
                m_base_TValue.Clear();
            }

            Count = m_Map.Count;
        }

        public TValue getDataByKey(TKey key)
        {
            TValue _baseMsg;
            m_Map.TryGetValue(key, out _baseMsg);//  [key];

            return _baseMsg;

        }
        public bool TryGetValue(TKey key, out TValue _baseMsg)
        {
            return m_Map.TryGetValue(key, out _baseMsg);//  [key];

        }

        public bool ContainsKey(TKey key)
        {
            return m_Map.ContainsKey(key);//  [key];

        }



    }

}


