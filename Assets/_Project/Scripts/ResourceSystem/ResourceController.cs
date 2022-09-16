using System.Collections;
using ArcadeIdle.SaveSystem;
using ArcadeIdle.ScriptableObjects;
using ArcadeIdle.Variables;
using UnityEngine;

namespace ArcadeIdle.ResourceSystem
{
    public class ResourceController : MonoBehaviour
    {
        [SerializeField] private ResourceSO blueResource;
        [SerializeField] private ResourceSO purpleResource;

        [SerializeField] private IntVariable blueSum;
        [SerializeField] private IntVariable purpleSum;
        
        private void OnEnable()
        {
            Load();
        }

        private void OnDisable()
        {
            Save();
        }

        #region Save-Load
        
        private void Save()
        {
            SaveManager.BinarySerialize("blueResource.arc", blueResource.Amount);
            SaveManager.BinarySerialize("purpleResource.arc", purpleResource.Amount);

        }

        private void Load()
        {
            blueResource.Amount = SaveManager.BinaryDeserialize<int>("blueResource.arc");
            purpleResource.Amount = SaveManager.BinaryDeserialize<int>("purpleResource.arc");
        }
        
        #endregion


        private IEnumerator Start() 
        {
            while(true) 
            {
                yield return new WaitForSeconds(1f);
                AddBlueResourceAmount();
                AddPurpleResourceAmount();
            }
        }
        
        private void AddBlueResourceAmount()
        {
            blueResource.Amount += blueSum.Value;
        }
        
        private void AddPurpleResourceAmount()
        {
            purpleResource.Amount += purpleSum.Value;
        }
    }
}