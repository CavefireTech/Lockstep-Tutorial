using Lockstep.Collision2D;
using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;
using Debug = Lockstep.Logging.Debug;

namespace Lockstep.Game {
    public class InputMono : UnityEngine.MonoBehaviour {
        private static bool IsReplay => Launcher.Instance?.IsVideoMode ?? false;
        [HideInInspector] public int floorMask;
        public float camRayLength = 100;

        public bool hasHitFloor;
        public FVector2 mousePos;
        public FVector2 inputUV;
        public bool isInputFire;
        public int skillId;
        public bool isSpeedUp;

        void Start(){
            floorMask = LayerMask.GetMask("Floor");
        }

        public void FixedUpdate(){
            if (World.Instance != null && !IsReplay) {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                inputUV = new FVector2(h.ToFP(), v.ToFP());
                
                isInputFire = Input.GetButton("Fire1");
                hasHitFloor = Input.GetMouseButtonDown(1);
                if (hasHitFloor) {
                    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit floorHit;
                    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
                        mousePos = floorHit.point.ToFVector2();
                    }
                }

                skillId = 0;
                for (int i = 0; i < 6; i++) {
                    if (Input.GetKey(KeyCode.Keypad1 + i)) {
                        skillId = i+1;
                    }
                }

                isSpeedUp = Input.GetKeyDown(KeyCode.Space);
                GameInputService.CurGameInput = new PlayerInput() {
                    mousePos = mousePos,
                    inputUV = inputUV,
                    isInputFire = isInputFire,
                    skillId = skillId,
                    isSpeedUp = isSpeedUp,
                };
            }
        }
    }
}