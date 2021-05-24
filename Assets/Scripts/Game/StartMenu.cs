using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace Unity.TPS.Game
{
    public class StartMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        public Button StartButton;
        public UnityAction StartGame;
        void Start()
        {
            StartButton.onClick.AddListener(() => { StartGame.Invoke(); });
        }

    }
    
}
