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
            AssignActions();
        }

        private void OnDisable()
        {
            UnAssignActions();
        }
        private void AssignActions()
        {
            _tile.OnTileOpenAction += TileOnTileOpenAction;
        }

        private void UnAssignActions()
        {
            _tile.OnTileOpenAction -= TileOnTileOpenAction;
        }

        private void TileOnTileOpenAction(int value)
        {
            if (priceText != null)
            {
                priceText.text = Utils.FormatCash(value);
            }
        }
    }
}