using System.Collections;
using TheGuild.Control;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace TheGuild.Core
{
    public class Portal : MonoBehaviour
    {
        private enum PortalID
        {
            A,
            B,
            C,
            D,
            E,
        }

        [SerializeField] private int sceneIndexToLoad = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PortalID portalID;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transistion());
            }
        }

        private IEnumerator Transistion()
        {
            if (sceneIndexToLoad < 0)
            {
                Debug.LogError("Scene to load is not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneIndexToLoad);
            print("Scene Loaded");
            Portal otherPortal = GetOtherPortal();
            otherPortal.UpdatePlayerPositionRotation();
            Destroy(gameObject);
        }

        public void UpdatePlayerPositionRotation()
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            player.GetComponent<NavMeshAgent>().Warp(spawnPoint.position);
            //player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsByType<Portal>(FindObjectsSortMode.None))
            {
                if (portal != this && portalID == portal.portalID)
                {
                    return portal;
                }
            }
            return null;
        }
    }
}
