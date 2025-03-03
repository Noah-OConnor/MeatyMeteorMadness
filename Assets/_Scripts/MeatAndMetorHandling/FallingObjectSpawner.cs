using UnityEngine;

// DESIGN PATTERNS - Factory Method, Object Pool
// Implements the Factory Method pattern by requesting Meat and Meteor objects from their respective factories,
// ensuring that object creation is encapsulated and follows a structured approach.
// Implements the Object Pool pattern by reusing inactive objects instead of destroying and re-instantiating them,
// optimizing performance and reducing memory overhead.

public class FallingObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meatPrototype;
    [SerializeField] private GameObject meteorPrototype;

    [SerializeField] private int numMeatInPool = 3;
    [SerializeField] private int numMeteorInPool = 6;

    private ObjectPool<MeatController> meatPool;
    private ObjectPool<MeteorController> meteorPool;

    private float meteorTimer;
    private float meatTimer;

    private void Start()
    {
        MeatFactory meatFactory = new MeatFactory(meatPrototype.GetComponent<MeatController>());
        MeteorFactory meteorFactory = new MeteorFactory(meteorPrototype.GetComponent<MeteorController>());

        meatPool = new ObjectPool<MeatController>(meatFactory, numMeatInPool);
        meteorPool = new ObjectPool<MeteorController>(meteorFactory, numMeteorInPool);

        GameManager.OnGameOver += meatPool.RemoveExtraObjects;
        GameManager.OnGameOver += meteorPool.RemoveExtraObjects;
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= meatPool.RemoveExtraObjects;
        GameManager.OnGameOver -= meteorPool.RemoveExtraObjects;
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
        MeteorController meteor = meteorPool.GetObject();
        meteor.transform.position = new Vector2(Random.Range(-GameManager.instance.GetHorizontalClamp(), GameManager.instance.GetHorizontalClamp()), 7);
        float randomModifier = (Random.Range(0, 200) * 0.01f) - 1;
        meteor.GetComponent<MeteorController>().SetMovementSpeed(GameManager.instance.GetMeteorSpeed() + randomModifier);
    }

    private void SpawnMeat()
    {
        MeatController meat = meatPool.GetObject();
        meat.transform.position = new Vector2(Random.Range(-GameManager.instance.GetHorizontalClamp(), GameManager.instance.GetHorizontalClamp()), 7);
        float randomModifier = (Random.Range(0, 150) * 0.01f) - 0.75f;
        meat.GetComponent<MeatController>().SetMovementSpeed((GameManager.instance.GetMeteorSpeed() * 0.75f) + randomModifier);
    }
}
