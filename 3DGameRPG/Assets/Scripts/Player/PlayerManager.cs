using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableListJson<T>
{
    public List<T> list;
}

public class PlayerManager : MonoBehaviour
{
    internal static PlayerManager instance;

    [Header("SAVE STATE")]
    [SerializeField] internal SerializableListJson<string> listRobotJson;

    private void Start()
    {
        //dung static de xac dinh duy nhat 1 player ton tai, ko bi nhan len
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //ko huy player khi chuyen scene. nen instantiante player moi thay vi dung
        //DontDestroyOnLoad(gameObject);
    }

    public void AddNewRobot(string json)
    {
        listRobotJson.list.Add(json);
    }
}
