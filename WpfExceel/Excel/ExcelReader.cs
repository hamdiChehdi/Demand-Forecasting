namespace WpfExceel.Excel
{
    
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Excel = Microsoft.Office.Interop.Excel;

    public static class ExcelReader
    {
        public static void ReadExcel()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            string strPath = Path.GetFullPath(Directory.GetCurrentDirectory());
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(strPath + "\\" + "TestWorkGroup.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            

            Object[,] sheetValues = (Object[,])xlRange.Value;
            int rowCount = sheetValues.GetLength(0);
            int colCount = sheetValues.GetLength(1);

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //new line
                    if (j == 1)
                        Console.Write("\r\n");

                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");

                    //add useful things here!   
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}

// https://www.vitoshacademy.com/c-loop-through-excel-files-and-get-data-from-them/
// https://www.youtube.com/watch?v=-KCmSr7tMKg
// https://www.dotnetcurry.com/wpf/988/read-write-excel-files-using-wpf
// https://coderwall.com/p/app3ya/read-excel-file-in-c
// https://www.codeproject.com/Tips/1232569/Importing-and-Exporting-DataTable-To-From-Excel-Fi