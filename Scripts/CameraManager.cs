using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject camB;

    public void DisableCamB()
    {
        camB.SetActive(false);
    }

    public void EnableCamB()
    {
        camB.SetActive(true);
    }
    
}
