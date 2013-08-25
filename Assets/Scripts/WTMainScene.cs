using UnityEngine;
using System.Collections;

public class WTMainScene : WTScene {
	WTPlayer player;
	Rect deadZone;

	public WTMainScene() {
		float zoneSize = 50;
		deadZone = new Rect(Futile.screen.halfWidth - zoneSize / 2f, Futile.screen.halfHeight - zoneSize / 2f, zoneSize, zoneSize);
		WTTileMap tileMap = new WTTileMap(25, 20);
		AddChild(tileMap);

		int xTile = 5;//Random.Range(0, tileMap.mapWidth);
		int yTile = -1;

		for (int i = 0; i < tileMap.mapData[xTile].Length; i++) {
			if (tileMap.mapData[xTile][i].tileType == TileType.Empty) {
				yTile = i;
				break;
			}
		}

		player = new WTPlayer("player", WTConfig.tileSize / 2f, WTConfig.tileSize * 0.95f);
		Vector2 newPos = WTTileMap.instance.GetOriginOfTile(xTile, yTile);
		player.SetPosition(newPos.x + WTConfig.tileSize / 2f, newPos.y + WTConfig.tileSize / 2f);
		AddChild(player);
	}

	override public void HandleLateUpdate() {
		Vector2 globalCharacterPoint = LocalToGlobal(player.GetPosition());

		if (globalCharacterPoint.x < deadZone.xMin) this.x = deadZone.xMin - player.x;
		if (globalCharacterPoint.x > deadZone.xMax) this.x = deadZone.xMax - player.x;
		if (globalCharacterPoint.y < deadZone.yMin) this.y = deadZone.yMin - player.y;
		if (globalCharacterPoint.y > deadZone.yMax) this.y = deadZone.yMax - player.y;
	}

	override public void HandleMultiTouch(FTouch[] touches) {

	}
}
