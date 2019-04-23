using System;
using System.Collections.Generic;


namespace AlgorithmsDataStructures2
{
    public static class Tests
    {
        public static void FindKeyIndex()
        {
            aBST tree = new aBST(3);
            tree.Tree[0] = 50;
            tree.Tree[1] = 25;
            tree.Tree[2] = 75;
            tree.Tree[4] = 37;
            tree.Tree[5] = 62;
            tree.Tree[6] = 84;
            tree.Tree[9] = 31;
            tree.Tree[10] = 43;
            tree.Tree[11] = 55;
            tree.Tree[14] = 92;

            Console.WriteLine(tree.FindKeyIndex(93));
        }
        public static void AddKey()
        {
            aBST tree = new aBST(3);
            //tree.AddKey(50);
            tree.AddKey(50);
            tree.AddKey(25);
            tree.AddKey(75);
            tree.AddKey(37);
            tree.AddKey(62);
            tree.AddKey(84);                   
            tree.AddKey(31);                   
            tree.AddKey(43);                   
            tree.AddKey(55);                   
            tree.AddKey(92);
            Console.WriteLine(tree.AddKey(24));
        }
        public static void BalancedTree()
        {
            int[] unsortedArray = { 1,2,3,4,12,13,14,15,16,17,18};
            //int[] unsortedArray = { 1, 2, 3 };

            var sortedArray = BalancedBST.GenerateBBSTArray(unsortedArray);
            foreach (var item in sortedArray)
            {
                Console.WriteLine(item);
            }
            //Console.WriteLine(BalancedBST.CheckDepth(16));
        }
        public static void CreateTree()
        {
            BST<int> tree = new BST<int>(new BSTNode<int>(8,8,null));
            var arr = BalancedBST.GenerateBBSTArray(new int[] { 1, 2, 3, 4, 12, 13, 14, 15, 16, 17 });
            Console.WriteLine(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                tree.AddKeyValue(arr[i],arr[i]);
            }

            var list = tree.DeepAllNodes(0);
            foreach (var item in list)
            {
                Console.WriteLine(item.NodeKey);
            }
        }
    }
}