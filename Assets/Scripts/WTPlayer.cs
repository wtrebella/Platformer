using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : WTMovingObject {
	public DirectionType facingDirection = DirectionType.Right;

	public WTPlayer(string name, float width, float height) : base(name, width, height) {
		physicsComponent.gameObject.layer = LayerMask.NameToLayer("Player");
	}

	override public void HandleUpdate() {
		base.HandleUpdate();

		UpdateShooting();
	}

	public void UpdateShooting() {
		if (Input.GetKeyDown(KeyCode.F)) {
			WTMovingObject bo = new WTMovingObject("rock", 5, 5);
			bo.physicsComponent.tag = "Empty";
			bo.sprite.color = new Color(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), Random.Range(0f, 0.5f));
			float amt = 1000;

			if (facingDirection == DirectionType.Left) {
				bo.SetPosition(this.x - physicsComponent.GetGlobalHitBox().width / 2f, this.y);
				bo.velocity.x = -amt;

			}
			else if (facingDirection == DirectionType.Right) {
				bo.SetPosition(this.x + physicsComponent.GetGlobalHitBox().width / 2f, this.y);
				bo.velocity.x = amt;
			}

			this.container.AddChild(bo);
		}
	}

	override public void UpdateMovement() {
		float velAmt = 3000;

		if (Input.GetKeyDown(KeyCode.RightArrow)) facingDirection = DirectionType.Right;
		else if (Input.GetKeyDown(KeyCode.LeftArrow)) facingDirection = DirectionType.Left;

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) isConstantlyMoving = true;
		else isConstantlyMoving = false;

		if (Input.GetKey(KeyCode.RightArrow)) {
			velocity.x += velAmt * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			velocity.x -= velAmt * Time.deltaTime;
		}

		if (isOnGround && Input.GetKeyDown(KeyCode.Space)) {
			velocity.y = maxVelocity.y;
			isOnGround = false;
		}

		if (velocity.x > maxVelocity.x) velocity.x = maxVelocity.x;
		else if (velocity.x < -maxVelocity.x) velocity.x = -maxVelocity.x;
	
		base.UpdateMovement();
	}
}
