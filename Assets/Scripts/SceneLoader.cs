using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
  private static SceneLoader _instance;
  public static SceneLoader Instance { get { return _instance ;}}

  private void Awake()
  {
      if(_instance != null && _instance != this)
      {
          Destroy(this.gameObject);
      }
      else{
          _instance = this;
      }

  }

  public void RestartScene()
  {
      SceneManager.LoadScene(0);
  }
}
