namespace BarcodeHelper.Pages;

public partial class Index
{
    private string UpcCode { get; set; } = string.Empty;

    private string CompletedUpcCode { get; set; } = string.Empty;

    private void SolveMissingDigit()
    {
        if (!UpcCode.Contains('?'))
        {
            DialogService.ShowMessageBox("Error", "Invalid input: No missing digit found.");
            return;
        }

        var calculatedDigit = CalculateMissingDigit(UpcCode);
        CompletedUpcCode = UpcCode.Replace('?', Convert.ToChar(calculatedDigit + '0'));
    }

    private static int CalculateMissingDigit(string upcCode)
    {
        var bestModulo = -1;
        var bestDigit = -1;

        for (var missingDigit = 0; missingDigit <= 9; missingDigit++)
        {
            var testUpcCode = upcCode.Replace('?', Convert.ToChar(missingDigit + '0'));
            var sumEven = 0;
            var sumOdd = 0;

            for (var i = 0; i < testUpcCode.Length; i++)
            {
                var digit = testUpcCode[i] - '0';

                if (i % 2 == 0)
                {
                    sumOdd += digit;
                }
                else
                {
                    sumEven += digit;
                }
            }

            var total = sumOdd * 3 + sumEven;
            var modulo = total % 10;

            if (modulo == 0)
            {
                bestDigit = missingDigit;
                break;
            }
            else if (bestModulo == -1 || modulo < bestModulo)
            {
                bestModulo = modulo;
                bestDigit = missingDigit;
            }
        }

        return bestDigit;
    }
}
