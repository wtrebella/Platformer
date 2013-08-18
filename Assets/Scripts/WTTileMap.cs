using UnityEngine;
using System.Collections;

public class WTTileMap : FContainer {
	public static WTTileMap instance;
	public WTTileData[][] mapData;
	public int mapWidth {get; private set;}
	public int mapHeight {get; private set;}

	public WTTileMap(int mapWidth, int mapHeight) {
		instance = this;

		this.mapWidth = mapWidth;
		this.mapHeight = mapHeight;

		mapData = new WTTileData[mapWidth][];
		for (int i = 0; i < mapWidth; i++) mapData[i] = new WTTileData[mapHeight];

		for (int i = 0; i < mapWidth; i++) {
			int spotHeight = Random.Range(0, 3);
			for (int j = 0; j < mapHeight; j++) {
				WTTileData tileData = new WTTileData();
				tileData.x = i;
				tileData.y = j;

				if (j <= spotHeight) tileData.tileType = TileType.Solid;
				else tileData.tileType = TileType.Empty;

				mapData[i][j] = tileData;
			}
		}

		GenerateSprites();
	}

	private void GenerateSprites() {
		for (int i = 0; i < mapData.Length; i++) {
			for (int j = 0; j < mapData[i].Length; j++) {
				WTTileData tileData = mapData[i][j];

				FSprite sprite = new FSprite("whiteSquare");
				sprite.width = sprite.height = WTConfig.tileSize;
				sprite.SetPosition(GetPositionForTile(i, j));
				AddChild(sprite);
				if (tileData.tileType == TileType.Solid) sprite.color = Color.black;
				else if (tileData.tileType == TileType.Empty) sprite.color = Color.white;
			}
		}
	}

	public Vector2 GetPositionForTile(int x, int y) {
		return new Vector2((x + 0.5f) * WTConfig.tileSize, (y + 0.5f) * WTConfig.tileSize);
	}

	public WTTileData GetTileForPoint(float x, float y) {
		int xTile, yTile;

		xTile = (int)(x / WTConfig.tileSize);
		yTile = (int)(y / WTConfig.tileSize);

		if (xTile >= 0 && xTile < mapData.Length && yTile >= 0 && yTile < mapData[0].Length) return mapData[xTile][yTile];
		else return null;
	}
}
