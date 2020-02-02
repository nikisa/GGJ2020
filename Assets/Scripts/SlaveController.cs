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
    public float radius;
    public float explosionMultiplier;
    public ForceMode forceMode;
    public float timerMultiplier;
    public Animator animator;



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
    CharacterController characterController;
    Vector3 AxisDirection;
    Vector3 epicentro = Vector3.zero;
    bool inputEnabled;
    Rigidbody rb;
    float time;
    float timerSbuffo;
    

    #region inputs
    string HSC1 = "HorizontalSlaveController1";
    string HSC2 = "HorizontalSlaveController2";
    string VSC1 = "VerticalSlaveController1";
    string VSC2 = "VerticalSlaveController2";
    #endregion

    #region tags
    string slaveTag = "Slave";
    #endregion

    private void Awake() {
        timerSbuffo = 10;
        inputEnabled = true;
        rb = GetComponent<Rigidbody>();
        animator.GetComponent<Animator>();
    }

    void Update() {

        inputEnabled = isTimerFinished();
        if (inputEnabled) {
            CheckInput();
            //GetSlaveWalking();
        }
        else {
            animator.SetBool("isWalking", false);
        }

    }



    public void Movement() {

        if (MyPlayerSelection == PlayerSelection.Player1) {
            if (Mathf.Pow(Input.GetAxis(HSC1), 2) + Mathf.Pow(Input.GetAxis(VSC1), 2) > Mathf.Pow(DeadZone, 2)) {
                animator.SetBool("isWalking", true);
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
                animator.SetBool("isWalking", true);
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
                Rotate();
                Movement();
                SlaveRotation();
            }
            else {
                animator.SetBool("isWalking", false);
            }
        }
        else if (MyPlayerSelection == PlayerSelection.Player2) {
            if (Mathf.Pow(Input.GetAxis(HSC2), 2) + Mathf.Pow(Input.GetAxis(VSC2), 2) > Mathf.Pow(DeadZone, 2)) {
                Rotate();
                Movement();
                SlaveRotation();
            }
            else {
                animator.SetBool("isWalking", false);
            }
        }
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag(slaveTag)) {
            SlaveController slave = collision.gameObject.GetComponent<SlaveController>();
            Rigidbody slaveRB = slave.GetComponent<Rigidbody>();
            epicentro = collision.contacts[0].point;
            power = MoveSpeed;
            timer = MoveSpeed / (MoveSpeed / timerMultiplier);
            time = timer;
            slaveRB.AddExplosionForce(power * explosionMultiplier , epicentro , radius , 0 , forceMode);
            Debug.Log("Epicentro: " + epicentro);
        }
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

    public void playSbuffo() {
        timerSbuffo -= Time.deltaTime;

        if (timer <= 0) {
            SoundManager.PlaySound(SoundManager.Sound.nitrito1);
            timer = 10;
        }
    }

    void GetSlaveWalking() {
        int rng = Random.Range(0, 4);
        int previousNumber = Random.Range(0, 4);

        if (rng == previousNumber && rng != 3) {
            rng++;
        }
        else {
            rng = 0;
        }

        switch (rng) {
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.slaveWalking1);
                previousNumber = 1;
                break;
            case 2:
                SoundManager.PlaySound(SoundManager.Sound.slaveWalking2);
                previousNumber = 2;
                break;
            case 3:
                SoundManager.PlaySound(SoundManager.Sound.slaveWalking3);
                previousNumber = 3;
                break;
            case 4:
                SoundManager.PlaySound(SoundManager.Sound.slaveWalking4);
                previousNumber = 4;
                break;
        }
    }

}
