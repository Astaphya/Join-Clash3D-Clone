using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    
    #region TouchControl
   
   
    private Touch theTouch;
    private Vector2 touchStartPos,touchEndPos;
    #endregion
    
    
   

    [SerializeField] private GameObject virtualCamera;
    public float speed;
    private float characterMovingBorder = 8f;

    [SerializeField] private float speedAccelerator;
    private Animator m_Animator;
    private Rigidbody rb;
    private float movingSpeed = 2f;
    public bool isOnBorder;


    static readonly int k_HashSpeed = Animator.StringToHash("Speed");
    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    public void RunToBoss(Vector3 bossPosition)
    {
        Vector3 target = new Vector3(bossPosition.x , 0 , bossPosition.z );
        Vector3 newPos = Vector3.MoveTowards(rb.position , target , Time.fixedDeltaTime * movingSpeed);
        rb.MovePosition(newPos);
        m_Animator.SetFloat(k_HashSpeed,1);

        if(Vector3.Distance(bossPosition,this.transform.position) <= 5f)
        {
           // m_Animator.SetFloat(k_HashSpeed,0);
           m_Animator.SetBool("isWin",true);
           virtualCamera.SetActive(false);
           
        }



    }

    

    
    void Update()
    {
      
        //Karakterimizi sınır değerlerin içerisinde hareket etmesini sağlıyoruz.
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-characterMovingBorder,characterMovingBorder),transform.position.y,transform.position.z);
                  
        TouchControl();

     
    }

    public void TouchControl()
    {
         if(Input.touchCount>0)
         {
            theTouch = Input.GetTouch(0);
            
            if(theTouch.phase == TouchPhase.Began)
            {
                //Ekrana ilk dokunulan pozisyon
                touchStartPos = theTouch.position; 
            }

             else if(theTouch.phase == TouchPhase.Stationary)
            {
                //Ekrana basılı tutulduğu sürece karakter z ekseninde ileri doğru hareket edecek.
                  speed = Mathf.Lerp(speed , 1 , Time.deltaTime * speedAccelerator);
                  AnimationSpeedSetter(speed);

            }
            

         }


        // Ekrana herhangi bir temas olmadığı zaman speed ve  değeri 0 olacak.
        else
        {
            speed = Mathf.Lerp(speed,0, Time.deltaTime * speedAccelerator);
            AnimationSpeedSetter(speed);
        }


    }




    public void AnimationSpeedSetter(float Speed)
    {
         m_Animator.SetFloat(k_HashSpeed, speed);

    }

  
}

