using TheGuild.Control;
using TheGuild.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace TheGuild.Cinamatics
{
    public class CinamaticControlRemover : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<PlayableDirector>().played += CinamaticControlRemover_played; ;
            GetComponent<PlayableDirector>().stopped += CinamaticControlRemover_stopped;
        }

        private void CinamaticControlRemover_played(PlayableDirector obj)
        {
            DisableControl();
        }

        private void CinamaticControlRemover_stopped(PlayableDirector obj)
        {
            EnableControl();
        }

        private void EnableControl()
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            playerGameObject.GetComponent<PlayerController>().enabled = true;

        }

        private void DisableControl()
        {
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            playerGameObject.GetComponent<ActionScheduler>().CancelCurrentAction();
            playerGameObject.GetComponent<PlayerController>().enabled = false;
        }

    }
}
