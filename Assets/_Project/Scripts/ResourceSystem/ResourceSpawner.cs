using System;
using ArcadeIdle.TileItemSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArcadeIdle.ResourceSystem
{
    public class ResourceSpawner : MonoBehaviour
    {
        private TileItem _tileItem;

        [SerializeField] private GameObject blueMoney;
        [SerializeField] private GameObject purpleMoney;
        
        public event Action OnResourceSpawned;

        private void Awake()
        {
            _tileItem = GetComponent<TileItem>();
        }

        private void OnEnable()
        {
            AssignActions();
        }
        
        private void AssignActions()
        {
            _tileItem.OnItemOpenedAction += TileItemOnItemOpenedAction;
        }

        private void OnDisable()
        {
            UnAssignActions();
        }
        
        private void UnAssignActions()
        {
            _tileItem.OnItemOpenedAction -= TileItemOnItemOpenedAction;
        }

        private void TileItemOnItemOpenedAction(int value)
        {
            for (int i = 0; i < value / 5; i++)
            {
                var position = transform.position;
                var positionX = Random.Range(position.x - 1.5f, position.x + 1.5f);
                var positionY = Random.Range(position.y - 1.5f, position.y + 1.5f);
                var positionZ = Random.Range(position.z - 1.5f, position.z + 1.5f);

                GameObject go = Instantiate(purpleMoney, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddExplosionForce(1f, position, 2f, 1f, ForceMode.Impulse);
            }
            OnResourceSpawned?.Invoke();
        }

        private void ChangeResourceSumAmount(GameObject gO, int val)
        {
            gO.GetComponent<Resource>().SumAmount = val;
        }
    }
}