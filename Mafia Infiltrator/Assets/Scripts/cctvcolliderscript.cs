using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cctvcolliderscript : MonoBehaviour
{
    public Collider2D detectionCollider;
    private bool isPlayerDetected = false;
    void Update(){
        if(isPlayerDetected){
            Debug.Log("Player Detected");
        }else{
        }
    }
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }
    public void DisableCamera()
    {
        detectionCollider.enabled = false;
    }
}
