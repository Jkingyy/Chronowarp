using System.Collections.Generic;
using UnityEngine;

namespace Ghost
{
    public class Playback : MonoBehaviour, IDamageable
    {
    
        //list of inputs and movements for each frame
        public List<Dictionary<string,bool>> frameRecording = new List<Dictionary<string, bool>>();
        public List<Vector2> playerPositions = new List<Vector2>();
        public List<Vector2> movementInputs = new List<Vector2>();

        Dictionary<string, bool> _currentFrameInputs = new Dictionary<string, bool>();
        private Vector2 _currentMovementInput;
        private Vector2 _direction = Vector2.up;
        private int _frameCounter;
    
        private bool _isDashing;
        private bool _hasDashed;
        private bool _isSprinting;
    
        [SerializeField] Animator animator;
        [SerializeField] SpriteRenderer sr;
        [SerializeField] new CapsuleCollider2D collider;
        
        [SerializeField] AudioClip[] footstepSounds;
    
        [SerializeField] private ParticleSystem dust;  
        [SerializeField] private ParticleSystem loopingDust;  
        
        const string GHOST_TIME_LOOP = "Ghost Time Loop";
        
        public PlayerMovement playerMovement;
        
        public bool isLive = false;
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        // Start is called before the first frame update
        void Start()
        {
            playerMovement.OnLoop.AddListener(HideGhost);
        }

        // Update is called once per frame
        void Update()
        {
            if (isLive && _frameCounter < playerPositions.Count)
            {
                PlayFrame();
            }
            
            
            if (Input.GetButtonDown("Loop"))
            {
                HideGhost();
            }
        }

        public void PlayStepSound()
        {
            SoundFXManager.Instance.PlayRandomSoundFXClip(footstepSounds, transform, 1f);
        }
        
        public void ResetFrameCounter()
        {
            _frameCounter = 0;
        }
        
        public void HideGhost()
        {
            sr.enabled = false;
            collider.enabled = false;
        }

        
        public void ShowGhost()
        {
            sr.enabled = true;
            collider.enabled = true;
        }

        public void PlayFrame()
        {
            transform.position = playerPositions[_frameCounter];
            _currentMovementInput = movementInputs[_frameCounter];
            GetCurrentFrameInputs();
        
            AssignCurrentFrameInputs();
            _frameCounter++;

            Particles();
            Animate();
        }
    
        void GetCurrentFrameInputs()
        {
            _currentFrameInputs = frameRecording[_frameCounter];
        }
    
        void AssignCurrentFrameInputs()
        {
            _isDashing = _currentFrameInputs["IsDashing"];
            _hasDashed = _currentFrameInputs["HasDashed"];
            _isSprinting = _currentFrameInputs["Sprint"];
        
        }

        public void SetPositionList(List<Vector2> PlayerPositions)
        {
            playerPositions = PlayerPositions;
        }

        public void SetInputList(List<Dictionary<string, bool>> FrameRecording)
        {
            frameRecording = FrameRecording;
        }
    

        bool IsGhostMoving()
        {
            return _currentMovementInput != Vector2.zero;
        }

        bool HasSprintBeenPressed()
        {
            if(_frameCounter < 2)
            {
                return _isSprinting;
            }
            return _currentFrameInputs["Sprint"] && !frameRecording[_frameCounter - 2]["Sprint"];
        }
    
        bool HasSprintBeenReleased()
        {
            if(_frameCounter < 2) return false;
        
            return !_currentFrameInputs["Sprint"] && frameRecording[_frameCounter - 2]["Sprint"];
        }
    
        void Particles()
        {
        

            if (HasSprintBeenPressed())
            {
                CreateParticle(loopingDust);
            } else if(HasSprintBeenReleased())
            {
                StopParticle(loopingDust);
            }
            if (_hasDashed)
            {
            
                CreateParticle(dust);
            }
        }

        static void CreateParticle(ParticleSystem Particle)
        {
        
            Particle.Play();
        }

        static void StopParticle(ParticleSystem Particle)
        {
            Particle.Stop();
        }
        void Animate()
        {
            if (IsGhostMoving())
            {
                animator.SetFloat("Horizontal", _currentMovementInput.x);
                animator.SetFloat("Vertical", _currentMovementInput.y);
            }

            if (!_isDashing)
            {
                animator.SetFloat("Speed", _currentMovementInput.sqrMagnitude); 
            }

            if (_hasDashed)
            {
                animator.SetTrigger("Dash");
                _hasDashed = false;
            }
        
            animator.SetBool("isSprinting", _isSprinting);

        }
        public void Damage(int DamageAmount)
        {
            Destroy(gameObject);
        }
    }
}
