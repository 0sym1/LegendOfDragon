using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Sprite spriteBlock1;
    [SerializeField] private Sprite spriteBlock2;
    [SerializeField] private GameObject listBlockObj;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject groupEggs; // gr trung gian lưu egg để layer của egg đè lên các layer khác khi move
 
    private BlockController[,] matrixBlocks; 

    public override void Awake(){
        base.Awake();

        matrixBlocks = new BlockController[6, 6];
    }
    void Start()
    {
        Graph.Instance.SetMatrixBlocks(matrixBlocks); //truyền matrix cho graph
        initBlock();
        initEggs();
    }

    public GameObject GetGroupEggs(){
        return groupEggs;
    }

    private void initBlock(){
        for(int i=1 ; i<=5 ; i++){
            for(int j=1; j<=5 ; j++){
                GameObject block = Instantiate(blockPrefab);
                block.transform.parent = listBlockObj.transform;
                block.transform.localScale = new Vector3(1, 1,1); //chỉnh lại scale cho block, vì k hiểu sao khi set làm con của obj thì bị thay đổi scale

                BlockController blockController = block.GetComponent<BlockController>();
                blockController.setXY(i, j);
                if((i+j)%2==0) blockController.setSpriteGround(spriteBlock1);
                else blockController.setSpriteGround(spriteBlock2);

                matrixBlocks[i,j] = blockController;
            }
        }
    }

    private void initEggs(){
        for(int i=1; i<=5 ; i++){
            for(int j=1 ; j<=5 ; j++){
                BlockController blockController = matrixBlocks[i, j].GetComponent<BlockController>();
                GameObject egg = ObjectPool.Instance.GetFromPool(Random.Range(0, 3));

                blockController.setSpriteEgg(egg.GetComponent<Image>().sprite);
            }
        }
    }
}
