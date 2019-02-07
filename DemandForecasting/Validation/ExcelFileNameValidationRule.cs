namespace DemandForecasting.Validation
{
    using System.Globalization;
    using System.IO;
    using System.Windows.Controls;

    public class ExcelFileNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Empty");
            }

            string path = value.ToString();

            if (path.Length < 5)
            {
                return new ValidationResult(false, "Invalid file path");
            }

            if (!File.Exists(path))
            {
                return new ValidationResult(false, "File does not exist.");
            }

            string extension = Path.GetExtension(path);

            if (extension != ".xls" && extension != ".xlsx")
            {
                return new ValidationResult(false, "Wrong extension should be Excel file (.xls, .xlsx).");
            }

            return ValidationResult.ValidResult;
        }
    }
}
