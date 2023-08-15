
using Mirror;
using UnityEngine;

namespace GameCore
{
    public class MovementSync2D : NetworkBehaviour
    {
        [SyncVar] private float syncX;
        [SyncVar] private float syncY;
        [SyncVar] private float angleZ;
        [SerializeField] private Transform _movable;
        [SerializeField] private Transform _rotatable;
        [SerializeField] private float _lerpRate;

        private void Awake()
        {
            var pos = _movable.position;
            syncX = pos.x;
            syncY = pos.y;
            angleZ = _rotatable.eulerAngles.z;   
        }

        private void Update()
        {
            // Debug.Log($"[Sync2D], Authority: {authority}")
            if (isServer && !isClient)
            {
                LerpPosition();
                return;
            }
            if (authority)
            {
                var pos = _movable.position;
                CmdSetTransformData(pos.x, pos.y, _rotatable.eulerAngles.z);
            }
            else
                LerpPosition();
        }

        [Command]
        private void CmdSetTransformData(float x, float y, float zAngle)
        {
            syncX = x;
            syncY = y;
            angleZ = zAngle;
        }
        
        private void LerpPosition()
        {
            var oldPos = _movable.position;
            var t = Time.deltaTime * _lerpRate;
            _movable.position = new Vector3(
                Mathf.Lerp(oldPos.x, syncX, t),
                Mathf.Lerp(oldPos.y, syncY, t), 0);
            var targetRot = Quaternion.Euler(0f, 0f, angleZ);
            _rotatable.rotation = Quaternion.Lerp(_rotatable.rotation, targetRot, t);    
        }
    }
}