using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTMovingObject : WTPhysicsNode {
	public Vector2 velocity = Vector2.zero;
	public Vector2 maxVelocity = new Vector2(200, 475);
	public Vector2 drag = new Vector2(2500, 0);
	public FSprite sprite;
	public bool isConstantlyMoving = false;
	public bool isOnGround = false;

	public WTMovingObject(string name, float width, float height) : base(name) {
		sprite = new FSprite("whiteSquare");
		sprite.width = width;
		sprite.height = height;
		sprite.color = Color.red;
		AddChild(sprite);

		physicsComponent.AddRigidBody(0f, 1f);
		physicsComponent.rigidbody.tag = "Solid";
		physicsComponent.rigidbody.freezeRotation = true;
		physicsComponent.AddBoxCollider(width, height);
		physicsComponent.SetupPhysicMaterial(0.0f, 0.0f, 0.0f, PhysicMaterialCombine.Minimum);

		physicsComponent.gameObject.layer = LayerMask.NameToLayer("Object");
	}

	virtual public void UpdateMovement() {
		int layerMask = 1 << LayerMask.NameToLayer("Environment");

		WTPhysicsRay lLowRay = new WTPhysicsRay(this, new Vector2(0, 0), Vector3.left);
		WTPhysicsRay lHighRay = new WTPhysicsRay(this, new Vector2(0, 1), Vector3.left);
		WTPhysicsRay rLowRay = new WTPhysicsRay(this, new Vector2(1, 0), Vector3.right);
		WTPhysicsRay rHighRay = new WTPhysicsRay(this, new Vector2(1, 1), Vector3.right);

		WTPhysicsRaycastHit lLowHit;
		WTPhysicsRaycastHit lHighHit;
		WTPhysicsRaycastHit rLowHit;
		WTPhysicsRaycastHit rHighHit;

		float xRayDistance = Mathf.Abs(velocity.x * Time.deltaTime);

		// leftwards
		if (velocity.x < 0) {
			if (WTPhysicsRay.Raycast(lLowRay, out lLowHit, xRayDistance, layerMask)) {
				if (lLowHit.GetPhysicsNode().CompareTag("Solid")) {
					this.x = lLowHit.GetPoint().x + physicsComponent.GetGlobalHitBox().width / 2f + 0.01f;
					velocity.x = 0;
				}
			}

			else if (WTPhysicsRay.Raycast(lHighRay, out lHighHit, xRayDistance, layerMask)) {
				if (lHighHit.GetPhysicsNode().CompareTag("Solid")) {
					this.x = lHighHit.GetPoint().x + physicsComponent.GetGlobalHitBox().width / 2f + 0.01f;
					velocity.x = 0;
				}
			}
		}

		// rightwards
		else if (velocity.x > 0) {
			if (WTPhysicsRay.Raycast(rLowRay, out rLowHit, xRayDistance, layerMask)) {
				if (rLowHit.GetPhysicsNode().CompareTag("Solid")) {
					this.x = rLowHit.GetPoint().x - physicsComponent.GetGlobalHitBox().width / 2f - 0.01f;
					velocity.x = 0;
				}
			}

			else if (WTPhysicsRay.Raycast(rHighRay, out rHighHit, xRayDistance, layerMask)) {
				if (rHighHit.GetPhysicsNode().CompareTag("Solid")) {
					this.x = rHighHit.GetPoint().x - physicsComponent.GetGlobalHitBox().width / 2f - 0.01f;
					velocity.x = 0;
				}
			}
		}

		if (!isConstantlyMoving) {
			if (velocity.x - (drag.x * Time.deltaTime) > 0) velocity.x -= drag.x * Time.deltaTime;
			else if (velocity.x + (drag.x * Time.deltaTime) < 0) velocity.x += drag.x * Time.deltaTime;
			else velocity.x = 0;
		}

		this.x += velocity.x * Time.deltaTime;

		WTPhysicsRay lCeilingRay = new WTPhysicsRay(this, new Vector2(0, 1), Vector3.up);
		WTPhysicsRay rCeilingRay = new WTPhysicsRay(this, new Vector2(1, 1), Vector3.up);
		WTPhysicsRay lFloorRay = new WTPhysicsRay(this, new Vector2(0, 0), Vector3.down);
		WTPhysicsRay rFloorRay = new WTPhysicsRay(this, new Vector2(1, 0), Vector3.down);

		WTPhysicsRaycastHit lFloorHit;
		WTPhysicsRaycastHit rFloorHit;
		WTPhysicsRaycastHit lCeilingHit;
		WTPhysicsRaycastHit rCeilingHit;

		velocity.y += WTConfig.gravity * Time.deltaTime;

		float yRayDistance = Mathf.Abs(velocity.y * Time.deltaTime);

		// downwards
		if (velocity.y < 0) {
			if (WTPhysicsRay.Raycast(lFloorRay, out lFloorHit, yRayDistance, layerMask)) {
				if (lFloorHit.GetPhysicsNode().CompareTag("Solid")) {
					if (velocity.y <= 0) {
						isOnGround = true;
						velocity.y = 0;
					}
					this.y = lFloorHit.GetPoint().y + physicsComponent.GetGlobalHitBox().height / 2f + 0.01f;
				}
			}

			else if (WTPhysicsRay.Raycast(rFloorRay, out rFloorHit, yRayDistance, layerMask)) {
				if (rFloorHit.GetPhysicsNode().CompareTag("Solid")) {
					if (velocity.y <= 0) {
						isOnGround = true;
						velocity.y = 0;
					}
					this.y = rFloorHit.GetPoint().y + physicsComponent.GetGlobalHitBox().height / 2f + 0.01f;
				}
			}
			else isOnGround = false;
		}

		// upwards
		else if (velocity.y > 0) {
			if (WTPhysicsRay.Raycast(lCeilingRay, out lCeilingHit, yRayDistance, layerMask)) {
				if (lCeilingHit.GetPhysicsNode().CompareTag("Solid")) {
					this.y = lCeilingHit.GetPoint().y - physicsComponent.GetGlobalHitBox().height / 2f - 0.01f;
					velocity.y = 0;
				}
			}

			else if (WTPhysicsRay.Raycast(rCeilingRay, out rCeilingHit, yRayDistance, layerMask)) {
				if (rCeilingHit.GetPhysicsNode().CompareTag("Solid")) {
					this.y = rCeilingHit.GetPoint().y - physicsComponent.GetGlobalHitBox().height / 2f - 0.01f;
					velocity.y = 0;
				}
			}
		}

		if (velocity.y > maxVelocity.y) velocity.y = maxVelocity.y;
		else if (velocity.y < -maxVelocity.y) velocity.y = -maxVelocity.y;

		this.y += velocity.y * Time.deltaTime;
	}

	override public void HandleUpdate() {
		base.HandleUpdate();

		UpdateMovement();
	}
}
