using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Camera
{
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
    
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "CamTrigger":
                    EnableCamB();
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "CamTrigger":
                    DisableCamB();
                    break;
            }
        }
    
    }

}
