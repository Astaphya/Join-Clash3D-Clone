using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
 
    private SwerveInput swerveInput;
    private CharacterController characterController;
    [SerializeField] private float swerveSpeed;
    private float swerveLimit = 1f;
    private float speedLimitTurn = 0.6f;
    public float  swerveAmount;
    private GameManager gameManager;



   
    private void Awake()
    {
        swerveInput = GetComponent<SwerveInput>();
        characterController = GetComponent<CharacterController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
    }

    public void SwerveMove()
    {
        
        if(characterController.speed > speedLimitTurn)
        {

            swerveAmount = Time.deltaTime * swerveSpeed * swerveInput.MoveFactor;
            swerveAmount = Mathf.Clamp(swerveAmount , -swerveLimit,swerveLimit);

            // Followers sol köşe sınırında ise sola swerve engelleniyor.
            if(!gameManager.isLeftSwipeable  && swerveInput.MoveFactor < 0 )
            {
                return;
            } 

            //Takipçiler sağ köşe sınırında ise sağa swerve engelleniyor.

            else if(!gameManager.isRightSwipeable  && swerveInput.MoveFactor > 0)
            {
                return;
            }

          

            else
            {
                 transform.Translate(swerveAmount ,0 ,0);

            }

        
        

        

        


        }

        
    }

    // Update is called once per frame
    void Update()
    {
    SwerveMove();
    
    }
}
