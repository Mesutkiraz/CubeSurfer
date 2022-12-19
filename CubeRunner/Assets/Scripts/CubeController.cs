using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    
    PlayerController playerController;
    bool isTrigger;
    RaycastHit hit;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (isTrigger) return;
        
       
        Debug.DrawLine(transform.position, transform.forward+transform.position, Color.red);
        
        if (Physics.BoxCast(transform.position,transform.lossyScale/2,transform.forward,out hit,transform.rotation,.1f))
        {
            
            if (hit.collider.CompareTag("Wall"))
            {
                playerController.score -= 10;
                playerController.scoreTxt.text = "Score: " + playerController.score.ToString();
                transform.parent = null;
                playerController.startRoutine();
                Destroy(this);

            }
            else if(hit.collider.CompareTag("Wall2"))
            {
                transform.parent = null;
               
                Destroy(this);

            }
            else if (hit.collider.CompareTag("Cube"))
            {
                isTrigger = true;
                playerController.score += 10;
                playerController.scoreTxt.text = "Score: " +playerController.score.ToString();
                test(hit);
                
            }
           
        }
        
        

    }
    private void OnDrawGizmos()
    {
        RaycastHit hit2;
        Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit2, transform.rotation, 10f);
        Gizmos.DrawCube(transform.position + transform.forward* hit2.distance,transform.lossyScale);
    }
    bool test(RaycastHit hit)
    {
        hit.collider.tag = "Player";
        playerController.transform.position += Vector3.up;
        hit.collider.transform.position = playerController.transform.position - ((playerController.transform.childCount-2) +.5f) * Vector3.up;
        isTrigger = false;
       
        hit.collider.transform.parent = playerController.transform;
        return true;        
    }
   


}
