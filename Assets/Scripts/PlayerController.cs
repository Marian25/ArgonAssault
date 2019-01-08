using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [SerializeField] float xSpeed = 12f;
    [SerializeField] float xMovementRange = 8f;
    [SerializeField] float ySpeed = 10f;
    [SerializeField] float yMin = 6f;
    [SerializeField] float yMax = 6f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow = 0;
    float yThrow = 0;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update ()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * xSpeed;
        float yOffset = yThrow * Time.deltaTime * ySpeed;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xMovementRange, xMovementRange);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yMin, yMax);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);

    }

    void ProcessRotation()
    {

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToControl + pitchDueToPosition;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
