using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] float speed = 15f;
    [SerializeField] float xRange = 8f;
    [SerializeField] float yRange = 5f;

    [SerializeField] float positionPitchFactor =   -5f;
    [SerializeField] float controlPitchFactor =   -20f;
    [SerializeField] float positionYawFactor =      5f;
    [SerializeField] float controlRollFactor =    -20f;

    float xThrow = 0;
    float yThrow = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessTranslation();
        ProcessRotation();
    }
    private void ProcessRotation()
    {
        float pitch = transform.localRotation.y * positionPitchFactor + yThrow * positionPitchFactor;
        float yaw = transform.localRotation.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
        float rawYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        transform.localPosition = new Vector3(rawXPos, rawYPos, transform.localPosition.z);
    }
}
