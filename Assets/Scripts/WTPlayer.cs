using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : WTPhysicsNode {
	public Vector2 acceleration;
	public Vector2 velocity;
	FSprite sprite;
	List<WTPhysicsNode> collidedBlocks;

	public WTPlayer(string name, float width, float height) : base(name) {
		collidedBlocks = new List<WTPhysicsNode>();

		sprite = new FSprite("whiteSquare");
		sprite.width = width;
		sprite.height = height;
		sprite.color = Color.red;
		AddChild(sprite);

		physicsComponent.AddBoxCollider(width, height);
		physicsComponent.AddRigidBody(1.0f, 1.0f);
		physicsComponent.SetupPhysicMaterial(0.0f, 0.5f, 0.5f);
		physicsComponent.SetIsTrigger(true);
	}

	override public void HandleUpdate() {
		acceleration = new Vector2(0, -10);
		velocity = new Vector2(velocity.x + acceleration.x, velocity.y + acceleration.y);

		float speed = 300;

		if (Input.GetKey(KeyCode.RightArrow)) {
			velocity.x = speed;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) {
			velocity.x = -speed;
		}

		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			velocity.x = 0;
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			velocity.x = -0;
		}

		SetPosition(GetPosition().x + velocity.x * Time.deltaTime, GetPosition().y + velocity.y * Time.deltaTime);

		for (int i = 0; i < collidedBlocks.Count; i++) {
			WTWall block = (WTWall)collidedBlocks[i];
			if (GetPosition().y - sprite.height / 2f < block.GetPosition().y + block.sprite.height / 2f) {
				SetPosition(GetPosition().x, block.GetPosition().y + block.sprite.height / 2f + sprite.height / 2f);
			}
		}
	}

	override public void HandleOnTriggerEnter(Collider coll) {
		WTWall block = (WTWall)WTUtils.physicsNodeAttachedToGameObject(coll.gameObject);
		collidedBlocks.Add(block);
	}

	override public void HandleOnTriggerExit(Collider coll) {
		WTWall block = (WTWall)WTUtils.physicsNodeAttachedToGameObject(coll.gameObject);
		collidedBlocks.Remove(block);
	}

	override public void HandleOnTriggerStay(Collider coll) {
		//WTWall block = (WTWall)WTUtils.physicsNodeAttachedToGameObject(coll.gameObject);
	}
}
