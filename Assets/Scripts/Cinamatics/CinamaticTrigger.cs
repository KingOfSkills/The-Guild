using TheGuild.Control;
using TheGuild.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace TheGuild.Cinamatics
{
    public class CinamaticTrigger : MonoBehaviour, ISaveable
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

        public void Save(string id)
        {
            ES3.Save($"{id}hasPlayed", hasPlayed);
        }

        public void Load(string id)
        {
            hasPlayed = ES3.Load<bool>($"{id}hasPlayed");
        }
    }
}
