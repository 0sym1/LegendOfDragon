using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : Singleton<Graph>
{
    private BlockController[,] matrixBlocks; 
    private int maxPoint;
    private int amount;

    public int GetAmount(){
        return amount;
    }
    public int GetMaxPoint(){
        return maxPoint;
    }
    public void SetMatrixBlocks(BlockController[,] matrixBlocks){
        this.matrixBlocks = matrixBlocks;
    }

    //point=0 thì chỉ ktra các ptu của đồ thị, point=1 thì gắn điểm cho các thành phần đó
    public void FindTPLT(int x, int y, int point){
        amount = 0;
        string nameEgg = matrixBlocks[x, y].GetNameEgg();
        matrixBlocks[x, y].SetPoint(point);

        Queue<BlockController> queue = new Queue<BlockController>();
        queue.Enqueue(matrixBlocks[x,y]);

        maxPoint = 0;

        while(queue.Count > 0){
            amount++;
            BlockController block = queue.Dequeue();

            maxPoint = Math.Max(maxPoint, block.GetPoint());

            //nếu point = 0 nghĩa là khối đang đc chọn, nên sẽ MoveUp(nổi lên) lên
            if(point == 0) block.MoveUp();

            //duyệt 4 cạnh kề, check xem có cùng tên trứng với block gốc không
            BlockController blockUp = GetBlockUp(block);
            BlockController blockDown = GetBlockDown(block);
            BlockController blockRight = GetBlockRight(block);
            BlockController blockLeft = GetBlockLeft(block);

            // Debug.Log(block.getX() + " " + block.getY() + " " + block.GetPoint());

            //trên
            if (blockUp != null && blockUp.GetNameEgg() == nameEgg && blockUp.GetPoint() == (point - 1)) //block = point-1 vì khi point=0 thì block=-1 là chưa đc xét, khi point=1 thì block=0 là chưa đc xét
            {
                blockUp.SetPoint(block.GetPoint() + point);
                queue.Enqueue(blockUp);
            }

            // Dưới
            if (blockDown != null && blockDown.GetNameEgg() == nameEgg && blockDown.GetPoint() == (point - 1))
            {
                blockDown.SetPoint(block.GetPoint() + point);
                queue.Enqueue(blockDown);
            }

            // Phải
            if (blockRight != null && blockRight.GetNameEgg() == nameEgg && blockRight.GetPoint() == (point - 1))
            {
                blockRight.SetPoint(block.GetPoint() + point);
                queue.Enqueue(blockRight);
            }

            // Trái
            if (blockLeft != null && blockLeft.GetNameEgg() == nameEgg && blockLeft.GetPoint() == (point - 1))
            {
                blockLeft.SetPoint(block.GetPoint() + point);
                queue.Enqueue(blockLeft);
            }
            
        }
    }

    public BlockController GetBlockUp(BlockController block){
        if(block.getX() - 1 >= 1) {return matrixBlocks[block.getX() - 1,block.getY()];}
        else return null;
    }
    public BlockController GetBlockDown(BlockController block){
        if(block.getX() + 1 <= 5) {return matrixBlocks[block.getX() + 1,block.getY()];}
        else return null;
    }
    public BlockController GetBlockRight(BlockController block){
        if(block.getY() + 1 <= 5) {return matrixBlocks[block.getX(),block.getY() + 1];}
        else return null;
    }
    public BlockController GetBlockLeft(BlockController block){
        if(block.getY() - 1 >= 1) {return matrixBlocks[block.getX(),block.getY() - 1];}
        else return null;
    }
}
