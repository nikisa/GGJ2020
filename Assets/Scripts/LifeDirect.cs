using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDirect : MonoBehaviour
{
    //Inspector
    public int lifes;
    public float timerAttack;

    //Public
    [HideInInspector]
    public float timeAttack;

    //Private
    bool canAttack;

    private void Awake() {
        timeAttack = timerAttack;
    }

    void Update()
    {
        isAttackTimerFinished();
    }

    public void GetDamage() {
        if (canAttack) {
            timeAttack = timerAttack;
            Debug.LogFormat("{0} has {1} lifes: ", transform.gameObject.name, lifes);
            lifes--;
        }
    }

    public void isAttackTimerFinished() {
        timeAttack -= Time.deltaTime;
        if (timeAttack < 0) {
            canAttack = true;
        }
        else {
            canAttack = false;
        }
    }
}
