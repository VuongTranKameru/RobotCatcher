using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotEngage : MonoBehaviour
{
    [SerializeField] string spawnLocation;
    [SerializeField] GameObject dis;

    //either load only one scene, or making two scenes with a bunch of it
    //https://stackoverflow.com/questions/38668569/object-resetting-after-loading-a-scene-for-the-second-time-in-unity

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(dis);
            SceneManager.LoadScene(spawnLocation);
            gameObject.SetActive(false);
        }
    }
}
