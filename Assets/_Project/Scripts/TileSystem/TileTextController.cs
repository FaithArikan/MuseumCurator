using ArcadeIdle.Helpers;
using UnityEngine;
using TMPro;

namespace ArcadeIdle.TileSystem
{
    public class TileTextController : MonoBehaviour
    {
        private Tile _tile;
        [SerializeField] private TextMeshProUGUI priceText;

        private void Awake()
        {
            _tile = GetComponent<Tile>();
        }

        private void OnEnable()
        {
            _tile.OnTileOpenAction += TileOnTileOpenAction; 
        }
        private void OnDisable()
        {
            _tile.OnTileOpenAction -= TileOnTileOpenAction; 
        }

        private void TileOnTileOpenAction(int value)
        {
            priceText.text = Utils.FormatCash(value);
        }
    }
}