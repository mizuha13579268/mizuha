using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFlow : MonoBehaviour
{
    private Transform target; //跟随目标：车
    private float height = 3;
    private float distance = -7;
    private float smoothSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Car").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        Vector3 currentForward = transform.forward;
        currentForward.y = 0;
        Vector3 forward = Vector3.Lerp(currentForward.normalized, targetForward.normalized, smoothSpeed * Time.deltaTime);
        this.transform.position = target.position + Vector3.up * height + forward * distance;
        transform.LookAt(target);
    }
}
