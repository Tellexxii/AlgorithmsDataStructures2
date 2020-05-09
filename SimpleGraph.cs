using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public class Vertex<T>
    {
        public bool Hit;
        public T Value;
        public Vertex(T val)
        {
            Value = val;
            Hit = false;
        }
    }

    public class SimpleGraph<T>
    {
        public Vertex<T>[] vertex;
        public int[,] m_adjacency;
        public int max_vertex;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int[size, size];
            vertex = new Vertex<T>[size];
        }

        public void AddVertex(T value)
        {
            // ваш код добавления новой вершины 
            // с значением value 
            // в свободную позицию массива vertex
            
            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] == null)
                {
                    vertex[i] = new Vertex<T>(value); ;
                    break;
                }
            }
            
        }

        // здесь и далее, параметры v -- индекс вершины
        // в списке  vertex
        public void RemoveVertex(int v)
        {
            // ваш код удаления вершины со всеми её рёбрами
            vertex[v] = null;
            
            for (int i = 0; i < max_vertex; i++)
            {
                RemoveEdge(v, i);
            }
        }

        public bool IsEdge(int v1, int v2)
        {
            // true если есть ребро между вершинами v1 и v2
            return (m_adjacency[v1, v2] == 1 || m_adjacency[v2, v1] == 1);
        }

        public void AddEdge(int v1, int v2)
        {
            // добавление ребра между вершинами v1 и v2
            m_adjacency[v1, v2] = 1;
            m_adjacency[v2, v1] = 1;
        }

        public void RemoveEdge(int v1, int v2)
        {
            // удаление ребра между вершинами v1 и v2
            m_adjacency[v1, v2] = 0;
            m_adjacency[v2, v1] = 0;
        }

        public List<Vertex<T>> DepthFirstSearch(int VFrom, int VTo)
        {
            List<Vertex<T>> path = new List<Vertex<T>>();
            Stack<Vertex<T>> vertices = new Stack<Vertex<T>>();
            
            foreach (var item in vertex) { item.Hit = false; }
            
            Vertex<T> currentVertex = vertex[VFrom];
            currentVertex.Hit = true;
            vertices.Push(currentVertex);
            
            while (true)
            {
                if (IsEdge(Array.IndexOf(vertex, currentVertex), VTo))
                {
                    vertices.Push(vertex[VTo]);
                    path.AddRange(vertices);
                    path.Reverse();
                    return path;
                }

                List<Vertex<T>> adjVertex = new List<Vertex<T>>();
                
                adjVertex.AddRange(Array.FindAll(vertex, (item) =>
                    !item.Hit &&
                    item != currentVertex &&
                    IsEdge(Array.IndexOf(vertex, currentVertex), Array.IndexOf(vertex, item))));

                if (!adjVertex.Any())
                {
                    vertices.Pop();
                    if (!vertices.Any()) return path;
                    currentVertex = vertices.Peek();
                    currentVertex.Hit = true;
                }
                else
                {
                    currentVertex = adjVertex[0];
                    currentVertex.Hit = true;
                    vertices.Push(currentVertex);
                }
            }
        }

        public List<Vertex<T>> BreadthFirstSearch(int VFrom, int VTo)
        {
            // узлы задаются позициями в списке vertex.
            // возвращает список узлов -- путь из VFrom в VTo
            // или пустой список, если пути нету
            List<Vertex<T>> path = new List<Vertex<T>>();
            List<Vertex<T>> adjVertex = new List<Vertex<T>>();
            Queue<Vertex<T>> adjVQueue = new Queue<Vertex<T>>();

            foreach (var item in vertex) { item.Hit = false; }

            Vertex<T> currentVertex = vertex[VFrom];
            currentVertex.Hit = true;
            path.Add(currentVertex);

            while (true)
            {
                adjVertex.Clear();
                adjVertex.AddRange(Array.FindAll(vertex, (item) =>
                    !item.Hit &&
                    item != currentVertex &&
                    IsEdge(Array.IndexOf(vertex, currentVertex), Array.IndexOf(vertex, item))));

                if (adjVertex.Count == 0)
                {
                    if (adjVQueue.Count == 0)
                    {
                        path.Clear();
                        return path;
                    }

                    currentVertex = adjVQueue.Dequeue();
                    path.Add(currentVertex);
                }
                else
                {
                    if (adjVertex[0] == vertex[VTo])
                    {
                        path.Add(adjVertex[0]);
                        Vertex<T> lastVertex = path[path.Count - 1];
                        List<Vertex<T>> tempList = new List<Vertex<T>> { lastVertex };

                        int currentIndex = path.Count - 1;
                        int startIndex = 0;
                        while (currentIndex > startIndex)
                        {
                            if (IsEdge(Array.IndexOf(vertex, path[currentIndex]), Array.IndexOf(vertex, path[startIndex])))
                            {
                                tempList.Add(path[startIndex]);
                                currentIndex = startIndex;
                                startIndex = 0;
                            }
                            else
                                ++startIndex;
                        }

                        tempList.Reverse();
                        path = tempList;

                        return path;
                    }

                    adjVertex[0].Hit = true;
                    adjVQueue.Enqueue(adjVertex[0]);
                }
            }
        }
    }
}