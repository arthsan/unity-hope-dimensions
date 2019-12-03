using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 80f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 16f;
    [Tooltip("In m")] [SerializeField] float xRange = 20f;
    [Tooltip("In m")] [SerializeField] float yRange = 13f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = 2.2f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow;
    float yThrow;

    public bool isAlive = true;

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFire();
        }
    }

    void OnPLayerDeath()
    {
        isAlive = !isAlive;
    }

    private void ProcessRotation()
    {
        float pitchPosition = transform.localPosition.y * positionPitchFactor;
        float pitchControl = yThrow * controlPitchFactor;
        float pitch = pitchControl + pitchPosition;

        float yawPosition = transform.localPosition.x * positionYawFactor;
        float yaw = yawPosition;

        float rollControl = xThrow * controlRollFactor;
        float roll = rollControl;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        float rawNewYPos = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z);
    }

    private void ProcessFire()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns(true);
        }
        else
        {
            ActivateGuns(false);
        }
    }

    private void ActivateGuns(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var shot = gun.GetComponent<ParticleSystem>().emission;
            shot.enabled = isActive;
        }
    }
}
