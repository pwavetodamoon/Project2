using UnityEngine;



public class SpawnMob : MonoBehaviour
{
    public Enemy enemyData;
    public GameObject enemypf;
    private void Start()
    {
        enemyData = Game_DataBase.Instance.GetEnemyData(EnemyScript.Instance.ID);
        enemypf = enemyData.prefab;
    }

    void Update()
    {
        Debug.Log("Spawn");
        InvokeRepeating(nameof(Spawn), 0.5f, 1f);
    }
    void Spawn()
    {
        var posX = Random.Range(-15, 15);
        var oneBlock = Instantiate(enemyData.prefab, new Vector3(posX, 5, 0),Quaternion.identity);
        Destroy(oneBlock, 2f);
    }

}


