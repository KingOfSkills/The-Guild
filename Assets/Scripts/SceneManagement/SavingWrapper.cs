using System.Collections;
using TheGuild.Saving;
using UnityEngine;

namespace TheGuild.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] private float fadeInTime = .4f;

        //private IEnumerator Start()
        //{
        //    yield return LoadLastSavedScene();
        //}

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

            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(LoadLastSavedScene());
            }
        }

        public  void Save()
        {
            GetComponent<SavingSystem>().Save();
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load();
        }

        private IEnumerator LoadLastSavedScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene();
            yield return fader.FadeInRoutine(fadeInTime);
        }
    }
}
