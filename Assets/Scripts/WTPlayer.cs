using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : WTPhysicsNode {
	Vector2 velocity = Vector2.zero;
	Vector2 maxVelocity = new Vector2(0.1f, 0.2f);
	Vector2 drag = new Vector2(0.8f, 0);
	FSprite sprite;
	bool isJumping = false;
	bool isMoving = false;
	bool isOnGround = false;

	public WTPlayer(string name, float width, float height) : base(name) {
		sprite = new FSprite("whiteSquare");
		sprite.width = width;
		sprite.height = height;
		sprite.color = Color.red;
		AddChild(sprite);

		physicsComponent.AddRigidBody(0f, 1f);
		physicsComponent.rigidbody.freezeRotation = true;
		physicsComponent.AddBoxCollider(width, height);
		physicsComponent.SetupPhysicMaterial(0.0f, 0.0f, 0.0f, PhysicMaterialCombine.Minimum);
	}

	override public void HandleFixedUpdate() {
		base.HandleFixedUpdate();

		float velAmt = 10;

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) isMoving = true;
		else isMoving = false;

		if (Input.GetKey(KeyCode.RightArrow)) {
			velocity.x += velAmt * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			velocity.x -= velAmt * Time.deltaTime;
		}

		if (isOnGround && Input.GetKeyDown(KeyCode.Space)) {
			velocity.y = velAmt * Time.deltaTime;
			isOnGround = false;
			isJumping = true;
		}

		Ray lFloorRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.down);
		Ray rFloorRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.down);
		Ray lLowRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.min.y+0.1f, physicsComponent.collider.bounds.center.z), Vector3.left);
		Ray lHighRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.max.y-0.1f, physicsComponent.collider.bounds.center.z), Vector3.left);
		Ray rLowRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.min.y+0.1f, physicsComponent.collider.bounds.center.z), Vector3.right);
		Ray rHighRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.max.y-0.1f, physicsComponent.collider.bounds.center.z), Vector3.right);
		Ray lCeilingRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.max.y, physicsComponent.collider.bounds.center.z), Vector3.up);
		Ray rCeilingRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.max.y, physicsComponent.collider.bounds.center.z), Vector3.up);
		RaycastHit lFloorHit;
		RaycastHit rFloorHit;
		RaycastHit lLowHit;
		RaycastHit lHighHit;
		RaycastHit rLowHit;
		RaycastHit rHighHit;
		RaycastHit lCeilingHit;
		RaycastHit rCeilingHit;
		float rayDistance = 0.2f;

		// leftwards
		if (Physics.Raycast(lLowRay, out lLowHit, rayDistance)) {
			if (velocity.x <= 0 && lLowHit.collider.gameObject.CompareTag("Solid")) {
				this.x = lLowHit.point.x * FPhysics.METERS_TO_POINTS + sprite.width / 2f + 0.2f;
				velocity.x = 0;
			}
		}

		else if (Physics.Raycast(lHighRay, out lHighHit, rayDistance)) {
			if (velocity.x <= 0 && lHighHit.collider.gameObject.CompareTag("Solid")) {
				this.x = lHighHit.point.x * FPhysics.METERS_TO_POINTS + sprite.width / 2f + 0.2f;
				velocity.x = 0;
			}
		}

		// rightwards
		if (Physics.Raycast(rLowRay, out rLowHit, rayDistance)) {
			if (velocity.x >= 0 && rLowHit.collider.gameObject.CompareTag("Solid")) {
				this.x = rLowHit.point.x * FPhysics.METERS_TO_POINTS - sprite.width / 2f - 0.1f;
				velocity.x = 0;
			}
		}

		else if (Physics.Raycast(rHighRay, out rHighHit, rayDistance)) {
			if (velocity.x >= 0 && rHighHit.collider.gameObject.CompareTag("Solid")) {
				this.x = rHighHit.point.x * FPhysics.METERS_TO_POINTS - sprite.width / 2f - 0.1f;
				velocity.x = 0;
			}
		}

		// downwards
		if (Physics.Raycast(lFloorRay, out lFloorHit, rayDistance)) {
			if (lFloorHit.collider.gameObject.CompareTag("Solid")) {
				if (isOnGround == false && velocity.y <= 0) {
					isOnGround = true;
					isJumping = false;
					velocity.y = 0;
				}
			  	if (!isJumping) this.y = lFloorHit.point.y * FPhysics.METERS_TO_POINTS + sprite.height / 2f + 0.1f;
			}
			else {
				isOnGround = false;
				velocity.y += WTConfig.gravity * Time.deltaTime;
			}
		}

		else if (Physics.Raycast(rFloorRay, out rFloorHit, rayDistance)) {
			if (rFloorHit.collider.gameObject.CompareTag("Solid")) {
				if (isOnGround == false && velocity.y <= 0) {
					isOnGround = true;
					isJumping = false;
					velocity.y = 0;
				}
				if (!isJumping) this.y = rFloorHit.point.y * FPhysics.METERS_TO_POINTS + sprite.height / 2f + 0.1f;
			}
			else {
				isOnGround = false;
				velocity.y += WTConfig.gravity * Time.deltaTime;
			}
		}

		else velocity.y += WTConfig.gravity * Time.deltaTime;

		// upwards
//		if (Physics.Raycast(lCeilingRay, out lCeilingHit, rayDistance)) {
//			if (lCeilingHit.collider.gameObject.CompareTag("Solid")) {
//				this.y = lCeilingHit.point.y * FPhysics.METERS_TO_POINTS - sprite.height / 2f + 0.1f;
//			}
//			velocity.y += WTConfig.gravity * Time.deltaTime;
//		}
//
//		else if (Physics.Raycast(rCeilingRay, out rCeilingHit, rayDistance)) {
//			if (rCeilingHit.collider.gameObject.CompareTag("Solid")) {
//				this.y = rCeilingHit.point.y * FPhysics.METERS_TO_POINTS - sprite.height / 2f + 0.1f;
//			}
//			velocity.y += WTConfig.gravity * Time.deltaTime;
//		}

		if (velocity.x > maxVelocity.x) velocity.x = maxVelocity.x;
		else if (velocity.x < -maxVelocity.x) velocity.x = -maxVelocity.x;

		if (velocity.y > maxVelocity.y) velocity.y = maxVelocity.y;
		else if (velocity.y < -maxVelocity.y) velocity.y = -maxVelocity.y;

		if (!isMoving) {
			if (velocity.x - (drag.x * Time.deltaTime) > 0) velocity.x -= drag.x * Time.deltaTime;
			else if (velocity.x + (drag.x * Time.deltaTime) < 0) velocity.x += drag.x * Time.deltaTime;
			else velocity.x = 0;
		}

		this.x += (velocity.x * 2000) * Time.deltaTime;
		this.y += (velocity.y * 2000) * Time.deltaTime;
	}
}
