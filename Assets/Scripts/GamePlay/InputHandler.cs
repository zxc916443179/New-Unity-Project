using UnityEngine;
using Unity.TPS.Game;

namespace Unity.TPS.Gameplay
{
    public class InputHandler : MonoBehaviour {
        public float LookSensitivity = 1f;
        public float TriggerAxisThreshold = 0.4f;
        PlayerCharacterController m_CharacterController;
        public bool InvertYAxis = false;
        bool m_FireInputWasHeld;
        private void Start() {
            m_CharacterController = GetComponent<PlayerCharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
        }
        private void LateUpdate() {
            m_FireInputWasHeld = GetFireHeld();
            if (Input.GetMouseButtonDown(0)) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        public bool GetReload() {
            return Input.GetButtonDown(GameConstants.k_ButtonReload);
        }
        public bool CanProcessInput() {
            return Cursor.lockState == CursorLockMode.Locked;
        }
        public bool GetFireHeld() {
            if (CanProcessInput()) {
                return Input.GetButton(GameConstants.k_ButtonNameFire);
            }
            return false;
        }
        public float GetLookInputsHorizontal()
        {
            if (CanProcessInput()) {
                return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameHorizontal,
                    GameConstants.k_AxisNameJoystickLookHorizontal);
            }
            return 0f;
        }

        public float GetLookInputsVertical()
        {
            if (CanProcessInput()) {
                return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameVertical,
                    GameConstants.k_AxisNameJoystickLookVertical);
            }
            return 0f;
        }
        float GetMouseOrStickLookAxis(string mouseInputName, string stickInputName)
        {
            if (CanProcessInput())
            {
                bool isGamepad = Input.GetAxis(stickInputName) != 0f;
                float i = isGamepad ? Input.GetAxis(stickInputName) : Input.GetAxisRaw(mouseInputName);

                if (InvertYAxis)
                    i *= -1f;

                i *= LookSensitivity;

                if (isGamepad)
                {
                    i *= Time.deltaTime;
                }
                else
                {
                    i *= 0.01f;
                }

                return i;
            }

            return 0f;
        }
        public Vector3 GetMoveInput()
        {
            if (CanProcessInput())
            {
                Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f,
                    Input.GetAxisRaw(GameConstants.k_AxisNameVertical));

                // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
                move = Vector3.ClampMagnitude(move, 1);

                return move;
            }

            return Vector3.zero;
        }
        public bool GetJumpInputDown() {
             if (CanProcessInput()) {
                return Input.GetButtonDown(GameConstants.k_ButtonNameJump);
            }

            return false;
        }
        public bool GetKeyDown(string keyName) {
            if (CanProcessInput()) {
                return Input.GetButtonDown(keyName);
            }
            return false;
        }
        public bool GetKeyUp(string keyName) {
            if (CanProcessInput()) {
                return Input.GetButtonUp(keyName);
            }
            return false;
        }
        public bool GetFireDown() {
            return GetFireHeld() && !m_FireInputWasHeld;
        }
        public bool GetFireRelease() {
            return !GetFireHeld() && m_FireInputWasHeld;
        }
        public bool GetSwitchFireMode() {
            return Input.GetButtonDown(GameConstants.k_ButtonNameSwitchFireMode);
        }

    }    
}