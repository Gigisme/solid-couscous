using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleController : MonoBehaviour
{
    public int damage = 5;
    public float cooldown = 0.5f;
    public int durability = 5; //How many times the puddle can damage an enemy before dissipating

    private Collider _collider;
    private float lastAttackTime=-9999;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CanCauseDamage()
    {
        return Time.time - lastAttackTime >= cooldown;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!CanCauseDamage()) return;

        MonsterController monster;
        var isMonster = other.gameObject.TryGetComponent<MonsterController>(out monster);
        if (isMonster)
        {
            lastAttackTime = Time.time;
            durability--;
            monster.OnHit(damage, Element.Fire);
        }

        if (durability <= 0) Destroy(gameObject);
    }
}
