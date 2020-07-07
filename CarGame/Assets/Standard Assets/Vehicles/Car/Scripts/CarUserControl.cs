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
        public bool isFirstPersonView = false;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            thirdCamera = GameObject.Find("ThirdCamera");
            firstCamera = GameObject.Find("FirstCamera");
        }

        private void Update()
        {
#if !MOBILE_INPUT
            if (Input.GetKeyDown(KeyCode.V))
            {
                isFirstPersonView = !isFirstPersonView;
            }
#else
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)//判断几个点击位置而且是最开始点击的屏幕，而不是滑动屏幕
                    {
                        if (Input.GetTouch(0).tapCount == 2)//tapcount是点击次数
                        {
                            isFirstPersonView = !isFirstPersonView;
                        }
                    }
                }
            }
#endif
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

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
