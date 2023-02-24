using UnityEngine;

namespace Common.Ui
{
    /// <summary>
    /// Parent class for Canvas component's initialize
    /// </summary>
    public abstract class UiCanvas : MonoBehaviour
    {
        [Header("Ui camera for set screen resolution")]
        [SerializeField] private Camera uiCamera;
    
        public bool IsInitialized { get; private set; }

        protected void Awake()
        {
            GameUtility.SetScreenResolution(uiCamera);
        
            Initialize();
        }

        protected virtual void Initialize()
        {
            IsInitialized = true;
        }
    }    
}