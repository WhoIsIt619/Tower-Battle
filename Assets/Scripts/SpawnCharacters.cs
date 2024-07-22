using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCharacters : MonoBehaviour
{
    public GameObject Swordsman;
    public GameObject Spearman;
    public GameObject Horseman;

    public Transform spawnPoint;
    public float spawnCooldown = 5f;
    private float currentCooldown;

    public Button swordsmanButton;
    public Button spearmanButton;
    public Button horsemanButton;

    public Image spawnCooldownIndicator;

    private List<GameObject> spawnedCharacters = new List<GameObject>();

    void Start()
    {
        swordsmanButton.onClick.AddListener(() => SpawnUnit(1));
        spearmanButton.onClick.AddListener(() => SpawnUnit(2));
        horsemanButton.onClick.AddListener(() => SpawnUnit(3));
        currentCooldown = 0f;
        spawnCooldownIndicator.fillAmount = 0f;
    }

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            spawnCooldownIndicator.fillAmount = currentCooldown / spawnCooldown;
        }
    }

    private void SpawnUnit(int unitType)
    {
        if (currentCooldown <= 0)
        {
            GameObject unitToSpawn = null;

            switch (unitType)
            {
                case 1:
                    unitToSpawn = Swordsman;
                    break;
                case 2:
                    unitToSpawn = Spearman;
                    break;
                case 3:
                    unitToSpawn = Horseman;
                    break;
            }

            if (unitToSpawn != null)
            {
                Instantiate(unitToSpawn, spawnPoint.position, spawnPoint.rotation);
                currentCooldown = spawnCooldown;
                spawnCooldownIndicator.fillAmount = 0f;
            }
        }
    }
}
