using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGuild.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public static int LastSceneSavedIndex = 0;

        //public IEnumerator LoadLastScene(string saveFile)
        //{
        //    Dictionary<string, object> state = LoadFile(saveFile);
        //    int buildIndex = SceneManager.GetActiveScene().buildIndex;
        //    if (state.ContainsKey("lastSceneBuildIndex"))
        //    {
        //        buildIndex = (int)state["lastSceneBuildIndex"];
        //    }
        //    yield return SceneManager.LoadSceneAsync(buildIndex);
        //    RestoreState(state);
        //}

        public IEnumerator LoadLastScene()
        {
            if (ES3.KeyExists("LastSceneSavedIndex"))
            {
                LastSceneSavedIndex = ES3.Load<int>("LastSceneSavedIndex");
            }
            else
            {
                LastSceneSavedIndex = 0;
            }
            yield return SceneManager.LoadSceneAsync(LastSceneSavedIndex);
            Load();
            Load();
        }

        public void Save()
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                saveable.Save();
            }

            ES3.Save("LastSceneSavedIndex", LastSceneSavedIndex);
        }

        public void Load()
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                saveable.Load();
            }
        }

        public void Delete(string saveFile)
        {
            //File.Delete(GetPathFromSaveFile(saveFile));
        }
    }
}