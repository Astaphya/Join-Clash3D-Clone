using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInput : MonoBehaviour
{
   private float lastFingerPosX;
   //private float _moveFactor;
  // public float MoveFactor {get { return _moveFactor ; }}
   public float MoveFactor;


   private void Update()
   
   {
       if(Input.GetMouseButtonDown(0))
       {
           lastFingerPosX = Input.mousePosition.x;
       }

       else if( Input.GetMouseButton(0))
       {
          // _moveFactor = Input.mousePosition.x - lastFingerPosX;
           MoveFactor = Input.mousePosition.x - lastFingerPosX;
           lastFingerPosX = Input.mousePosition.x;
           

          

       }

       else if(Input.GetMouseButtonUp(0))
       {
           //_moveFactor = 0f;
           MoveFactor = 0f;
       }
   }
}
