using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float maxHealth = 6000;
    public float currentHealth;
    [SerializeField] private Transform player;
    private Vector3 playerPos;
    private Rigidbody bossRigid;
    private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private ParticleSystem dieParticle;

    public Transform attackPoint;
    public float attackRadius = 0.5f;

    public int attackDamage = 50;
    public LayerMask followerLayers;
    public float rotationSpeed = 1f;

     private Vector3 fightPosition = new Vector3(0,0,590);



    
    private void Start()
    {
        animator = GetComponent<Animator>();
        bossRigid = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }
    
  
    private void FixedUpdate()
    {
       // BossBehaviour();
       // LookAtPlayer();
      //  playerPos = player.position;
    }


    public void LookAtPlayer()
    {
        //Vector3 targetPos = new Vector3(bossRigid.rotation.x , playerPos.y , bossRigid.rotation.z);
        transform.LookAt(fightPosition);
    }

    public void BossBehaviour()
    {

       // Vector3 target = new Vector3(playerPos.x , bossRigid.position.y , playerPos.z );
       // Vector3 newPos = Vector3.MoveTowards(bossRigid.position , target , Time.fixedDeltaTime * speed);
        Vector3 target = new Vector3(fightPosition.x , bossRigid.position.y , fightPosition.z );
        Vector3 newPos = Vector3.MoveTowards(bossRigid.position , target , Time.fixedDeltaTime * speed);
       
        bossRigid.MovePosition(newPos);

        animator.SetTrigger("Running");

        

        if(Vector3.Distance(fightPosition , bossRigid.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Debug.Log("Boss atack yapıyor.");
                BossHit();
            }
            
        }

        else 
        {
            animator.ResetTrigger("Attack");

        }
    }

    public void BossHit()
    {
        //Deteck enemies in range of attack
       Collider[] hitFollowers  =  Physics.OverlapSphere(attackPoint.position,attackRadius,followerLayers);

       //Damage them
       
       foreach (Collider follower in hitFollowers)
       {
           follower.GetComponent<Follower>().TakeDamage(attackDamage);
           Debug.Log("We hit : " + follower.name);
       }

    }

    public void BossDie()
    {
        ParticleSystem particle = Instantiate(dieParticle , transform.position, transform.rotation);
        Destroy(particle,1.5f);
        this.gameObject.SetActive(false);
        Debug.Log("Died");
    }

    public void BossTakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            // Die animation
            Debug.Log("Boss died");
            BossDie();
        }

    }

   
   void OnDrawGizmosSelected()
    {
      if(attackPoint == null) return;

      Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
      
    }
}
