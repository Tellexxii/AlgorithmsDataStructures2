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
                NewChild.Parent = null;
                Root = NewChild;
            }
            else
            {
                if (ParentNode.Children != null)
                {
                    ParentNode.Children.Add(NewChild);
                }
                else
                {
                    ParentNode.Children = new List<SimpleTreeNode<T>>();
                    ParentNode.Children.Add(NewChild);
                }

                NewChild.Parent = ParentNode;
            }

            count++;
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            // ваш код удаления существующего узла NodeToDelete
            SimpleTreeNode<T> current = Root;
            Stack<SimpleTreeNode<T>> stack = new Stack<SimpleTreeNode<T>>();

            stack.Push(current);

            while (current != null && stack.Count != 0)
            {
                if (current.Equals(NodeToDelete))
                {
                    current.Parent.Children.Remove(NodeToDelete);
                    current.Parent = null;
                    count--;
                    break;
                }
                else
                {
                    if (current.Children != null)
                    {
                        List<SimpleTreeNode<T>> children = current.Children;
                        foreach (var child in children)
                        {
                            stack.Push(child);
                        }
                    }

                }

                current = stack.Pop();
            }
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            // ваш код выдачи всех узлов дерева в определённом порядке
            SimpleTreeNode<T> current = Root;
            Stack<SimpleTreeNode<T>> stack = new Stack<SimpleTreeNode<T>>();
            List<SimpleTreeNode<T>> nodes = new List<SimpleTreeNode<T>>();

            stack.Push(current);

            while (current != null && stack.Count != 0)
            {
                if (current.Children != null)
                {
                    List<SimpleTreeNode<T>> children = current.Children;
                    foreach (var child in children)
                    {
                        stack.Push(child);
                    }
                }
                nodes.Add(current);
                current = stack.Pop();
            }

            return nodes.Count == 0 ? null : nodes;
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            // ваш код поиска узлов по значению
            List<SimpleTreeNode<T>> nodes = new List<SimpleTreeNode<T>>();
            SimpleTreeNode<T> current = Root;
            Stack<SimpleTreeNode<T>> stack = new Stack<SimpleTreeNode<T>>();

            stack.Push(current);

            while (current != null && stack.Count != 0)
            {
                if(current.NodeValue.Equals(val)) nodes.Add(current);
                else
                {
                    if (current.Children != null)
                    {
                        List<SimpleTreeNode<T>> children = current.Children;
                        foreach (var child in children)
                        {
                            stack.Push(child);
                        }
                    }

                }

                current = stack.Pop();
            }
            return nodes.Count == 0 ? null : nodes;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            // ваш код перемещения узла вместе с его поддеревом -- 
            // в качестве дочернего для узла NewParent
            OriginalNode.Parent.Children.Remove(OriginalNode);
            this.AddChild(NewParent, OriginalNode);
            count--;

        }

        public int Count()
        {
            // количество всех узлов в дереве
            return count;
        }

        public int LeafCount()
        {
            List<SimpleTreeNode<T>> list = this.GetAllNodes();
            int leafs = 0;
            foreach (var node in list)
            {
                if (node.Children == null || node.Children.Count == 0) leafs++;
            }

            return leafs;
        }
        public List<int> EvenTrees()
        {
            if (Count() % 2 == 1) return new List<int>();
            
            List<SimpleTreeNode<T>> leafs = Recursive(Root).FindAll(
                node => node.Children == null || node.Children.Count == 0);

            List<int> edges = new List<int>();
            SimpleTreeNode<T> previousLeafParrent = null;
            foreach (var leaf in leafs)
            {
                if (previousLeafParrent != leaf.Parent)
                {
                    SimpleTreeNode<T> node = leaf.Parent;
                    while (node != Root)
                    {
                        int nodesCount = Recursive(node).Count;
                        if (nodesCount % 2 == 0)
                        {
                            if (typeof(T) == typeof(int))
                            {
                                if (!edges.Contains(Convert.ToInt32(node.Parent.NodeValue)) ||
                                    !edges.Contains(Convert.ToInt32(node.NodeValue)))
                                {
                                    edges.Add(Convert.ToInt32(node.Parent.NodeValue));
                                    edges.Add(Convert.ToInt32(node.NodeValue));
                                }
                            }
                        }

                        node = node.Parent;
                    }
                }

                previousLeafParrent = leaf.Parent;
            }

            return edges;
        }

        private List<SimpleTreeNode<T>> Recursive(SimpleTreeNode<T> targetNode, T val = default(T), bool isFind = false)
        {
            SimpleTreeNode<T> node = targetNode;
            var result = new List<SimpleTreeNode<T>> { node };

            for (int i = 0; node.Children != null && i < node.Children.Count; i++)
            {
                result.AddRange(Recursive(node.Children[i]));
            }

            return result;
        }
    }

}