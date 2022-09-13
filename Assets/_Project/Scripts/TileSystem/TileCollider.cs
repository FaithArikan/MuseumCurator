using System;
using UnityEngine;

namespace ArcadeIdle.TileSystem
{
    public class TileCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider outCol;
        [SerializeField] private BoxCollider inCol;
        
        private Tile _tile;

        private void Awake()
        {
            _tile = GetComponent<Tile>();
        }

        private void OnEnable()
        {
            _tile.OnColliderAction += OnThisTileOpened;
        }

        private void OnThisTileOpened(bool b)
        {
            if (outCol != null)
            {
                outCol.isTrigger = b;
                outCol.enabled = b;
            }

            if (inCol != null)
            {
                inCol.isTrigger = !b;
                inCol.enabled = b;
            }
        }

        private void OnDisable()
        {
            _tile.OnColliderAction -= OnThisTileOpened;
        }
    }
}