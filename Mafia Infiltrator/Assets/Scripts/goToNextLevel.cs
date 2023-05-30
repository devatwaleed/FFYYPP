using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToNextLevel : MonoBehaviour
{

        private int nextScene;


    public void NextLevel(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

        



}
