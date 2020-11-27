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
        
        public List<BSTNode<T>> GetAllNodes(BSTNode<T> Root)
        {
            List<BSTNode<T>> Nodes = new List<BSTNode<T>>(); // all nodes
            Nodes.Add(Root);

            if (Root.LeftChild != null)
                Nodes.AddRange(GetAllNodes(Root.LeftChild));

            if (Root.RightChild != null)
                Nodes.AddRange(GetAllNodes(Root.RightChild));

            return Nodes;
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
            if (Root == null)
            {
                Root = new BSTNode<T>(key, val, null);
            }
            else
            {
                BSTFind<T> foundNode = FindNodeByKey(key);
                if (foundNode.NodeHasKey == false)
                {
                    BSTNode<T> newNode = new BSTNode<T>(key, val, foundNode.Node);
                    if (foundNode.ToLeft)
                    {
                        foundNode.Node.LeftChild = newNode;
                        newNode.Parent = foundNode.Node;
                    }
                    else
                    {
                        foundNode.Node.RightChild = newNode;
                        newNode.Parent = foundNode.Node;
                    }
                }
                else
                    return false;
            }
            return true;
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
            // удаляем узел по ключу
            BSTFind<T> foundNode = FindNodeByKey(key);
            BSTNode<T> Node = foundNode.Node;
            BSTNode<T> successorNode;


            if (foundNode.NodeHasKey)
            {
                if (Node.LeftChild != null && Node.RightChild != null)
                {
                    if (Node == Root)
                    {
                        successorNode = FinMinMax(Node.RightChild, false);
                        if (GetAllNodes(successorNode).Count == 1)
                        {
                            Node.RightChild.Parent = null;
                            Node.LeftChild.Parent = successorNode;
                            successorNode.LeftChild = Node.LeftChild;
                            Root = Node.RightChild;
                        }
                        else
                        {
                            if (successorNode != Node.RightChild)
                            {
                                successorNode.Parent.LeftChild = null;
                                successorNode.Parent = null;

                                Root.LeftChild.Parent = successorNode;
                                successorNode.LeftChild = Root.LeftChild;

                                Root.RightChild.Parent = successorNode;
                                successorNode.RightChild = Root.RightChild;
                                Root = successorNode;
                            }
                            else
                            {
                                Root.LeftChild.Parent = successorNode;
                                successorNode.LeftChild = Root.LeftChild;

                                Root.RightChild.Parent = null;
                                Root = successorNode;
                            }
                        }
                    }
                    else
                    {
                        successorNode = FinMinMax(Node.RightChild, false); // searching for a successor

                        if (successorNode != Node.RightChild)
                        {
                            successorNode.Parent.LeftChild = null;
                            successorNode.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                            {
                                Node.Parent.LeftChild = successorNode;
                            }
                            else
                                Node.Parent.RightChild = successorNode;

                            if (Node.LeftChild != null)
                            {
                                Node.LeftChild.Parent = successorNode;
                                successorNode.LeftChild = Node.LeftChild;
                            }

                            if (Node.RightChild == null) return true;
                            Node.RightChild.Parent = successorNode;
                            successorNode.RightChild = Node.RightChild;
                        }
                        else
                        {
                            successorNode.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                            {
                                Node.Parent.LeftChild = successorNode;
                            }
                            else
                                Node.Parent.RightChild = successorNode;

                            successorNode.LeftChild = Node.LeftChild;
                            Node.LeftChild.Parent = successorNode;
                        }
                    }
                }
                else
                {
                    if (Node == Root)
                    {
                        if (GetAllNodes(Root).Count == 1)
                            Root = null;
                        else 
                        {
                            if (Node.LeftChild != null)
                            {
                                Node.LeftChild.Parent = null;
                                Root = Node.LeftChild;
                            }
                            else
                            {
                                Node.RightChild.Parent = null;
                                Root = Node.RightChild;
                            }
                        }
                    }
                    else // CASE (node has 1 child)
                    {
                        if (Node.LeftChild == null && Node.RightChild == null)
                        {
                            if (Node.Parent.LeftChild == Node)
                                Node.Parent.LeftChild = null;
                            else
                                Node.Parent.RightChild = null;
                            Node.Parent = null;
                        }

                        else if (Node.LeftChild != null)
                        {
                            Node.LeftChild.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                                Node.Parent.LeftChild = Node.LeftChild;
                            else
                                Node.Parent.RightChild = Node.RightChild;
                        }
                        else
                        {
                            Node.RightChild.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                                Node.Parent.LeftChild = Node.LeftChild;
                            else
                                Node.Parent.RightChild = Node.RightChild;
                        }
                    }
                }

                return true;
            }
            else
                return false;
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