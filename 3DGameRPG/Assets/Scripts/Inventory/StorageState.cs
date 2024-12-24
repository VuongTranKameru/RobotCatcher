using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageState : MonoBehaviour
{
    [SerializeField] RobotStorageUI storageUI;

    public static StorageState Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
