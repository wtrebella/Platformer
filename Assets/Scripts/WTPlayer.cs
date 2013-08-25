using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : WTMovingObject {
	public DirectionType facingDirection = DirectionType.Right;

	public WTPlayer(string name, float width, float height) : base(name, width, height) {
		physicsComponent.gameObject.layer = LayerMask.NameToLayer("Player");
		shouldBounce = false;
		drag = WTConfig.playerDrag;
		friction = 1;
	}

	override public void HandleUpdate() {
		base.HandleUpdate();
		UpdateShooting();
	}

	public void UpdateShooting() {
		if (Input.GetKeyDown(KeyCode.F)) {
			WTMovingObject bo = new WTMovingObject("rock", WTConfig.tileSize / 6f, WTConfig.tileSize / 6f);
			bo.physicsComponent.tag = "Empty";
			bo.sprite.color = new Color(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), Random.Range(0f, 0.5f));
			float amt = Random.Range(800, 1200);

			if (facingDirection == DirectionType.Left) {
				bo.SetPosition(this.x, this.y);
				bo.velocity.x = -amt;
			}
			else if (facingDirection == DirectionType.Right) {
				bo.SetPosition(this.x, this.y);
				bo.velocity.x = amt;
			}

			bo.velocity.y = Mathf.Abs(bo.velocity.x);

			this.container.AddChild(bo);
			this.container.AddChild(this);
		}
	}

	override public void UpdateMovement() {
		float velAmt = 6000;

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
			velocity.y = WTConfig.maxVelocity.y;
			isOnGround = false;
		}

		if (velocity.x > WTConfig.maxVelocity.x) velocity.x = WTConfig.maxVelocity.x;
		else if (velocity.x < -WTConfig.maxVelocity.x) velocity.x = -WTConfig.maxVelocity.x;
	
		base.UpdateMovement();
	}
}
