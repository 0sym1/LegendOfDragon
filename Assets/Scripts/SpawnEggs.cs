using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnEggs : Singleton<SpawnEggs>
{
    public GameObject GetNewEgg(int st, int end, Vector3 position, float distanceEggMoveY, float speedEggMove){
        int idEgg = Random.Range(st, end+1);
        GameObject egg = ObjectPool.Instance.GetFromPool(idEgg);
        egg.transform.localScale = new Vector3(1,1,1);
        egg.SetActive(true);
        egg.transform.position = new Vector3(position.x, position.y + distanceEggMoveY, position.y);

        egg.transform.DOMoveY(position.y, speedEggMove);
        return egg;
    }

    public void MoveDownNewEgg(GameObject egg){
        // egg.transform.DOMove()
    }
}
