using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject towerManagerObject;
    public GameObject shipManagerObject;

    private TowerManager towerManagerScript;
    private ShipManager shipManagerScript;
    
    public Text coinLabel;
    public static int numCoins = 100;
    public static int totalCoinsSpent = 0;

    public Text upgradeCostLabel;
    public static int currentUpgradeCost = 25;

    public Text waveLabel;
    private int wave = 0;
    private bool newWavePossible;
    private bool allShipsSpawned;
    private int shipsToSpawn;
    private float spawnInterval;
    private float timeSinceLastSpawn;
    private int shipsSpawned;

    public Text towerPriceLabel;

    // Start is called before the first frame update
    void Start()
    {
        towerManagerScript = towerManagerObject.transform.GetComponent<TowerManager>();
        shipManagerScript = shipManagerObject.transform.GetComponent<ShipManager>();

        newWavePossible = true;
    }

    void Update()
    {
        towerManagerScript.towerManagerMainFunction();
        towerManagerScript.aimTowers();
        runWave();

        //Update UI
        coinLabel.text = $"COINS: {numCoins}";
        towerPriceLabel.text = $"Tower Price: {TowerManager.towerCost}";
        upgradeCostLabel.text = $"Upgrade Price: {currentUpgradeCost}";
    }

    void FixedUpdate()
    {        
        shipManagerScript.shipManagerMainFunction();

        if(allShipsSpawned && shipManagerObject.transform.childCount == 0)
        {
            newWavePossible = true;
        }

    }

    void newWave()
    {
        newWavePossible = false;
        allShipsSpawned = false;
        wave++;
        waveLabel.text = $"WAVE: {wave}";
        shipsToSpawn = wave;
        spawnInterval = 0.25f - (wave/100);
        shipsSpawned = 0;
    }

    void runWave()
    {
        if(shipsSpawned < shipsToSpawn) //Are there still ships left to spawn?
        {
            if(timeSinceLastSpawn > spawnInterval) //Has enough time passed since the last ship spawn?
            {
                float health = Random.Range(1, wave + 1);
                float speed = Random.Range(0.1f, 1f) + wave/10;
                shipManagerScript.spawnShip(health, 1, speed);
                shipsSpawned++;
                timeSinceLastSpawn = 0;
                spawnInterval = Random.Range(0.1f, 1) - (shipsSpawned % 50) / 10;
            }
            else
            {
                timeSinceLastSpawn += Time.deltaTime;
            }
        }
        else
        {
            allShipsSpawned = true;
        }

        if (newWavePossible)
        {
            newWave();
        }
    }

    public static void spendCoins(int numCoinsSpent)
    {
        numCoins -= numCoinsSpent;
        totalCoinsSpent += numCoinsSpent;
    }
}
