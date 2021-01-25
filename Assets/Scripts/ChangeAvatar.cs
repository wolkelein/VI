using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatar : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    private KinectManager _kinectManager;

    private void Start()
    {
        _kinectManager = FindObjectOfType<KinectManager>();
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Avatar")
                {
                    if (GetComponentInParent<BotControlScript>())
                        PerformChange(GetComponentInParent<BotControlScript>());
                }
            }
        }
    }
    */

    public void PerformChange(BotControlScript botControlScript)
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Avatar")
            {
                GetComponentInParent<Animator>().enabled = true;

                GetComponentInParent<AvatarController>().externalRootMotion = true;

                Transform hitTransform = hit.transform.Find("CameraPosition").transform;
                transform.parent = hitTransform;
                transform.position = hitTransform.position;
                transform.rotation = hitTransform.rotation;

                if (botControlScript != null)
                    botControlScript.enabled = false;

                if (hit.collider.GetComponent<BotControlScript>())
                    hit.collider.GetComponent<BotControlScript>().enabled = true;

                GetComponentInParent<Animator>().enabled = false;

                AvatarController avatarController = GetComponentInParent<AvatarController>();

                avatarController.externalRootMotion = false;

                _kinectManager.avatarControllers.Clear();
                _kinectManager.avatarControllers.Add(avatarController);
            }
        }
    }
}
