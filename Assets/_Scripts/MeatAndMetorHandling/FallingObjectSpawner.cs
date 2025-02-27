using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    [SerializeField] private _MeteorController meteorPrototype;
    [SerializeField] private _MeatController meatPrototype;

    private MeteorFactory meteorFactory;
    private MeatFactory meatFactory;

    private ObjectPool<_MeteorController> meteorPool;
    private ObjectPool<_MeatController> meatPool;

    private float meteorTimer;
    private float meatTimer;

    private void Start()
    {
        meteorFactory = new MeteorFactory(meteorPrototype);
        meatFactory = new MeatFactory(meatPrototype);

        meteorPool = new ObjectPool<_MeteorController>(meteorPrototype, 10);
        meatPool = new ObjectPool<_MeatController>(meatPrototype, 5);
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
    }

    private void SpawnMeat()
    {
        _MeatController meat = meatPool.GetObject();
        meat.transform.position = new Vector2(Random.Range(-_GameManager.instance.GetHorizontalClamp(), _GameManager.instance.GetHorizontalClamp()), 7);
    }
}
