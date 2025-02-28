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

    private ObjectPool<_MeatController> meatPool;
    private ObjectPool<_MeteorController> meteorPool;

    private float meteorTimer;
    private float meatTimer;

    private void Start()
    {
        meatPool = new ObjectPool<_MeatController>(meatPrototype.GetComponent<_MeatController>(), numMeatInPool);
        meteorPool = new ObjectPool<_MeteorController>(meteorPrototype.GetComponent<_MeteorController>(), numMeteorInPool);
    }

    private void Update()
    {
        if (_GameManager.instance.lose) return;

        meteorTimer += Time.deltaTime;
        meatTimer += Time.deltaTime;

        if (meteorTimer > _GameManager.instance.GetMeteorSpawnInterval())
        {
            SpawnMeteor();
            meteorTimer = 0;
        }

        if (meatTimer > _GameManager.instance.GetMeteorSpawnInterval() * _GameManager.instance.GetMeatSpawnRateMultiplier())
        {
            SpawnMeat();
            meatTimer = 0;
        }
    }

    private void SpawnMeteor()
    {
        _MeteorController meteor = meteorPool.GetObject();
        meteor.transform.position = new Vector2(Random.Range(-_GameManager.instance.GetHorizontalClamp(), _GameManager.instance.GetHorizontalClamp()), 7);
        float randomModifier = (Random.Range(0, 200) * 0.01f) - 1;
        meteor.GetComponent<_MeteorController>().SetMovementSpeed(_GameManager.instance.GetMeteorSpeed() + randomModifier);
    }

    private void SpawnMeat()
    {
        _MeatController meat = meatPool.GetObject();
        meat.transform.position = new Vector2(Random.Range(-_GameManager.instance.GetHorizontalClamp(), _GameManager.instance.GetHorizontalClamp()), 7);
        float randomModifier = (Random.Range(0, 150) * 0.01f) - 0.75f;
        meat.GetComponent<_MeatController>().SetMovementSpeed((_GameManager.instance.GetMeteorSpeed() * 0.75f) + randomModifier);
    }
}
