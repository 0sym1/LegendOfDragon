using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class AchievementPanel : Panel
{
    [SerializeField] private GameObject contentGroup;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject secretEgg;
    [SerializeField] private GameObject imgFit;
    private List<Sprite> listSprites;
    private void Start(){
        LoadAchievementDragon();
        LoadImgFit();
    }
    private void LoadAchievementDragon(){
        LoadSpriteEgg();

        int level = PlayerPrefs.GetInt(GameConfig.Level_Max);
        for(int i=0 ; i<level ; i++){
            GameObject ground = Instantiate(groundPrefab);
            BlockController blockController = ground.GetComponent<BlockController>();
            blockController.setSpriteEgg(listSprites[i]);
            ground.transform.parent = contentGroup.transform;
            ground.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }

        for(int i=level ; i<14 ; i++){
            GameObject ground = Instantiate(secretEgg);
            ground.transform.parent = contentGroup.transform;
            ground.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }
    }
    private void LoadSpriteEgg(){
        listSprites = new List<Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(GameConfig.Eggs_Sprite_Path);
        //sắp xếp theo số ở tên
        sprites = sprites.OrderBy(res => int.Parse(Regex.Match(res.name, @"\d+").Value)).ToArray();
        listSprites.AddRange(sprites);
    }

    private void LoadImgFit(){
        GameObject img = Instantiate(imgFit);
        img.transform.parent = contentGroup.transform;
        img.transform.localScale = new Vector3(1,1,1);
    }
}
