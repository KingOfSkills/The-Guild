using TheGuild.Control;
using UnityEngine;
using UnityEngine.Playables;

namespace TheGuild.Cinamatics
{
    public class CinamaticTrigger : MonoBehaviour
    {
        bool hasPlayed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player) && !hasPlayed)
            {
                hasPlayed = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
