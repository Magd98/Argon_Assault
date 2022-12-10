using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{   [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float Speed = 10f;
    [Tooltip("In m")] [SerializeField] float xRange = 5.13f;
    [Tooltip("In m")] [SerializeField] float yRange =2.74f;
    
    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -25f;

    [Header("Control-Throw based")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -25f;

    [SerializeField] GameObject[] guns;

    float xThrow, yThrow;
    bool isControlEnabled = true;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void OnPlayerDeath()
    {
        print("Player Controls Frozen");
        isControlEnabled = false;
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
        float RawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(RawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * Speed * Time.deltaTime;
        float RawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(RawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

     void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetActiveGuns(true);
        }
        else
        {
            SetActiveGuns(false);
        }
    }

     void SetActiveGuns(bool isActive)
    {
       foreach(GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive; 
        }

    }

    
}
