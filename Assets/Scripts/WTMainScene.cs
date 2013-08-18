using UnityEngine;
using System.Collections;

public class WTMainScene : WTScene {
	WTPlayer player;

	public WTMainScene() {
//		float blockSize = 32;
//		int minFloorBlockCount = (int)Futile.screen.width / 16;
//
//		for (int i = 0; i < minFloorBlockCount; i++) {
//			WTWall block = new WTWall(blockSize, blockSize);
//			block.SetPosition(i * blockSize + blockSize / 2f, blockSize / 2f);
//			Futile.stage.AddChild(block);
//
//			int extraHeight = Random.Range(0, 3);
//
//			for (int j = 0; j < extraHeight; j++) {
//				block = new WTWall(blockSize, blockSize);
//				block.SetPosition(i * blockSize + blockSize / 2f, j * blockSize + blockSize * 1.5f);
//				Futile.stage.AddChild(block);
//			}
//		}

//		player = new WTPlayer("player", 16, 16);
//		player.SetPosition(Futile.screen.halfWidth, blockSize * 4.5f);
//		AddChild(player);

		WTTileMap tileMap = new WTTileMap(25, 20);
		AddChild(tileMap);

		int xTile = Random.Range(0, tileMap.mapWidth);
		int yTile = Random.Range(0, tileMap.mapHeight);

		player = new WTPlayer(WTConfig.tileSize, WTConfig.tileSize);
		Vector2 newPos = WTTileMap.instance.GetPositionForTile(xTile, yTile);
		player.SetPosition(newPos);
		AddChild(player);
	}

	override public void HandleUpdate() {
		//player.HandleUpdate();
	}

	override public void HandleMultiTouch(FTouch[] touches) {

	}
}
