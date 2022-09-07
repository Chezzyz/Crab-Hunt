using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services
{
    [RequireComponent(typeof(Tilemap))]
    public class WallsService : BaseTilemapService
    {
        [SerializeField]
        private List<Tile> _wallTiles;
        
        protected override List<Tile> GetTiles()
        {
            return _wallTiles;
        }

        //Является ли клетка развилкой
        public bool IsIntersectionCell(Vector2Int cellPosition)
        {
            if (!HasTileOnPosition(cellPosition))
            {
                bool[,] wallMap = GetWallMap();
                List<Vector2Int> directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right };
                Vector2Int localPos = GetMapLocalPosition(cellPosition);
                int roadCount = 0;

                foreach (var direction in directions)
                {
                    Vector2Int next = localPos + direction;
                    if (!wallMap[next.y, next.x])
                    {
                        roadCount++;
                    }
                }

                //Если клетка не стена и вокруг нее 3 или больше дорог, то это развилка;
                return roadCount >= 3;
            }

            return false;
        }
        
    }
}
