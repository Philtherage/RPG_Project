using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) { return; }
            if(currentAction != null) // so on first action we dont get a null pointer.
            {
                print("canceling " + currentAction);
            }
            currentAction = action;
            
        }
    }

}
