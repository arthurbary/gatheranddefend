using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMinionPool : MonoBehaviour
{
    private Stack<RegularMinionPoolMember> pool = new();
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
            newOne.GetComponent<RegularMinionPoolMember>().pool = this;
            Kill(newOne.GetComponent<RegularMinionPoolMember>());
        }
    }

    public RegularMinionPoolMember Spawn(Vector3 position, Quaternion rotation, bool isEnemy)
    {
        if (pool.Count == 0)
        {
            Create(batch);
        }
        RegularMinionPoolMember member = pool.Pop();
        member.isEnemy = isEnemy;
        Revive(member,position, rotation);
        return member;
    }

    public void Kill(RegularMinionPoolMember member)
    {
        member.gameObject.SetActive(false);
        member.hasBeenInitialized = false;
        pool.Push(member);
    }

    public void Revive(RegularMinionPoolMember member, Vector3 position, Quaternion rotation){
        member.gameObject.SetActive(true);
        member.transform.position = position;
        member.transform.rotation = rotation;

    }
}
