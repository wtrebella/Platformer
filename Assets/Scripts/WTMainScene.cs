using UnityEngine;
using System.Collections;

public class WTMainScene : WTScene {
	WTPlayer player;

	public WTMainScene() {
		WTTileMap tileMap = new WTTileMap(25, 20);
		AddChild(tileMap);

		int xTile = 10;//Random.Range(0, tileMap.mapWidth);
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

		ListenForUpdate(HandleUpdate);
	}

	override public void HandleUpdate() {
		this.SetPosition(Futile.screen.halfWidth - player.x, Futile.screen.halfHeight - player.y);
	}

	override public void HandleMultiTouch(FTouch[] touches) {

	}
}
