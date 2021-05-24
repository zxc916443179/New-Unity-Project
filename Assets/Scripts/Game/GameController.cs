using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.TPS.Gameplay;
using Unity.TPS.UI;

namespace Unity.TPS.Game {
    public class GameController : MonoBehaviour
    {
        [Header("UI")]
        public GameObject StartMenu;
        public GameObject GameHUD;
        public GameObject GameOverMenu;
        public GameObject Player;
        public GameObject Enemy;
        public Transform StartPoint;

        List<GameObject> Players;
        public List<Transform> EnemyPoints;
        List<GameObject> Enemies;
        // Start is called before the first frame update
        void Start()
        {
            var startMenu = StartMenu.GetComponent<StartMenu>();
            var gameoverMenu = GameOverMenu.GetComponent<GameOver>();
            startMenu.StartGame += StartGame;
            gameoverMenu.ResetGame += ResetGame;
            ResetGame();
        }
        void StartGame() {
            print("Game Start");
            var go = Instantiate(Player, StartPoint.position, StartPoint.rotation);
            Players.Add(go);
            for (int i = 0; i < EnemyPoints.Count; i ++) {
                var goEnemy = Instantiate(Enemy, EnemyPoints[i].position, EnemyPoints[i].rotation);
                goEnemy.GetComponent<Health>().onDie += onDie;
                Enemies.Add(goEnemy);
            }
            StartMenu.SetActive(false);
            GameHUD.SetActive(true);
            var hud = GameHUD.GetComponent<PlayerHUD>();
            hud.health = go.GetComponent<Health>();
            hud.health.onDie += onDie;
            hud.playerWeaponController = go.GetComponent<PlayerWeaponController>();
        }
        void onDie(GameObject gameObject) {
            if (gameObject.transform.tag == "Player")
                Players.Remove(gameObject);
            else {
                Enemies.Remove(gameObject);
            }
            if (Players.Count == 0 || Enemies.Count == 0) EndGame(Players.Count != 0);
            print("Players Count:" + Players.Count);
            print("Enemies Count:" + Enemies.Count);
        }
        void EndGame(bool win) {
            print("Game Over!");
            GameHUD.SetActive(false);
            GameOverMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (win) {
                var gameOverMenu = GameOverMenu.GetComponent<GameOver>();
                gameOverMenu.slogan.text = "YOU WIN!!";
                gameOverMenu.slogan.color = new Color(0, 255, 0, 255);
            }
            for (int i = 0; i < Enemies.Count; i ++) {
                Destroy(Enemies[i]);
            }
            for (int i = 0; i < Players.Count; i ++) {
                Destroy(Players[i]);
            }
        }
        void ResetGame() {
            StartMenu.SetActive(true);
            GameHUD.SetActive(false);
            GameOverMenu.SetActive(false);
            
            Players = new List<GameObject>();
            Enemies = new List<GameObject>();
            print("Players Count:" + Players.Count);
            print("Enemies Count:" + Enemies.Count);
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}