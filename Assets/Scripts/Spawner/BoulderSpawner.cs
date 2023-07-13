using System.Collections;
using System.Collections.Generic;
using cpeak.cPool;
using DG.Tweening;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public static BoulderSpawner Instance { get; private set; }
    [SerializeField] private cPool pool;
    [SerializeField] private BoulderI[] boulders;
    [SerializeField] private Transform[] boulderSpawnTransforms;
    [SerializeField] private int unlockAfterCrackedValue = 3;
    [SerializeField] private float minSpawnBoulderIteration = 5f;
    [SerializeField] private float maxSpawnBoulderIteration = 15f;
    [SerializeField] private float boulderMoveInGameDuration = 2f;
    [SerializeField] private float boulderMoveInGameAmount = 2f;
    private float boulderSpawnTime;
    private float lastBoulderSpawnTime;
    private bool boulderSpawnOnProgress = true;
    private bool spawnBoulderImmadiate = false;
    private WaitForSeconds boulderMoveInGameCoroutineDuration;
    private List<Boulder> spawnableBoulders = new List<Boulder>();
    private List<Boulder> activeBouldersInTheScene = new List<Boulder>();

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }    
        else
        {
            Destroy(this);
        }
    }

    private void Start() 
    {
        Boulder.OnBoulderCrack += HandleOnBoulderCrack;

        if(!spawnableBoulders.Contains(boulders[0].boulderPrefab))
        {
            spawnableBoulders.Add(boulders[0].boulderPrefab);
        }

        boulderMoveInGameCoroutineDuration = new WaitForSeconds(boulderMoveInGameDuration);
    }

    private void Update() 
    {
        if (!boulderSpawnOnProgress)
        {
            boulderSpawnOnProgress = true;
            boulderSpawnTime = UnityEngine.Random.Range(minSpawnBoulderIteration, maxSpawnBoulderIteration);
        }
        if(Time.time > lastBoulderSpawnTime + boulderSpawnTime || spawnBoulderImmadiate)
        {
            spawnBoulderImmadiate = false;

            SpawnBoulder();

            boulderSpawnOnProgress = false;

            lastBoulderSpawnTime = Time.time;
        }    
    }

    /// <summary>
    /// Spawn Boulders after splitting one.
    /// </summary>
    public void SpawnBoulder(Boulder boulder, Vector3 position, Vector3 velocity)
    {
        for (int i = 0; i < boulders.Length; i++)
        {
            if (boulders[i].index != boulder.GetBoulderIndex) { continue; }

            Vector3 spawnPoint = position;

            GameObject boulderInstance = pool.GetPoolObject(
                "boulder_" + boulder.GetBoulderIndex,
                spawnPoint,
                Quaternion.Euler(0f, 180f, 0f));

            if(boulderInstance.TryGetComponent(out Boulder boulderC))
            {
                boulderC.GetRigidbody.velocity = velocity;
                
                boulderC.SetNoneSplittable();
            }
        }
    }
    
    private void SpawnBoulder()
    {
        int spawnableBoulderCount = spawnableBoulders.Count;
        
        int randomBoulderIndex = UnityEngine.Random.Range(0, spawnableBoulderCount);

        int randomBoulderSpawnPoint = UnityEngine.Random.Range(0, boulderSpawnTransforms.Length);

        GameObject boulderInstance = pool.GetPoolObject(
            "boulder_" + randomBoulderIndex,
            boulderSpawnTransforms[randomBoulderSpawnPoint].position,
            Quaternion.Euler(0f, 180f, 0f));

        StartCoroutine(BoulderSpawnRoutine(boulderInstance));
    }

    IEnumerator BoulderSpawnRoutine(GameObject boulder)
    {
        if (boulder.TryGetComponent(out Boulder boulderC)) 
        {
            activeBouldersInTheScene.Add(boulderC);

            boulderC.GetRigidbody.isKinematic = true;

            float boulderHorizontalMoveAmount = (boulder.transform.position.x > 0)
            ? boulderMoveInGameAmount
            : -boulderMoveInGameAmount;

            boulder.transform.DOMoveX(boulderHorizontalMoveAmount, boulderMoveInGameDuration);

            yield return boulderMoveInGameCoroutineDuration;

            boulderC.GetRigidbody.isKinematic = false;
        }
        
    }

    private void HandleOnBoulderCrack(Boulder boulder)
    {
        Debug.Log("Boulder cracked " + boulder.name);

        activeBouldersInTheScene.Remove(boulder);

        // If no boudler is active in the scene. Create a new one.
        if(activeBouldersInTheScene.Count == 0)
        {
            spawnBoulderImmadiate = true;
        }

        for (int i = 0; i < boulders.Length; i++)
        {
            if (boulders[i].index != boulder.GetBoulderIndex) { continue; }
            
            boulders[i].crackedCount++;

            if(boulders[i].crackedCount >= unlockAfterCrackedValue)
            {
                UnlockBoulder(boulder);
            }

            break;
        }
    }

    private void UnlockBoulder(Boulder boulder)
    {
        int boulderIndex = 0;

        for (int i = 0; i < boulders.Length; i++)
        {
            if (boulders[i].index != boulder.GetBoulderIndex) { continue; }

            boulderIndex = i + 1;
            break;
        }

        if (boulderIndex >= boulders.Length) { return; }

        if (spawnableBoulders.Contains(boulders[boulderIndex].boulderPrefab)) { return; }
        
        boulders[boulderIndex].isUnlocked = true;
        
        spawnableBoulders.Add(boulders[boulderIndex].boulderPrefab);
    }
}

[System.Serializable]
public class BoulderI
{
    public Boulder boulderPrefab;
    public int index;
    public int crackedCount;
    public bool isUnlocked = false;
}
