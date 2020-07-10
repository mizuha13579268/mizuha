using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        private GameObject firstCamera, thirdCamera;
        private bool isFirstPersonView = false;
        private bool isCan = true;
        private bool isTurnLeft = false;
        private bool isTurnRight = false;
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            thirdCamera = GameObject.Find("ThirdCamera");
            firstCamera = GameObject.Find("FirstCamera");
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            if (h < 0)
            {
                isTurnLeft = true;
            }
            else
            {
                isTurnLeft = false;
            }
            if (h > 0)
            {
                isTurnRight = true;
            }
            else
            {
                isTurnRight = false;
            }
            m_Car.Move(h, v, v, handbrake,isCan, isTurnLeft, isTurnRight);
          
#else
           if (h < 0)
            {
                isTurnLeft = true;
            }
            else
            {
                isTurnLeft = false;
            }
            if (h > 0)
            {
                isTurnRight = true;
            }
            else
            {
                isTurnRight = false;
            }
            m_Car.Move(h, v, v, 0f,isCan, isTurnLeft, isTurnRight);
#endif
        }

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.V))
            {
                isFirstPersonView = !isFirstPersonView;
            }
            if ((Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
                || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
            {
                isCan = false;
            }
            else
            {
                isCan = true;
            }
            if (isFirstPersonView)
            {
                firstCamera.SetActive(true);
                thirdCamera.SetActive(false);
            }
            else
            {
                firstCamera.SetActive(false);
                thirdCamera.SetActive(true);
            }
        }
    }
}
