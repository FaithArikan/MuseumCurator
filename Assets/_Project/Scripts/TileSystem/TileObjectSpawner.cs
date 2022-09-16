using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ArcadeIdle.TileSystem
{
    public class TileObjectSpawner : MonoBehaviour
    {
        private Tile _tile;

        private void Awake()
        {
            _tile = GetComponent<Tile>();
        }
        
        private void OnEnable()
        {
            _tile.OnTileSpawnObjectsAction += OnTileEnable;
        }
        
        private void OnDisable()
        {
            _tile.OnTileSpawnObjectsAction -= OnTileEnable;
        }

        private void OnTileEnable()
        {
            if (_tile.tileObjects.Count <= 0) return;
            for (int i = 0; i < _tile.tileObjects.Count; i++)
            {
                GameObject obj = Instantiate(_tile.tileObjects[i], 
                    _tile.tileObjectsPositions[i] + transform.localPosition, Quaternion.identity, transform);
                obj.gameObject.SetActive(true);
                
                if (!_tile.TileObjectAnim) return;
                ObjectTweenAnimation(obj);
            }
        }

        private void ObjectTweenAnimation(GameObject gO)
        {
            gO.transform.DOMoveY(transform.localPosition.y + 1, 1f).OnComplete
                (() => gO.transform.DOMoveY(transform.localPosition.y, 1f));
            gO.transform.DORotate(new Vector3(0f, 360f, 0f), 2f, RotateMode.FastBeyond360).
                OnComplete(() => gO.GetComponentInChildren<TextMeshProUGUI>().enabled = true);
        }
    }
}