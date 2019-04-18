using System;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Tilemaps;

namespace Gamekit2D
{
    public class PlayerInput :InputComponent//, IDataPersister
    {

        public static PlayerInput Instance
        {
            get {        
                if (instance==null)
                    instance = FindObjectOfType<PlayerInput>();
                return instance;
            }
        }

        protected static PlayerInput instance;

        void Start()
        {
            instance = FindObjectOfType<PlayerInput>();
            instance.GainControl();
            Instance.MoveLeft = new InputButton(KeyCode.LeftArrow);
            Instance.MoveRight = new InputButton(KeyCode.RightArrow);
            Instance.MoveUp = new InputButton(KeyCode.UpArrow);
            Instance.MoveDown = new InputButton(KeyCode.DownArrow);
            Instance.CellSelected = new InputButton(KeyCode.Mouse0);
            Instance.Exit = new InputButton(KeyCode.Escape);
        }
        public bool HaveControl { get { return m_HaveControl; } }
        public InputButton MoveLeft;
        public InputButton MoveRight;
        public InputButton MoveUp;
        public InputButton MoveDown;
        public InputButton CellSelected;
        public InputButton Exit;

        protected bool m_HaveControl = true;

        protected bool m_DebugMenuIsOpen = false;

        void OnDisable()
        {
          //  PersistentDataManager.UnregisterPersister(this);
            instance = null;
        }

        protected override void GetInputs(bool fixedUpdateHappened)
        {
            Instance.MoveLeft.Get(fixedUpdateHappened, inputType);
            Instance.MoveRight.Get(fixedUpdateHappened, inputType);
            Instance.MoveUp.Get(fixedUpdateHappened, inputType);
            Instance.MoveDown.Get(fixedUpdateHappened, inputType);
            Instance.CellSelected.Get(fixedUpdateHappened, inputType);
            Instance.Exit.Get(fixedUpdateHappened, inputType);
        }

        public void EnableButtons()
        {
            Instance.MoveLeft.Enable();
            Instance.MoveRight.Enable();
            Instance.MoveUp.Enable();
            Instance.MoveDown.Enable();
            Instance.CellSelected.Enable();
            Instance.Exit.Enable();
        }
        public override void GainControl()
        {
            m_HaveControl = true;
            GainControl(Instance.MoveLeft);
            GainControl(Instance.MoveRight);
            GainControl(Instance.MoveUp);
            GainControl(Instance.MoveDown);
            GainControl(Instance.CellSelected);
            GainControl(Instance.Exit);
        }

        public override void ReleaseControl(bool resetValues = true)
        {
            m_HaveControl = false;
            ReleaseControl(Instance.MoveLeft, resetValues);
            ReleaseControl(Instance.MoveRight, resetValues);
            ReleaseControl(Instance.MoveUp, resetValues);
            ReleaseControl(Instance.MoveDown, resetValues);
            ReleaseControl(Instance.CellSelected, resetValues);
            ReleaseControl(Instance.Exit, resetValues);
        }
      
    }
}
