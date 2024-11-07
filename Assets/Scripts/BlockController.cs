using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlockController : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject egg;
    [SerializeField] private Image imgGround;
    [SerializeField] private Image imgEgg;
    [SerializeField] private float lengthUp;
    [SerializeField] private float speedUp;
    [SerializeField] private float distanceEggMoveX;
    [SerializeField] private float distanceEggMoveY;
    [SerializeField] private float speedEggMove;


    private Vector3 positonEgg;
    // private RectTransform rectTransform;
    private int point;
    private string status;
    private int x, y;

    private void Start(){
        // rectTransform = egg.GetComponent<RectTransform>();
        StartCoroutine(SetPositionEgg());
        status = GameConfig.normal;
        ResetStatus();
    }

    void OnEnable(){
        Messenger.AddListener(EventKey.SELECT, ResetStatus);
        Messenger.AddListener<BlockController>(EventKey.EAT_EGG, ClearBlock);
        Messenger.AddListener(EventKey.FILL_EGG, FallBlock);
    }

    void OnDisable() {
        Messenger.RemoveListener(EventKey.SELECT, ResetStatus);
        Messenger.RemoveListener<BlockController>(EventKey.EAT_EGG, ClearBlock);
        Messenger.RemoveListener(EventKey.FILL_EGG, FallBlock);
    }

    public void SetPoint(int point){
        this.point = point;
    }
    public int GetPoint(){
        return point;
    }

    public void setXY(int x, int y){
        this.x = x;
        this.y = y;
    }
    public int getX(){
        return x;
    }
    public int getY(){
        return y;
    }
    public void setSpriteGround(Sprite sprite){
        imgGround.sprite = sprite;
    }
    public void setSpriteEgg(Sprite sprite){
        imgEgg.sprite = sprite;
    }
    public Sprite GetSpriteEgg(){
        return imgEgg.sprite;
    }
    public string GetNameEgg(){
        return egg.GetComponent<Image>().sprite.name;
        // return imgEgg.sprite.name;
    }
    public void SetStatus(string status){
        this.status = status;
    }
    public string GetStatus(){
        return status;
    }
    public void SetEgg(GameObject egg){
        this.egg = egg;
    }
    public GameObject GetEgg(){
        return egg;
    }
    public Vector3 GetPositionEgg(){
        return positonEgg;
    }

    public void OnClickBlock(){
        if(status.Equals(GameConfig.normal)){
            status = GameConfig.selected;
            Messenger.Broadcast(EventKey.SELECT);
            Graph.Instance.FindTPLT(x, y, 0);
        }
        else{
            Graph.Instance.FindTPLT(x, y, 1);
            Messenger.Broadcast(EventKey.EAT_EGG, this);
        }
    }

    public void MoveUp(){
        Vector3 postitionCurrent = transform.position;
        postitionCurrent = new Vector3(postitionCurrent.x, postitionCurrent.y + lengthUp, postitionCurrent.z);
        gameObject.transform.DOMove(postitionCurrent, speedUp);
        status = GameConfig.selected;
    }
    public void MoveDown(){
        if(status.Equals(GameConfig.selected)){
            Vector3 postitionCurrent = transform.position;
            postitionCurrent = new Vector3(postitionCurrent.x, postitionCurrent.y - lengthUp, postitionCurrent.z);
            gameObject.transform.DOMove(postitionCurrent, speedUp);            
        }
    }

    private void ClearBlock(BlockController block){
        Debug.Log("|| " + x + " " + y + " " + egg.transform.position);
        if(status.Equals(GameConfig.selected) && point > 0){
            BlockController blockUp = Graph.Instance.GetBlockUp(this);
            BlockController blockDown = Graph.Instance.GetBlockDown(this);
            BlockController blockRight = Graph.Instance.GetBlockRight(this);
            BlockController blockLeft = Graph.Instance.GetBlockLeft(this);

            Vector3 destination = positonEgg;

            if(blockUp != null && blockUp.GetPoint() == point - 1){
                destination = blockUp.GetPositionEgg();
            }
            else if(blockDown != null && blockDown.GetPoint() == point - 1){
                destination = blockDown.GetPositionEgg();
            }
            else if(blockRight != null && blockRight.GetPoint() == point - 1){
                destination = blockRight.GetPositionEgg();
            }
            else if(blockLeft != null && blockLeft.GetPoint() == point - 1){
                destination = blockLeft.GetPositionEgg();
            }

            StartCoroutine(EggMovement(destination, point));
        }
    }
    public void FallBlock(){
        //block hang dau
        if(x == 1 && status.Equals(GameConfig.empty)){
            egg = SpawnEggs.Instance.GetNewEgg(1, 3, transform.position, distanceEggMoveY, speedEggMove);
            Debug.Log(x + " " + y + " " + egg.transform.position);
            ResetStatus();
            SetParentEgg(this);
        }
        if(!status.Equals(GameConfig.empty)){
            BlockController blockDown = Graph.Instance.GetBlockDown(this);
            if(blockDown != null && blockDown.GetStatus().Equals(GameConfig.empty)){

                //set status cho block
                status = GameConfig.empty;
                blockDown.SetStatus(GameConfig.normal);

                Vector3 destination = blockDown.GetPositionEgg();
                blockDown.SetEgg(egg);
                SetParentEgg(blockDown);

                egg.transform.DOMove(destination, speedEggMove);
                
                Messenger.Broadcast(EventKey.FILL_EGG);
            }
        }
        ResetStatus();
    }

    private IEnumerator EggMovement(Vector3 destination, int delayPoint){
        yield return new WaitForSeconds((Graph.Instance.GetMaxPoint() - delayPoint)*speedEggMove);
        if(point > 1){
            ReturnEggToPool();
            egg.transform.DOMove(destination, speedEggMove); //sau khi trứng di chuyển thì cho nó về lại vị trí ban đầu

            yield return new WaitForSeconds(speedEggMove); //delay để sau khi trứng di chuyển sẽ xóa trứng

            egg.SetActive(false);
            ResetStatus();
            status = GameConfig.empty;

            yield return new WaitForSeconds(Graph.Instance.GetMaxPoint() * speedEggMove); //delay để khi nào tất cả trứng di chuyển xong mới cho fall
            Messenger.Broadcast(EventKey.FILL_EGG);
        }
        else ResetStatus();
    }

    public void ResetStatus(){
        MoveDown();
        status = GameConfig.normal;
        point = -1;
    }

    private void SetParentEgg(BlockController block){
        egg.transform.parent = block.transform; //tra lai cho trung con cua block
        // rectTransform.anchoredPosition = positonEgg;
    }
    private void ReturnEggToPool(){
        egg.transform.parent = GameController.Instance.GetGroupEggs().transform;
    }
    private IEnumerator SetPositionEgg(){
        yield return new WaitForSeconds(0.2f);
        positonEgg = egg.transform.position;
    }
}