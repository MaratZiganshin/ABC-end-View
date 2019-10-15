namespace Generator
{
    public class Node
    {
        public Node right { get; set; }//ссылка на правый элемент
        public Node left { get; set; }//ссылка на левый элемент
        public Node up { get; set; }//ссылка на верхний элемент
        public Node down { get; set; }//ссылка на нижний элемент
        public Head header { get; set; }//ссылка на элемент типа Head сверху
        public int rowNumber { get; set; }//номер строки
    }
    public class Head : Node
    {
        public Head leftHead { get; set; }//ссылка на элемент типа Head слева
        public Head rightHead { get; set; }//ссылка на элемент типа Head справа
        public int rowCount { get; set; }//количество элементов типа Node в столбце
        public int columnNumber { get; set; }//номер столбца
    }
}
