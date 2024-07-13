using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerMinionPool : MonoBehaviour
{
    private Stack<FlyerMinionPoolMember> pool = new();
    [Range(1, 100)][SerializeField] private int initialBatch = 50;
    [Range(1, 100)][SerializeField] private int batch = 10;

    [SerializeField] GameObject prefab;


    public void Awake()
    {
        Create(initialBatch);
    }

    private void Create(int number)
    {
        for (int _ = 0; _ < number; _++)
        {
            GameObject newOne = Instantiate(prefab);
            newOne.GetComponent<FlyerMinionPoolMember>().pool = this;
            Kill(newOne.GetComponent<FlyerMinionPoolMember>());
        }
    }

    public FlyerMinionPoolMember Spawn(Vector3 position, Quaternion rotation, bool isEnemy)
    {
        if (pool.Count == 0)
        {
            Create(batch);
        }
        FlyerMinionPoolMember member = pool.Pop();
        member.isEnemy = isEnemy;
        Revive(member,position, rotation);
        return member;
    }

    public void Kill(FlyerMinionPoolMember member)
    {
        member.gameObject.SetActive(false);
        member.hasBeenInitialized = false;
        pool.Push(member);
    }

    public void Revive(FlyerMinionPoolMember member, Vector3 position, Quaternion rotation){
        member.gameObject.SetActive(true);
        member.transform.position = position;
        member.transform.rotation = rotation;

    }
}
