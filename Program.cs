using System;
using System.Buffers;

namespace arraypoolcore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Array Pool Demo");
            UsingSimpleArray();
            UsingPoolArray();
        }

        private static void UsingSimpleArray()
        {
            Console.WriteLine(nameof(UsingSimpleArray));
            for (int i = 0; i < 20; i++)
            {
                LocalUseOfArray(i);
            }
        }

        private static void UsingPoolArray()
        {
            Console.WriteLine(nameof(UsingSimpleArray));
            for (int i = 0; i < 20; i++)
            {
                LocalUseOfSharePool(i);
            }
        }
        private static void LocalUseOfArray(int i)
        {
            var arr = new int[20];

            FillArray(arr);
            PrintArray(arr);
            ShowAddress($"", arr);
        }

        private static void LocalUseOfSharePool(int i)
        {
            var arr = ArrayPool<int>.Shared.Rent(20);
            FillArray(arr);
            PrintArray(arr);
            ShowAddress($"", arr);
            ArrayPool<int>.Shared.Return(arr);
        }

        unsafe private static void ShowAddress(string name, int[] item)
        {
            fixed (int* addr = item)
            {
                Console.WriteLine($"{name} \t0x{(ulong)addr:X}");
            }
        }
        
        private static void FillArray(int[] arr)
        {
            for (int i = 0; i < 20; i++)
            {
                arr[i] = i;
            }
        }

        private static void PrintArray(int[] arr)
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write($"{arr[i]} ");
            }
        }
    }
}
