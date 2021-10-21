using System.Text;

namespace BarcodeHelper
{
    public class BarcodeObject
    {
        private int digit1 = 0;
        private int digit2 = 0;
        private int digit3 = 0;
        private int digit4 = 0;
        private int digit5 = 0;
        private int digit6 = 0;
        private int digit7 = 0;
        private int digit8 = 0;
        private int digit9 = 0;
        private int digit10 = 0;
        private int digit11 = 0;

        public int? UnknownDigitIndex { get; set; } = null;

        public BarcodeObject() { }

        public BarcodeObject(int[] digits)
        {
            if (digits == null || digits.Length != 11)
            {
                throw new ArgumentException($"{nameof(digits)} length must be 11");
            }
            for (int i = 0; i < 11; i++)
            {
                this[i] = digits[i];
            }
        }

        public int this[int i]
        {
            get => i switch
            {
                0 => digit1,
                1 => digit2,
                2 => digit3,
                3 => digit4,
                4 => digit5,
                5 => digit6,
                6 => digit7,
                7 => digit8,
                8 => digit9,
                9 => digit10,
                10 => digit11,
                _ => 0
            };
            set
            {
                switch (i)
                {
                    case 0: digit1 = value; break;
                    case 1: digit2 = value; break;
                    case 2: digit3 = value; break;
                    case 3: digit4 = value; break;
                    case 4: digit5 = value; break;
                    case 5: digit6 = value; break;
                    case 6: digit7 = value; break;
                    case 7: digit8 = value; break;
                    case 8: digit9 = value; break;
                    case 9: digit10 = value; break;
                    case 10: digit11 = value; break;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < 11; i++)
            {
                sb.Append(i == UnknownDigitIndex ? "?" : i switch
                {
                    1 or 6 => $" {this[i]}",
                    0 or 2 or 3 or 4 or 5 or 7 or 8 or 9 or 10 => $"{this[i]}",
                    _ => string.Empty
                });
            }
            return sb.ToString();
        }
    }
}
