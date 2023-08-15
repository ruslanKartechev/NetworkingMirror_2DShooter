using Mirror;
using UnityEngine;

namespace GameCore.Player
{
    public class SceneObjectActivator : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        private void Awake()
        {
            Debug.Log($"[{nameof(SceneObjectActivator)}] Awake, target active: {_target.activeInHierarchy}");
            _target.SetActive(true);
        }
        
        private void Start()
        {
            Debug.Log($"[{nameof(SceneObjectActivator)}] Start, target active: {_target.activeInHierarchy}");
            _target.SetActive(true);
        }


    }
}