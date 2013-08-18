using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTMain : MonoBehaviour {
	WTBasicWall player;

	void Start() {
		Go.defaultEaseType = EaseType.SineInOut;

		FutileParams fp = new FutileParams(true, true, false, false);
		fp.AddResolutionLevel(480f, 1.0f, 1.0f, "-res1");
		fp.AddResolutionLevel(1136f, 2.0f, 2.0f, "-res2");
		fp.AddResolutionLevel(2048f, 4.0f, 4.0f, "-res4");
		
		fp.backgroundColor = Color.white;
		fp.origin = Vector2.zero;

		Futile.instance.Init(fp);

		Futile.atlasManager.LoadAtlas("Atlases/MainSheet");
		Futile.atlasManager.LoadFont("franchise", "franchise", "Atlases/franchise", -7, -16);
		// futile done initing

		WTUtils.Init();

		FPWorld.Create(64.0f);

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

		player = new WTBasicWall(blockSize, blockSize);
		player.sprite.color = Color.red;
		player.SetNewPosition(Futile.screen.halfWidth, blockSize * 4.5f);
		player.SetNewRotation(37);
		player.physicsComponent.AddRigidBody(1.0f, 1.0f);
		player.physicsComponent.SetupPhysicMaterial(0.0f, 0.5f, 0.5f);
		player.physicsComponent.StartPhysics();
		Futile.stage.AddChild(player);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			player.physicsComponent.AddForce(0, 300);
		}
	}
}