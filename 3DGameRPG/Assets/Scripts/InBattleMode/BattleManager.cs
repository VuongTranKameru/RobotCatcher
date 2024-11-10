using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    BattleState state;
    PlayerInput input;

    [Header("Annoucement")]
    [SerializeField] TMP_Text message;
    [SerializeField] GameObject annouceBoard, playerBoard, statusBoard;
    bool donePTurn, doneETurn;

    [Header("Location Spawn")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform playerStand, rPobotStand, enemyStand;
    IHaveSameStat chosen;
    RobotStat rPobots, eStats;
    PlayerStat pStats;
    GameObject robotInfo;

    [Header("Player Info Screen")]
    [SerializeField] TMP_Text playerNameScr;
    [SerializeField] TMP_Text maxhpScr, hpRemainScr, spRemainScr, levelScr;
    [SerializeField] Image hpBar, spBar;
    [SerializeField] GameObject spAvailable;
    bool hOr; //human or robot in battle, human is false, robot is true
    int curHP, maxHP; //use for updating health bar
    bool spUsed; //only robot can have sp

    [Header("Enemy Info Screen")]
    [SerializeField] TMP_Text enemyNameScr;
    [SerializeField] TMP_Text eMaxhpScr, eHpRemainScr, eLevelScr;
    [SerializeField] Image eHpBar;

    [Header("Skill")]
    [SerializeField] ToggleGroup groupSkill;
    [SerializeField] Transform roomSkill;
    List<Toggle> emptySkills = new List<Toggle>();

    public BattleState CurrentState() { return state; }
    void MessageReceive(string mes) { message.text = mes; }

    void Awake()
    {
        state = BattleState.BeginBattle;
        input = new PlayerInput();

        PreparingWrapCharactersIn();

        TurnAnnouceBoard();
        if (state == BattleState.BeginBattle)
            MessageReceive($"{pStats.NameStat()} engage {eStats.NameStat()} on a battle!.");
    }

    private void OnEnable()
    {
        input.OnBattle.Enable();
        input.Player.Disable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        ControllingMessages();
        StartCoroutine(EnemyAttack());
    }

    void PreparingWrapCharactersIn()
    {
        playerPrefab = FindObjectOfType<PlayerStat>().gameObject;
        playerPrefab.transform.position = playerStand.position;
        pStats = playerPrefab.GetComponent<PlayerStat>();

        enemyPrefab = FindObjectOfType<RobotStat>().gameObject;
        enemyPrefab.transform.position = enemyStand.position;
        eStats = enemyPrefab.GetComponent<RobotStat>();

        eStats.HPRemain = eStats.MaxHPStat();

        //check if player have robot. if have, stats first update, player robot
        robotInfo = Instantiate(pStats.UsedThatRobot().gameObject, rPobotStand.position, rPobotStand.rotation);
        rPobots = robotInfo.GetComponent<RobotStat>();

        playerNameScr.text = rPobots.NameStat();
        levelScr.text = rPobots.LvStat().ToString();

        chosen = rPobots;
        hOr = true;
        maxHP = rPobots.MaxHPStat();
        maxhpScr.text = "/" + maxHP.ToString();
        spUsed = true;
        spAvailable.SetActive(true);

        rPobots.SPRemain = 0;
        rPobots.HPRemain = rPobots.MaxHPStat();

        PreparingCallSkill(rPobots);

        //stats first update, enemy
        enemyNameScr.text = eStats.NameStat();
        eLevelScr.text = eStats.LvStat().ToString();
        eMaxhpScr.text = "/" + eStats.MaxHPStat().ToString();

        UpdatingStatOnScreen();
        //ChoosingCharacterToPlay();
    }

    void ChoosingCharacterToPlay()
    {
        if (pStats.CheckAvailableRobot())
        {
            //check if player have robot. if have, stats first update, player robot
            robotInfo = Instantiate(pStats.UsedThatRobot().gameObject, rPobotStand.position, rPobotStand.rotation);
            rPobots = robotInfo.GetComponent<RobotStat>();

            playerNameScr.text = rPobots.NameStat();
            levelScr.text = rPobots.LvStat().ToString();

            chosen = rPobots;
            hOr = true;
            maxHP = rPobots.MaxHPStat();
            maxhpScr.text = "/" + maxHP.ToString();
            spUsed = true;
            spAvailable.SetActive(true);

            rPobots.SPRemain = 0;

            PreparingCallSkill(rPobots);
        }
        else
        {
            //stats first update, player
            playerNameScr.text = pStats.NameStat();
            levelScr.text = pStats.LvStat().ToString();

            chosen = pStats;
            hOr = false;
            maxHP = pStats.MaxHPStat();
            maxhpScr.text = "/" + maxHP.ToString();
            spUsed = false;
            spAvailable.SetActive(false);

            pStats.HPRemain = pStats.MaxHPStat();

            PreparingCallSkill(pStats);
        }

        UpdatingStatOnScreen();
    }

    void PreparingCallSkill(IHaveSameStat user)
    {
        if (emptySkills.Count > 0)
            for (int i = 0; i < emptySkills.Count; i++)
            {
                Destroy(emptySkills[i].gameObject); 
            }

        emptySkills.Clear();

        foreach (SkillConfig s in user.ListOfAction())
        {
            GameObject that = Instantiate(s.skillBtn, roomSkill);
            that.GetComponent<Toggle>().group = groupSkill;
            emptySkills.Add(that.GetComponent<Toggle>());
        }
    }

    void UpdatingStatOnScreen()
    {
        if (hOr)
            curHP = rPobots.HPRemain;
        else curHP = pStats.HPRemain;

        //Updating Player's Health Bar
        float ratio = (float) curHP / maxHP; //tim % mau sau khi mat hp
        hpBar.rectTransform.localPosition = new Vector3(hpBar.rectTransform.rect.width * ratio - hpBar.rectTransform.rect.width,
            0, 0); //day thanh image qua trai, bang (tong thanh image * 0.so mau mat - tong thanh image hien tai)
        hpRemainScr.text = curHP.ToString();

        if (spUsed) //Updating PlayerRobot's SP Bar
        {
            float spRatio = (float) rPobots.SPRemain / rPobots.MaxSPStat(); //tim % nang luong sau khi tu dong hoi
            spBar.rectTransform.localPosition = new Vector3(0, spBar.rectTransform.rect.height * spRatio - spBar.rectTransform.rect.height,
                0); //day thanh image len tren, bang (tong thanh image * 0.so sp tang - tong thanh image hien tai)
            spRemainScr.text = rPobots.SPRemain.ToString() + "%";
        }

        //Updating Enemy's Health Bar
        float eRatio = (float) eStats.HPRemain / eStats.MaxHPStat();
        eHpBar.rectTransform.localPosition = new Vector3(eHpBar.rectTransform.rect.width * eRatio - eHpBar.rectTransform.rect.width, 0, 0);
        eHpRemainScr.text = eStats.HPRemain.ToString();
    }

    public void OnAttackButton()
    {
        if (groupSkill.AnyTogglesOn())
        {
            for (int i = 0; i < emptySkills.Count; i++)
                if (emptySkills[i].isOn)
                {
                    emptySkills[i].GetComponent<ICanUseSkill>().SkillUsed(chosen, eStats);

                    TurnAnnouceBoard();
                    MessageReceive(emptySkills[i].GetComponent<ICanUseSkill>().MessageUsedSkill(chosen, eStats));

                    break;
                }

            UpdatingStatOnScreen();

            if (spUsed)
                rPobots.SPRemain += 20;

            if (eStats.HPRemain <= 0)
            {
                state = BattleState.WonBattle;
                if (state == BattleState.WonBattle)
                    MessageReceive($"{pStats.NameStat()} won the battle!");

                Destroy(enemyPrefab);
                SceneManager.LoadScene("SpawnRoute");
            }
            else
            {
                donePTurn = true;
                state = BattleState.EnemyTurn;
            }
        }
        else
        {
            TurnAnnouceBoard();
            MessageReceive("You arent choose any action yet...");
        }
    }

    IEnumerator EnemyAttack()
    {
        if (input.OnBattle.SkipDialogue.triggered && donePTurn)
        {
            MessageReceive($"{eStats.NameStat()} turns.");
            donePTurn = false;
        }
        else if (input.OnBattle.SkipDialogue.triggered && state == BattleState.EnemyTurn && !doneETurn)
        {
            input.Disable();
            SkillConfig usedSkill = eStats.ListOfAction()[0];
            usedSkill.skillBtn.GetComponent<ICanUseSkill>().SkillUsed(eStats, chosen);

            MessageReceive(usedSkill.skillBtn.GetComponent<ICanUseSkill>().MessageUsedSkill(eStats, chosen));
            yield return new WaitForSeconds(1f);

            TurnStatusBoard();
            UpdatingStatOnScreen();

            doneETurn = true;
            input.OnBattle.Enable();
        }

        if (input.OnBattle.SkipDialogue.triggered && state == BattleState.EnemyTurn && doneETurn)
        {
            TurnAnnouceBoard();

            if (pStats.HPRemain <= 0)
            {
                state = BattleState.LoseBattle;
                if (state == BattleState.LoseBattle)
                    MessageReceive($"{chosen.NameStat()} lost the battle!");
                Destroy(playerPrefab);
            }
            else if (chosen.HPRemain <= 0)
            {
                doneETurn = false;
                MessageReceive($"{chosen.NameStat()} lost the battle! {pStats.NameStat()} using another one.");
                Destroy(robotInfo);

                ChoosingCharacterToPlay();
                state = BattleState.PlayerTurn;
            }
            else
            {
                doneETurn = false;
                MessageReceive($"{chosen.NameStat()} turns.");
                state = BattleState.PlayerTurn;
            }
        }
    }

    public void OnRunaway()
    {
        Destroy(enemyPrefab);
        SceneManager.LoadScene("SpawnRoute");
    }

    //Annoucement Script
    void ControllingMessages()
    {
        if (input.OnBattle.SkipDialogue.triggered && state == BattleState.BeginBattle)
        {
            if (chosen.SpeedStat() < eStats.SpeedStat())
            {
                MessageReceive($"{eStats.NameStat()} turns.");
                donePTurn = true;
                state = BattleState.EnemyTurn;
            }
            else
            {
                MessageReceive($"{pStats.NameStat()} turns.");
                state = BattleState.PlayerTurn;
            }
        }
        else if (input.OnBattle.SkipDialogue.triggered && state == BattleState.PlayerTurn)
            if (!playerBoard.activeInHierarchy)
                TurnPlayerBoard();
    }

    void TurnAnnouceBoard()
    {
        playerBoard.SetActive(false);
        statusBoard.SetActive(false);
        annouceBoard.SetActive(true);
    }

    void TurnPlayerBoard()
    {
        annouceBoard.SetActive(false);
        statusBoard.SetActive(true);
        playerBoard.SetActive(true);
    }

    void TurnStatusBoard()
    {
        annouceBoard.SetActive(false);
        statusBoard.SetActive(true);
        playerBoard.SetActive(false);
    }

    #region Unused
    /*IEnumerator HPBarGoDownAnimation(float ratio)
    {
        float barDownMax = hpBar.rectTransform.rect.width * ratio - hpBar.rectTransform.rect.width;
        float i = hpBar.rectTransform.localPosition.x; //current hp
        for (; i >= barDownMax; i -= .5f)
        {
            yield return new WaitForSeconds(0f);
            hpBar.rectTransform.localPosition = new Vector3(i, 0, 0);
        }
    }*/

    /*public void OnLevelUp()
    {
        pRStats.LevelUp();
        Debug.Log($"{pStats.MaxHPStat()} up");
    }*/
    #endregion
}
