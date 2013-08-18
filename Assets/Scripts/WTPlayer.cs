using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : FContainer {
	public Vector2 velocity;
	FSprite sprite;

	public WTPlayer(float width, float height) {
		sprite = new FSprite("whiteSquare");
		sprite.width = width;
		sprite.height = height;
		sprite.color = Color.red;
		AddChild(sprite);

		ListenForUpdate(HandleUpdate);
	}

	public void HandleUpdate() {
		//velocity = new Vector2(velocity.x + acceleration.x, velocity.y + acceleration.y);

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
			velocity.x = 0;
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			velocity.y = speed;
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			velocity.y = -speed;
		}

		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			velocity.y = 0;
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow)) {
			velocity.y = 0;
		}

		this.x += velocity.x * Time.deltaTime;
		this.y += velocity.y * Time.deltaTime;

//		for (int i = 0; i < collidedBlocks.Count; i++) {
//			WTWall block = (WTWall)collidedBlocks[i];
//			if (GetPosition().y - sprite.height / 2f < block.GetPosition().y + block.sprite.height / 2f) {
//				SetPosition(GetPosition().x, block.GetPosition().y + block.sprite.height / 2f + sprite.height / 2f);
//			}
//		}

		WTTileData t1 = WTTileMap.instance.GetTileForPoint(this.x - WTConfig.tileSize / 2f, this.y + WTConfig.tileSize / 2f);
		WTTileData t2 = WTTileMap.instance.GetTileForPoint(this.x + WTConfig.tileSize / 2f, this.y + WTConfig.tileSize / 2f);
		WTTileData t3 = WTTileMap.instance.GetTileForPoint(this.x - WTConfig.tileSize / 2f, this.y - WTConfig.tileSize / 2f);
		WTTileData t4 = WTTileMap.instance.GetTileForPoint(this.x + WTConfig.tileSize / 2f, this.y - WTConfig.tileSize / 2f);
	}
}
