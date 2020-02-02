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
    public GameObject Spear;
    public Rigidbody EnemyRB;
    public LifeDirect myLifeDirect;
    public float MaxSpeed;
    public float TimeAcceleration;
    public float DynamicDrag;
    [Range(0,1)]
    public float DeadZone;
    public float radius;
    public float explosionMultiplier;
    public ForceMode forceMode;
    public float timerMultiplier;
    public float spearLength;
    #endregion


    //Public 
    [HideInInspector]
    public float MoveSpeed;
    [HideInInspector]
    public float power;
    [HideInInspector]
    public Vector3 Inertia;
    [HideInInspector]
    public Vector3 OldPos;
    [HideInInspector]
    public Vector3 VectorAngle;
    [HideInInspector]
    public float timer;

    //Private
    bool inputEnabled;
    Vector3 epicentro = Vector3.zero;
    Rigidbody rb;
    float time;
    float timeAttack;

    #region tags
    string spearTag = "Spear";
    string knightTag = "Knight";
    #endregion


    #region Inputs
    string HKC1 = "HorizontalKnightController1";
    string HKC2 = "HorizontalKnightController2";
    string VKC1 = "VerticalKnightController1";
    string VKC2 = "VerticalKnightController2";
    #endregion


    private void Awake() {
        inputEnabled = true;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputEnabled = isTimerFinished();
        if (inputEnabled) CheckInput();
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

    public void KnightRotate() {
        transform.LookAt(Target.transform.position);
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

    public void CheckInput() {

        if (MyPlayerSelection == PlayerSelection.Player1) {
            if (Mathf.Pow(Input.GetAxis(HKC1), 2) + Mathf.Pow(Input.GetAxis(VKC1), 2) > Mathf.Pow(DeadZone, 2)) {
                Rotate();
                Move();
                KnightRotate();
            }
        }
        else if (MyPlayerSelection == PlayerSelection.Player2) {
            if (Mathf.Pow(Input.GetAxis(HKC2), 2) + Mathf.Pow(Input.GetAxis(VKC2), 2) > Mathf.Pow(DeadZone, 2)) {
                Rotate();
                Move();
                KnightRotate();
            }
        }
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag(spearTag)) {
            Debug.Log("SPEAR HAS BEEN HIT");
            KnightController knight = collision.gameObject.GetComponent<KnightController>();
            Rigidbody knightRB = knight.GetComponent<Rigidbody>();
            epicentro = collision.contacts[0].point;
            power = MoveSpeed;
            timer = MoveSpeed / (MoveSpeed / timerMultiplier);
            time = timer;
            myLifeDirect.GetDamage();
            doBounce(knightRB , power);
        }
        else if (collision.transform.CompareTag(knightTag)) {
            LifeDirect lifeDirect = collision.gameObject.GetComponent<LifeDirect>();
            epicentro = collision.contacts[0].point;
            power = MoveSpeed;
            timer = MoveSpeed / (MoveSpeed / timerMultiplier);
            time = timer;
            lifeDirect.GetDamage();
            Debug.Log("KNIGHT HAS BEEN HIT " + EnemyRB.name);
            doBounce(rb, power);
        }

    }

    public void doBounce(Rigidbody _knightRB , float _power) {
        _knightRB.AddExplosionForce(_power * explosionMultiplier, epicentro, radius, 0, forceMode);
    }

    public bool isTimerFinished() {
        bool result = false;
        time -= Time.deltaTime;
        if (time < 0) {
            timer = 0;
            rb.isKinematic = true;
            rb.isKinematic = false;
            result = true;
        }
        return result;
    }

    
}
