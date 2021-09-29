﻿using NPOI.SS.Formula.Functions;
using System;

namespace LR2
{
    class Program
    {
        static void Main(string[] args)
        {
            // "а" -> "б" означает передачу сообщения соответственно от a к б
            Console.WriteLine("Алиса -> Боб");
            var rnd_a = GetRandom();
            var a = GenPrime(rnd_a);
            Console.WriteLine("Алиса выбирает простое базовое число <a>: {0}", a);
            var rnd_b = GetRandom();
            var b = GenPrime(rnd_b);
            Console.WriteLine("Боб выбирает простое базовое число <b>: {0}", b);
            //B = g^b mod p   A = g^a mod p
            int p, g;
            var rnd_p = GetRandom(); p = GenPrime(rnd_p);
            var rnd_g = GetRandom(); g = GenPrime(rnd_g);
            Console.WriteLine();
            Console.WriteLine("Алиса и Боб выбирают простые базовые числа <p> и <g>: {0} и {1}", p, g);
            Console.WriteLine("Эти числа используются для вычислений");
            int A, B;
            A = OpenKey(a, g, p);
            B = OpenKey(b, g, p);
            Console.WriteLine();
            Console.WriteLine("Алиса вычисляет значение A = {0}^{1} mod {2}", g, a, p);
            Console.WriteLine("Алиса -> Бобу");
            Console.WriteLine("Боб вычисляет значение B = {0}^{1} mod {2}", g, b, p);
            Console.WriteLine("Боб -> Алиса");
            int key_alice, key_bob;
            key_alice = SecretKey(B, a, p);
            key_bob = SecretKey(A, b, p);
            Console.WriteLine();
            Console.WriteLine("Алиса вычисляет секретный ключ {3} = {0}^{1} mod {2}", B, a, p, key_alice);
            Console.WriteLine("Боб вычисляет секретный ключ {3} = {0}^{1} mod {2}", A, b, p, key_bob);
            Console.WriteLine();
            if (key_alice == key_bob) Console.WriteLine("Секретные ключи совпадают");
            else Console.WriteLine("Секретные ключи не совпадают");
        }
        
        public static int GenPrime(int n) //поиск простого числа из n-го количества
        {            
            bool[] mass = new bool[n]; //булевый массив
            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                mass[i] = true;
            }
            for (int p = 2; p < n; p++)
            {
                if (mass[p])
                {
                    for (int i = p * p; i < n; i += p)
                    {
                        mass[i] = false; //заменяем значения массива
                    }
                }
            }
            for (int i = 2; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mass[i]) { array[j] = i; } //получаем массив из простых чисел
                }
            }
            var num = array[new Random().Next(0, array.Length)]; //из массива простых чисел рандомно выбираем ОДНО число
            
            return num;
        }
        public static int GetRandom() //вывод рандомного числа
        {
            Random rnd = new Random();
            int value = rnd.Next(1, 10000);
            return value;
        }
        public static int OpenKey(int a, int g, int p) //открытый ключ
        {
            double A = Math.Pow((double)g, (double)a);
            A = (int)A % p;
            return (int)A;
        }
        public static int SecretKey(int B, int a, int p) //секретный ключ
        {
            double K = Math.Pow((double)B, (double)a);
            K = (int)K % p;
            return (int)K;
        }
    }
}
