using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlaveGrabberController : MonoBehaviour
{
    //Inspector
    public DropZoneManager dropZoneManager;
    public float jumpPower;
    public int numJumps;
    public float duration;

    //Private
    private Vector3 distribution;


    private void OnTriggerEnter(Collider other) {
        Debug.Log("Grabbed");
        if (other.gameObject.CompareTag("RedPiece") || other.gameObject.CompareTag("BluePiece")) {
            distribution = new Vector3(Random.Range(-5,5) , 0 , Random.Range(-5f, 5f));
            other.transform.DOJump(dropZoneManager.dropZones[Random.Range(0 , dropZoneManager.dropZones.Length)].transform.position + distribution, jumpPower , numJumps , duration);
            SoundManager.PlaySound(SoundManager.Sound.funnyLaunch);
        }
    }
}
