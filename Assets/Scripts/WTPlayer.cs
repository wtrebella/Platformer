using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : WTMovingObject {
	public DirectionType facingDirection = DirectionType.Right;

	private float timeLeftGround = 0;
	private float extraJumpTimeLeeway = 0.1f;

	public WTPlayer(FSprite sprite, string name, float colliderWidth, float colliderHeight) : base(sprite, name, colliderWidth, colliderHeight) {
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
			FSprite rock = new FSprite("whiteSquare");
			float colliderWidth = WTConfig.tileSize / 6f;
			float colliderHeight = WTConfig.tileSize / 6f;
			rock.width = colliderWidth;
			rock.height = colliderHeight;
			rock.color = Color.red;

			WTMovingObject bo = new WTMovingObject(rock, "rock", colliderWidth, colliderHeight);
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

		if ((this.isOnGround || (Time.time - timeLeftGround) <= extraJumpTimeLeeway) && Input.GetKeyDown(KeyCode.Space)) {
			velocity.y = WTConfig.maxVelocity.y;
			this.isOnGround = false;
		}

		if (velocity.x > WTConfig.maxVelocity.x) velocity.x = WTConfig.maxVelocity.x;
		else if (velocity.x < -WTConfig.maxVelocity.x) velocity.x = -WTConfig.maxVelocity.x;
	
		base.UpdateMovement();
	}

	override public bool isOnGround {
		get {return isOnGround_;}
		set {
			if (isOnGround_ != value) timeLeftGround = Time.time;

			isOnGround_ = value;
		}
	}
}
