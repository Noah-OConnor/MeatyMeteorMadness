using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class _MeatyMeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    private float meteorTimer;
    [SerializeField] private GameObject meatPrefab;
    private float meatTimer;

    private List<GameObject> availableMeteors = new List<GameObject>();

    public GameObject CreateMeteor()
    {
        if (availableMeteors.Any())
        {
            GameObject newMeteor = availableMeteors.First();
            availableMeteors.RemoveAt(0);
            //newMeteor.Reset();
            return newMeteor;
        }
        else
        {
            //meteorPrototype.Clone();
            return null;
        }
    }

    private void Update()
    {
        if (GameManager.instance.lose) return;
        meteorTimer += Time.deltaTime;
        meatTimer += Time.deltaTime;

        if (meteorTimer > GameManager.instance.GetMeteorSpawnInterval())
        {
            SpawnMeteor();
            meteorTimer = 0;
        }

        if (meatTimer > GameManager.instance.GetMeteorSpawnInterval() * GameManager.instance.GetMeatSpawnRateMultiplier())
        {
            SpawnMeat();
            meatTimer = 0;
        }
    }

    private void SpawnMeteor()
    {
        GameObject meteor;
        float horizontalPosition;
        horizontalPosition = (0.01f * Random.Range(0, 100)) * (GameManager.instance.GetHorizontalClamp() * 2) - GameManager.instance.GetHorizontalClamp();
        meteor = Instantiate(meteorPrefab, new Vector2(horizontalPosition, 7), Quaternion.identity);
        GameManager.instance.objectsToDelete.Add(meteor);
        float randomModifier = (Random.Range(0, 200) * 0.01f) - 1;
        meteor.GetComponent<MeteorController>().SetMovementSpeed(GameManager.instance.GetMeteorSpeed() + randomModifier);
    }

    private void SpawnMeat()
    {
        GameObject meat;
        float horizontalPosition;
        horizontalPosition = (0.01f * Random.Range(0, 100)) * (GameManager.instance.GetHorizontalClamp() * 2) - GameManager.instance.GetHorizontalClamp();
        meat = Instantiate(meatPrefab, new Vector2(horizontalPosition, 7), Quaternion.identity);
        GameManager.instance.objectsToDelete.Add(meat);
        float randomModifier = (Random.Range(0, 150) * 0.01f) - 0.75f;
        meat.GetComponent<MeatController>().SetMovementSpeed((GameManager.instance.GetMeteorSpeed() * 0.75f) + randomModifier);
    }
}
