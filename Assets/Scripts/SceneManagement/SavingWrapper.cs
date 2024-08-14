using System.Collections;
using TheGuild.Saving;
using UnityEngine;

namespace TheGuild.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] private float fadeInTime = .4f;

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene();
            yield return fader.FadeInRoutine(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                Load();
            }
        }

        public  void Save()
        {
            print("Saving Game");
            GetComponent<SavingSystem>().Save();
        }

        public void Load()
        {
            print("Loading Game");
            GetComponent<SavingSystem>().Load();
        }
    }
}
