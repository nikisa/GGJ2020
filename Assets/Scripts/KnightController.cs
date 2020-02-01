using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    //Inspector
    #region data
    public GameObject Target;
    public float MaxSpeed;
    public float TimeAcceleration;
    public float DynamicDrag;
    [Range(0,1)]
    public float DeadZone;
    #endregion


    //Public 
    //[HideInInspector]
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
    string HSC1 = "HorizontalSlaveController1";
    string HSC2 = "HorizontalSlaveController2";
    string VSC1 = "VerticalSlaveController1";
    string VSC2 = "VerticalSlaveController2";
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        
        if (Mathf.Pow(Input.GetAxis(HKC1),2) + Mathf.Pow(Input.GetAxis(VKC1), 2) > Mathf.Pow(DeadZone,2)) {
            Debug.LogFormat("Horizontal: {0} --- Vertical: {1}", Input.GetAxis(HKC1), Input.GetAxis(VKC1));
            Rotate();
            MoveSpeed = (MaxSpeed / TimeAcceleration * Time.deltaTime );
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

    public void Rotate() {
        Vector3 inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis(HKC1);
        inputDirection.z = Input.GetAxis(VKC1);
        Target.transform.position = transform.position + inputDirection;

        //Target.transform.RotateAround(transform.position, new Vector3(0, 1, 0), AnalogAngle * Time.deltaTime);
    }
}
