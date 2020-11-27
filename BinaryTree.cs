using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode<T>
    {
        public int NodeKey; // ключ узла
        public T NodeValue; // значение в узле
        public BSTNode<T> Parent; // родитель или null для корня
        public BSTNode<T> LeftChild; // левый потомок
        public BSTNode<T> RightChild; // правый потомок	

        public BSTNode(int key, T val, BSTNode<T> parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    // промежуточный результат поиска
    public class BSTFind<T>
    {
        // null если не найден узел, и в дереве только один корень
        public BSTNode<T> Node;

        // true если узел найден
        public bool NodeHasKey;

        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;

        public BSTFind() { Node = null; }
    }

    public class BST<T>
    {
        BSTNode<T> Root; // корень дерева, или null

        public BST(BSTNode<T> node)
        {
            Root = node;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            if (Root == null) return null;

            BSTNode<T> currentNode = Root;
            BSTNode<T> parrent;

            while (!key.Equals(currentNode.NodeKey))
            {
                parrent = currentNode;

                if (key >= currentNode.NodeKey)
                {
                    currentNode = currentNode.RightChild; // двигаемся вправо
                    if (currentNode == null)
                        return new BSTFind<T> {Node = parrent, NodeHasKey = false, ToLeft = false};
                }
                else
                {
                    currentNode = currentNode.LeftChild;
                    if (currentNode == null)
                    {
                        return new BSTFind<T> {Node = parrent, NodeHasKey = false, ToLeft = true};
                    }
                }
            }

            return new BSTFind<T> { Node = currentNode, NodeHasKey = true }; // найденный узел
        }

        public bool AddKeyValue(int key, T val)
        {
            // добавляем ключ-значение в дерево, если корень не null
            if (Root != null)
            {
                BSTFind<T> foundNode = FindNodeByKey(key);

                if (!foundNode.NodeHasKey)
                {
                    if (foundNode.ToLeft)
                        foundNode.Node.LeftChild = new BSTNode<T>(key, val, foundNode.Node);
                    else
                        foundNode.Node.RightChild = new BSTNode<T>(key, val, foundNode.Node);

                    return true;
                }
            }
            else
            {
                Root = new BSTNode<T>(key, val, null);
            }

            return false;
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            // ищем максимальное/минимальное в поддереве
            BSTNode<T> current = FromNode;

            if (FindMax)
            {
                if (current.RightChild != null)
                    current = FinMinMax(current.RightChild, FindMax);
            }
            else
            {
                if (current.LeftChild != null)
                    current = FinMinMax(current.LeftChild, FindMax);
            }

            return current;
        }

        public bool DeleteNodeByKey(int key)
        {
            BSTFind<T> foundNode = FindNodeByKey(key);
            if (!foundNode.NodeHasKey) return false;
            if (foundNode.Node.LeftChild == null && foundNode.Node.RightChild == null)
            {
                if (foundNode.Node.Parent.LeftChild != null && foundNode.Node.Parent.LeftChild.Equals(foundNode.Node))
                    foundNode.Node.Parent.LeftChild = null;
                else if (foundNode.Node.Parent.RightChild != null && foundNode.Node.Parent.RightChild.Equals(foundNode.Node))
                    foundNode.Node.Parent.RightChild = null;
            }
            else if (foundNode.Node.LeftChild == null ^ foundNode.Node.RightChild == null)
            {
                if (foundNode.Node.LeftChild != null)
                {
                    if (foundNode.Node.Parent.LeftChild != null && foundNode.Node.Parent.LeftChild.Equals(foundNode.Node))
                        foundNode.Node.Parent.LeftChild = foundNode.Node.LeftChild;
                    else
                        foundNode.Node.Parent.RightChild = foundNode.Node.LeftChild;

                    foundNode.Node.LeftChild.Parent = foundNode.Node.Parent;
                }
                else
                {
                    if (foundNode.Node.Parent.LeftChild != null && foundNode.Node.Parent.LeftChild.Equals(foundNode.Node))
                        foundNode.Node.Parent.LeftChild = foundNode.Node.RightChild;
                    else
                        foundNode.Node.Parent.RightChild = foundNode.Node.RightChild;

                    foundNode.Node.RightChild.Parent = foundNode.Node.Parent;
                }
            }
            else
            {
                BSTNode<T> successorNode = FinMinMax(foundNode.Node.RightChild, false);

                if (successorNode.RightChild != null)
                {
                    successorNode.Parent.LeftChild = successorNode.RightChild;
                    successorNode.RightChild.Parent = successorNode.Parent;
                }
                else
                {
                    if (successorNode.Parent.LeftChild == successorNode)
                        successorNode.Parent.LeftChild = null;
                    else
                        successorNode.Parent.RightChild = null;
                }
                if (foundNode.Node.Parent.RightChild == foundNode.Node)
                    foundNode.Node.Parent.RightChild = successorNode;
                else
                    foundNode.Node.Parent.LeftChild = successorNode;

                successorNode.Parent = foundNode.Node.Parent;

                successorNode.LeftChild = foundNode.Node.LeftChild;
                successorNode.RightChild = foundNode.Node.RightChild;
                    
                if (foundNode.Node.RightChild != null)
                    foundNode.Node.RightChild.Parent = successorNode;
                if (foundNode.Node.LeftChild != null)
                    foundNode.Node.LeftChild.Parent = successorNode;
            }

            return true;

        }

        public int Count()
        {
            return Recursive(Root); // количество узлов в дереве
        }
        private int Recursive(BSTNode<T> node, int count = 0)
        {
            if (node == null) return count;
            count++;
            count = Recursive(node.LeftChild, count);
            count = Recursive(node.RightChild, count);

            return count;
        }
    }

}