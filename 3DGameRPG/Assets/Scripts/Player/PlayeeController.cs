using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeeController : MonoBehaviour
{
    PlayerInput inputAction;
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
        movementInput = inputAction.Player.Movement.ReadValue<Vector2>();
        movementInput.Normalize();

        Moving();
        Running();

        if (speed > 2)
            anim.SetFloat("isSpeeding", movementInput.sqrMagnitude + 1);
        else
            anim.SetFloat("isSpeeding", movementInput.sqrMagnitude); //sqrMag tra lai do dai cua vector
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

            charCtrl.Move(transform.forward * Time.deltaTime * speed);
        }
    }

    void Running()
    {
        if (inputAction.Player.Running.IsPressed())
            speed = 5;
        else
            speed = 2;
    }

    /*void Jumping()
    {
        if (inputAction.Player.Jumping.triggered)
            anim.SetTrigger("checkJumping");
    }*/
}
