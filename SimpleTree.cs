using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue; // значение в узле
        public SimpleTreeNode<T> Parent; // родитель или null для корня
        public List<SimpleTreeNode<T>> Children; // список дочерних узлов или null

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root; // корень, может быть null
        private int count;

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
            count = root == null ? 0 : 1;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            // ваш код добавления нового дочернего узла существующему ParentNode
            if (ParentNode == null)
            {
                Root = NewChild;
            }
            else
            {
                if (ParentNode.Children == null) ParentNode.Children = new List<SimpleTreeNode<T>>();

                ParentNode.Children.Add(NewChild);
            }
            
            NewChild.Parent = ParentNode;

            count++;
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            // ваш код удаления существующего узла NodeToDelete
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            // ваш код выдачи всех узлов дерева в определённом порядке
            SimpleTreeNode<T> currentNode = Root;
            Stack<SimpleTreeNode<T>> stack = new Stack<SimpleTreeNode<T>>();
            List<SimpleTreeNode<T>> nodes = new List<SimpleTreeNode<T>>();

            stack.Push(currentNode);

            while (currentNode != null && stack.Count != 0)
            {
                if (currentNode.Children != null)
                {
                    foreach (SimpleTreeNode<T> child in currentNode.Children)
                    {
                        stack.Push(child);
                    }
                }
                nodes.Add(currentNode);
                currentNode = stack.Pop();
            }

            return nodes.Count == 0 ? null : nodes;
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            // ваш код поиска узлов по значению
            return null;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            // ваш код перемещения узла вместе с его поддеревом -- 
            // в качестве дочернего для узла NewParent
        }

        public int Count()
        {
            // количество всех узлов в дереве
            return count;
        }

        public int LeafCount()
        {
            // количество листьев в дереве
            return 0;
        }
    }
}