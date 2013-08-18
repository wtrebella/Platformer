using UnityEngine;
using System.Collections;

public class WTMainScene : WTScene {
	WTPlayer player;

	public WTMainScene() {
		float blockSize = 32;
		int minFloorBlockCount = (int)Futile.screen.width / 16;

		for (int i = 0; i < minFloorBlockCount; i++) {
			WTBasicWall block = new WTBasicWall(blockSize, blockSize);
			block.SetNewPosition(i * blockSize + blockSize / 2f, blockSize / 2f);
			Futile.stage.AddChild(block);

			int extraHeight = Random.Range(0, 3);

			for (int j = 0; j < extraHeight; j++) {
				block = new WTBasicWall(blockSize, blockSize);
				block.SetNewPosition(i * blockSize + blockSize / 2f, j * blockSize + blockSize * 1.5f);
				Futile.stage.AddChild(block);
			}
		}

		player = new WTPlayer("player", 16, 16);
		player.SetNewPosition(Futile.screen.halfWidth, blockSize * 4.5f);
		AddChild(player);
	}

	override public void HandleUpdate() {
		player.HandleUpdate();
	}

	override public void HandleMultiTouch(FTouch[] touches) {

	}
}
