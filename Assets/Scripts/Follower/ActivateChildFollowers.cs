using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildFollowers : MonoBehaviour
{
    [SerializeField] private GameObject[] childObjects;
   
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Following"))
        {
            for(int i = 0 ; i < childObjects.Length ; i++)
            {
                
                childObjects[i].GetComponent<FollowerCollision>().ActivateFollower();
               // childObjects[i].transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
            }
        }
        
    }
}
