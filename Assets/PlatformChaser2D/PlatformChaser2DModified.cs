using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// @kurtdekker
// 2D platform follower and leaper
//
// See platform_chaser_2d.png for visual explanation.
//

public class PlatformChaser2DModified : NetworkBehaviour
{
	// "left" means from the perspective of the platform you are on, so "left"
	// actually means "right" when you're dancing on the ceiling. What a feeling.
	bool facingLeft;

	private float PlatformWalkSpeed = 5.0f;
	const float InterPlatformSpeed = 10.0f;

	// only applies to visuals: this is the Z rotation to match ground contour
	float currentRotation;
	float desiredRotation;

	// increase this as you move faster otherwise it looks funny as you go around corners
	const float RateOfRotation = 600.0f;

	// this is around the Y rotation as the player turns left / right
	float currentFacing;
	const float RateOfFacing = 1000.0f;

	// how much above our contact point do we cast? (to avoid entering slopes)
	const float SensingCastLift = 0.25f;
	// limit our cast to avoid grabbing distant things
	const float SensingCastDistance = 1.0f;

	LayerMask mask;

	public Transform startingPos;

	[SerializeField] private bool isMovable;
	public override void OnNetworkSpawn()
	{
		Debug.Log("Start");
		LayerMask mask = LayerMask.GetMask("Enviorment");
		var hit = Physics2D.Raycast(origin: startingPos.transform.position, direction: -transform.up, mask);
		JumpToHit(hit);
	}

	void SetDesiredRotationFromNormal(Vector3 normal)
	{
		desiredRotation = Mathf.Atan2(-normal.x, normal.y) * Mathf.Rad2Deg;
	}

	public void JumpToHit(RaycastHit2D hit)
	{
		transform.up = hit.normal;

		SetDesiredRotationFromNormal(hit.normal);

		isAirborne = true;
		Destination = hit.point;
	}

	bool AttemptToJump()
	{
		var hit = Physics2D.Raycast(origin: transform.position + transform.up * SensingCastLift, direction: transform.up, mask);

		if (hit.collider)
		{
			// NOTE: this is a matter of taste: you might want
			// to flip the walk direction so it seems more
			// continuous upon arrival. Remove this if so.
			//facingLeft = !facingLeft;

			JumpToHit(hit);     // I regret nothing!

			return true;
		}
		return false;
	}
	bool jumpIntent;

	void GatherInputIntents()
	{
		jumpIntent = false;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jumpIntent = true;
		}
	}

	public bool isAirborne;
	Vector3 Destination;
	bool ProcessAirborne()
	{
		if (isAirborne)
		{
			transform.position = Vector3.MoveTowards(transform.position, Destination, InterPlatformSpeed * Time.deltaTime);

			if (Vector3.Distance(transform.position, Destination) < 0.1f)
			{
				transform.position = Destination;
				isAirborne = false;
			}
		}

		return isAirborne;
	}

	void Update()
	{
		// input is disregarded while you're flying through the air
		if (ProcessAirborne())
		{
			return;
		}

		GatherInputIntents();

		if (Input.GetKey(KeyCode.D))
		{
			facingLeft = false;
		} if (Input.GetKey(KeyCode.A))
		{
			facingLeft = true;
		}

		if (isMovable)
		{
			if (jumpIntent)
			{
				if (AttemptToJump())
				{
					return;
				}
			}
			Move(KeyCode.D);
			Move(KeyCode.A);
		}
	}

    private void Move(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            Vector3 downVector = -transform.up;

            Vector3 walkVector = transform.right;

            if (facingLeft)
            {
                walkVector = -walkVector;
            }

            walkVector *= PlatformWalkSpeed;

            walkVector *= Time.deltaTime;

            // let's just keep each step semi-reasonable
            const float MaximumStepDistance = 0.2f;
            if (walkVector.magnitude > MaximumStepDistance)
            {
                walkVector = walkVector.normalized * MaximumStepDistance;
            }

            var position = transform.position;

            Vector3 CastLiftOffset = transform.up * SensingCastLift;

            position += walkVector;


            var nextHit = Physics2D.Raycast(
                origin: position + CastLiftOffset,
                direction: downVector,
                distance: SensingCastDistance,
				mask);

            // if you step into the void, we have to track the ground
            if (!nextHit.collider)
            {
                float rotate45 = (facingLeft ? +1 : -1) * 45.0f;

                var rot45 = Quaternion.Euler(0, 0, rotate45);

                // step a teeny bit further out along this way
                Vector3 extraForwardOffset = walkVector.normalized * 0.01f;

                Vector3 tiltedPosition = position + extraForwardOffset + rot45 * CastLiftOffset;
                Vector3 tiltedDirection = rot45 * downVector;

                // try and hit that collider now, leaning forward and angling back
                var tiltedHit = Physics2D.Raycast(
                    origin: tiltedPosition,
                    direction: tiltedDirection,
                    distance: SensingCastDistance,
					mask);

                nextHit = tiltedHit;
            }

            position = nextHit.point;

            SetDesiredRotationFromNormal(nextHit.normal);

            // primary object instant-snaps always
            transform.position = position;
            transform.rotation = Quaternion.Euler(0, 0, desiredRotation);
        }
    }

    public void StartSpeedBoost()
    {
        StartCoroutine(SpeedBoost());
    }

    private IEnumerator SpeedBoost()
    {
        PlatformWalkSpeed = 12f;
        yield return new WaitForSeconds(6f);
        PlatformWalkSpeed = 5f;
    }
}
