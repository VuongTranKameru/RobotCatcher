using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject blockText;
    private void OnTriggerEnter(Collider other)
    {
        blockText.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        blockText.SetActive(false);
    }
}
