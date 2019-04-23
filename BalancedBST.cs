using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public static class BalancedBST
    {
        public static int[] GenerateBBSTArray(int[] a)
        {
            Array.Sort(a);

            int depth = CheckDepth(a.Length);
            aBSTHelper tree = new aBSTHelper(depth); // Using array tree

            AddToTree(a, tree, 0, a.Length - 1);

            var arr = new List<int>();
            for (int i = 0; i < tree.Tree.Length; i++)
            {
                if(tree.Tree[i] != null && tree.Tree[i] != -1)
                {
                    arr.Add(tree.Tree[i].Value);
                }
            }
            return arr.ToArray();
        }
        
        public static void AddToTree(int[] nums,  aBSTHelper tree, int start, int end)
        {
            if (start > end) return;

            int middle = (start + end) / 2;

            tree.AddKey(nums[middle]);
            AddToTree(nums, tree, start, middle - 1);
            AddToTree(nums, tree, middle + 1, end);
        }

        public static int CheckDepth(int size)
        {
            int depth = 0;
            while (true)
            {

                if ((int)Math.Pow(2, (Convert.ToDouble(depth + 1))) - 1 >= size && size <= (int)Math.Pow(2, (Convert.ToDouble(depth + 2))) - 1)
                {
                    return depth;
                }
                depth++;
            }
        }
    }
    public class aBSTHelper
    {
        public int?[] Tree; // массив ключей
        int tree_size;
        public aBSTHelper(int depth)
        {
            // правильно рассчитайте размер массива для дерева глубины depth:
            tree_size = (int)Math.Pow(2, (Convert.ToDouble(depth + 1))) - 1;
            Tree = new int?[tree_size];
            for (int i = 0; i < tree_size; i++) Tree[i] = null;
        }

        public int? FindKeyIndex(int key)
        {
            // ищем в массиве индекс ключа
            int i = 0;
            for (; i < tree_size;)
            {
                if (Tree[i] != null)
                {
                    if (Tree[i] == key) return i;
                    else
                    {
                        i = Tree[i] > key ? (2 * i) + 1 : (2 * i) + 2; // If greater, go left, Else go right
                    }
                }
                else
                {
                    return -i;
                }

            }
            return null; // не найден
        }

        public int AddKey(int key)
        {
            // добавляем ключ в массив
            int? found = this.FindKeyIndex(key);
            if (found <= 0)
            {
                Tree[(int)-found] = key;
                return (int)-found;
            }
            else if (found > 0)
            {
                return (int)found;
            }
            return -1;
            // индекс добавленного/существующего ключа или -1 если не удалось
        }

    }
}