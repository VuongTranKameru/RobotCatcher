using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnZone : MonoBehaviour
{
    int randomSpawnRate;
    [Header("Robot Appear")]
    [SerializeField] Transform spawnAppear;
    [SerializeField] TMP_Text text;
    [SerializeField] StatConfig[] robotSpawn;
    [SerializeField] string battleScene;

    [Header("Effect")]
    [SerializeField] Material[] mode;
    [SerializeField] MeshRenderer mesh;

    [Header("PlayerRespawn")]
    [SerializeField] GameObject respawnLoca;

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
                    

                    //creating
                    int i = Random.Range(0, robotSpawn.Length);
                    GameObject robotEngage = Instantiate(robotSpawn[i].Itself(), spawnAppear.position, Quaternion.identity);
                    DontDestroyOnLoad(robotEngage);

                    //pinning
                    GameObject afterBattleRespawn = Instantiate(respawnLoca, respawnLoca.transform.position, Quaternion.identity);
                    afterBattleRespawn.tag = "TeleportFromBtl";
                    DontDestroyOnLoad(afterBattleRespawn);

                    //changing
                    SceneManager.LoadScene(battleScene);
                }
            }
        }
    }
}
