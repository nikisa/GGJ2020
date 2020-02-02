using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightGrabberController : MonoBehaviour
{
    //Inspector
    public LifeDirect lifeDirect;
    public ArmorManager armor;

    //Private
    private Vector3 distribution;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Grabbed");
        if (other.gameObject.CompareTag("RedPiece")) {
            lifeDirect.getLife();
            other.gameObject.transform.SetParent(armor.transform);
            other.gameObject.transform.parent.transform.position = Vector3.zero;
            other.gameObject.SetActive(false);
            SoundManager.PlaySound(SoundManager.Sound.wearingArmor);
        }
    }
}
