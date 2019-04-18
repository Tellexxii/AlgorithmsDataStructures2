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
            BSTFind<T> foundNode = FindNodeByKey(key);
            if (foundNode.NodeHasKey)
            {
                // удаляемый узел не имеет потомков
                if (foundNode.Node.LeftChild == null && foundNode.Node.RightChild == null)
                {
                    if (foundNode.Node.Parent.LeftChild != null && foundNode.Node.Parent.LeftChild.Equals(foundNode.Node))
                        foundNode.Node.Parent.LeftChild = null;
                    else if (foundNode.Node.Parent.RightChild != null && foundNode.Node.Parent.RightChild.Equals(foundNode.Node))
                        foundNode.Node.Parent.RightChild = null;
                }
                // удаляемый узел имеет только одного потомка
                else if (foundNode.Node.LeftChild == null ^ foundNode.Node.RightChild == null)
                {
                    if (foundNode.Node.LeftChild != null) // левый потомок привязываем к родителю удаленного узла
                    {
                        if (foundNode.Node.Parent.LeftChild != null && foundNode.Node.Parent.LeftChild.Equals(foundNode.Node))
                            foundNode.Node.Parent.LeftChild = foundNode.Node.LeftChild;
                        else
                            foundNode.Node.Parent.RightChild = foundNode.Node.LeftChild;

                        foundNode.Node.LeftChild.Parent = foundNode.Node.Parent;
                    }
                    else // правый потомок привязываем к родителю удаленного узла
                    {
                        if (foundNode.Node.Parent.LeftChild != null && foundNode.Node.Parent.LeftChild.Equals(foundNode.Node))
                            foundNode.Node.Parent.LeftChild = foundNode.Node.RightChild;
                        else
                            foundNode.Node.Parent.RightChild = foundNode.Node.RightChild;

                        foundNode.Node.RightChild.Parent = foundNode.Node.Parent;
                    }
                }
                // удаляемый узел имеет двух потомков
                else
                {
                    BSTNode<T> successorNode = FinMinMax(foundNode.Node.RightChild, false); // наименьший потомок, который больше удаляемого узла

                    if (successorNode.RightChild != null) // если наименьший потомок не является листом
                    {
                        successorNode.Parent.LeftChild = successorNode.RightChild; // передаем его правого потомка левым узлом родителю
                        successorNode.RightChild.Parent = successorNode.Parent; // правому потомку назначаем родителя
                    }
                    else
                    {
                        if (successorNode.Parent.LeftChild == successorNode)
                            successorNode.Parent.LeftChild = null; // удаляем левый лист 
                        else
                            successorNode.Parent.RightChild = null; // удаляем правый лист 
                    }
                    // преемник замещает удаленный узел
                    if (foundNode.Node.Parent.RightChild == foundNode.Node)
                        foundNode.Node.Parent.RightChild = successorNode;
                    else
                        foundNode.Node.Parent.LeftChild = successorNode;

                    successorNode.Parent = foundNode.Node.Parent; // новый родитель для узла-преемника

                    successorNode.LeftChild = foundNode.Node.LeftChild; // левый потомок удаленного узла становится потомком узла-преемника
                    successorNode.RightChild = foundNode.Node.RightChild; // правый потомок удаленного узла становится потомком узла-преемника

                    // связать потомков удаленного узла с новым родителем
                    if (foundNode.Node.RightChild != null)
                        foundNode.Node.RightChild.Parent = successorNode;
                    if (foundNode.Node.LeftChild != null)
                        foundNode.Node.LeftChild.Parent = successorNode;
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