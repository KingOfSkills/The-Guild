using TheGuild.Saving;
using UnityEngine;

namespace TheGuild.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {

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

        private void Save()
        {
            print("Saving Game");
            GetComponent<SavingSystem>().Save();
        }

        private void Load()
        {
            print("Loading Game");
            GetComponent<SavingSystem>().Load();
        }
    }
}
