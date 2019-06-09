//using System;
//using UnityEngine;
//using Random = UnityEngine.Random;

//namespace AnySync.Examples
//{
//    public class WebSocketPlayerSync : MonoBehaviour
//    {
//        private const float SendInterval = 0.1f; // 10 messages per second
//        private const float MovementAcceleration = 13f;

//        public int Identity;
//        public bool HasAuthority;

//        private Vector2 _input;

//        private Rigidbody _rigidbody;
//        private SyncBuffer _syncBuffer;
//        private Transform _transform;
//        private void Awake()
//        {
//            _rigidbody = GetComponent<Rigidbody>();
//            _syncBuffer = GetComponent<SyncBuffer>();
//            _transform = transform;
//        }

//        private void OnEnable()
//        {
//            WebSocketClient.MessageEvent += OnMessage;
//        }

//        private void OnDisable()
//        {
//            WebSocketClient.MessageEvent -= OnMessage;
//        }

//        private void Update()
//        {
//            if (!HasAuthority)
//            {
//                if (_syncBuffer.HasKeyframes)
//                {
//                    _syncBuffer.UpdatePlayback(Time.deltaTime);
//                    _transform.position = _syncBuffer.Position;
//                    _transform.rotation = _syncBuffer.Rotation;
//                }
//            }
//            else
//            {
//                // movement
//                _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

//                // teleportation
//                if (Input.GetKeyDown(KeyCode.Space))
//                {
//                    // send a keyframe to interpolate to before teleporting
//                    SendSyncMessage();

//                    var newPosition = new Vector3(Random.Range(-6f, 6f), 2f, Random.Range(-6f, 6f));
//                    _rigidbody.position = newPosition;
//                    _rigidbody.rotation = Quaternion.identity;
//                    _rigidbody.velocity = Vector3.zero;
//                    _rigidbody.angularVelocity = Vector3.zero;

//                    // and then immediately send a teleportation keyframe with 0 interpolation time
//                    SendSyncMessage();
//                }
//            }
//        }

//        private float _timeSinceLastSync;
//        private Vector3 _lastSentVelocity;
//        private void FixedUpdate()
//        {
//            if (HasAuthority)
//            {
//                // control the rigidbody locally
//                _rigidbody.AddForce(new Vector3(-_input.x, 0f, -_input.y) * MovementAcceleration * Time.deltaTime, ForceMode.VelocityChange);

//                _timeSinceLastSync += Time.deltaTime;

//                if (_rigidbody.velocity != _lastSentVelocity)
//                {
//                    if (Mathf.Approximately(_timeSinceLastSync, SendInterval) || _timeSinceLastSync > SendInterval)
//                    {
//                        // send rigidbody data from client to server on change
//                        SendSyncMessage();
//                    }
//                }
//            }
//        }

//        private void SendSyncMessage()
//        {
//            WebSocketClient.SendMessageObject(new WebSocketMessage.PlayerSync
//            {
//                Identity = Identity,
//                InterpolationTime = _timeSinceLastSync,
//                Position = _rigidbody.position,
//                Rotation = _rigidbody.rotation,
//                Velocity = _rigidbody.velocity
//            });
//            _lastSentVelocity = _rigidbody.velocity;
//            _timeSinceLastSync = 0f;
//        }

//        private void OnMessage(Type type, string json)
//        {
//            if (type == typeof(WebSocketMessage.PlayerSync))
//            {
//                if (!HasAuthority)
//                {
//                    var playerSyncMessage = JsonUtility.FromJson<WebSocketMessage.PlayerSync>(json);
//                    if (playerSyncMessage.Identity == Identity)
//                        _syncBuffer.AddKeyframe(playerSyncMessage.InterpolationTime, playerSyncMessage.Position, playerSyncMessage.Rotation, playerSyncMessage.Velocity);
//                }
//            }
//            else if (type == typeof(WebSocketMessage.PlayerDespawn))
//            {
//                var playerDespawnMessage = JsonUtility.FromJson<WebSocketMessage.PlayerDespawn>(json);
//                if (playerDespawnMessage.Identity == Identity)
//                    Destroy(gameObject);
//            }
//            else if (type == typeof(WebSocketMessage.PlayerSpawn))
//            {
//                // force to send new position when someone just spawned and needs your initial state
//                SendSyncMessage();
//            }
//        }
//    }
//}
