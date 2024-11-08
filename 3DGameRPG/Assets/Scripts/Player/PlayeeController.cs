using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeeController : MonoBehaviour
{
    PlayerInput inputAction;
    CharacterController charCtrl;

    [SerializeField] int speed;
    Vector2 movementInput;

    void Awake()
    {
        inputAction = new PlayerInput();
        charCtrl = GetComponent<CharacterController>();
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

        //convert vector2 thanh vector3, khong di chuyen len cao y nen = 0
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        //dich chuyen tuc thoi vi o khoang cach qua be
        charCtrl.Move(moveDirection * Time.deltaTime);


        /*if (movementInput.sqrMagnitude > 0)
        {
            Quaternion angleQCam = Quaternion.AngleAxis(
              Camera.main.transform.rotation.eulerAngles.y, Vector3.up); //lay tham so y khi nv quay truc
            Quaternion lookRotation = Quaternion.LookRotation(angleQCam * new Vector3(movementInput.x, 0, movementInput.y));
            //huong nhin cua camera khi quay

            Quaternion.RotateTowards(transform.rotation, lookRotation, 560 * Time.deltaTime);
            //co the thay rotatetowards bang lerp, slerp, lerpunclamped, slerpunlamped, transform.forward

            charCtrl.Move(speed * Time.deltaTime * transform.forward);
        }*/

        /*Running();

        if (speed > 2)
            anim.SetFloat("Speed", movementInput.sqrMagnitude + 1);
        else
            anim.SetFloat("Speed", movementInput.sqrMagnitude); //sqrMag tra lai do dai cua vector

        Jumping();*/
    }
}
