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
    [SerializeField] TMP_Text messange;
    [SerializeField] GameObject annouceBoard, playerBoard;
    bool donePTurn, doneETurn;

    [Header("Location Spawn")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform playerStand, enemyStand;
    RobotStat rPStats, eStats;
    PlayerStat pStats;

    [Header("Player Info Screen")]
    [SerializeField] TMP_Text playerNameScr;
    [SerializeField] TMP_Text maxhpScr, hpRemainScr, spRemainScr, levelScr;
    [SerializeField] Image hpBar, spBar;

    [Header("Enemy Info Screen")]
    [SerializeField] TMP_Text enemyNameScr;
    [SerializeField] TMP_Text eMaxhpScr, eHpRemainScr, eLevelScr;
    [SerializeField] Image eHpBar;

    [Header("Skill")]
    [SerializeField] ToggleGroup groupSkill;
    [SerializeField] Transform roomSkill;
    List<Toggle> emptySkills;

    public BattleState CurrentState() { return state; }

    void Awake()
    {
        state = BattleState.BeginBattle;
        input = new PlayerInput();

        PreparingWrapCharactersIn();
        PreparingCallSkill();

        TurnAnnouceBoard();
        if (state == BattleState.BeginBattle)
            messange.text = $"{pStats.NameStat()} engage {eStats.NameStat()} on a battle!.";
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

        /*GameObject playerInfo = Instantiate(playerPrefab, playerStand);
        pStats = playerInfo.GetComponent<RobotStat>();*/

        pStats.HPRemain = pStats.MaxHPStat();
        eStats.HPRemain = eStats.MaxHPStat();

        //stats first update, player
        playerNameScr.text = pStats.NameStat();
        levelScr.text = pStats.LvStat().ToString();
        maxhpScr.text = "/" + pStats.MaxHPStat().ToString();

        //stats first update, enemy
        enemyNameScr.text = eStats.NameStat();
        eLevelScr.text = eStats.LvStat().ToString();
        eMaxhpScr.text = "/" + eStats.MaxHPStat().ToString();

        UpdatingStatOnScreen();
    }

    void UpdatingStatOnScreen()
    {
        //Updating Player's Health Bar
        float ratio = (float) pStats.HPRemain / pStats.MaxHPStat(); //tim % mau sau khi mat hp
        hpBar.rectTransform.localPosition = new Vector3(hpBar.rectTransform.rect.width * ratio - hpBar.rectTransform.rect.width,
            0, 0); //day thanh image qua trai, bang (tong thanh image * 0.so mau mat - tong thanh image hien tai)

        hpRemainScr.text = pStats.HPRemain.ToString();

        //Updating Enemy's Health Bar
        float eRatio = (float) eStats.HPRemain / eStats.MaxHPStat(); //tim % mau sau khi mat hp
        eHpBar.rectTransform.localPosition = new Vector3(eHpBar.rectTransform.rect.width * eRatio - eHpBar.rectTransform.rect.width,
            0, 0); //day thanh image qua trai, bang (tong thanh image * 0.so mau mat - tong thanh image hien tai)

        eHpRemainScr.text = eStats.HPRemain.ToString();
    }

    void PreparingCallSkill()
    {
        emptySkills = new List<Toggle>();
        foreach (SkillConfig s in pStats.ListOfAction())
        {
            GameObject that = Instantiate(s.skillBtn, roomSkill);
            that.GetComponent<Toggle>().group = groupSkill;
            emptySkills.Add(that.GetComponent<Toggle>());
        }
    }

    public void OnAttackButton()
    {
        if (groupSkill.AnyTogglesOn())
        {
            for (int i = 0; i < emptySkills.Count; i++)
                if (emptySkills[i].isOn)
                {
                    emptySkills[i].GetComponent<ICanUseSkill>().SkillUsed(pStats, eStats);

                    TurnAnnouceBoard();
                    messange.text = emptySkills[i].GetComponent<ICanUseSkill>().MessageUsedSkill(pStats, eStats);

                    break;
                }

            UpdatingStatOnScreen();

            if (eStats.HPRemain <= 0)
            {
                state = BattleState.WonBattle;
                if (state == BattleState.WonBattle)
                    messange.text = $"{pStats.NameStat()} won the battle!";
                Destroy(enemyPrefab);

                SceneManager.LoadScene("Demo");
            }
            else
            {
                donePTurn = true;
                state = BattleState.EnemyTurn;
            }
        }
    }

    IEnumerator EnemyAttack()
    {
        if (input.OnBattle.SkipDialogue.triggered && donePTurn)
        {
            messange.text = $"{eStats.NameStat()} turns.";
            donePTurn = false;
        }
        else if (input.OnBattle.SkipDialogue.triggered && state == BattleState.EnemyTurn && !doneETurn)
        {
            SkillConfig usedSkill = eStats.ListOfAction()[0];
            usedSkill.skillBtn.GetComponent<ICanUseSkill>().SkillUsed(eStats, pStats);

            messange.text = usedSkill.skillBtn.GetComponent<ICanUseSkill>().MessageUsedSkill(eStats, pStats);

            UpdatingStatOnScreen();

            doneETurn = true;
        }

        if (input.OnBattle.SkipDialogue.triggered && state == BattleState.EnemyTurn && doneETurn)
        {
            yield return new WaitForSeconds(1f);

            if (pStats.HPRemain <= 0)
            { 
                state = BattleState.LoseBattle;
                if (state == BattleState.LoseBattle)
                    messange.text = $"{pStats.NameStat()} lost the battle!";
                Destroy(playerPrefab);
            }
            else
            {
                doneETurn = false;
                messange.text = $"{pStats.NameStat()} turns.";
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
            if (pStats.SpeedStat() < eStats.SpeedStat())
            {
                messange.text = $"{eStats.NameStat()} turns.";
                state = BattleState.EnemyTurn;
            }
            else
            {
                messange.text = $"{pStats.NameStat()} turns.";
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
        annouceBoard.SetActive(true);
    }

    void TurnPlayerBoard()
    {
        annouceBoard.SetActive(false);
        playerBoard.SetActive(true);
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
