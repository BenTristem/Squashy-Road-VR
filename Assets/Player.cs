﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    public Text gazeText;
    public float jumpAngleInDegrees;
    public float jumpSpeed;

    private CardboardHead head;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        Cardboard.SDK.OnTrigger += PullTrigger;
        head = GameObject.FindObjectOfType<CardboardHead>();
        rb = GetComponent<Rigidbody>();
	}

    private void PullTrigger()
    {
        Vector3 planeProjectedGaze = Vector3.ProjectOnPlane(head.Gaze.direction, Vector3.up);
        Vector3 unitPlaneProjectedGaze = Vector3.Normalize(planeProjectedGaze);
        float jumpAngleInRadians = jumpAngleInDegrees * Mathf.Deg2Rad;
        Vector3 unitJumpVelocity = Vector3.RotateTowards(unitPlaneProjectedGaze, Vector3.up, jumpAngleInRadians, 0);
        Vector3 jumpVelocity = unitJumpVelocity * jumpSpeed;
        rb.velocity = jumpVelocity;
    }

    // Update is called once per frame
    void Update () {
	    gazeText.text = head.Gaze.ToString();
	}
}