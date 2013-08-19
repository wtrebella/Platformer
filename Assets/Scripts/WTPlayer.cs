using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTPlayer : FContainer {
	public Vector2 velocity;
	FSprite sprite;
	FContainer shellContainer;
	public Rect hitRect;
	List<WTTileData> surroundingTiles;
	List<WTTileData> nextSurroundingTiles;

	public WTPlayer(float width, float height) {
		surroundingTiles = new List<WTTileData>();
		nextSurroundingTiles = new List<WTTileData>();
		shellContainer = new FContainer();
		AddChild(shellContainer);

		sprite = new FSprite("whiteSquare");
		sprite.width = width;
		sprite.height = height;
		sprite.color = Color.red;
		AddChild(sprite);

		hitRect = new Rect(-width / 2f, -height / 2f, width, height);

		ListenForUpdate(HandleUpdate);
	}

	public void HandleUpdate() {
		if (!_isOnStage) return;

		UpdateVelocity();
		UpdateSurroundingTiles();
		UpdateMovement();
	}

	private void UpdateVelocity() {
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

		//velocity.y -= 100;
	}

	private void UpdateMovement() {
		shellContainer.x = this.x + velocity.x * Time.deltaTime;

		WTTileData t1, t2;

		if (shellContainer.x > this.x) {
			t1 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalTopRightRectPoint(hitRect, shellContainer));
			t2 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalBottomRightRectPoint(hitRect, shellContainer));
			
			if ((t1 != null && t1.tileType == TileType.Solid) || (t2 != null && t2.tileType == TileType.Solid)) {
				shellContainer.x = WTTileMap.instance.GetOriginOfTile(t1.x, t1.y).x - WTConfig.tileSize / 2f;// hitRect.width / 2f;
			}
		}
		else if (shellContainer.x <= this.x) {
			t1 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalTopLeftRectPoint(hitRect, shellContainer));
			t2 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalBottomLeftRectPoint(hitRect, shellContainer));

			if ((t1 != null && t1.tileType == TileType.Solid) || (t2 != null && t2.tileType == TileType.Solid)) {
				shellContainer.x = WTTileMap.instance.GetOriginOfTile(t1.x + 1, t1.y).x + WTConfig.tileSize / 2f;// hitRect.width / 2f;
			}
		}

		this.x = shellContainer.x;

//		shellContainer.y = this.y + velocity.y * Time.deltaTime;
//
//		WTTileData t1 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalTopLeftRectPoint(hitRect, shellContainer));
//		WTTileData t2 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalTopRightRectPoint(hitRect, shellContainer));
//		WTTileData t3 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalBottomRightRectPoint(hitRect, shellContainer));
//		WTTileData t4 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalBottomLeftRectPoint(hitRect, shellContainer));
//
//		nextSurroundingTiles.Clear();
//
//		if (t1 != null && t1.tileType != TileType.Empty) nextSurroundingTiles.Add(t1);
//		if (t2 != null && t2.tileType != TileType.Empty && !nextSurroundingTiles.Contains(t2)) nextSurroundingTiles.Add(t2);
//		if (t3 != null && t3.tileType != TileType.Empty && !nextSurroundingTiles.Contains(t3)) nextSurroundingTiles.Add(t3);
//		if (t4 != null && t4.tileType != TileType.Empty && !nextSurroundingTiles.Contains(t4)) nextSurroundingTiles.Add(t4);
//
//		shellContainer.RemoveFromContainer();
	}

	private void UpdateSurroundingTiles() {
		WTTileData t1 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalTopLeftRectPoint(hitRect, this));
		WTTileData t2 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalTopRightRectPoint(hitRect, this));
		WTTileData t3 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalBottomRightRectPoint(hitRect, this));
		WTTileData t4 = WTTileMap.instance.GetTileForPoint(WTUtils.GetGlobalBottomLeftRectPoint(hitRect, this));

		surroundingTiles.Clear();

		if (t1 != null && t1.tileType != TileType.Empty) surroundingTiles.Add(t1);
		if (t2 != null && t2.tileType != TileType.Empty && !nextSurroundingTiles.Contains(t2)) surroundingTiles.Add(t2);
		if (t3 != null && t3.tileType != TileType.Empty && !nextSurroundingTiles.Contains(t3)) surroundingTiles.Add(t3);
		if (t4 != null && t4.tileType != TileType.Empty && !nextSurroundingTiles.Contains(t4)) surroundingTiles.Add(t4);
	}
}
