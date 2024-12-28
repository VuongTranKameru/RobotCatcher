using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotEngage : MonoBehaviour
{
    [SerializeField] string spawnLocation;
    [SerializeField] GameObject robot;
    [SerializeField] StatConfig robotSpawn;

    //either load only one scene, or making two scenes with a bunch of it
    //https://stackoverflow.com/questions/38668569/object-resetting-after-loading-a-scene-for-the-second-time-in-unity

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            robot.tag = "Enemy";
            DontDestroyOnLoad(robot);

            SceneManager.LoadScene(spawnLocation);
        }
    }
}
