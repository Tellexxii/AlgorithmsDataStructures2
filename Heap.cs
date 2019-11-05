using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Heap
    {

        public int[] HeapArray;
        private int _count;
        public Heap() { HeapArray = null; }

        public void MakeHeap(int[] a, int depth)
        {
            int treeSize = (int)Math.Pow(2, depth + 1) - 1;
            HeapArray = new int[treeSize];

            foreach (var num in a)
                Add(num);
        }

        public int GetMax()
        {
            if (HeapArray == null || _count == 0) return -1;

            int maxKey = HeapArray[0];

            HeapArray[0] = HeapArray[_count - 1];
            HeapArray[_count - 1] = 0;
            _count--;
            RecalculateDown(0);

            return maxKey;
        }

        public bool Add(int key)
        {
            if (_count == HeapArray.Length) return false;

            int index = _count;
            HeapArray[index] = key;
            int parent = (index - 1) / 2;

            while (index > 0 && parent >= 0)
            {
                if (HeapArray[index] > HeapArray[parent])
                {
                    int temp = HeapArray[index];
                    HeapArray[index] = HeapArray[parent];
                    HeapArray[parent] = temp;
                }

                index = parent;
                parent = (index - 1) / 2;
            }
            _count++;

            return true;
        }

        private void RecalculateDown(int index)
        {
            while (true)
            {
                int largest = index;
                int left = index * 2 + 1;
                int right = index * 2 + 2;

                if (left < _count && HeapArray[left] > HeapArray[largest]) largest = left;
                if (right < _count && HeapArray[right] > HeapArray[largest]) largest = right;
                if (largest == index) break;

                int temp = HeapArray[index];
                HeapArray[index] = HeapArray[largest];
                HeapArray[largest] = temp;

                index = largest;
            }
        }

    }
}