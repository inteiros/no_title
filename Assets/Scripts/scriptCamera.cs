using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCamera : MonoBehaviour
{
    private Quaternion rotIniX;
    public float velRotX;
    private float countX = 0;
    public float maxVerticalAngle = 90f;
    public float minVerticalAngle = -30f;

    void Start()
    {
        velRotX = 200;
        rotIniX = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        countX += Input.GetAxisRaw("Mouse Y") * velRotX * Time.deltaTime;
        countX = Mathf.Clamp(countX, minVerticalAngle, maxVerticalAngle);

        Quaternion rotX = Quaternion.AngleAxis(countX, Vector3.left);
        transform.localRotation = rotIniX * rotX;
    }
}
