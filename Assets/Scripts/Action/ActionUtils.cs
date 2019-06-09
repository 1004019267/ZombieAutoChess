using UnityEngine;
//无用

public class ActionUtils
    {
        /// <summary>
        /// 在指定的对象上运行给予的动作集合.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static void RunAction(GameObject target,GameObject codes)
        {
            BaseAction.isWating = true;
            BaseAction fast = null;
            BaseAction last = null;
            BaseAction[] _list = codes.GetComponents<BaseAction>();
            foreach (BaseAction a in _list)
            {
                System.Type _type = a.GetType();
                BaseAction b = (BaseAction)target.AddComponent(_type);
                b.distoryOnOver = true;
                a.copyData(b);
                if (fast == null)
                {
                    fast = b;
                }
                last = b;
            }
            BaseAction.isWating = false;
            if (fast != null)
            {
                fast.enabled = true;
            }
        }

        public static bool RunAction(GameObject target,int index)
        {
            BaseAction[] _list = target.GetComponents<BaseAction>();
            int length = _list.Length;
            if (index >= 0 && index < length)
            {
                _list[index].enabled = true;
                return true;
            }
            return false;
        }
        
        public static bool RunAction(GameObject target,string ActionName)
        {
            if (ActionName == null)
            {
                return RunAction(target, 0);
            }
            BaseAction[] _list = target.GetComponents<BaseAction>();
            foreach (BaseAction a in _list)
            {
                if (a.info == ActionName)
                {
                    a.enabled = true;
                    return true;
                }
            }
            return false;
        }
        

        public static int StopAction(GameObject target, string ActionName)
        {
            bool isstart = false;
            int n = 0;
            BaseAction[] _list = target.GetComponents<BaseAction>();
            foreach (BaseAction a in _list)
            {
                if (a.info == ActionName)
                {
                    isstart = true;
                }
                if (isstart)
                {
                    if (!a.openNext)
                    {
                        break;
                    }
                    if (a.enabled)
                    {
                        a.doover();
                        n++;
                    }
                }
            }
            return n;
        }
        public static int StopAction(GameObject target,int index)
        {
            int n = 0;
            BaseAction[] _list = target.GetComponents<BaseAction>();
            for(int i = index; i < _list.Length;i++ )
            {
                if (!_list[i].openNext)
                {
                    break;
                }
                if (_list[i].enabled)
                {
                    _list[i].doover();
                    n++;
                }
            }
            return n;
        }

        public static int StopAllAction(GameObject target)
        {
            int n = 0;
            BaseAction[] _list = target.GetComponents<BaseAction>();
            for(int i = 0; i < _list.Length;i++ )
            {
                if (_list[i].enabled)
                {
                    _list[i].doover();
                    n++;
                }
            }
            return n;
        }
        
        
    }
