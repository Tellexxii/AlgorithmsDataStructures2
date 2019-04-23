using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public static class BalancedBST
    {
        public static int[] GenerateBBSTArray(int[] a)
        {
            Array.Sort(a);
            var list = new List<int>();
            int middle = a.Length / 2;
            list.Add(a[middle]);

            var llist = new List<int>();
            var rlist = new List<int>();

            llist.AddRange(Checking(a, 0, middle - 1));
            rlist.AddRange(Checking(a, middle + 1, a.Length-1));

            int index = 0;
            while (index != llist.Count && index != rlist.Count)
            {
                list.Add(llist[index]);
                list.Add(rlist[index]);
                index++;
            }
            return list.ToArray();
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