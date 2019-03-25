using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OutReachBusinessLayer
{
    public class ExportExcel
    {
        public void ReadExcelIntoDatatable(string excelPath)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(excelPath, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();

                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }

                    foreach (Row row in rows)
                    {
                        DataRow tempRow = dt.NewRow();

                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {
                            tempRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                        }

                        dt.Rows.Add(tempRow);
                    }

                }
                dt.Rows.RemoveAt(0);

                ExcelToDB excelToDB = new ExcelToDB();
                int q = excelPath.LastIndexOf('\\') + 1;
                excelToDB.FindTableName(excelPath.Substring(q), dt);
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "ExportExcel",
                    ActionrName = "ReadExcelIntoDatatable",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }           
        }
        public string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            try
            {
                SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
                if (cell.CellValue != null)
                {
                    string value = cell.CellValue.InnerXml;

                    if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                    {
                        return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }
    }
}
