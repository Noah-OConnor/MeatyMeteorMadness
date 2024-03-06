using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    private float meteorTimer;
    [SerializeField] private GameObject meatPrefab;
    private float meatTimer;

    private void Update()
    {
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
        float randomModifier = (Random.Range(0, 200) * 0.01f) - 1;
        meteor.GetComponent<MeteorController>().SetMovementSpeed(GameManager.instance.GetMeteorSpeed() + randomModifier);
    }

    private void SpawnMeat()
    {
        GameObject meat;
        float horizontalPosition;
        horizontalPosition = (0.01f * Random.Range(0, 100)) * (GameManager.instance.GetHorizontalClamp() * 2) - GameManager.instance.GetHorizontalClamp();
        meat = Instantiate(meatPrefab, new Vector2(horizontalPosition, 7), Quaternion.identity);
        float randomModifier = (Random.Range(0, 150) * 0.01f) - 0.75f;
        meat.GetComponent<MeatController>().SetMovementSpeed((GameManager.instance.GetMeteorSpeed() * 0.75f) + randomModifier);
    }
}
