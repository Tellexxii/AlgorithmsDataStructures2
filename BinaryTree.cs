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
                // No child
                if (found.Node.LeftChild == null && found.Node.RightChild == null)
                {
                    if (found.Node.Parent.LeftChild != null && found.Node.Parent.LeftChild.Equals(found.Node))
                        found.Node.Parent.LeftChild = null;
                    else if (found.Node.Parent.RightChild != null && found.Node.Parent.RightChild.Equals(found.Node))
                        found.Node.Parent.RightChild = null;
                }
                // One child
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
                    else // Bind chuld to parent
                    {
                        if (found.Node.Parent.LeftChild != null && found.Node.Parent.LeftChild.Equals(found.Node))
                            found.Node.Parent.LeftChild = found.Node.RightChild;
                        else
                            found.Node.Parent.RightChild = found.Node.RightChild;

                        found.Node.RightChild.Parent = found.Node.Parent;
                    }
                }
                // 2 child
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

            return false; // Not found
        }

        public List<BSTNode<T>> WideAllNodes()
        {
            var list = new List<BSTNode<T>>();
            var queue = new Queue<BSTNode<T>>();
            var node = Root;

            if (Root == null) return list;

            queue.Enqueue(node);
            while(queue.Count != 0)
            {
                node = queue.Dequeue();
                list.Add(node);

                if(node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                }
                if(node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                }
            }
            return list;
        }

        public List<BSTNode<T>> DeepAllNodes(int method)
        {
            // method - 0(in -order), 1(post - order), 2(pre - order)
            var list = new List<BSTNode<T>>();
            var node = Root;

            if (Root == null) return list;
            switch (method)
            {
                case 0:
                    list = this.Inorder(node);
                    break;
                case 1:
                    list = this.Postorder(node);
                    break;
                case 2:
                    list = this.Preorder(node);
                    break;
                default:
                    break;
            }
            return list;
        }
        private List<BSTNode<T>> Inorder(BSTNode<T> node)
        {
            var list = new List<BSTNode<T>>();
            if (node != null)
            {
                if (node.LeftChild != null)
                {
                    list.AddRange(Inorder(node.LeftChild));
                }

                list.Add(node);

                if (node.RightChild != null)
                {
                    list.AddRange(Inorder(node.RightChild));
                }


            }
            return list;
        }
        private List<BSTNode<T>> Postorder(BSTNode<T> node)
        {
            var list = new List<BSTNode<T>>();
            if (node != null)
            {
                if (node.LeftChild != null)
                {
                    list.AddRange(Postorder(node.LeftChild));
                }
                
                if (node.RightChild != null)
                {
                    list.AddRange(Postorder(node.RightChild));
                }

                list.Add(node);

            }
            return list;
        }
        private List<BSTNode<T>> Preorder(BSTNode<T> node)
        {
            var list = new List<BSTNode<T>>();
            if (node != null)
            {
                list.Add(node);

                if (node.LeftChild != null)
                {
                    list.AddRange(Preorder(node.LeftChild));
                }

                if (node.RightChild != null)
                {
                    list.AddRange(Preorder(node.RightChild));
                }


            }
            return list;
        }

        public int Count()
        {
            return count; // количество узлов в дереве
        }

        
    }
}