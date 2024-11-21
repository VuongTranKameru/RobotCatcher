using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.tag = "Respawn";
        StartCoroutine(SwitchToPlayerTag());
    }

    IEnumerator SwitchToPlayerTag()
    {
        yield return new WaitForSeconds(1f);
        gameObject.tag = "Player";
    }
}
