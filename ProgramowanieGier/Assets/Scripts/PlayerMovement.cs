using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrength = 20f;
    public float verticalRotationLimit = 80f;
    private float verticalRotation = 0;
    private float forwardMovement;
    private float sidewaysMovement;
    private float verticalVelocity;
    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Rozglądanie lewo prawo
        float horizonalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizonalRotation, 0);

        //Rozglądanie góra dół
        this.verticalRotation -= Input.GetAxis("Mouse Y");
        this.verticalRotation = Mathf.Clamp(this.verticalRotation, -this.verticalRotationLimit, this.verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(this.verticalRotation, 0, 0);

        //Poruszanie graczem
        forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
        sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;

        //Bieganie jeśli gracz wcisnął Lewy Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
        }

        if (cc.isGrounded)
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Recoil.spread = Recoil.PISTOL_SHOTTING_RUN;
                }
                else
                {
                    Recoil.spread = Recoil.PISTOL_SHOTTING_WALK;
                }
            }
        }
        else
        {
            Recoil.spread = Recoil.PISTOL_SHOTTING_JUMP;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        //Skok
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            this.verticalVelocity = jumpStrength;
        }

        Vector3 playerMovement = new Vector3(this.sidewaysMovement, this.verticalVelocity, this.forwardMovement);


        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }

}
