using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    internal static PlayerManager instance;

    private void Start()
    {
        //dung static de xac dinh duy nhat 1 player ton tai, ko bi nhan len
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //ko huy player khi chuyen scene
        DontDestroyOnLoad(gameObject);
    }
}
