using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : Singleton<ObjectPool>
{
    private int _amountEgg;
    private int _amountEggType;
    private List<List<GameObject>> listObjects;
    private List<Sprite> listSprites;
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject pool;

    public override void Awake(){
        base.Awake();
        LoadDataEggs();

        _amountEgg = 25;
        _amountEggType = 14;

        listObjects = new List<List<GameObject>>();
        for(int i=0 ; i < _amountEggType ; i++){
            List<GameObject> listTMP = new List<GameObject>();
            for(int j=0 ; j<_amountEgg ; j++){
                GameObject tmp = Instantiate(_object);
                tmp.transform.parent = pool.transform;
                Image img = tmp.GetComponent<Image>();
                img.sprite = listSprites[i];
                tmp.SetActive(false);
                listTMP.Add(tmp);
            }
            listObjects.Add(listTMP);
        }
    }

    public GameObject GetFromPool(int id){
        foreach(GameObject obj in listObjects[id]){
            if(obj.activeInHierarchy == false){
                return obj;
            }
        }
        GameObject tmp = Instantiate(_object);
        tmp.SetActive(false);
        listObjects[id].Add(tmp);
        _amountEgg++;
        return tmp;
    }

    private void LoadDataEggs(){
        listSprites = new List<Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(GameConfig.Eggs_Sprite_Path);
        listSprites.AddRange(sprites);
    }
}
