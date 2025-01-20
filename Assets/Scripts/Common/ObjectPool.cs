using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : Singleton<ObjectPool>
{
    private int _amountEgg;
    private List<GameObject> listEggObjects;
    private List<Image> listImagesEgg;
    private List<Sprite> listSprites;
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject pool;

    public override void Awake(){
        base.Awake();
        LoadDataEggs();

        _amountEgg = 30;

        listEggObjects = new List<GameObject>();
        listImagesEgg = new List<Image>();

        for(int i=0 ; i<_amountEgg ; i++){
            GameObject tmp = Instantiate(_object);
            Image imgTmp = tmp.GetComponent<Image>();
            tmp.transform.parent = pool.transform;
            tmp.SetActive(false);

            listImagesEgg.Add(imgTmp);
            listEggObjects.Add(tmp);
        }
    }

    public GameObject GetFromPool(int id){
        for(int i=0 ; i < _amountEgg ; i++){
            if(listEggObjects[i].activeInHierarchy == false){
                listImagesEgg[i].sprite = listSprites[id];
                return listEggObjects[i];
            }
        }

        GameObject tmp = Instantiate(_object);
        tmp.SetActive(false);
        _amountEgg++;
        return tmp;
    }

    private void LoadDataEggs(){
        listSprites = new List<Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(GameConfig.Eggs_Sprite_Path);
        //sắp xếp theo số ở tên
        sprites = sprites.OrderBy(res => int.Parse(Regex.Match(res.name, @"\d+").Value)).ToArray();
        listSprites.AddRange(sprites);

       
    }

    public List<Sprite> GetListSprite(){
        return listSprites;
    }
    public Sprite GetSpriteEgg(int id){
        return listSprites[id];
    }
}
