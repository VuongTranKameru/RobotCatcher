using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnZone : MonoBehaviour
{
    int randomSpawnRate;
    [SerializeField] Transform spawnAppear;
    [SerializeField] TMP_Text text;

    [SerializeField] Material[] mode;
    [SerializeField] MeshRenderer mesh;

    [SerializeField] StatConfig[] robotSpawn;

    void Start()
    {
        randomSpawnRate = Random.Range(0, 9);
        //text.text = randomSpawnRate.ToString();
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Respawn"))
            randomSpawnRate = 1;

        if (player.CompareTag("Player"))
        {
            Debug.Log(randomSpawnRate);
            if (randomSpawnRate % 3 == 0 && randomSpawnRate != 0)
            {
                mesh.material = mode[1];

                if (!FindAnyObjectByType<RobotStat>()) //already exist once
                {
                    int i = Random.Range(0, robotSpawn.Length);
                    GameObject robotEngage = Instantiate(robotSpawn[i].Itself(), spawnAppear.position, Quaternion.identity);
                    DontDestroyOnLoad(robotEngage);

                    SceneManager.LoadScene("BattleMechanicCalc");
                }
            }
        }
    }
}
