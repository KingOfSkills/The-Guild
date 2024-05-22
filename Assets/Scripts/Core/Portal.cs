using System.Collections;
using TheGuild.Control;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGuild.Core
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneIndexToLoad = -1;

        [SerializeField] private Transform spawnPoint;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transistion());
            }
        }

        private IEnumerator Transistion()
        {
            yield return SceneManager.LoadSceneAsync(sceneIndexToLoad);
            print("Scene Loaded");
            Portal otherPortal = GetOtherPortal();
            otherPortal.UpdatePlayerPositionRotation();
            Destroy(gameObject);
        }

        public void UpdatePlayerPositionRotation()
        {
            PlayerController player = FindObjectOfType<PlayerController>();

            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsByType<Portal>(FindObjectsSortMode.None))
            {
                if (portal != this)
                {
                    return portal;
                }
            }
            return null;
        }
    }
}
