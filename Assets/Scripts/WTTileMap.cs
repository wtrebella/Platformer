using UnityEngine;
using System.Collections;

public class WTTileMap : FContainer {
	public TileType[][] mapData;
	public int mapWidth {get; private set;}
	public int mapHeight {get; private set;}

	public WTTileMap(int mapWidth, int mapHeight) {
		this.mapWidth = mapWidth;
		this.mapHeight = mapHeight;

		mapData = new TileType[mapWidth][];
		for (int i = 0; i < mapWidth; i++) mapData[i] = new TileType[mapHeight];

		for (int i = 0; i < mapWidth; i++) {
			for (int j = 0; j < mapHeight; j++) {
				mapData[i][j] = (TileType)Random.Range(0, 2);
			}
		}

		GenerateSprites();
	}

	private void GenerateSprites() {
		for (int i = 0; i < mapData.Length; i++) {
			for (int j = 0; j < mapData[i].Length; j++) {
				TileType tileType = mapData[i][j];

				FSprite sprite = new FSprite("whiteSquare");
				sprite.width = sprite.height = WTConfig.tileSize;
				sprite.x = (i + 0.5f) * WTConfig.tileSize;
				sprite.y = (j + 0.5f) * WTConfig.tileSize;
				AddChild(sprite);
				if (tileType == TileType.Solid) sprite.color = Color.black;
				else if (tileType == TileType.Empty) sprite.color = Color.white;
			}
		}
	}
}
