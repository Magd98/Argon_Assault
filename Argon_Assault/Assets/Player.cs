using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float Speed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 5.13f;
    [Tooltip("In m")] [SerializeField] float yRange =2.74f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -25f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow, yThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        float pitchDuetoControl = yThrow * controlPitchFactor;
        float pitchDuetoPosition = transform.localPosition.y * positionPitchFactor;
        float pitch = pitchDuetoPosition + pitchDuetoControl;

        float yawDuetoPostion = transform.localPosition.x * positionYawFactor;
        float yaw = yawDuetoPostion;

        float rollDuetoConrol = xThrow * controlRollFactor;
        float roll = rollDuetoConrol;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * Speed * Time.deltaTime;
        print(xOffset);
        float RawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(RawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * Speed * Time.deltaTime;
        print(yOffset);
        float RawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(RawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
