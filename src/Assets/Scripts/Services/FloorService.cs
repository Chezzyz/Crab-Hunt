using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services
{
    public class FloorService : BaseTilemapService
    {
        [SerializeField] private Tile _floorTile;

        protected override List<Tile> GetTiles()
        {
            return new List<Tile>() { _floorTile };
        }
    }
}