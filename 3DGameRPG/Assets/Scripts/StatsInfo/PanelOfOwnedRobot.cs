using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelOfOwnedRobot : MonoBehaviour
{
    [SerializeField] TMP_Text nameTag;
    [SerializeField] TMP_Text levelHolder, hpNum, spNum;
    [SerializeField] Image charAva;
    [SerializeField] GameObject playableStatus;

    public TMP_Text NameTag
    {
        get { return nameTag; }
        set { nameTag = value; }
    }

    public TMP_Text LevelHolder
    {
        get { return levelHolder; }
        set { levelHolder = value; }
    }

    public TMP_Text HPNum
    {
        get { return hpNum; }
        set { hpNum = value; }
    }

    public TMP_Text SPNum
    {
        get { return spNum; }
        set { spNum = value; }
    }

    public Image CharAva
    {
        get { return charAva; }
        set 
        { 
            charAva = value;
            charAva.SetNativeSize();
            charAva.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void PlayableOrDead(Sprite spriteField)
    {
        playableStatus.GetComponent<Image>().sprite = spriteField;
        playableStatus.GetComponent<Toggle>().transition = Selectable.Transition.None;
        playableStatus.GetComponent<Toggle>().interactable = false;
    }

    public void DeadStatus(Sprite spriteField)
    {
        playableStatus.GetComponent<Image>().sprite = spriteField;
        playableStatus.GetComponent<Button>().transition = Selectable.Transition.None;
    }
}
