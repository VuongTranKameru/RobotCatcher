using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssemtialsLoader : MonoBehaviour
{
    static EssemtialsLoader instance;

    [Header("Player")]
    [SerializeField] GameObject Player;

    private void Awake()
    {
        //dung static de xac dinh duy nhat 1 ton tai, ko bi nhan len
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        //ko huy khi chuyen scene
        DontDestroyOnLoad(gameObject);

        //only using to spawn out the character for the first time
        //load ui system manager (health, inventory, option..)

        //load player
        if (PlayerManager.instance == null)
            LoadPlayer();
    }

    //can load each time scene change, fit for dontdestroy. run after awake and onenabled
    private void OnSceneChange(Scene current, Scene next) 
    {
        
    }

    private void Start()
    {
        //SceneManager.activeSceneChanged += OnSceneChange;

        PlayerStat initialStat = Player.GetComponent<PlayerStat>();
        BoostPlayerBeginStat(initialStat);
    }

    void LoadPlayer()
    {
        GameObject player = Instantiate(Player);
        DontDestroyOnLoad(player); //only destroy when finish a battle
    }

    #region For TestEditor Only
    void BoostPlayerBeginStat(PlayerStat initialStat)
    {
        initialStat.HPRemain = initialStat.MaxHPStat();

        //FullHPRobotOwned(initialStat);
    }

    void FullHPRobotOwned(PlayerStat stat)
    {
        //stat.UsedThatRobot().HPRemain = stat.UsedThatRobot().MaxHPStat();
        stat.FirstPickRobot.health = stat.FirstPickRobot.maxHP;
    }
    #endregion
}
