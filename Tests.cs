using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Tests
    {
        public static void TreeFindNodesByValue()
        {
            SimpleTreeNode<int> node9 = new SimpleTreeNode<int>(9,null);
            SimpleTreeNode<int> node4 = new SimpleTreeNode<int>(4, node9);
            SimpleTreeNode<int> node17 = new SimpleTreeNode<int>(17, node9);
            SimpleTreeNode<int> node3 = new SimpleTreeNode<int>(3, node4);
            SimpleTreeNode<int> node6 = new SimpleTreeNode<int>(6, node4);
            SimpleTreeNode<int> node5 = new SimpleTreeNode<int>(5, node6);
            SimpleTreeNode<int> node7 = new SimpleTreeNode<int>(7, node6);
            SimpleTreeNode<int> node22 = new SimpleTreeNode<int>(22, node17);
            SimpleTreeNode<int> node20 = new SimpleTreeNode<int>(20, node22);
            

            SimpleTree<int> tree = new SimpleTree<int>(node9);

            tree.AddChild(node9,node4);
            tree.AddChild(node9, node17);
            tree.AddChild(node4, node3);
            tree.AddChild(node4, node6);
            tree.AddChild(node6, node5);
            tree.AddChild(node6, node7);
            tree.AddChild(node17, node22);
            tree.AddChild(node22, node20);

            List<SimpleTreeNode<int>> list = tree.FindNodesByValue(6);
            
            Console.WriteLine(list.Count == 1 ? "PASSED" : "ERROR, TREE FIND NODE BY VALUE");
        }
        public static void TreeDeleteNode()
        {
            SimpleTreeNode<int> node9 = new SimpleTreeNode<int>(9, null);
            SimpleTreeNode<int> node4 = new SimpleTreeNode<int>(4, node9);
            SimpleTreeNode<int> node17 = new SimpleTreeNode<int>(17, node9);
            SimpleTreeNode<int> node3 = new SimpleTreeNode<int>(3, node4);
            SimpleTreeNode<int> node6 = new SimpleTreeNode<int>(6, node4);
            SimpleTreeNode<int> node5 = new SimpleTreeNode<int>(5, node6);
            SimpleTreeNode<int> node7 = new SimpleTreeNode<int>(7, node6);
            SimpleTreeNode<int> node22 = new SimpleTreeNode<int>(22, node17);
            SimpleTreeNode<int> node20 = new SimpleTreeNode<int>(20, node22);


            SimpleTree<int> tree = new SimpleTree<int>(node9);

            tree.AddChild(node9, node4);
            tree.AddChild(node9, node17);
            tree.AddChild(node4, node3);
            tree.AddChild(node4, node6);
            tree.AddChild(node6, node5);
            tree.AddChild(node6, node7);
            tree.AddChild(node17, node22);
            tree.AddChild(node22, node20);

            tree.DeleteNode(node6);
            List<SimpleTreeNode<int>> list = tree.FindNodesByValue(6);

            Console.WriteLine(list == null ? "PASSED" : "ERROR, DELETE NODE");
        }
        public static void TreeLeafCount()
        {
            SimpleTreeNode<int> node9 = new SimpleTreeNode<int>(9, null);
            SimpleTreeNode<int> node4 = new SimpleTreeNode<int>(4, node9);
            SimpleTreeNode<int> node17 = new SimpleTreeNode<int>(17, node9);
            SimpleTreeNode<int> node3 = new SimpleTreeNode<int>(3, node4);
            SimpleTreeNode<int> node6 = new SimpleTreeNode<int>(6, node4);
            SimpleTreeNode<int> node5 = new SimpleTreeNode<int>(5, node6);
            SimpleTreeNode<int> node7 = new SimpleTreeNode<int>(7, node6);
            SimpleTreeNode<int> node22 = new SimpleTreeNode<int>(22, node17);
            SimpleTreeNode<int> node20 = new SimpleTreeNode<int>(20, node22);


            SimpleTree<int> tree = new SimpleTree<int>(node9);

            tree.AddChild(node9, node4);
            tree.AddChild(node9, node17);
            tree.AddChild(node4, node3);
            tree.AddChild(node4, node6);
            tree.AddChild(node6, node5);
            tree.AddChild(node6, node7);
            tree.AddChild(node17, node22);
            tree.AddChild(node22, node20);

            //SimpleTree<int> tree2 = new SimpleTree<int>(new SimpleTreeNode<int>(0,null));
            //Console.WriteLine(tree2.LeafCount());
            Console.WriteLine(tree.LeafCount() == 4 ? "PASSED" : "ERROR, LEAF COUNT");
            //Console.WriteLine(tree.LeafCount());

        }
        public static void TreeNodesCount()
        {
            SimpleTreeNode<int> node9 = new SimpleTreeNode<int>(9, null);
            SimpleTreeNode<int> node4 = new SimpleTreeNode<int>(4, node9);
            SimpleTreeNode<int> node17 = new SimpleTreeNode<int>(17, node9);
            SimpleTreeNode<int> node3 = new SimpleTreeNode<int>(3, node4);
            SimpleTreeNode<int> node6 = new SimpleTreeNode<int>(6, node4);
            SimpleTreeNode<int> node5 = new SimpleTreeNode<int>(5, node6);
            SimpleTreeNode<int> node7 = new SimpleTreeNode<int>(7, node6);
            SimpleTreeNode<int> node22 = new SimpleTreeNode<int>(22, node17);
            SimpleTreeNode<int> node20 = new SimpleTreeNode<int>(20, node22);


            SimpleTree<int> tree = new SimpleTree<int>(node9);

            tree.AddChild(node9, node4);
            tree.AddChild(node9, node17);
            tree.AddChild(node4, node3);
            tree.AddChild(node4, node6);
            tree.AddChild(node6, node5);
            tree.AddChild(node6, node7);
            tree.AddChild(node17, node22);
            tree.AddChild(node22, node20);

            List<SimpleTreeNode<int>> list = tree.GetAllNodes();
            Console.WriteLine(list.Count == 9 ? "PASSED" : "ERROR, NODES TO LIST");
            
        }
        public static void TreeMoveNodes()
        {
            SimpleTreeNode<int> node9 = new SimpleTreeNode<int>(9, null);
            SimpleTreeNode<int> node4 = new SimpleTreeNode<int>(4, node9);
            SimpleTreeNode<int> node17 = new SimpleTreeNode<int>(17, node9);
            SimpleTreeNode<int> node3 = new SimpleTreeNode<int>(3, node4);
            SimpleTreeNode<int> node6 = new SimpleTreeNode<int>(6, node4);
            SimpleTreeNode<int> node5 = new SimpleTreeNode<int>(5, node6);
            SimpleTreeNode<int> node7 = new SimpleTreeNode<int>(7, node6);
            SimpleTreeNode<int> node22 = new SimpleTreeNode<int>(22, node17);
            SimpleTreeNode<int> node20 = new SimpleTreeNode<int>(20, node22);


            SimpleTree<int> tree = new SimpleTree<int>(node9);

            tree.AddChild(node9, node4);
            tree.AddChild(node9, node17);
            tree.AddChild(node4, node3);
            tree.AddChild(node4, node6);
            tree.AddChild(node6, node5);
            tree.AddChild(node6, node7);
            tree.AddChild(node17, node22);
            tree.AddChild(node22, node20);

            //Console.WriteLine(node6.Parent.NodeValue);
            
            tree.MoveNode(node6,node22);
            
            //Console.WriteLine(node6.Parent.NodeValue);
            

            Console.WriteLine(node6.Parent.NodeValue == 22 ? "PASSED" : "ERROR, NODES TO LIST");
        }

    }
}