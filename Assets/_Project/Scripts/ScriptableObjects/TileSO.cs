using System.Collections.Generic;
using UnityEngine;

namespace ArcadeIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "TILE", order = 0)]
    public class TileSO : ScriptableObject
    {
        [SerializeField] private SectionTypes section;
        
        [SerializeField] private int tilePrice;
            
        [SerializeField] private bool isTileOpened;
        [SerializeField] private bool isOpenable;
        [SerializeField] private bool tileObjectAnim = true;
        
        public List<GameObject> tileObjects;
        public Vector3 tileObjectPosition;

        [Header("Tile Features")]
        [SerializeField] private ResourceSO tilePriceResource;
        [SerializeField] private TileTypes tileType;


        public bool IsTileOpened
        {
            get => isTileOpened;
            set => isTileOpened = value;
        }

        public bool IsOpenable
        {
            get => isOpenable;
            set => isOpenable = value;
        }

        public int TilePrice
        {
            get => tilePrice;
            set => tilePrice = value;
        }

        public ResourceSO TilePriceResource
        {
            get => tilePriceResource;
            set => tilePriceResource = value;
        }

        public TileTypes TileType
        {
            get => tileType;
            set => tileType = value;
        }

        public bool TileObjectAnim
        {
            get => tileObjectAnim;
            set => tileObjectAnim = value;
        }

        public SectionTypes Section
        {
            get => section;
            set => section = value;
        }
    }
}