using ArcadeIdle.ResourceSystem;
using UnityEngine;

namespace ArcadeIdle.Player
{
    public class ResourcePicker : MonoBehaviour
    {
        [SerializeField] private AudioSource resourcePickAudioSource;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Resource"))
            {
                Resource res = other.gameObject.GetComponent<Resource>();
                res.ResourceSo.Amount += res.SumAmount;
                resourcePickAudioSource.Play();
                Destroy(other.gameObject);
            }
        }
    }
}