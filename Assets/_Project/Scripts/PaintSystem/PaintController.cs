using System.Collections.Generic;
using ArcadeIdle.ScriptableObjects;
using UnityEngine;

namespace ArcadeIdle.PaintSystem
{
    public class PaintController : MonoBehaviour
    {
        [SerializeField] private GameObject paintBackpack;
        [SerializeField] private List<PaintSO> paintSos;
        private Material _material;

        private void Awake()
        {
            paintBackpack.SetActive(false);
        }

        #region Assigning Actions

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
            foreach (var paint in paintSos)
            {
                paint.OnPaintSelected += OnColorSelected;
            }
        }

        private void UnAssignActions()
        {
            foreach (var paint in paintSos)
            {
                paint.OnPaintSelected -= OnColorSelected;
            }
        }

        #endregion

        private void OnColorSelected(Material mat)
        {
            paintBackpack.SetActive(true);
            _material = mat;
            paintBackpack.GetComponent<MeshRenderer>().material = mat;
        }

        public void OnPaint()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10))
            {
                var hitGameObject = hit.transform.gameObject;
                if (hitGameObject.CompareTag("Plane"))
                {
                    hitGameObject.GetComponent<MeshRenderer>().material = _material;
                }
            }
        }
    }
}