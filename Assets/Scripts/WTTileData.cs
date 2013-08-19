using UnityEngine;
using System.Collections;

public class WTTileData {
	public static bool TilesShareLocation(WTTileData tile1, WTTileData tile2) {
		return tile1.x == tile2.x && tile1.y == tile2.y;
	}

	public TileType tileType = TileType.Empty;
	public int x = -1;
	public int y = -1;

	public WTTileData() {

	}
}
