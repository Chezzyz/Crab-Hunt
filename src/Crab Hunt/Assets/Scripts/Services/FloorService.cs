using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services
{
    public class FloorService : BaseTilemapService
    {
        [SerializeField] private List<Tile> _tiles;
        protected override List<Tile> GetTiles()
        {
            return _tiles;
        }
    }
}