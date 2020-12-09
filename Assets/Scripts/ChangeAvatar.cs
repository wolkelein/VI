using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatar : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PerformChange();
        }
    }

    public void PerformChange(BotControlScript botControlScript)
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Avatar")
            {
                Transform hitTransform = hit.transform.Find("CameraPosition").transform;
                transform.parent = hitTransform;
                transform.position = hitTransform.position;
                transform.rotation = hitTransform.rotation;

                if (botControlScript != null)
                    botControlScript.enabled = false;

                if (hit.collider.GetComponent<BotControlScript>())
                    hit.collider.GetComponent<BotControlScript>().enabled = true;
            }
        }
    }
}
