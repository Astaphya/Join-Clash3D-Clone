using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun : MonoBehaviour
{


    private Transform Player;
    private Animator animator ;
    private Vector3 playerPos;
    private Rigidbody rb;
    private float moveSpeed = 12f;
    public float runRange = 200f;
  
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    
    void FixedUpdate()
    {
        playerPos = Player.position;
        Move();
        
    }
    
   public void Move()
   {
       if(Vector3.Distance(playerPos , this.transform.position ) <= runRange)
       {
           Vector3 target = new Vector3(0 , 0 , playerPos.z );
           Vector3 newPos = Vector3.MoveTowards(rb.position , target , Time.fixedDeltaTime * moveSpeed);
           rb.MovePosition(newPos);
           animator.SetTrigger("Running");
           //transform.LookAt(playerPos);
           
       }


   }
}
