namespace BarcodeHelper.Pages;

public partial class Index
{
    private UpcEditModel UpcEditModel { get; } = new() { UpcCode = string.Empty };

    private string CompletedUpcCode { get; set; } = string.Empty;

    private readonly IMask digitMask = new RegexMask(@"^[0-9?]{0,12}$");

    private static IEnumerable<string> ValidateUpc(string ch) =>
        ch.Select(c => c switch
        {
            _ when c is not (>= '0' and <= '9') and not '?' => "Only digits and '?' allowed.",
            _ when ch is not { Length: 12 } => "Must be exactly 12 digits.",
            _ when !ch.Any(x => x is '?') => "Please use '?' for the missing digit.",
            _ when ch.Count(x => x is '?') > 1 => "Only one '?' allowed.",
            _ => string.Empty,
        })
        .Where(error => error is { Length: > 0 })
        .Distinct();

    private bool UpcIsInvalid => string.IsNullOrEmpty(UpcEditModel.UpcCode) || ValidateUpc(UpcEditModel.UpcCode).Any();

    private void SolveMissingDigit()
    {
        if (!UpcEditModel.UpcCode.Contains('?'))
        {
            DialogService.ShowMessageBox("Error", "Invalid input: No missing digit found.");
            return;
        }

        var calculatedDigit = CalculateMissingDigit(UpcEditModel.UpcCode);
        CompletedUpcCode = UpcEditModel.UpcCode.Replace('?', Convert.ToChar(calculatedDigit + '0'));
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
