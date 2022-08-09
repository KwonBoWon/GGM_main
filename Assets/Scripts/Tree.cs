using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // ���� Ʈ�� ��� Ŭ����
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

    // ���� Ʈ�� Ŭ����
    public class BinaryTree<T>
    {
        public TreeNode<T> Root { get; set; }

        // Ʈ�� ������ ��� ��
        public void PreOrderTraversal(TreeNode<T> node)
        {
            if (node == null) return;
            Debug.Log(node.Data);
            PreOrderTraversal(node.L);
            PreOrderTraversal(node.R);
        }
    }

    // �׽�Ʈ ����





            void Start()
        {
            BinaryTree<int> stageTree = new BinaryTree<int>();
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
            //������ L
            stageTree.Root.L.L.L.L = new TreeNode<int>(1);
            stageTree.Root.L.L.R.L = new TreeNode<int>(2);
            stageTree.Root.L.R.L.L = new TreeNode<int>(2);
            stageTree.Root.L.R.R.L = new TreeNode<int>(3);

            stageTree.Root.R.L.L.L = new TreeNode<int>(2);
            stageTree.Root.R.L.R.L = new TreeNode<int>(1);
            stageTree.Root.R.R.L.L = new TreeNode<int>(4);//���纸��(4)
            stageTree.Root.R.R.R.L = new TreeNode<int>(3);

            /*
                     1
                 2        3
              3   4    2    4
            4 4 3 3 4 4 2 2
            1 2 2 3 2 1 4 3 //����
            */

            //stageTree.PreOrderTraversal(stageTree.Root);
        }

}
