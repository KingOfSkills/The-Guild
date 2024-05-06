using UnityEngine;

namespace TheGuild.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction = null;

        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                Debug.Log($"Canceling {currentAction}");
            }
            currentAction = action;
        }
    }
}
