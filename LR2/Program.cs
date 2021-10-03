using NPOI.SS.Formula.Functions;
using System;
using System.Numerics;

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
            BigInteger A, B;
            A = BigInteger.ModPow(g, a, p);
            B = BigInteger.ModPow(g, b, p);
            Console.WriteLine();
            Console.WriteLine("Алиса вычисляет значение A = {0}^{1} mod {2}", g, a, p);
            Console.WriteLine("Алиса -> Бобу");
            Console.WriteLine("Боб вычисляет значение B = {0}^{1} mod {2}", g, b, p);
            Console.WriteLine("Боб -> Алиса");
            BigInteger key_alice, key_bob;
            key_alice = BigInteger.ModPow(B, a, p);
            key_bob = BigInteger.ModPow(A, b, p);
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
            int j=0;
            for (int i = 2; i < n; i++)
            {

                    if (mass[i]) { array[j] = i; j++; } //получаем массив из простых чисел
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
        
    }
}
