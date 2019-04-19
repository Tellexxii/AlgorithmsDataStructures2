using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class aBST
    {
        public int?[] Tree; // массив ключей
        int tree_size;
        public aBST(int depth)
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
            for (;i < tree_size;)
            {
                if(Tree[i] != null)
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
            if(found <= 0)
            {
                Tree[(int)-found] = key;
                return (int)-found;
            }
            else if(found > 0)
            {
                return (int)found;
            }
            return -1;
            // индекс добавленного/существующего ключа или -1 если не удалось
        }

    }
}