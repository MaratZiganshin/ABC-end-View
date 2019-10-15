using System;

namespace Generator
{
    public class DancingLinks
    {
        static Random random = new Random();
        //матрица 1 и 0 для данной задачи
        public Matrix basic { get; set; }
        Head root;//корень 
        Node[,] data;//матрица элементов типа Node
        Head[] head;//массив элементов типа Head
        Node[] solution;
        Solution[] solutions;
        public class Solution
        {
            public Node[] solutionList;
        }
        //количество решений
        public int countSolutions { get; set; }
        
        public DancingLinks(Matrix basic)
        {
            //заполнение структуры данных
            countSolutions = 0;
            this.basic = basic;
            data = new Node[basic.candidatesCount, basic.requestsCount];
            head = new Head[basic.requestsCount];
            solutions = new Solution[0];
            solution = new Node[0];
            root = new Head();
            for (int i = 0; i < basic.candidatesCount; i++)
            {
                for (int j = 0; j < basic.requestsCount; j++)
                {
                    if (basic.Data[i, j] == 1) { data[i, j] = new Node(); }
                }
            }
            for (int j = 0; j < basic.requestsCount; j++)
            {
                head[j] = new Head();
                head[j].left = head[j];
                head[j].right = head[j];
                int x = -1;
                do
                {
                    x++;
                    if (x > basic.candidatesCount - 1) break;
                } while (data[x, j] == null);
                head[j].down = (x > basic.candidatesCount - 1) ? head[j] : data[x, j];
                x = basic.candidatesCount;
                do
                {
                    x--;
                    if (x < 0) break;
                } while (data[x, j] == null);
                head[j].up = (x < 0) ? head[j] : data[x, j];
                head[j].rowNumber = -1;
                head[j].header = head[j];
                if (j == 0) head[j].leftHead = root;
                else head[j].leftHead = head[j - 1];
                head[j].leftHead.rightHead = head[j];
                if (j == basic.requestsCount - 1) { head[j].rightHead = root; }
                int s = 0;
                for (int i = 0; i < basic.candidatesCount; i++)
                {
                    if (data[i, j] != null) s++;
                }
                head[j].rowCount = s;
                head[j].columnNumber = j;
            }
            root.rightHead = head[0];
            root.leftHead = head[basic.requestsCount - 1];
            for (int i = 0; i < basic.candidatesCount; i++)
            {
                for (int j = 0; j < basic.requestsCount; j++)
                {
                    if (data[i, j] == null) continue;
                    int x = i;
                    int y = j;
                    //поиск левого элемента
                    do
                    {
                        y--;
                        if (y < 0) y = basic.requestsCount - 1;
                    } while (data[x, y] == null);
                    data[i, j].left = data[x, y];
                    //поиск правого элемента
                    x = i;
                    y = j;
                    do
                    {
                        y++;
                        if (y > basic.requestsCount - 1) y = 0;
                    } while (data[x, y] == null);
                    data[i, j].right = data[x, y];
                    //поиск верхнего элемента
                    x = i;
                    y = j;
                    do
                    {
                        x--;
                        if (x < 0) break;
                    } while (data[x, y] == null);
                    data[i, j].up = (x < 0) ? head[j] : data[x, y];
                    //поиск нижнего элемента
                    x = i;
                    y = j;
                    do
                    {
                        x++;
                        if (x > basic.candidatesCount - 1) break;
                    } while (data[x, y] == null);
                    data[i, j].down = (x > basic.candidatesCount - 1) ? head[j] : data[x, y];
                    //
                    data[i, j].rowNumber = i;
                    data[i, j].header = head[j];
                }
            }
        }
        //удаление столбца 
        public void Remove(Head h)
        {
            for (Node i = h.down; i != h; i = i.down)
            {
                for (Node j = i.right; j != i; j = j.right)
                {
                    j.down.up = j.up;
                    j.up.down = j.down;
                    j.header.rowCount--;
                }
            }
            h.rightHead.leftHead = h.leftHead;
            h.leftHead.rightHead = h.rightHead;
        }
        //возврат столбца
        public void Restore(Head h)
        {
            for (Node i = h.up; i != h; i = i.up)
            {
                for (Node j = i.left; j != i; j = j.left)
                {
                    j.header.rowCount++;
                    j.down.up = j;
                    j.up.down = j;
                }
            }
            h.rightHead.leftHead = h;
            h.leftHead.rightHead = h;
        }
        //поиск одного решения
        public bool Solver1()
        {
            if (root.rightHead == root) { return true; }
            Head h = ShortestHead();
            if (h.rowCount != 0)
            {
                Node n = h.down;
                while (n != h)
                {
                    Remove(h);
                    Array.Resize(ref solution, solution.Length + 1);
                    solution[solution.Length - 1] = n;
                    for (Node j = n.right; j != n; j = j.right) Remove(j.header);
                    if (Solver1()) return true;
                    Array.Resize(ref solution, solution.Length - 1);
                    for (Node j = n.left; j != n; j = j.left) Restore(j.header);
                    Restore(h);
                    n = n.down;
                }
            }
            return false;
        }
        //поиск случайного решения
        public bool Solver3()
        {
            if (root.rightHead == root) { return true; }
            Head h = RandomShortestHead();
            if (h.rowCount != 0)
            {
                int temp = random.Next(0, h.rowCount) - 1;
                Node n = h.down;
                for (int t = 0; t < temp; t++) { n = n.down; }
                while (n != h)
                {
                    Remove(h);
                    Array.Resize(ref solution, solution.Length + 1);
                    solution[solution.Length - 1] = n;
                    for (Node j = n.right; j != n; j = j.right) Remove(j.header);
                    if (Solver3()) return true;
                    Array.Resize(ref solution, solution.Length - 1);
                    for (Node j = n.left; j != n; j = j.left) Restore(j.header);
                    Restore(h);
                    n = n.down;
                }
            }
            return false;
        }
        //поиск всех решений
        public bool Solver2()
        {
            if (root.rightHead == root)
            {
                OutSolution();
                countSolutions++;
                Array.Resize(ref solutions, solutions.Length + 1);
                solutions[solutions.Length - 1] = new Solution();
                solutions[solutions.Length - 1].solutionList = solution;
                return true;
            }
            Head h = ShortestHead();
            if (h.rowCount != 0)
            {
                Node n = h.down;
                while (n != h)
                {
                    Remove(h);
                    Array.Resize(ref solution, solution.Length + 1);
                    solution[solution.Length - 1] = n;
                    for (Node j = n.right; j != n; j = j.right) Remove(j.header);
                    Solver2();
                    Array.Resize(ref solution, solution.Length - 1);
                    for (Node j = n.left; j != n; j = j.left) Restore(j.header);
                    Restore(h);
                    n = n.down;
                }
            }
            return false;
        }
        //поиск столбца с наименьшим количеством элементов Node
        public Head ShortestHead()
        {
            Head a = null;
            int min = int.MaxValue;
            for (Head h = root.rightHead; h != root; h = h.rightHead) { if (h.rowCount < min) { min = h.rowCount; a = h; } }
            return a;
        }
        //поиск случайного столбца с наименьшим количеством элементов Node
        public Head RandomShortestHead()
        {
            Head[] headList = new Head[0];
            int min = int.MaxValue;
            for (Head h = root.rightHead; h != root; h = h.rightHead) { if (h.rowCount < min) { min = h.rowCount; } }
            for (Head h = root.rightHead; h != root; h = h.rightHead) { if (h.rowCount == min) { Array.Resize(ref headList, headList.Length + 1); headList[headList.Length - 1] = h; } }
            int temp = random.Next(0, headList.Length);
            return headList[temp];
        }
        public Head[] ShortestHeads()
        {
            Head[] headList = new Head[0];
            int min = int.MaxValue;
            for (Head h = root.rightHead; h != root; h = h.rightHead) { if (h.rowCount < min) { min = h.rowCount; } }
            for (Head h = root.rightHead; h != root; h = h.rightHead) { if (h.rowCount == min) { Array.Resize(ref headList, headList.Length + 1); headList[headList.Length - 1] = h; } }
            return headList;
        }
        //вывод решения в виде матрицы букв
        public char[,] OutSolution()
        {
            char[,] a = new char[basic.N, basic.N];
            for (int i = 0; i < solution.Length; i++)
            {
                int x = basic.additionalColumn[solution[i].rowNumber, 0];
                int y = basic.additionalColumn[solution[i].rowNumber, 1];
                int g = basic.additionalColumn[solution[i].rowNumber, 2];
                if (g < basic.N-1) a[x, y] = (char)(g + 65);
                else a[x, y] = ' ';
            }
            return a;
        }
        //вывод всех решений в виде матрицы букв
        public char[][,] OutSolutions()
        {
            char[][,] m = new char[0][,];
            for (int k = 0; k < solutions.Length; k++)
            {
                char[,] a = new char[basic.N, basic.N];
                for (int i = 0; i < solutions[k].solutionList.Length; i++)
                {
                    int x = basic.additionalColumn[solutions[k].solutionList[i].rowNumber, 0];
                    int y = basic.additionalColumn[solutions[k].solutionList[i].rowNumber, 1];
                    int g = basic.additionalColumn[solutions[k].solutionList[i].rowNumber, 2];
                    if (g < basic.N-1) a[x, y] = (char)(g + 65);
                    else a[x, y] = ' ';
                }
                Array.Resize(ref m, m.Length + 1);
                m[m.Length - 1] = a;
            }
            return m;
        }
    }
}
