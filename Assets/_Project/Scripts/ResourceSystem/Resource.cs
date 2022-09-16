using ArcadeIdle.ScriptableObjects;
using UnityEngine;

namespace ArcadeIdle.ResourceSystem
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceSO resourceSo;
        public ResourceSO ResourceSo => resourceSo;
        [SerializeField] private int sumAmount;

        public int SumAmount
        {
            get => sumAmount;
            set => sumAmount = value;
        }
    }
}