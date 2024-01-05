using UnityEngine;

public class BulletMoving : MovingGameObj
{
    public EnemyData enemy;

    // Start is called before the first frame update
    private void Start()
    {
        rotate = 180;
        //enemy = Game_DataBase.Instance.GetEnemyData(EnemyCharacters.Instance.ID);
        target = enemy.Base;
        Moving();
        speed = 25;
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyData"))
        {
            Destroy(gameObject);
        }
    }
}