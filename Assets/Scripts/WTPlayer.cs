using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : WTPhysicsNode {
	Vector2 velocity = Vector2.zero;
	Vector2 maxVelocity = new Vector2(300, 500);
	Vector2 drag = new Vector2(2500, 0);
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
		float velAmt = 3000;

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) isMoving = true;
		else isMoving = false;

		if (Input.GetKey(KeyCode.RightArrow)) {
			velocity.x += velAmt * Time.fixedDeltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			velocity.x -= velAmt * Time.fixedDeltaTime;
		}

		if (isOnGround && Input.GetKeyDown(KeyCode.Space)) {
			velocity.y = maxVelocity.y;
			isOnGround = false;
			isJumping = true;
		}

		if (velocity.x > maxVelocity.x) velocity.x = maxVelocity.x;
		else if (velocity.x < -maxVelocity.x) velocity.x = -maxVelocity.x;

		// jump

		Ray lLowRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.left);
		Ray lHighRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.max.y, physicsComponent.collider.bounds.center.z), Vector3.left);
		Ray rLowRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.right);
		Ray rHighRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.max.y, physicsComponent.collider.bounds.center.z), Vector3.right);
		Ray lFloorRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.down);
		Ray rFloorRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.down);

		RaycastHit lLowHit;
		RaycastHit lHighHit;
		RaycastHit rLowHit;
		RaycastHit rHighHit;

		float xRayDistance = Mathf.Abs(velocity.x * FPhysics.POINTS_TO_METERS * Time.fixedDeltaTime);

		// leftwards
		if (velocity.x < 0) {
			if (Physics.Raycast(lLowRay, out lLowHit, xRayDistance)) {
				if (lLowHit.collider.gameObject.CompareTag("Solid")) {
					this.x = (lLowHit.point.x + physicsComponent.collider.bounds.size.x / 2f) * FPhysics.METERS_TO_POINTS + 0.01f;
					velocity.x = 0;

					FSprite s = new FSprite("whiteSquare");
					s.scale = 0.3f;
					s.SetPosition(lLowHit.point * FPhysics.METERS_TO_POINTS);
					s.color = Color.red;
					this.container.AddChild(s);
				}
			}

			else if (Physics.Raycast(lHighRay, out lHighHit, xRayDistance)) {
				if (lHighHit.collider.gameObject.CompareTag("Solid")) {
					this.x = (lHighHit.point.x + physicsComponent.collider.bounds.size.x / 2f) * FPhysics.METERS_TO_POINTS + 0.01f;
					velocity.x = 0;

					FSprite s = new FSprite("whiteSquare");
					s.scale = 0.3f;
					s.SetPosition(lHighHit.point * FPhysics.METERS_TO_POINTS);
					s.color = Color.red;
					this.container.AddChild(s);
				}
			}
		}

		// rightwards
		else if (velocity.x > 0) {
			if (Physics.Raycast(rLowRay, out rLowHit, xRayDistance)) {
				if (rLowHit.collider.gameObject.CompareTag("Solid")) {
					this.x = (rLowHit.point.x - physicsComponent.collider.bounds.size.x / 2f) * FPhysics.METERS_TO_POINTS - 0.01f;
					velocity.x = 0;

					FSprite s = new FSprite("whiteSquare");
					s.scale = 0.3f;
					s.SetPosition(rLowHit.point * FPhysics.METERS_TO_POINTS);
					s.color = Color.red;
					this.container.AddChild(s);
				}
			}

			else if (Physics.Raycast(rHighRay, out rHighHit, xRayDistance)) {
				if (rHighHit.collider.gameObject.CompareTag("Solid")) {
					this.x = (rHighHit.point.x - physicsComponent.collider.bounds.size.x / 2f) * FPhysics.METERS_TO_POINTS - 0.01f;
					velocity.x = 0;

					FSprite s = new FSprite("whiteSquare");
					s.scale = 0.3f;
					s.SetPosition(rHighHit.point * FPhysics.METERS_TO_POINTS);
					s.color = Color.red;
					this.container.AddChild(s);
				}
			}
		}

		if (!isMoving) {
			if (velocity.x - (drag.x * Time.fixedDeltaTime) > 0) velocity.x -= drag.x * Time.fixedDeltaTime;
			else if (velocity.x + (drag.x * Time.fixedDeltaTime) < 0) velocity.x += drag.x * Time.fixedDeltaTime;
			else velocity.x = 0;
		}

		this.x += velocity.x * Time.fixedDeltaTime;
	
		Ray lCeilingRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.max.y, physicsComponent.collider.bounds.center.z), Vector3.up);
		Ray rCeilingRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.max.y, physicsComponent.collider.bounds.center.z), Vector3.up);
		RaycastHit lFloorHit;
		RaycastHit rFloorHit;
		RaycastHit lCeilingHit;
		RaycastHit rCeilingHit;

		velocity.y += WTConfig.gravity * Time.fixedDeltaTime;

		float yRayDistance = Mathf.Abs(velocity.y * FPhysics.POINTS_TO_METERS * Time.fixedDeltaTime);

		// downwards
		if (velocity.y < 0) {
			if (Physics.Raycast(lFloorRay, out lFloorHit, yRayDistance)) {
				if (lFloorHit.collider.gameObject.CompareTag("Solid")) {
					if (velocity.y <= 0) {
						isOnGround = true;
						isJumping = false;
						velocity.y = 0;
					}
				  	this.y = lFloorHit.point.y * FPhysics.METERS_TO_POINTS + sprite.height / 2f + 0.1f;

					FSprite s = new FSprite("whiteSquare");
					s.scale = 0.3f;
					s.SetPosition(lFloorHit.point * FPhysics.METERS_TO_POINTS);
					s.color = Color.red;
					this.container.AddChild(s);
				}
				else isOnGround = false;
			}

			else if (Physics.Raycast(rFloorRay, out rFloorHit, yRayDistance)) {
				if (rFloorHit.collider.gameObject.CompareTag("Solid")) {
					if (velocity.y <= 0) {
						isOnGround = true;
						isJumping = false;
						velocity.y = 0;
					}
					this.y = rFloorHit.point.y * FPhysics.METERS_TO_POINTS + sprite.height / 2f + 0.1f;

					FSprite s = new FSprite("whiteSquare");
					s.scale = 0.3f;
					s.SetPosition(rFloorHit.point * FPhysics.METERS_TO_POINTS);
					s.color = Color.red;
					this.container.AddChild(s);
				}
				else isOnGround = false;
			}
		}

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

		if (velocity.y > maxVelocity.y) velocity.y = maxVelocity.y;
		else if (velocity.y < -maxVelocity.y) velocity.y = -maxVelocity.y;

		this.y += velocity.y * Time.fixedDeltaTime;
	}
}
