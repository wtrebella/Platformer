using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : WTPhysicsNode {
	Vector2 velocity = Vector2.zero;
	Vector2 maxVelocity = new Vector2(0.1f, 1.0f);
	Vector2 drag = new Vector2(0.8f, 0);
	FSprite sprite;
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
		}

		if (velocity.x > maxVelocity.x) velocity.x = maxVelocity.x;
		else if (velocity.x < -maxVelocity.x) velocity.x = -maxVelocity.x;

		if (velocity.y > maxVelocity.y) velocity.y = maxVelocity.y;
		else if (velocity.y < -maxVelocity.y) velocity.y = -maxVelocity.y;

		if (!isMoving) {
			if (velocity.x - (drag.x * Time.deltaTime) > 0) velocity.x -= drag.x * Time.deltaTime;
			else if (velocity.x + (drag.x * Time.deltaTime) < 0) velocity.x += drag.x * Time.deltaTime;
			else velocity.x = 0;
		}

		Ray lFloorRay = new Ray(new Vector3(physicsComponent.collider.bounds.min.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.down);
		Ray rFloorRay = new Ray(new Vector3(physicsComponent.collider.bounds.max.x, physicsComponent.collider.bounds.min.y, physicsComponent.collider.bounds.center.z), Vector3.down);
		RaycastHit lFloorHit;
		RaycastHit rFloorHit;

		if (Physics.Raycast(lFloorRay, out lFloorHit, 0.1f)) {
			if (lFloorHit.collider.gameObject.CompareTag("Solid")) {
				if (isOnGround == false && velocity.y <= 0) {
					isOnGround = true;
					velocity.y = 0;
				}
				this.y = lFloorHit.point.y * FPhysics.METERS_TO_POINTS + sprite.height / 2f + 0.1f;
			}
			else {
				isOnGround = false;
				velocity.y += WTConfig.gravity * Time.deltaTime;
			}
		}

		else if (Physics.Raycast(rFloorRay, out rFloorHit, 0.1f)) {
			if (rFloorHit.collider.gameObject.CompareTag("Solid")) {
				if (isOnGround == false && velocity.y <= 0) {
					isOnGround = true;
					velocity.y = 0;
				}
				this.y = rFloorHit.point.y * FPhysics.METERS_TO_POINTS + sprite.height / 2f + 0.1f;
			}
			else {
				isOnGround = false;
				velocity.y += WTConfig.gravity * Time.deltaTime;
			}
		}

		else velocity.y += WTConfig.gravity * Time.deltaTime;

		this.x += (velocity.x * 2000) * Time.deltaTime;
		this.y += (velocity.y * 2000) * Time.deltaTime;
	}
}
