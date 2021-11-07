using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageBuilder : MonoBehaviour
{
    private Tilemap _paintTileMap;

    public Transform paintLayer;
    public GameObject defaultTile;

    private void Awake()
    {
        _paintTileMap = paintLayer.GetComponent<Tilemap>();

        Vector3Int paintLayerSize = _paintTileMap.size;
        Debug.Log(paintLayerSize.x);

        for (int x = 0; x < paintLayerSize.x; x++)
        {
            for (int y = 0; y < paintLayerSize.y; y++)
            {
                Vector2 spawnPosition = new Vector2(x + 0.5f - paintLayerSize.x / 2, y + 0.5f - paintLayerSize.y / 2);

                Vector2Int tileMapPosition = new Vector2Int(x - paintLayerSize.x / 2, y - paintLayerSize.y/2);
                Sprite tileSprite = _paintTileMap.GetSprite((Vector3Int)tileMapPosition);

                if (tileSprite != null)
                    BreakableTile.Create(defaultTile, spawnPosition, tileSprite, transform);
            }
        }

        paintLayer.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        GetComponent<CompositeCollider2D>().GenerateGeometry();
    }
}
