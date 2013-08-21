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
			int spaceHeight = Random.Range(3, 6);

			for (int j = 0; j < mapHeight; j++) {
				WTTileData tileData = new WTTileData();
				tileData.x = i;
				tileData.y = j;

				if (j <= spotHeight || j > spotHeight + spaceHeight) tileData.tileType = TileType.Solid;
				else tileData.tileType = TileType.Empty;

				mapData[i][j] = tileData;
			}
		}

		GenerateBlocks();
	}

	private void GenerateBlocks() {
		for (int i = 0; i < mapData.Length; i++) {
			for (int j = 0; j < mapData[i].Length; j++) {
				WTTileData tileData = mapData[i][j];

				if (tileData.tileType == TileType.Solid) {
					WTWall block = new WTWall(WTConfig.tileSize, WTConfig.tileSize);
					Vector2 newPos = GetOriginOfTile(i, j);
					block.SetPosition(newPos.x + WTConfig.tileSize / 2f, newPos.y + WTConfig.tileSize / 2f);
					block.sprite.color = Color.black;
					AddChild(block);
				}
			}
		}
	}

	public Vector2 GetOriginOfTile(int x, int y) {
		return new Vector2(x * WTConfig.tileSize, y * WTConfig.tileSize);
	}

	public WTTileData GetTileForPoint(float x, float y) {
		int xTile, yTile;

		xTile = (int)(x / (WTConfig.tileSize-1));
		yTile = (int)(y / (WTConfig.tileSize-1));

		if (xTile >= 0 && xTile < mapData.Length && yTile >= 0 && yTile < mapData[0].Length) return mapData[xTile][yTile];
		else return null;
	}

	public WTTileData GetTileForPoint(Vector2 point) {
		return GetTileForPoint(point.x, point.y);
	}
}
