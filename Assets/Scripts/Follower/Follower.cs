using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour,IRunner
{
    private int maxHealth = 600;
    private int currentHealth;
    private float movingSpeed = 3f;
    private float speedToEnemy = 10f;

    private float followerAttackRange = 3f;

    private Rigidbody followerRigid;
    private Animator animator;
    private CharacterController characterController;
    private GameObject Boss;
    private float bossHealth;

    [SerializeField] private ParticleSystem dieParticle;

     private  Vector3 fightPosition = new Vector3(0,0,580);

     [SerializeField] private Transform leftHandAttackPoint;
    [SerializeField] private Transform rightHandAttackPoint;
     private int attackDmg = 20;
    private float attackRadius = 0.2f ;


    public LayerMask BossLayer;
    void Start()
    {
        currentHealth = maxHealth ;
        followerRigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Boss = GameObject.FindGameObjectWithTag("Boss");
        
    }

   
    void Update()
    {
        bossHealth = Boss.GetComponent<Boss>().currentHealth;
        Win();
    }

  

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Play hurt animation

        if(currentHealth <= 0)
        {
             Die();
            //Die animation and disable object
        }
    }

    public void Die()
    {
        ParticleSystem particle = Instantiate(dieParticle , transform.position, transform.rotation);
        Destroy(particle,1f);
        this.gameObject.SetActive(false);
        Debug.Log("Died");

    }

    public void HitDamage()
    {
        Collider[] leftHandColliders = Physics.OverlapSphere(leftHandAttackPoint.position ,attackRadius,BossLayer );
        Collider[] rightHandColliders = Physics.OverlapSphere(rightHandAttackPoint.position ,attackRadius,BossLayer );

        //Left hand attack Range
        foreach (Collider leftCollider in leftHandColliders)
        {
            Boss.GetComponent<Boss>().BossTakeDamage(attackDmg);
            Debug.Log("Left hand hit:");
            
        }

            // Right hand attack Range

           foreach (Collider rightCollider in rightHandColliders)
        {
            Boss.GetComponent<Boss>().BossTakeDamage(attackDmg);
            Debug.Log("Right hand hit:");
            
        }
    }

    void OnDrawGizmosSelected()
    {
      if(leftHandAttackPoint == null || rightHandAttackPoint == null) return;

      Gizmos.DrawWireSphere(leftHandAttackPoint.position,attackRadius);
      Gizmos.DrawWireSphere(rightHandAttackPoint.position,attackRadius);
      
    }

  

    //interface fonksiyon
    public void MoveToEnemy(Vector3 direction)
    {
        // Eğer enemy range içerisinde ise enemy'e doğru hızla koş
        // Kaç enemy var ise o kadar follower koşacak.

        Vector3 target = new Vector3(direction.x , 0 , direction.z );
        Vector3 newPos = Vector3.MoveTowards(followerRigid.position , target , Time.fixedDeltaTime * speedToEnemy);
        followerRigid.MovePosition(newPos);
        transform.LookAt(newPos);
        transform.SetParent(null);
        animator.SetFloat("Speed",1);
        GetComponent<CharacterController>().enabled = false;
      

    }

    public void Win()
    {
        if(bossHealth <= 0)
        {
            animator.SetBool("isWin",true);
            transform.LookAt(null);
            followerRigid.isKinematic = true;
            followerRigid.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    public  void MoveToPosition()
    {
        // Following taglı objeler trigger tetiklenince Boss'a doğru koşacak ve savaşacak.
         if(this.gameObject.CompareTag("Following"))
        {
        Vector3 target = new Vector3(Boss.transform.position.x , 0 , Boss.transform.position.z );
        Vector3 newPos = Vector3.MoveTowards(followerRigid.position , target , Time.fixedDeltaTime * movingSpeed);
        followerRigid.MovePosition(newPos);
        transform.LookAt(newPos);
        
       
        this.transform.SetParent(null);
        characterController.enabled = false;

        if(Boss.gameObject.activeInHierarchy)
        {
                if(Vector3.Distance(followerRigid.position , Boss.transform.position) <= followerAttackRange)
                {
        
                    //Follower attack animation çalışacak.
                    animator.SetTrigger("Punch");
                    HitDamage();
            
           
                }

                else
                { 
           
                    animator.SetFloat("Speed",1);
                }
        }
        

        }
        
    }
}
