using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services
{
    abstract public class BaseTilemapService : MonoBehaviour
    {
        private bool[,] _binaryMap;

        private Tilemap _tilemap;

        protected abstract List<Tile> GetTiles();

        protected void OnEnable()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        protected bool[,] GetWallMap()
        {
            if (_binaryMap != null)
            {
                return _binaryMap;
            }

            _tilemap.CompressBounds();

            bool[,] map = new bool[_tilemap.size.y, _tilemap.size.x];

            for (int x = 0; x < _tilemap.size.x; x++)
            {
                for (int y = 0; y < _tilemap.size.y; y++)
                {
                    Vector2Int tilemapOriginPos = new Vector2Int(_tilemap.cellBounds.xMin, _tilemap.cellBounds.yMin);

                    Vector3Int localPos = new Vector3Int(tilemapOriginPos.x + x, tilemapOriginPos.y + y, 0);
                    if (_tilemap.HasTile(localPos))
                    {
                        map[y, x] = HasTileInList(GetTiles(), _tilemap.GetTile(localPos));
                    }
                }
            }

            _binaryMap = map;

            return map;
        }

        public bool HasTileOnPosition(Vector2Int cellPosition)
        {
            Vector2Int localPos = GetMapLocalPosition(cellPosition);
            return GetWallMap()[localPos.y, localPos.x];
        }

        //Получить координаты центра клетки
        public Vector2 CellToWorldPosition(Vector2Int cellPosition) =>
            _tilemap.CellToWorld((Vector3Int)cellPosition) + _tilemap.cellSize / 2;

        public Vector2Int WorldToCellPosition(Vector2 worldPosition) => (Vector2Int)_tilemap.WorldToCell(worldPosition);

        //Переводит координаты клетки в координаты для двумерного массива карты
        protected Vector2Int GetMapLocalPosition(Vector2Int cellPosition)
        {
            _tilemap.CompressBounds();
            return new Vector2Int(-_tilemap.cellBounds.xMin, -_tilemap.cellBounds.yMin) + cellPosition;
        }

        protected bool HasTileInList(List<Tile> tiles, TileBase tile)
        {
            return tiles.Select(item => item.name).Contains(tile.name);
        }
    }
}