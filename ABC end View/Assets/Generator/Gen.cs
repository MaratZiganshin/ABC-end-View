using System;

namespace Generator
{
    public class Gen
    {
        static Random random = new Random();
        public int N { get; set; }//размер поля
        public int numberOfLetters { get; set; }//количество допустимых букв
        public char[,] Field { get; set; }//поле игры
        //массивы букв по бокам
        public char[] upperRow { get; set; }
        public char[] leftColumn { get; set; }
        public char[] lowerRow { get; set; }
        public char[] rightColumn { get; set; }
        //массив всех букв по бокам
        Letter[] letters;
        //все решения
        public char[][,] OutSolution { get; set; }
        public Gen(int N, int numberOfLetters)
        {
            //инициализация полей класса
            this.N = N;
            this.numberOfLetters = numberOfLetters;
            Field = new char[N, N];
            upperRow = new char[N];
            leftColumn = new char[N];
            lowerRow = new char[N];
            rightColumn = new char[N];
            for (int i = 0; i < N; i++)
            {
                upperRow[i] = ' ';
                leftColumn[i] = ' ';
                lowerRow[i] = ' ';
                rightColumn[i] = ' ';
            }
            //нахождение случайного решения пустого поля
            Matrix temp = new Matrix(N, numberOfLetters, upperRow, leftColumn, lowerRow, rightColumn);
            DancingLinks d = new DancingLinks(temp);
            d.Solver3();
            Field = d.OutSolution();
            //заполнение букв по краю поля
            for (int i = 0; i < N; i++)
            {
                int j = 0;
                while (Field[j, i] == ' ')
                {
                    j++;
                }
                upperRow[i] = Field[j, i];

                j = 0;
                while (Field[i, j] == ' ') j++;
                leftColumn[i] = Field[i, j];

                j = N - 1;
                while (Field[j, i] == ' ') j--;
                lowerRow[i] = Field[j, i];

                j = N - 1;
                while (Field[i, j] == ' ') j--;
                rightColumn[i] = Field[i, j];
            }
            //массив всех букв
            letters = new Letter[4 * N];
            for (int i = 0; i < N; i++)
            {
                letters[i] = new Letter();
                letters[i].value = upperRow[i];
                letters[i].arr = 0;
                letters[i].index = i;

                letters[N + i] = new Letter();
                letters[N + i].value = leftColumn[i];
                letters[N + i].arr = 1;
                letters[N + i].index = i;

                letters[2 * N + i] = new Letter();
                letters[2 * N + i].value = lowerRow[i];
                letters[2 * N + i].arr = 2;
                letters[2 * N + i].index = i;

                letters[3 * N + i] = new Letter();
                letters[3 * N + i].value = rightColumn[i];
                letters[3 * N + i].arr = 3;
                letters[3 * N + i].index = i;
            }
            //нахождение всех решений
            Matrix m = new Matrix(N, numberOfLetters, upperRow, leftColumn, lowerRow, rightColumn);
            DancingLinks dan = new DancingLinks(m);
            dan.Solver2();
            //количество решений
            solutions = dan.countSolutions;
            //перемешиваем массив букв в случаном порядке
            Mix(letters);
            //удаляем буквы
            DeleteLetters();
            for (int i = 0; i < letters.Length; i++)
            {
                switch (letters[i].arr)
                {
                    case 0: upperRow[letters[i].index] = letters[i].value; break;
                    case 1: leftColumn[letters[i].index] = letters[i].value; break;
                    case 2: lowerRow[letters[i].index] = letters[i].value; break;
                    case 3: rightColumn[letters[i].index] = letters[i].value; break;
                }
            }
            OutSolution = dan.OutSolutions();
        }
        //перемешивание букв в случайном порядке
        public static void Mix(Letter[] letters)
        {
            for (int i = 0; i < letters.Length; i++)
            {
                int k = random.Next(0, letters.Length);
                Letter temp = letters[i];
                letters[i] = letters[k];
                letters[k] = temp;
            }
        }
        //класс буква
        public class Letter
        {
            public char value;//значение буквы
            public int arr;//в каком массиве она находится
            public int index;//индексв этом массиве
        }
        
        public int solutions;
        //удаление букв до тех пор пока количество решений остается прежним
        public void DeleteLetters()
        {
            Letter temp = letters[letters.Length - 1];
            int i = temp.arr * N + temp.index;
            if (i >= 0 && i < N) { upperRow[i] = ' '; }
            else if (i >= N && i < 2 * N) { leftColumn[i - N] = ' '; }
            else if (i >= 2 * N && i < 3 * N) { lowerRow[i - 2 * N] = ' '; }
            else if (i >= 3 * N && i < 4 * N) { rightColumn[i - 3 * N] = ' '; }
            Matrix m = new Matrix(N, numberOfLetters, upperRow, leftColumn, lowerRow, rightColumn);
            DancingLinks d = new DancingLinks(m);
            d.Solver2();
            if (d.countSolutions == solutions&&letters.Length>=((N*N)/2)+1) { Array.Resize(ref letters, letters.Length - 1); DeleteLetters(); }
        }
    }
}