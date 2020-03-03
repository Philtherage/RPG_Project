using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) { return; }
            if(currentAction != null) // so on first action we dont get a null pointer.
            {
                currentAction.Cancel();
            }
            currentAction = action;
            
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }

}
