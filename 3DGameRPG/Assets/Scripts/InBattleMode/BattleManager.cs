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
    [SerializeField] Transform playerStand;
    [SerializeField] Transform rPobotStand, enemyStand;
    [SerializeField] GameObject playerPrefab, robotPPrefab, enemyPrefab;
    IHaveSameStat chosen; //what kind of robot is on battle
    RobotStat rPobots, eStats;
    PlayerStat pStats;

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
    List<SkillConfig> eUsedSkill = new();

    [Header("Skill")]
    [SerializeField] ToggleGroup groupSkill;
    [SerializeField] Transform roomSkill;
    List<Toggle> emptySkills = new List<Toggle>();

    public BattleState CurrentState() { return state; }
    void MessageReceive(string mes) { message.text = mes; }

    private void Awake()
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

    private void Update()
    {
        ControllingMessages();
        StartCoroutine(EnemyAction());
    }

    void PreparingWrapCharactersIn()
    {
        playerPrefab = FindObjectOfType<PlayerStat>().gameObject;
        playerPrefab.transform.SetPositionAndRotation(playerStand.position, playerStand.rotation);
        pStats = playerPrefab.GetComponent<PlayerStat>();

        enemyPrefab = FindObjectOfType<RobotStat>().gameObject;
        enemyPrefab.transform.position = enemyStand.position;
        eStats = enemyPrefab.GetComponent<RobotStat>();
        eStats.CallOutTempStat(); //Call the skill and temp stat

        eStats.HPRemain = eStats.MaxHPStat();

        //stats first update, enemy
        enemyNameScr.text = eStats.NameStat();
        eLevelScr.text = eStats.LvStat().ToString();
        eMaxhpScr.text = "/" + eStats.MaxHPStat().ToString();

        ChoosingCharacterToPlay();
    }

    void ChoosingCharacterToPlay()
    {
        if (pStats.CheckAvailableRobot())
            ChoosingRobot(0); //pick first one
        else ChoosingHuman();

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
            if (rPobots.MaxSPStat() > 0)
            {
                float spRatio = (float)rPobots.SPRemain / rPobots.MaxSPStat(); //tim % nang luong sau khi tu dong hoi
                spBar.rectTransform.localPosition = new Vector3(0, spBar.rectTransform.rect.height * spRatio - spBar.rectTransform.rect.height,
                    0); //day thanh image len tren, bang (tong thanh image * 0.so sp tang - tong thanh image hien tai)
                spRemainScr.text = rPobots.SPRemain.ToString() + "%";
            }
            else if (rPobots.MaxSPStat() == 0)
            {
                spBar.rectTransform.localPosition = new Vector3(0, spBar.rectTransform.rect.height, 0); 
                spRemainScr.text = rPobots.MaxSPStat().ToString() + "%";
            }
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
                    if (rPobots.SPRemain >= emptySkills[i].GetComponent<ICanUseSkill>().CostOfSP())
                    {
                        rPobots.SPRemain -= emptySkills[i].GetComponent<ICanUseSkill>().CostOfSP();

                        if (chosen.AFF == AffectSkill.Normal)
                        {
                            emptySkills[i].GetComponent<ICanUseSkill>().SkillUsed(chosen, eStats);

                            input.Disable();
                            TurnAnnouceBoard();
                            StartCoroutine(FinishingAction(emptySkills[i].GetComponent<ICanUseSkill>(), chosen, eStats));

                            break;
                        }
                        else
                        {
                            TurnAnnouceBoard();
                            StartCoroutine(AffectAction(chosen.AFF, chosen));
                            PlayerAction();
                            break;
                        }
                    }
                    else
                    {
                        TurnAnnouceBoard();
                        MessageReceive("Not enough SP to use this action!?");
                        break;
                    }
                }
        }
        else
        {
            TurnAnnouceBoard();
            MessageReceive("You arent choose any action yet...");
        }
    }

    IEnumerator FinishingAction(ICanUseSkill skill, IHaveSameStat user, IHaveSameStat opp)
    {
        MessageReceive(skill.MessageActionOnly(user));
        yield return new WaitForSeconds(1f); 

        TurnStatusBoard();
        UpdatingStatOnScreen();

        //for animation, wait how long to perform it?
        AttackAnimation();
        yield return new WaitForSeconds(1.8f);

        MessageReceive(skill.MessageUsedSkill(user, opp));
        TurnAnnouceBoard();
        yield return new WaitForSeconds(0.7f);

        if (state == BattleState.PlayerTurn)
            PlayerAction();

        input.OnBattle.Enable();
    }

    void PlayerAction()
    {
        if (eStats.HPRemain <= 0)
        {
            state = BattleState.WonBattle;
            if (state == BattleState.WonBattle)
                MessageReceive($"{pStats.NameStat()} won the battle!");
        }
        else
        {
            if (spUsed && rPobots.SPRemain < rPobots.MaxSPStat())
                rPobots.SPRemain += 20;

            donePTurn = true;
        }
    }

    IEnumerator EnemyAction()
    {
        if (input.OnBattle.SkipDialogue.triggered && donePTurn)
        {
            MessageReceive($"{eStats.NameStat()} turns.");
            donePTurn = false;
        }
        else if (input.OnBattle.SkipDialogue.triggered && state == BattleState.EnemyTurn && !doneETurn)
        {
            input.Disable();
            EnemyAttackAI(out int num);

            if (eStats.AFF == AffectSkill.Normal)
            {
                eUsedSkill[num].skillBtn.GetComponent<ICanUseSkill>().SkillUsed(eStats, chosen);
                eStats.SPRemain -= eUsedSkill[num].skillBtn.GetComponent<ICanUseSkill>().CostOfSP();
                StartCoroutine(FinishingAction(eUsedSkill[num].skillBtn.GetComponent<ICanUseSkill>(), eStats, chosen));
            }
            else StartCoroutine(AffectAction(eStats.AFF, eStats));

            if (eStats.SPRemain < eStats.MaxSPStat())
                eStats.SPRemain += 20;

            doneETurn = true;
        }
        else if (input.OnBattle.SkipDialogue.triggered && state == BattleState.EnemyTurn && doneETurn)
        {
            if (pStats.HPRemain <= 0)
            {
                state = BattleState.LoseBattle;
                if (state == BattleState.LoseBattle)
                    MessageReceive($"{pStats.NameStat()} lost the battle!");

                yield return new WaitForSeconds(0.3f);
                Destroy(playerPrefab);
            }
            else if (chosen.HPRemain <= 0)
            {
                doneETurn = false;

                MessageReceive($"{chosen.NameStat()} lost the battle! {pStats.NameStat()} using another one.");
                rPobots.SPRemain = 0;
                Destroy(robotPPrefab);

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

    void EnemyAttackAI(out int randoAct)
    {
        eUsedSkill.Clear();
        for (int i = 0; i < eStats.ListOfAction().Count; i++)
            if (eStats.SPRemain >= eStats.UsedAction(i).skillBtn.GetComponent<ICanUseSkill>().CostOfSP())
                eUsedSkill.Add(eStats.UsedAction(i));

        randoAct = Random.Range(0, eUsedSkill.Count); 
    }

    IEnumerator AffectAction(AffectSkill affectOne, IHaveSameStat affecter)
    {
        switch (affectOne)
        {
            case AffectSkill.Dizzy:
                MessageReceive($"{affecter.NameStat()} is too dizzy to have an action!");
                affecter.AFF = AffectSkill.Normal;
                yield return new WaitForSeconds(0.7f);
                input.OnBattle.Enable();
                break;
            default:
                Debug.Log("no skill was use, suppose to run the normal one");
                break;
        }
    }

    void ChoosingRobot(int num)
    {
        //check if player have robot. if have, stats first update, player robot
        robotPPrefab = Instantiate(pStats.ChooseRobot(num, true).Itself(), rPobotStand.position, rPobotStand.rotation);
        rPobots = robotPPrefab.GetComponent<RobotStat>();
        rPobots.RobotStats = pStats.ChooseRobot(num);
        rPobots.CallOutTempStat(); //Call the skill and temp stat

        playerNameScr.text = rPobots.NameStat();
        levelScr.text = rPobots.LvStat().ToString();

        chosen = rPobots;
        hOr = true;
        maxHP = rPobots.MaxHPStat();
        maxhpScr.text = "/" + maxHP.ToString();
        spUsed = true;
        spAvailable.SetActive(true);

        PreparingCallSkill(rPobots);
    }

    void ChoosingHuman()
    {
        //stats first update, player
        pStats.IsInBattle(); //let isBattle know player in
        playerNameScr.text = pStats.NameStat();
        levelScr.text = pStats.LvStat().ToString();

        chosen = pStats;
        hOr = false;
        maxHP = pStats.MaxHPStat();
        maxhpScr.text = "/" + maxHP.ToString();
        spUsed = false;
        spAvailable.SetActive(false);

        PreparingCallSkill(pStats);
    }

    #region Other option
    public void OnSwitchRobotcatcher(int num)
    {
        rPobots.SPRemain = 0; //when retreat its temp will reset
        Destroy(robotPPrefab);

        if (num == 0)
            ChoosingHuman();
        else ChoosingRobot(num - 1);

        UpdatingStatOnScreen();
        TurnAnnouceBoard();
        MessageReceive($"{pStats.NameStat()} is changing their unit!");
        donePTurn = true; //switch char will change turn
    }

    public void OnRunaway()
    {
        int runawayChance = Random.Range(0, 2);
        if(runawayChance == 1)
        {
            TurnAnnouceBoard();
            MessageReceive($"You run away from the battle.");
            state = BattleState.LeaveBattle;
        }
        else
        {
            TurnAnnouceBoard();
            MessageReceive($"You can't run away...");
            donePTurn = true;
        }
    }
    #endregion

    #region Annoucement function
    void ControllingMessages()
    {
        if (input.OnBattle.SkipDialogue.triggered && state == BattleState.BeginBattle) //begin battle only
        {
            if (chosen.SpeedStat() < eStats.SpeedStat())
            {
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

        if (input.OnBattle.SkipDialogue.triggered && donePTurn)
        {
            TurnAnnouceBoard();
            state = BattleState.EnemyTurn;
        }

        if (input.OnBattle.SkipDialogue.triggered)
            if(state == BattleState.LeaveBattle || state == BattleState.WonBattle)
            {
                Destroy(enemyPrefab);
                SceneManager.LoadScene("SpawnRoute");
            }
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
    #endregion

    #region Animation-SFX function
    void AttackAnimation()
    {
        if (state == BattleState.PlayerTurn)
        {
            if (robotPPrefab != null)
                KindOfAttackAnimation(robotPPrefab);
            else KindOfAttackAnimation(playerPrefab);

            StartCoroutine(BeingHitAnimation(enemyPrefab));
        }
        else if (state == BattleState.EnemyTurn)
        {
            KindOfAttackAnimation(enemyPrefab);

            if (robotPPrefab != null)
                StartCoroutine(BeingHitAnimation(robotPPrefab));
            else StartCoroutine(BeingHitAnimation(playerPrefab));
        }
    }

    void KindOfAttackAnimation(GameObject userPrefab)
    {
        //prob dont have animation
        userPrefab.GetComponent<AnimationRPG>()?.RegAttackAnim();
    }

    IEnumerator BeingHitAnimation(GameObject userPrefab)
    {
        yield return new WaitForSeconds(1f);

        userPrefab.GetComponent<AnimationRPG>()?.IsHitAnim();
    }
    #endregion

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
