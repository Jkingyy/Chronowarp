using System.Collections.Generic;
using UnityEngine;

namespace Ghost
{
    public class Recording : MonoBehaviour
    {

    
        //list of inputs and movements for each frame
        public List<Dictionary<string,bool>> frameRecording = new List<Dictionary<string, bool>>();
        public List<Vector2> playerPositions = new List<Vector2>();
        public List<Vector2> movementInputs = new List<Vector2>();
    
        [SerializeField] GameObject ghostPrefab;

        private bool _isRecording;
        

    
        /// <summary>
        /// ////////////////COMPONENT REFERENCES////////////////////////
        /// </summary>
        private PlayerMovement _playerMovement;

        void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        // Start is called before the first frame update
        void Start()
        {
            StartRecording();
        }

        // Update is called once per frame
        void Update()
        {
            if (_isRecording)
            {
                RecordFrame();
            }
        }

        void RecordFrame()
        {

            GetInputs();
            GetPosition();
        }

        void GetInputs()
        {
            Dictionary<string, bool> _FrameInputs = new Dictionary<string, bool>();
            AddInputToDictionary("HasDashed", _playerMovement.hasDashed, _FrameInputs);
            AddInputToDictionary("IsDashing", _playerMovement.isDashing, _FrameInputs);
            AddInputToDictionary("Sprint", _playerMovement.isSprinting, _FrameInputs);
        
            AddFrameInputsToList(_FrameInputs);
        }

        void AddInputToDictionary(string Action, bool IsPressed, Dictionary<string,bool> Dictionary)
        {
            Dictionary.Add(Action, IsPressed);
        }
    
        void AddFrameInputsToList( Dictionary<string,bool> Dictionary)
        {
            frameRecording.Add(Dictionary);
        
        }
    
    
        void GetPosition()
        {
            playerPositions.Add(transform.position);
            movementInputs.Add(_playerMovement.movementInput);
        }
    
        void StartRecording()
        {
            _isRecording = true;
        }
    
    
        public void StopRecording()
        {
            _isRecording = false;
            SpawnPlayerGhost();
        }
    
        void SpawnPlayerGhost()
        {
            Playback _Playback = Instantiate(ghostPrefab).GetComponent<Playback>();
        
            _Playback.HideGhost();
            _Playback.frameRecording = new List<Dictionary<string, bool>>(frameRecording);
            _Playback.playerPositions = new List<Vector2>(playerPositions); 
            _Playback.movementInputs = new List<Vector2>(movementInputs);
            _Playback.playerMovement = _playerMovement;
        }

        void PlayRecording()
        {
            GameObject[] _GhostList = GameObject.FindGameObjectsWithTag("PlayerGhost"); 

            foreach (var ghost in _GhostList)
            {
                Playback _Playback = ghost.GetComponent<Playback>();
                _Playback.isLive = true;
                _Playback.ShowGhost();
                _Playback.ResetFrameCounter();
            }
        }
    }
}
