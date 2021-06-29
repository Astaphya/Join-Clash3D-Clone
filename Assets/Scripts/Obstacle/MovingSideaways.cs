using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSideaways : MonoBehaviour
{
    [SerializeField] private Vector3[] wayPoints;
    [SerializeField] private float speed;
    private float WRadius = 1;
    private int currentIndex = 0;
    private float rotationSpeed;

    

  
    void Update()
    {
        MoveSideaways();
       
        
    }

    public void MoveSideaways()
    {
         if(Vector3.Distance(wayPoints[currentIndex],transform.position) < WRadius)
        {
            currentIndex ++;

            if(currentIndex >= wayPoints.Length)
            {
                currentIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position,wayPoints[currentIndex], Time.deltaTime * speed);

    }
    
    
   
}
