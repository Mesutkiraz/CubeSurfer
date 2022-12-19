using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float Speed, times;
    float xCord = 0f;
    public Animator animator;
    bool isTrigger;
    public float score=10;
    public Text scoreTxt;

    void Update()
    {
        if (!animator.GetBool("isDead")&&!animator.GetBool("isWin"))
        {
        xCord += Input.GetAxis("Mouse X");

        }
        
        Vector3 position = new Vector3(xCord, transform.position.y, transform.position.z + Speed * Time.deltaTime);
        transform.position = position;

        xCord = Mathf.Clamp(xCord, -3.5f, 3.5f);
        if (isTrigger) return;

        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.forward + transform.position, Color.red);

        if (Physics.Raycast(transform.position+transform.up*.1f, transform.forward, out hit, .6f))
        {
            if (hit.collider.CompareTag("Cube"))
            {
                isTrigger = true;
                score += 10;
                Debug.Log(score);
                test(hit);
                scoreTxt.text ="Score: "+ score.ToString();
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                score -= 10;
                animator.SetBool("isDead", true) ;
                Speed = 0;
                position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed * Time.deltaTime);
                scoreTxt.text = "Score: " + score.ToString();
            }
            else if (hit.collider.CompareTag("Wall2")&&transform.childCount<3)
            {
                transform.rotation =  Quaternion.Euler(0, 180, 0);
                Speed = 0;
                animator.SetBool("isWin", true);
                position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed * Time.deltaTime);
            }
        }
       

       


        bool test(RaycastHit hit)
        {
            hit.collider.tag = "Player";
            transform.position += Vector3.up;
            hit.collider.transform.position = transform.position - ((transform.childCount - 2) + .5f) * Vector3.up;
            isTrigger = false;
            Debug.Log("ben çalýþtým");
            hit.collider.transform.parent = transform;
            return true;
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Pow"&&transform.childCount<3)
        {
            float.TryParse(other.transform.GetComponent<TextMeshPro>().text, out float scorePow);
             score *= scorePow;
            scoreTxt.text = "Score: " + Mathf.Round(score).ToString();
            Debug.Log("benÇalýþtým");
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.collider.CompareTag("Pow")&&transform.childCount<3)
    //    {
    //        float.TryParse(collision.collider.transform.GetComponent<TextMeshPro>().text, out float scorePow);
    //        score *= scorePow;
    //        scoreTxt.text = "Score: " + score.ToString();
    //        Debug.Log("benÇalýþtým");
    //    }
    //}
    public void startRoutine()
    {
        StartCoroutine(Test2());
    }
    IEnumerator Test2()
    {

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < 100 / times; i++)
        {
            transform.position -= Vector3.up * times / 100;
            yield return new WaitForSeconds(.01f);
        }
    }
}
