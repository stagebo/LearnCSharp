using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数学问题
{
    class 快速排序
    {
        public void Run() {
            int[] arr = { 56,32,45,65,89,54,23,45,64,18,29,73,91};
            QuickSort(arr,0,arr.Length-1);
            print(arr);
        }
        public void print(int[] a) {
            for (int i = 0; i < a.Length; i++) {
                Console.Write(a[i]+" ");
            }
        }
        public void QuickSort(int[] arr, int low, int high) {
            int l = low;
            int h = high;
            int k=arr[low];
            while (l < h)
            {
                while (l < h && arr[h] >= k) {
                    h--;
                }
                while (l < h && arr[l] <= k) {
                    l++;
                }
                if (arr[l] > arr[h] && l < h) {
                    int temp = arr[l];
                    arr[l] = arr[h];
                    arr[h] = temp;
                }
                if (l >= h&&arr[l]>k) {
                    int temp = arr[low];
                    arr[low] = arr[h];
                    arr[h] = temp;
                }
            }
            if (l > low) {
                QuickSort(arr,low,l-1);
            }
            if (h < high) {
                QuickSort(arr,h,high);
            }
        }
        public void sort(int[] arr, int low, int high)
        {
            int l = low;
            int h = high;
            int povit = arr[low];

            while (l < h)
            {
                /*寻找从h开始往左第一个小于arr[low]的数据*/
                while (l < h && arr[h] >= povit)
                    h--;
                /*如果该数据再l左侧，则交换，l增1*/
                if (l < h)
                {
                    int temp = arr[h];
                    arr[h] = arr[l];
                    arr[l] = temp;
                    l++;
                }
                /*寻找l开始往右第一个大于arr[low]的数据*/
                while (l < h && arr[l] <= povit)
                    l++;
                /*如果该数据再h左侧，则交换。h减1*/
                if (l < h)
                {
                    int temp = arr[h];
                    arr[h] = arr[l];
                    arr[l] = temp;
                    h--;
                }
            }
            if (l > low) sort(arr, low, l - 1);
            if (h < high) sort(arr, l + 1, high);
        }
    }
}
