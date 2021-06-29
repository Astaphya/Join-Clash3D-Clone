using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCollision : MonoBehaviour
{
 
  [SerializeField] private Material followerMat;
  [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
   private CharacterController characterController;
   //private SwerveMovement swerveMovement;
   //private SwerveInput swerveInput;
   private CharacterController player;
   private Animator followerAnimator;


   
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        characterController.enabled = false;
      //  swerveMovement = this.GetComponent<SwerveMovement>();
      //  swerveInput = this.GetComponent<SwerveInput>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        followerAnimator = GetComponent<Animator>();
     
    }

   
   
  
  void OnCollisionEnter(Collision other)
  {
      if(other.gameObject.CompareTag("Player"))
      {
          ActivateFollower();
         
         
      }
      
  }

 


  public void ActivateFollower()
  {
        this.transform.SetParent(player.transform);
        characterController.enabled = true;

       // this.swerveMovement.enabled = false;
       // this.swerveInput.enabled = false;

      this.gameObject.tag = "Following";
      skinnedMeshRenderer.material = followerMat;
  }
}
