using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public static class BalancedBST
    {
        public static int[] GenerateBBSTArray(int[] a)
        {
            Array.Sort(a);
            return Checking(a, 0, a.Length - 1).ToArray();
        }
        public static List<int> Checking(int[] nums, int start, int end)
        {
            if (start > end) return new List<int>();
            int middle = start + (end - start) / 2;
            var list = new List<int>();
            list.Add(nums[middle]);
            list.AddRange(Checking(nums, start, middle - 1));
            list.AddRange(Checking(nums, middle + 1, end));
            return list;
        }
    }
}