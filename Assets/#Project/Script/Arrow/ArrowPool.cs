using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    private Stack<ArrowPoolMember> pool = new();
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
            newOne.GetComponent<ArrowPoolMember>().pool = this;
            Kill(newOne.GetComponent<ArrowPoolMember>());
        }
    }

    public ArrowPoolMember Spawn(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            Create(batch);
        }
        ArrowPoolMember member = pool.Pop();
        Revive(member,position, rotation);
        return member;
    }

    public void Kill(ArrowPoolMember member)
    {
        member.gameObject.SetActive(false);
        member.gameObject.GetComponent<ArrowMove>().target = null;
        pool.Push(member);
    }

    public void Revive(ArrowPoolMember member, Vector3 position, Quaternion rotation){
        member.gameObject.SetActive(true);
        member.transform.position = position;
        member.transform.rotation = rotation;
    }
}
