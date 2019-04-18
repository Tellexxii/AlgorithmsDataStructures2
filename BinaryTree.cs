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
        //public override string ToString()
        //{
        //    string parent = Parent == null ? "null" : Parent.NodeKey.ToString();
        //    return $"key:{NodeKey},Parent:{parent}";
        //}
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

        public BSTFind(BSTNode<T> node, bool nodeHasKey, bool toLeft)
        {
            Node = node;
            NodeHasKey = nodeHasKey;
            ToLeft = toLeft;
        }
    }

    public class BST<T>
    {
        public BSTNode<T> Root; // корень дерева, или null

        private int count;

        public BST(BSTNode<T> node)
        {
            Root = node;
            count = Root == null ? 0 : 1;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            // ищем в дереве узел и сопутствующую информацию по ключу
            BSTNode<T> node = Root;

            if (node == null) return null;

            while (node != null)
            {
                int result = key.CompareTo(node.NodeKey);
                if (result < 0) // key less, go left
                {
                    if(node.LeftChild != null)
                    {
                        node = node.LeftChild;
                        continue;
                    }
                    return new BSTFind<T>(node, false, true); // key not found, LeftChild is null
                }
                else if(result > 0) // key greater, go right
                {
                    if (node.RightChild != null)
                    {
                        node = node.RightChild;
                        continue;
                    }
                    return new BSTFind<T>(node, false, false); // key not found
                }
                return new BSTFind<T>(node, true, false);
            }
            return null;
        }
        private bool Add(BSTNode<T> node, int key, T value)
        {
            int result = key.CompareTo(node.NodeKey);
            if (result < 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new BSTNode<T>(key, value, node);
                    count++;
                    return true;
                }
                else
                {
                    Add(node.LeftChild, key, value);
                }
            }
            else if(result > 0)
            {

                if (node.RightChild == null)
                {
                    node.RightChild = new BSTNode<T>(key, value, node);
                    count++;
                    return true;
                }
                else
                {
                    Add(node.RightChild, key, value);
                }
            }
            return false;
            
        }
        public bool AddKeyValue(int key, T val)
        {
            // добавляем ключ-значение в дерево
            BSTFind<T> find = this.FindNodeByKey(key);
            var node = find.Node;
            if (Root == null)
            {
                Root = node;
                count++;
                return true;
            }
            if (node.NodeKey == key) return false;
            else
            {
                if (node.NodeKey > key)
                {
                    node.LeftChild = new BSTNode<T>(key, val, node);
                }
                else
                {
                    node.RightChild = new BSTNode<T>(key, val, node);
                }
            }
            count++;
            return true;
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            // ищем максимальное/минимальное в поддереве
            BSTNode<T> current = FromNode;

            if (FromNode == null) return null;

            if (FindMax)
            {
                while(current.RightChild != null) current = current.RightChild;
                return current;
            }
            else
            {
                while (current.LeftChild != null) current = current.LeftChild;
                return current;
            }
        }

        public bool DeleteNodeByKey(int key)
        {
            BSTFind<T> found = FindNodeByKey(key);
            if (found.NodeHasKey)
            {
                // удаляемый узел не имеет потомков
                if (found.Node.LeftChild == null && found.Node.RightChild == null)
                {
                    if (found.Node.Parent.LeftChild != null && found.Node.Parent.LeftChild.Equals(found.Node))
                        found.Node.Parent.LeftChild = null;
                    else if (found.Node.Parent.RightChild != null && found.Node.Parent.RightChild.Equals(found.Node))
                        found.Node.Parent.RightChild = null;
                }
                // удаляемый узел имеет только одного потомка
                else if (found.Node.LeftChild == null ^ found.Node.RightChild == null)
                {
                    if (found.Node.LeftChild != null)
                    {
                        if (found.Node.Parent.LeftChild != null && found.Node.Parent.LeftChild.Equals(found.Node))
                            found.Node.Parent.LeftChild = found.Node.LeftChild;
                        else
                            found.Node.Parent.RightChild = found.Node.LeftChild;

                        found.Node.LeftChild.Parent = found.Node.Parent;
                    }
                    else // правый потомок привязываем к родителю удаленного узла
                    {
                        if (found.Node.Parent.LeftChild != null && found.Node.Parent.LeftChild.Equals(found.Node))
                            found.Node.Parent.LeftChild = found.Node.RightChild;
                        else
                            found.Node.Parent.RightChild = found.Node.RightChild;

                        found.Node.RightChild.Parent = found.Node.Parent;
                    }
                }
                // удаляемый узел имеет двух потомков
                else
                {
                    BSTNode<T> successorNode = FinMinMax(found.Node.RightChild, false); 

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
                    
                    if (found.Node.Parent.RightChild == found.Node)
                        found.Node.Parent.RightChild = successorNode;
                    else
                        found.Node.Parent.LeftChild = successorNode;

                    successorNode.Parent = found.Node.Parent; 

                    successorNode.LeftChild = found.Node.LeftChild; 
                    successorNode.RightChild = found.Node.RightChild; 

                    
                    if (found.Node.RightChild != null)
                        found.Node.RightChild.Parent = successorNode;
                    if (found.Node.LeftChild != null)
                        found.Node.LeftChild.Parent = successorNode;
                }
                count--;
                return true;
            }

            return false; // если узел не найден
        }

        public int Count()
        {
            return count; // количество узлов в дереве
        }

        
    }
}