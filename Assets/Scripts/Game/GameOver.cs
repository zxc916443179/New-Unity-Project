
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Button QuitButton;
    public Button ResetButton;

    public UnityAction ResetGame;
    public Text slogan;
    private void Start() {
        QuitButton.onClick.AddListener( () => { Application.Quit(); } );
        ResetButton.onClick.AddListener( () => { ResetGame?.Invoke(); });
    }
}
