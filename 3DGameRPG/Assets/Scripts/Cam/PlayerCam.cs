using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] Transform mRotateLookAt;
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;

    private void OnEnable()
    {
        mRotateLookAt = GameObject.FindGameObjectWithTag("LookAtPOVCam").transform;
        //player = FindFirstObjectByType<PlayerManager>().gameObject.transform;
        orientation = GameObject.FindGameObjectWithTag("Respawn")?.transform;
        orientation = GameObject.FindGameObjectWithTag("Player").transform;
        playerObj = FindObjectOfType<PlayerManager>().transform;
        player = playerObj;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (playerObj == null) //fix bug
        {
            playerObj = GameObject.FindGameObjectWithTag("PlayerModel").transform;
            player = playerObj;
        }

        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        Vector3 dirToCombatLookAt = mRotateLookAt.position - new Vector3(transform.position.x, mRotateLookAt.position.y, transform.position.z);
        orientation.forward = dirToCombatLookAt.normalized;

        playerObj.forward = dirToCombatLookAt.normalized;
    }
}
