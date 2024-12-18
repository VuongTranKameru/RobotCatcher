using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //[Header("Current Scene")]

    [Header("Next Scene")]
    [SerializeField] int sceneNum;
    [SerializeField] Transform exitGate;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(sceneNum);
    }
}
