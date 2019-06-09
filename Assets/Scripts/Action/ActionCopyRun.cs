using UnityEngine;


//Œﬁ”√
    public class ActionCopyRun : BaseAction
    {
        public GameObject target;
        protected override void onStart()
        {
            base.onStart();
            waitOver = false;
            openNext = false;
            ActionUtils.RunAction(GameObjectAgent.getAgentGameObject(gameObject, gameObject), target);
        }

        internal override void onCopyTo(BaseAction cloneto)
        {
            ActionCopyRun r = (ActionCopyRun) cloneto;
            r.target = target;
        }
    }
