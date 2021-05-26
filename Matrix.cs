using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_5_2
{
    class Matrix
    {


        // Скрытые поля
        private int n;
        private int m;
        private double[,] data;

        // Создаем конструкторы матрицы

        public void Create()      //метод ввода матрицы
        {
            Random rnd = new Random(); // объект, который будем исппользовать для генерации рандомных целых чисел
            for (int i = 0; i < n; i++) //строки
            {
                for (int j = 0; j < n; j++) //столбцы
                {
                    data[i, j] = rnd.Next(0, 20); // диапазон случайных чисел от 0 до 50
                }
            }
        }

        public void Print()   //метод вивода матриці
        {
            for (int i = 0; i < n; i++) //строки
            {
                for (int j = 0; j < n; j++) //столбцы
                {
                    //mass[i, j] = rnd.Next(-50, 50); // диапазон случайных чисел от -50 до 50
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine("\n");
            }

        }
        // Задаем аксессоры для работы с полями вне класса Matrix
        public Matrix(int m, int n)
        {
            this.m = n;
            this.n = n;
            this.data = new double[n, n];
        }

        public int this[int i, int j]
        {
            get
            {
                return (int)data[i, j];
            }
            set
            {
                data[i, j] = value;
            }
        }
        public static Matrix operator *(double a, Matrix B)
        {
            Matrix C = new Matrix(B.n, B.n);
            for (int i = 0; i < B.n; i++)
            {
                for (int j = 0; j < B.n; j++)
                {
                    C.data[i, j] = B.data[i, j];
                }
            }
            for (int i = 0; i < C.n; i++)
            {
                for (int j = 0; j < C.n; j++)
                {
                    C.data[i, j] *= a;
                }
            }

            return C;
        }

        public static Matrix operator *(Matrix A, double b)
        {
            return b * A;
        }



        public static Matrix operator +(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.n, B.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    C.data[i, j] = A.data[i, j] + B.data[i, j];
                }
            }
            return C;
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            Matrix D = new Matrix(A.n, B.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    D.data[i, j] = A.data[i, j] - B.data[i, j];
                }
            }
            return D;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            Matrix E = new Matrix(A.n, B.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    E.data[i, j] = 0;
                    for (int k = 0; k < A.n; k++)
                    {
                        E.data[i, j] = E.data[i, j] + A.data[k, j] * B.data[i, k];
                    }
                }
            }
            return E;
        }
        public double GetDeterminant()
        {
            if (n != n)
            {
                return 0;
            }
            return CalculateDeterminant(data);
        }
        public double CalculateDeterminant()
        {
            return CalculateDeterminant(data);
        }
        static unsafe double Det(double* rmX, int n)
        {
            double* mtx_u_ii, mtx_ii_j;
            double* mtx_end = rmX + n * (n - 1), mtx_u_ii_j = null;
            double val, det = 1;
            int d = 0;
            // rmX указывает на (i,i) элемент на каждом шаге и называется ведущим
            for (double* mtx_ii_end = rmX + n; rmX < mtx_end; rmX += n + 1, mtx_ii_end += n, d++)
            {
                // Ищем максимальный элемент в столбце(под ведущим) 
                {
                    //Ищем максимальный элемент и его позицию
                    val = System.Math.Abs(*(mtx_ii_j = rmX));
                    for (mtx_u_ii = rmX + n; mtx_u_ii < mtx_end; mtx_u_ii += n)
                    {
                        if (val < System.Math.Abs(*mtx_u_ii))
                            val = System.Math.Abs(*(mtx_ii_j = mtx_u_ii));
                    }
                    //Если максимальный эдемент = 0 -> матрица вырожденная
                    if (val == 0) return double.NaN;
                    //Если ведущий элемент не является максимальным - делаем перестановку строк и меняем знак определителя
                    else if (mtx_ii_j != rmX)
                    {
                        det = -det;
                        for (mtx_u_ii = rmX; mtx_u_ii < mtx_ii_end; mtx_ii_j++, mtx_u_ii++)
                        {
                            val = *mtx_u_ii;
                            *mtx_u_ii = *mtx_ii_j;
                            *mtx_ii_j = val;
                        }
                    }
                }
                //Обнуляем элементы под ведущим
                for (mtx_u_ii = rmX + n, mtx_u_ii_j = mtx_end + n; mtx_u_ii < mtx_u_ii_j; mtx_u_ii += d)
                {
                    val = *(mtx_u_ii++) / *rmX;
                    for (mtx_ii_j = rmX + 1; mtx_ii_j < mtx_ii_end; mtx_u_ii++, mtx_ii_j++)
                        *mtx_u_ii -= *mtx_ii_j * val;
                }
                det *= *rmX;
            }
            return det *= *rmX;
        }
        public unsafe static double CalculateDeterminant(double[,] A)
        {
            int n = A.GetLength(0);
            if (n == A.GetLength(1))
            {
                double[] temp = new double[A.Length];
                Buffer.BlockCopy(A, 0, temp, 0, temp.Length * sizeof(double));
                fixed (double* pm = &temp[0]) return Det(pm, n);
            }
            else throw new RankException();
        }

        public Matrix TransposedMatrix()
        {
            Matrix newMatrix = new Matrix(n, m);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    newMatrix.data[j, i] = data[i, j];
                }
            }

            return newMatrix;
        }

        public void Swap()  //транспон
        {
            int tmp;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    tmp = (int)data[i, j];
                    data[i, j] = data[j, i];
                    data[j, i] = tmp;
                }
            }
        }
        public Matrix InversedMatrix()
        {
            if (n != n)
            {
                return null;
            }

            double det = GetDeterminant();

            if (det == 0)
            {
                return null;
            }

            Matrix transposed = TransposedMatrix();
            Matrix DetMat = new Matrix(n, n);
            int size = data.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double[,] smallMatrix = new double[size - 1, size - 1];

                    int row = 0;
                    int col = 0;
                    for (int k = 0; k < size; k++)
                    {
                        for (int l = 0; l < size; l++)
                        {
                            if (k == i || l == j)
                            {
                                continue;
                            }

                            smallMatrix[row, col] = transposed.data[k, l];
                            col++;
                        }
                        if (col == smallMatrix.GetLength(0))
                        {
                            row++;
                            col = 0;
                        }
                    }

                    DetMat.data[i, j] = Math.Pow(-1, i - j) * CalculateDeterminant(smallMatrix);///////////////////////
                }
            }

            return (1 / det) * DetMat;
        }

        public void InverseThis()
        {
            Matrix newMatrix = InversedMatrix();
            n = newMatrix.n;
            n = newMatrix.n;
            data = newMatrix.data;
        }
        //Деструктор Matrix
        ~Matrix()
        {
            Console.WriteLine("Очистка");
        }

    }
}
