using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LifeDirect : MonoBehaviour
{



    //Inspector
    public GameObject PiecesPool;
    public int lifes;
    public float timerAttack;
    public ArmorManager armor;
    public DropZoneManager dropZoneManager;
    public float jumpPower;
    public int jumpNum;
    public float jumpDuration;

    //Public
    [HideInInspector]
    public float timeAttack;
    [HideInInspector]
    public DropZone dropZone;

    //Private
    bool canAttack;
    Vector3 distribution;

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
            if (lifes >= 0) {
                GameObject armorPiece = armor.ArmorPieces[lifes];
                GameObject actualPiece = armor.ActualPieces[lifes];
                distribution = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2f, 2));
                armorPiece.transform.gameObject.SetActive(true);
                armorPiece.transform.SetParent(PiecesPool.transform);
                actualPiece.transform.gameObject.SetActive(false);
                armorPiece.transform.DOJump(dropZone.dropPoints[Random.Range(0, dropZone.dropPoints.Length)].transform.position + distribution, jumpPower, jumpNum, jumpDuration);
                SoundManager.PlaySound(SoundManager.Sound.steelBrokenArmor1);
                SoundManager.PlaySound(SoundManager.Sound.spearSound);
                SoundManager.PlaySound(SoundManager.Sound.nitrito1);
            }
            else if (lifes < 0) {
                Debug.Log("GAMEOVER");
                ANIM
            }
        }
    }

    public void getLife() {
        GameObject actualPiece = armor.ActualPieces[lifes];
        actualPiece.SetActive(true);
        lifes++;
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

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Zone1")) {
            dropZone = dropZoneManager.dropZones[0];
        }
        else if (other.CompareTag("Zone2")) {
            dropZone = dropZoneManager.dropZones[1];
        }
        else if (other.CompareTag("Zone3")) {
            dropZone = dropZoneManager.dropZones[2];
        }
        else if (other.CompareTag("Zone4")) {
            dropZone = dropZoneManager.dropZones[3];
        }
    }
}
