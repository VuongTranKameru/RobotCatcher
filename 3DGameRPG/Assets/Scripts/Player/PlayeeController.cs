using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeeController : MonoBehaviour
{
    internal PlayerInput inputAction;
    CharacterController charCtrl;
    Animator anim;

    [SerializeField] int speed;
    Vector2 movementInput;

    void Awake()
    {
        inputAction = new PlayerInput();
        charCtrl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    void Update()
    {
        if (inputAction.Player.enabled)
        {
            movementInput = inputAction.Player.Movement.ReadValue<Vector2>();
            movementInput.Normalize();

            Moving();
            Running();

            if (speed > 3)
                anim.SetFloat("isSpeeding", movementInput.sqrMagnitude + 1);
            else
                anim.SetFloat("isSpeeding", movementInput.sqrMagnitude); //sqrMag tra lai do dai cua vector
        }
        else if (!inputAction.Player.enabled)
            anim.SetFloat("isSpeeding", 0);
    }

    void Moving()
    {
        /*Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        charCtrl.Move(moveDirection * Time.deltaTime);*/

        if (movementInput.sqrMagnitude > 0)
        {
            Quaternion angleQCam = Quaternion.AngleAxis(
              Camera.main.transform.rotation.eulerAngles.y, Vector3.up); //lay tham so y khi nv quay truc
            Quaternion lookRotation = Quaternion.LookRotation(angleQCam * new Vector3(movementInput.x, 0, movementInput.y));
            //huong nhin cua camera khi quay

            transform.rotation = lookRotation;

            charCtrl.Move(speed * Time.deltaTime * transform.forward);
        }
    }

    void Running()
    {
        if (inputAction.Player.Running.IsPressed())
            speed = 5;
        else
            speed = 3;
    }

    /*void Jumping()
    {
        if (inputAction.Player.Jumping.triggered)
            anim.SetTrigger("checkJumping");
    }*/
}
