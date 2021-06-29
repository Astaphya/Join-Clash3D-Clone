using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public  bool isFinish = false;
 // Player ve followerlar hedef positiona ilerleyecek. Boss attackRange' e girince followerlar boss'a atack yapıcak.
 // Kazanır isek player ve followerlar dance animasyonuna geçecek.

  void OnTriggerEnter(Collider other)
 {
     if(other.gameObject.CompareTag("Following"))
     {
         isFinish = true;
         Debug.Log("Finish: " + isFinish);
     }
     
 }
}
