using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode
    {
        public int NodeKey;         // ключ узла
        public BSTNode Parent;      // родитель или null для корня
        public BSTNode LeftChild;   // левый потомок
        public BSTNode RightChild;  // правый потомок	
        public int Level;           // глубина узла

        public BSTNode(int key, BSTNode parent)
        {
            NodeKey = key;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }
    public class BalancedBST
    {
        public BSTNode Root;

        public int[] BSTArray; // временный массив для ключей дерева

        public BalancedBST()
        {
            Root = null;
        }

        public void CreateFromArray(int[] a)
        {
            BSTArray = new int[a.Length];   
            Array.Sort(a);                  
            AddToArray(a, 0);
        }

        public void GenerateTree()
        {
            Root = AddNode(null, 0); 
        }

        public bool IsBalanced(BSTNode root_node)
        {
            BSTNode node = root_node;

            if (node != null)
            {
                int LeftLevel = node.Level;
                int RightLevel = node.Level;

                if (node.LeftChild != null)
                    LeftLevel = MaxLevel(node.LeftChild);

                if (node.RightChild != null)
                    RightLevel = MaxLevel(node.RightChild);

                if (Math.Abs(LeftLevel - RightLevel) > 1) return false;
            }

            return true;
        }

        private void AddToArray(int[] arr, int index)
        {
            int middle = arr.Length / 2;        
            BSTArray[index] = arr[middle];     

            if (arr.Length == 1) return;        

            int[] left = new int[middle];       
            int[] right = new int[middle];      

            for (int i = 0; i < middle; i++)
            {
                left[i] = arr[i];               
                right[i] = arr[middle + i + 1]; 
            }

            AddToArray(left, 2 * index + 1);    
            AddToArray(right, 2 * index + 2);   

        }

        private BSTNode AddNode(BSTNode parent, int index)
        {
            if (index >= BSTArray.Length) return null; 

            BSTNode node = new BSTNode(BSTArray[index], parent); 
            node.Parent = parent;
            if (parent == null) node.Level = 1;
            else node.Level = parent.Level + 1;

            node.LeftChild = AddNode(node, 2 * index + 1);
            if (node.LeftChild != null)
            {
                node.LeftChild.Level = node.LeftChild.Parent.Level + 1;
            }

            node.RightChild = AddNode(node, 2 * index + 2);
            if (node.RightChild != null)
            {
                node.RightChild.Level = node.RightChild.Parent.Level + 1;
            }

            return node;
        }

        private int MaxLevel(BSTNode FromNode)
        {
            BSTNode node = FromNode;
            int maxLevel = node.Level;

            if (node != null)
            {
                int leftLevel = node.Level;
                int rightLevel = node.Level;

                if (node.LeftChild != null)
                    leftLevel = MaxLevel(node.LeftChild);

                if (node.RightChild != null)
                    rightLevel = MaxLevel(node.RightChild);

                maxLevel = leftLevel > rightLevel ? leftLevel : rightLevel;
            }

            return maxLevel;
        }

    }
}