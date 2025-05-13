using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int playerId;
    private int damage;
    private float _speed;
    
    private Vector3 _direction;
    
    public void Initialize(float speed, Vector3 dir, int playerId, int damage)
    {
        _speed = speed;
        _direction = dir;
        this.playerId = playerId;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player" + (3 - playerId)))
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
