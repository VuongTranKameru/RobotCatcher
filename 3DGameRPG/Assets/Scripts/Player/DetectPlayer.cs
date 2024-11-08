using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private void Start()
    {
        gameObject.tag = "Respawn";
        Invoke(nameof(SwitchToPlayerTag), 1f);
    }

    void SwitchToPlayerTag()
    {
        gameObject.tag = "Player";
    }
}
