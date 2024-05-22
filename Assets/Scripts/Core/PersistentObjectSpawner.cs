using UnityEngine;

namespace TheGuild.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObject;

        private static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObjectInstance = Instantiate(persistentObject, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(persistentObjectInstance);
        }
    }
}
