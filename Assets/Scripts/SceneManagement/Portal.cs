using System.Collections;
using TheGuild.Control;
using TheGuild.Saving;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace TheGuild.SceneManagement
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
        [SerializeField] private float fadeOutTime = .5f;
        [SerializeField] private float fadeWaitTime = 1f;
        [SerializeField] private float fadeInTime = .5f;

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

            Fader fader = FindObjectOfType<Fader>();
            yield return StartCoroutine(fader.FadeOutRoutine(fadeOutTime));

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneIndexToLoad);
            SavingSystem.LastSceneSavedIndex = sceneIndexToLoad;
            savingWrapper.Load();
            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayerPositionRotation(otherPortal);

            savingWrapper.Save();
            yield return new WaitForSeconds(fadeWaitTime);

            yield return StartCoroutine(fader.FadeInRoutine(fadeInTime));

            Destroy(gameObject);
        }

        public void UpdatePlayerPositionRotation(Portal otherPortal)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            //player.transform.position = spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
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
