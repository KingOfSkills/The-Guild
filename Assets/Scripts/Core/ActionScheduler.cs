using UnityEngine;

namespace TheGuild.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction = null;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
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
