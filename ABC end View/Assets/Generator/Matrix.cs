namespace Generator
{
    public class Matrix
    {
        public int N { get; set; }//размер игрового поля
        public int[,] Data { get; set; }//матрица из 1 и 0
        public int[,] additionalColumn { get; set; }
        public int[,] additionalRow { get; set; }
        public int candidatesCount { get; set; }//число столбцов
        public int requestsCount { get; set; }//число строк
        //статический метод, выясняющий сколько букв в массиве символов
        static int LetterCount(char[] a)
        {
            int k = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != ' ') { k++; }
            }
            return k;
        }
        //конструктор
        public Matrix(int N, int numberOfLetters, char[] upperRow, char[] leftColumn, char[] lowerRow, char[] rightColumn)
        {
            //инициализация полей класса
            this.N = N;
            //строки - кандидаты на решение
            candidatesCount = N * N * N;
            //столбцы - требования к решению
            int letterCount = LetterCount(upperRow) + LetterCount(leftColumn) + LetterCount(lowerRow) + LetterCount(rightColumn);
            requestsCount = 3 * N * N + 2 * letterCount;
            //сам массив, представляющий входные данные
            Data = new int[candidatesCount, requestsCount];
            //дополнительные столбцы и строки для удобства
            additionalColumn = new int[candidatesCount, 3];
            additionalRow = new int[3, requestsCount];
            for (int i = 0; i < candidatesCount; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0) additionalColumn[i, j] = i / (N * N);
                    else if (j == 1) additionalColumn[i, j] = (i / N) % N;
                    else if (j == 2) additionalColumn[i, j] = i % N;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < requestsCount; j++)
                {
                    switch (i)
                    {
                        case 0:
                            if (j < N * N) { additionalRow[i, j] = 0; }
                            else if (j >= N * N && j < 2 * N * N) { additionalRow[i, j] = 1; }
                            else if (j >= 2 * N * N + 2 * letterCount) { additionalRow[i, j] = 6; }
                            break;

                        case 1:
                            if (j < N * N) { additionalRow[i, j] = (j / N) % N; }
                            else if (j >= N * N && j < 2 * N * N) { additionalRow[i, j] = ((j - N * N) / N) % N; }
                            else if (j >= 2 * N * N + 2 * letterCount) { additionalRow[i, j] = ((j - 2 * N * N - 2 * letterCount) / N) % N; }
                            break;

                        case 2:
                            if (j < N * N) { additionalRow[i, j] = j % N; }
                            else if (j >= N * N && j < 2 * N * N) { additionalRow[i, j] = (j - N * N) % N; }
                            else if (j >= 2 * N * N + 2 * letterCount) { additionalRow[i, j] = (j - 2 * N * N - 2 * letterCount) % N; }
                            break;
                    }
                }
            }
            //заполнение матрицы
            int temp = 2 * N * N;
            for (int i = 0; i < upperRow.Length; i++)
            {
                if (upperRow[i] != ' ')
                {
                    for (int k = 0; k < 2; k++)
                    {
                        additionalRow[0, temp + k] = 2;
                        additionalRow[1, temp + k] = i;
                        additionalRow[2, temp + k] = (int)(upperRow[i] - 65);
                    }
                    for (int k = 0; k < 2; k++) { Data[additionalRow[1, temp + k] * N + additionalRow[2, temp + k], temp + k] = 1; }
                    Data[additionalRow[1, temp] * N + (N - 1), temp] = 1;
                    Data[N * N + additionalRow[1, temp + 1] * N + additionalRow[2, temp + 1], temp + 1] = 1;

                    temp += 2;
                }

            }
            for (int i = 0; i < leftColumn.Length; i++)
            {
                if (leftColumn[i] != ' ')
                {
                    for (int k = 0; k < 2; k++)
                    {
                        additionalRow[0, temp + k] = 3;
                        additionalRow[1, temp + k] = i;
                        additionalRow[2, temp + k] = (int)(leftColumn[i] - 65);
                    }

                    for (int k = 0; k < 2; k++) { Data[additionalRow[1, temp + k] * N * N + additionalRow[2, temp + k], temp + k] = 1; }
                    Data[additionalRow[1, temp] * N * N + (N - 1), temp] = 1;
                    Data[additionalRow[1, temp + 1] * N * N + N + additionalRow[2, temp + 1], temp + 1] = 1;

                    temp += 2;
                }
            }
            for (int i = 0; i < lowerRow.Length; i++)
            {
                if (lowerRow[i] != ' ')
                {
                    for (int k = 0; k < 2; k++)
                    {
                        additionalRow[0, temp + k] = 4;
                        additionalRow[1, temp + k] = i;
                        additionalRow[2, temp + k] = (int)(lowerRow[i] - 65);
                    }
                    for (int k = 0; k < 2; k++) { Data[additionalRow[1, temp + k] * N + N * N * (N - 1) + additionalRow[2, temp + k], temp + k] = 1; }
                    Data[N * N * (N - 1) + additionalRow[1, temp] * N + (N - 1), temp] = 1;
                    Data[N * N * (N - 2) + additionalRow[1, temp + 1] * N + additionalRow[2, temp + 1], temp + 1] = 1;

                    temp += 2;
                }
            }
            for (int i = 0; i < rightColumn.Length; i++)
            {
                if (rightColumn[i] != ' ')
                {
                    for (int k = 0; k < 2; k++)
                    {
                        additionalRow[0, temp + k] = 5;
                        additionalRow[1, temp + k] = i;
                        additionalRow[2, temp + k] = (int)(rightColumn[i] - 65);
                    }
                    for (int k = 0; k < 2; k++) { Data[N * (N - 1) + additionalRow[1, temp + k] * N * N + additionalRow[2, temp + k], temp + k] = 1; }
                    Data[N * (N - 1) + additionalRow[1, temp] * N * N + (N - 1), temp] = 1;
                    Data[N * (N - 2) + additionalRow[1, temp + 1] * N * N + additionalRow[2, temp + 1], temp + 1] = 1;
                    temp += 2;
                }
            }
            for (int j = 0; j < requestsCount; j++)
            {
                switch (additionalRow[0, j])
                {
                    case 0:
                        for (int i = 0; i < N; i++)
                        {
                            Data[N * i + additionalRow[1, j] * N * N + additionalRow[2, j], j] = 1;
                        }
                        break;
                    case 1:
                        for (int i = 0; i < N; i++)
                        {
                            Data[N * N * i + additionalRow[1, j] * N + additionalRow[2, j], j] = 1;
                        }
                        break;
                    case 6:
                        for (int i = 0; i < N; i++)
                        {
                            Data[additionalRow[1, j] * N * N + additionalRow[2, j] * N + i, j] = 1;
                        }
                        break;
                    default: break;
                }
            }
        }
    }
}
