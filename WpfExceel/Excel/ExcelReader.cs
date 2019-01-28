namespace ForecastingDemand.Excel
{
    
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using Excel = Microsoft.Office.Interop.Excel;
    using ForecastingDemand.Model;

    public static class ExcelReader
    {
        public static OperationResult LoadExcel(string filePath, List<Demand> demands)
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = null;
            Excel._Worksheet xlWorksheet = null;
            Excel.Range xlRange = null;

            try
            {
               xlWorkbook = xlApp.Workbooks.Open(filePath);
            }
            catch (Exception ex)
            {
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                Console.WriteLine("Unable to open the excel file, Log Exception : " + ex.InnerException);
                return OperationResult.ExceptionResult(ex);
            }

            try
            {
                xlWorksheet = xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;

                Object[,] sheetValues = (Object[,])xlRange.Value;
                int rowCount = sheetValues.GetLength(0);
                int colCount = sheetValues.GetLength(1);

                if (colCount < 2)
                {
                    return OperationResult.FailureResult("The excel sheet should have two numeric columns [Period, Demand]");
                }

                if (rowCount < 2)
                {
                    return OperationResult.FailureResult("The excel sheet should have at least two rows");
                }

                for (int i = 1; i <= rowCount; i++)
                {
                    Demand demand = new Demand();

                    if (xlRange.Cells[i, 1] == null || xlRange.Cells[i, 1].Value2 == null)
                    {
                        return OperationResult.FailureResult("The cell [" + i + ", " + 1 + "] is empty");
                    }

                    if (xlRange.Cells[i, 2] == null || xlRange.Cells[i, 2].Value2 == null)
                    {
                        return OperationResult.FailureResult("The cell [" + i + ", " + 2 + "] is empty");
                    }

                    demand.period = (int)xlRange.Cells[i, 1].Value2;
                    demand.quantity = xlRange.Cells[i, 2].Value2;
                    demands.Add(demand);
                }

            }
            catch (Exception ex)
            {
                return OperationResult.ExceptionResult(ex);
            }
            finally
            {
                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

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

            return OperationResult.SuccessResult();
        }
    }
}

// https://www.vitoshacademy.com/c-loop-through-excel-files-and-get-data-from-them/
// https://www.youtube.com/watch?v=-KCmSr7tMKg
// https://www.dotnetcurry.com/wpf/988/read-write-excel-files-using-wpf
// https://coderwall.com/p/app3ya/read-excel-file-in-c
// https://www.codeproject.com/Tips/1232569/Importing-and-Exporting-DataTable-To-From-Excel-Fi