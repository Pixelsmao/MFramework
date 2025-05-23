using MFramework.DataPersistence;
using UnityEngine;

namespace MFramework.UnityApplication
{
    [PersistenceScript]
    public partial class ApplicationManager : MonoBehaviour
    {
        private static ApplicationManager self;

        public static ApplicationManager Instance
        {
            get
            {
                if (self != null) return self;
                self = FindFirstObjectByType<ApplicationManager>();
                if (self == null)
                {
                    var manager = new GameObject("ApplicationManager");
                    self = manager.AddComponent<ApplicationManager>();
                }

                if (Application.isPlaying)
                {
                    DontDestroyOnLoad(self);
                }

                return self;
            }
        }

        private void Awake()
        {
            self = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            InitHardwareInfos();
            ApplyApplicationSettings();
        }

        private void OnGUI()
        {
            GUIUpdate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(showGUIKeyCode)) showGUI = !showGUI;
            if (!showGUI) return;
            FPSUpdate();
            HardwareInfosUpdate();
        }

        private void OnApplicationQuit()
        {
            CollectPlayerLog();
        }
    }
}