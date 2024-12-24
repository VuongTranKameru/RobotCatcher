using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfItem
{
    InBattle,
    Upgrade,
    Scrap,
    KeyItem
}

[CreateAssetMenu(fileName = "ItemScriptData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemConfig : ScriptableObject
{
    [SerializeField] internal string itemID, itemName;
    [SerializeField] internal TypeOfItem type;
    [SerializeField] internal string itDesc;
    [SerializeField] internal int value;

    [Header("Png")]
    [SerializeField] internal Sprite icon;

    public bool Healing(StatConfig user, ItemConfig ite)
    {
        //ignore case when player use item on already full bar ally, so on lost turn and an item

        if (ite.itemID == "IN06") //heal status effect
        {
            if (user.status != StatusEffect.None)
            {
                Debug.Log("healing status to normal");
                user.status = StatusEffect.None;
                return true;
            }
            return false;
        }

        if (ite.itemID == "IN03") //heal sp
        {
            if (user.specialPoint < user.maxSP)
            {
                Debug.Log("healing special point");
                user.specialPoint += Mathf.RoundToInt(user.maxSP * ite.value / 100f);
                if (user.specialPoint > user.maxSP)
                    user.specialPoint = user.maxSP;
                return true;
            }
            return false;
        }

        if (user.health > 0 && user.health < user.maxHP)
        {
            if (user.uniqueID == "hmkdcalchione")
            {
                if (ite.itemID == "IN05") //for human only
                {
                    Debug.Log("heal on human");
                    user.health += ite.value;
                    if (user.health > user.maxHP)
                        user.health = user.maxHP;
                    return true;
                }
                return false;
            }

            if (ite.itemID != "IN05") //when ally still alive
            {
                Debug.Log("healing hp");
                user.health += ite.value;
                if (user.health > user.maxHP)
                    user.health = user.maxHP;
                return true;
            }
        }

        if (ite.itemID == "IN04" && user.health == 0)
        { 
            user.health = user.maxHP;
            user.status = StatusEffect.None;
            return true;
        }

        return false;
    }
}
