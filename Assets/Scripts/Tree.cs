using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree 
{


    // 이진 트리 노드 클래스
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> L { get; set; }
        public TreeNode<T> R { get; set; }

        public TreeNode(T data)
        {
            this.Data = data;
        }
    }

    // 이진 트리 클래스
    public class BinaryTree<T>
    {
        public TreeNode<T> Root { get; set; }

        // 트리 데이터 출력 예
        public void PreOrderTraversal(TreeNode<T> node)
        {
            if (node == null) return;
            Debug.Log(node.Data);
            PreOrderTraversal(node.L);
            PreOrderTraversal(node.R);
        }
    }

    public int ChooseL(TreeNode<int> node)
    {
        node = node.L;
        return node.Data;
    }
    public int ChooseR(TreeNode<int> node)
    {
        node = node.R;
        return node.Data;
    }


    public BinaryTree<int> stageTree;
    public TreeNode<int> stageNode;
    public int abc=1;




    public Tree (){

            stageTree = new BinaryTree<int>();
            stageTree.Root = new TreeNode<int>(1);
            stageTree.Root.L = new TreeNode<int>(2);
            stageTree.Root.R = new TreeNode<int>(3);

            stageTree.Root.L.L = new TreeNode<int>(3);
            stageTree.Root.R.L = new TreeNode<int>(2);
            stageTree.Root.L.R = new TreeNode<int>(4);
            stageTree.Root.R.R = new TreeNode<int>(4);

            stageTree.Root.L.L.L = new TreeNode<int>(4);
            stageTree.Root.L.L.R = new TreeNode<int>(4);
            stageTree.Root.L.R.L = new TreeNode<int>(3);
            stageTree.Root.L.R.R = new TreeNode<int>(3);

            stageTree.Root.R.L.L = new TreeNode<int>(4);
            stageTree.Root.R.L.R = new TreeNode<int>(4);
            stageTree.Root.R.R.L = new TreeNode<int>(3);
            stageTree.Root.R.R.R = new TreeNode<int>(3);
            //보스는 L
            stageTree.Root.L.L.L.L = new TreeNode<int>(1);
            stageTree.Root.L.L.R.L = new TreeNode<int>(2);
            stageTree.Root.L.R.L.L = new TreeNode<int>(2);
            stageTree.Root.L.R.R.L = new TreeNode<int>(3);

            stageTree.Root.R.L.L.L = new TreeNode<int>(2);
            stageTree.Root.R.L.R.L = new TreeNode<int>(1);
            stageTree.Root.R.R.L.L = new TreeNode<int>(4);//히든보스(4)
            stageTree.Root.R.R.R.L = new TreeNode<int>(3);
        stageNode = stageTree.Root;
        /*
                 1
             2        3
          3   4    2    4
        4 4 3 3 4 4 2 2
        1 2 2 3 2 1 4 3 //보스
        */

        //stageTree.PreOrderTraversal(stageTree.Root);

    }
}

