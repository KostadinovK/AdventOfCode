namespace _04_12
{
    public class BingoNumber
    {
        public int Number { get; set; }

        public bool IsMarked { get; set; }

        public BingoNumber(int num)
        {
            Number = num;
            IsMarked = false;
        }
    }
}
