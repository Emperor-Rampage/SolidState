// GENERATED AUTOMATICALLY FROM 'Assets/MasterInput.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class MasterInput : InputActionAssetReference
{
    public MasterInput()
    {
    }
    public MasterInput(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Movement = m_Player.GetAction("Movement");
        m_Player_Fire = m_Player.GetAction("Fire");
        m_Player_Jump = m_Player.GetAction("Jump");
        m_Player_Crouch = m_Player.GetAction("Crouch");
        m_Player_Dash = m_Player.GetAction("Dash");
        m_Player_Melee = m_Player.GetAction("Melee");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Player = null;
        m_Player_Movement = null;
        m_Player_Fire = null;
        m_Player_Jump = null;
        m_Player_Crouch = null;
        m_Player_Dash = null;
        m_Player_Melee = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Player
    private InputActionMap m_Player;
    private InputAction m_Player_Movement;
    private InputAction m_Player_Fire;
    private InputAction m_Player_Jump;
    private InputAction m_Player_Crouch;
    private InputAction m_Player_Dash;
    private InputAction m_Player_Melee;
    public struct PlayerActions
    {
        private MasterInput m_Wrapper;
        public PlayerActions(MasterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Player_Movement; } }
        public InputAction @Fire { get { return m_Wrapper.m_Player_Fire; } }
        public InputAction @Jump { get { return m_Wrapper.m_Player_Jump; } }
        public InputAction @Crouch { get { return m_Wrapper.m_Player_Crouch; } }
        public InputAction @Dash { get { return m_Wrapper.m_Player_Dash; } }
        public InputAction @Melee { get { return m_Wrapper.m_Player_Melee; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
    }
    public PlayerActions @Player
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerActions(this);
        }
    }
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get

        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard And Mouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get

        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
}