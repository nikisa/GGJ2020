using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlaveController : MonoBehaviour
{

    public enum PlayerSelection { Player1, Player2 }

    //Inspector
    public PlayerSelection MyPlayerSelection;
    public GameObject Target;
    public float MaxSpeed;
    public float TimeAcceleration;
    public float DynamicDrag;
    public float RotationSpeed;
    [Range(0, 1)]
    public float DeadZone;

    //Public
    [HideInInspector]
    public float MoveSpeed;
    [HideInInspector]
    public Vector3 Inertia;
    [HideInInspector]
    public Vector3 OldPos;
    [HideInInspector]
    public Vector3 VectorAngle;


    //Private
    CharacterController characterController;
    Vector3 AxisDirection;
    Vector3 moveDirection = Vector3.zero;

    #region inputs
    string HSC1 = "HorizontalSlaveController1";
    string HSC2 = "HorizontalSlaveController2";
    string VSC1 = "VerticalSlaveController1";
    string VSC2 = "VerticalSlaveController2";
    #endregion

    void Update() {
        CheckInput();
    }



    public void Movement() {

        if (MyPlayerSelection == PlayerSelection.Player1) {
            if (Mathf.Pow(Input.GetAxis(HSC1), 2) + Mathf.Pow(Input.GetAxis(VSC1), 2) > Mathf.Pow(DeadZone, 2)) {
                Rotate();
                MoveSpeed = (MaxSpeed / TimeAcceleration * Time.deltaTime);
                DynamicDrag = (MaxSpeed - MoveSpeed) / MaxSpeed;
                VectorAngle = Target.transform.position - transform.position;
                OldPos = transform.position;
                transform.position = transform.position + Inertia + MoveSpeed * VectorAngle.normalized * Time.deltaTime;
                Inertia = (transform.position - OldPos) * (DynamicDrag);
            }
            else {
                MoveSpeed = 0;
            }
        }
        else if (MyPlayerSelection == PlayerSelection.Player2) {
            if (Mathf.Pow(Input.GetAxis(HSC2), 2) + Mathf.Pow(Input.GetAxis(VSC2), 2) > Mathf.Pow(DeadZone, 2)) {
                Rotate();
                MoveSpeed = (MaxSpeed / TimeAcceleration * Time.deltaTime);
                DynamicDrag = (MaxSpeed - MoveSpeed) / MaxSpeed;
                VectorAngle = Target.transform.position - transform.position;
                OldPos = transform.position;
                transform.position = transform.position + Inertia + MoveSpeed * VectorAngle.normalized * Time.deltaTime;
                Inertia = (transform.position - OldPos) * (DynamicDrag);
            }
            else {
                MoveSpeed = 0;
            }
        }


    }


    public void SlaveRotation() {
        transform.LookAt(Target.transform.position);
    }

    public void Rotate() {

        if (MyPlayerSelection == PlayerSelection.Player1) {
            Vector3 inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxis(HSC1);
            inputDirection.z = Input.GetAxis(VSC1);
            Target.transform.position = transform.position + inputDirection;
        }
        else if (MyPlayerSelection == PlayerSelection.Player2) {
            Vector3 inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxis(HSC2);
            inputDirection.z = Input.GetAxis(VSC2);
            Target.transform.position = transform.position + inputDirection;
        }

    }

    public void CheckInput() {

        if (MyPlayerSelection == PlayerSelection.Player1) {
            if (Mathf.Pow(Input.GetAxis(HSC1), 2) + Mathf.Pow(Input.GetAxis(VSC1), 2) > Mathf.Pow(DeadZone, 2)) {
                Debug.Log("moveDirection: " + moveDirection);
                Rotate();
                Movement();
                SlaveRotation();
            }
        }
        else if (MyPlayerSelection == PlayerSelection.Player2) {
            if (Mathf.Pow(Input.GetAxis(HSC2), 2) + Mathf.Pow(Input.GetAxis(VSC2), 2) > Mathf.Pow(DeadZone, 2)) {
                Debug.Log("moveDirection: " + moveDirection);
                Rotate();
                Movement();
                SlaveRotation();
            }


        }

    }
}
