﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    
    public enum PlayerSelection{Player1, Player2}

    //Inspector
    #region data
    public PlayerSelection MyPlayerSelection;
    public GameObject Target;
    public float MaxSpeed;
    public float TimeAcceleration;
    public float DynamicDrag;
    [Range(0,1)]
    public float DeadZone;
    #endregion


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
    private float AnalogAngle;
    //private Vector3 startPosition;


    #region Inputs
    string HKC1 = "HorizontalKnightController1";
    string HKC2 = "HorizontalKnightController2";
    string VKC1 = "VerticalKnightController1";
    string VKC2 = "VerticalKnightController2";
    #endregion


    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (MyPlayerSelection == PlayerSelection.Player1) {
            if (Mathf.Pow(Input.GetAxis(HKC1), 2) + Mathf.Pow(Input.GetAxis(VKC1), 2) > Mathf.Pow(DeadZone, 2)) {
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
            if (Mathf.Pow(Input.GetAxis(HKC2), 2) + Mathf.Pow(Input.GetAxis(VKC2), 2) > Mathf.Pow(DeadZone, 2)) {
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

    public void Rotate() {

        if (MyPlayerSelection == PlayerSelection.Player1) {
            Vector3 inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxis(HKC1);
            inputDirection.z = Input.GetAxis(VKC1);
            Target.transform.position = transform.position + inputDirection;
        }
        else if (MyPlayerSelection == PlayerSelection.Player2) {
            Vector3 inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxis(HKC2);
            inputDirection.z = Input.GetAxis(VKC2);
            Target.transform.position = transform.position + inputDirection;
        }


        
    }
}
