using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private ParticleSystem CharacterParticle;
   
  
    public void OnCollisionEnter(Collision other)
    {
       if(!other.gameObject.CompareTag("Player"))
       {
           ParticleSystem particle = Instantiate(CharacterParticle , other.transform.position, other.transform.rotation);
           Destroy(particle,1.5f);
           other.gameObject.SetActive(false);
           
           if(this.gameObject.CompareTag("Enemy"))
           {
               this.gameObject.SetActive(false);
           }
           
       }

       else
       {
           // Player engele çarpar ise bölümü baştan başlat.
           SceneLoader.Instance.RestartScene();

       }
       
        
    }
}
