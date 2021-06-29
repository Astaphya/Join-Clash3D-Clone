using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject[] followers;
   [SerializeField] private GameObject player;
   [SerializeField] private GameObject Boss;
   [SerializeField] private FinishTrigger finishTrigger;
   [SerializeField] private GameObject[] enemies;
   private float xBorder = 7.5f;

   private Vector3 bossPos;
   private int followerLength;

   public bool isRightSwipeable ; 
   public bool isLeftSwipeable ;
   public List<GameObject> following; // Followers , following the player
   
   private Vector3 firstEnemy;
   private Vector3 secondEnemy;
   public bool isActive = false;
   private float followerRunRange = 75f;
   private int lenght;

   
  

  
   void Start()
   {
       followerLength = followers.Length;
       following = new List<GameObject>();
       firstEnemy = enemies[0].transform.position;
       secondEnemy = enemies[1].transform.position;
       
       
   }
  
    void Update()
    {
        bossPos = Boss.transform.position;
        // Bütün objelerin x değerleri kontrol edilecek ve herhangi bir obje sınır değerinde ise swerve işlemi o sınır değerin olduğu yöne yapılmayacak.

        // Followerlar ve player boss'a koşacak. Boss aktif olacak ve playera doğru koşacak.

        if(finishTrigger.isFinish)
        {
            Boss.GetComponent<Boss>().BossBehaviour();
            MoveFollowers();
            // Player move to Boss
            player.GetComponent<CharacterController>().RunToBoss(bossPos);
            player.GetComponent<SwerveInput>().enabled = false;
            player.GetComponent<SwerveMovement>().enabled = false;

        }
        
        firstEnemy = enemies[0].transform.position;
        secondEnemy = enemies[1].transform.position;

        

       

        ControlFollowerPositions();

      
       
        
    }

  
    void FixedUpdate()
    {
        RunToEnemy();
        
    }

    public void MoveFollowers()
    {
        
        for(int i = 0 ;i< followers.Length ; i++)
        {
            if(followers[i].activeInHierarchy)
            {
                followers[i].GetComponent<Follower>().MoveToPosition();

            }
            
        }
    }

    public void RunToEnemy()
    {
        
        // Çarpışacak olan düşman ile player arasındaki range

        if(Vector3.Distance(firstEnemy , player.transform.position) <= followerRunRange)
        {
            if(!isActive)
            {
                
                lenght = FindGameObjectInChildWithTag();
                isActive = true;
              
                Debug.Log("Girdi");
            }
              following[lenght -1].GetComponent<Follower>().MoveToEnemy(firstEnemy);
              following[lenght-2].GetComponent<Follower>().MoveToEnemy(secondEnemy);
             
             
            
            
            
        }

        else{
            return;
        }
        

    }

    public int  FindGameObjectInChildWithTag()
     {
         foreach (Transform child in player.transform)
         {
             if(child.tag == "Following" && child.gameObject.activeInHierarchy)
             following.Add(child.gameObject);
             
         }

         return following.Count;


 
    
     }


    public void  ControlFollowerPositions()
    {
        // Followerlar sağ köşede sağa sol köşede ise sola doğru swipe işlemi yapamacak.

        for(int i = 0 ; i< followers.Length;i++)
        {
            if(followers[i].activeInHierarchy)
            {
                if(followers[i].transform.position.x >= xBorder )
                {

                isRightSwipeable = false;
                break;

                

                }

                else if(followers[i].transform.position.x <= -xBorder )
                {
                isLeftSwipeable = false;
                break;

                }

                else
                {
                isLeftSwipeable = true;
                isRightSwipeable = true;

                }

            }
            
            
        }
        

        
    }



    
}
