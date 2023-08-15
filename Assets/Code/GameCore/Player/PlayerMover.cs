using Mirror;
using UI;
using UnityEngine;

namespace GameCore.Player
{
    // [System.Serializable]
    // public class WallsChecker
    // {
    //     [SerializeField] private LayerMask _layerMask;
    //     [SerializeField] private float _radius = 0.5f;
    //
    //     public Vector3 CorrectPosition(Vector3 pos)
    //     {
    //         var colls = Physics2D.OverlapCircleAll(pos, _radius, _layerMask);
    //         Physics2D.
    //         if(colls.Length == 0)
    //             return pos;
    //         foreach (var coll in colls)
    //         {
    //             var pp = coll.ClosestPoint(pos);
    //             
    //         }
    //     }
    // }
    public class PlayerMover : NetworkBehaviour, IMoveInputListener
    {
        private const float MinMoveMagn = 0.1f;
        [SyncVar] [SerializeField] private float _moveSpeed;
        [SerializeField] private Transform _movable;
        [SerializeField] private Transform _rotatable;
        [SerializeField] private Rigidbody2D _rb;
        private IBorderChecker _borderChecker;
        private IControlsUI _controlsUI;
        private Vector3 _prev;
        
        public bool CanMove { get; set; }

        public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;    
        
        public void SetMoveBorders(IBorderChecker borderChecker) =>  _borderChecker = borderChecker;

        public void SetControlsUI(IControlsUI ui)
        {
            _controlsUI = ui;
            ui.SetMoveInputListener(this);
            if(CanMove)
                ui.TurnOn();
        }

        public void AllowMovement()
        {
            CanMove = true;
            _controlsUI.TurnOn();
        }

        public void StopMovement()
        {
            CanMove = false;
            _controlsUI.TurnOff();
        }
        
        void IMoveInputListener.Move(Vector2 direction, float magnitude)
        {
            if (magnitude < MinMoveMagn)
                return;
            MoveInDir(direction);
        }
        
        [ClientCallback]
        private void Update()
        {
            if (!CanMove)
                return;
            var direction = Vector2Int.zero;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                direction.x = -1;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                direction.x = 1;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                direction.y = 1;
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                direction.y = -1;
            MoveInDir(direction);
        }

        private void MoveInDir(Vector2 direction)
        {
            _rb.velocity = direction.normalized * _moveSpeed;
            // var pos = GetNextPosition(direction);
            // if (_borderChecker.CheckBorders(pos))
            //     _movable.position = pos;
            if (direction.x == 0 && direction.y == 0)
                return;
            var rot = GetRotation(direction);
            _rotatable.rotation = rot;
            
        }
        
        private Quaternion GetRotation(Vector2 direction)
        {
            var angle = Vector2.SignedAngle(Vector2.up, direction);
            return Quaternion.Euler(0,0,angle);
        }

        private Vector3 GetNextPosition(Vector2 direction)
        {
            var newPos = 
                _movable.position + new Vector3(direction.x, direction.y, 0f).normalized * (_moveSpeed * Time.deltaTime);
            return newPos;
        }
        
    }
}