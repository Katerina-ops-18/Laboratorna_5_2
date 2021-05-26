using System;
using System.IO;

namespace Lab_5_2
{
    class Program
    {
        static void Main(string[] args)
        {

            //Saver FH = new Saver();
            //FH.ActionMenu("Hello", ConsoleColor.Red);

             Console.Write("Матриця на NxN = ");
            int n = int.Parse(Console.ReadLine());

            Matrix A = new Matrix(n, n);
            Matrix B = new Matrix(n, n);

            Console.WriteLine("Матрица А: ");
            A.Create();
            A.Print();

            Console.WriteLine("Матрица В: ");
            B.Create();
            B.Print();

            Console.WriteLine("добавления матриц А и Б: ");
            Console.WriteLine();
            Matrix C = A + B;
            C.Print();

            Console.WriteLine("умножения матриц А и Б:");
            Console.WriteLine();
            C = A * B;
            C.Print();

            Console.WriteLine("вычитание матриц А и Б:");
            C = A - B;
            C.Print();

           Console.WriteLine("определитель матрицы А: ");
            double det =  A.CalculateDeterminant();
            Console.WriteLine("определитель = {0}.", det);

            Console.WriteLine("\nТранспонированная матрица A: ");
            A.Swap();
            A.Print();

            Console.WriteLine("\nТранспонированная матрица B: ");
            B.Swap();
            B.Print();

            Console.WriteLine("\nОбернена матрица A: ");
            A.InverseThis();
            A.Print();
        }
    }
}
