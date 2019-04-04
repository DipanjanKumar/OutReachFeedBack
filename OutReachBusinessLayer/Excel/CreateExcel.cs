using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using OutReachDTO.DTO;
using System;
using System.IO;
using System.Linq;

namespace OutReachBusinessLayer.Excel
{
    public class CreateExcel
    {
        WorkbookPart wbPart = null;
        public string WriteToExcel(ReportDTO reportDTO, string templatePath, string exportpath, string EventName)
        {
            try
            {
                string UrlPath = "/ExcelReports/";
                string reportName = "EventSummary_" + EventName + ".xlsx";
                string sheetName = "";
                UInt32 counter = 0;
                File.Copy(templatePath, (exportpath + reportName), true);
                using (SpreadsheetDocument document = SpreadsheetDocument.Open((exportpath + reportName), true))
                {
                    wbPart = document.WorkbookPart;
                    counter = 2;
                    foreach (RegisteredReportDTO registeredReportDTO in reportDTO.RegisteredReportDTOs)
                    {
                        sheetName = ConstantValues.Participated;
                        UpdateValue(sheetName, "A", counter, registeredReportDTO.EventName, true);
                        UpdateValue(sheetName, "B", counter, registeredReportDTO.BeneficaryName, true);
                        UpdateValue(sheetName, "C", counter, registeredReportDTO.EventDate, true);
                        UpdateValue(sheetName, "D", counter, registeredReportDTO.Location, true);
                        UpdateValue(sheetName, "E", counter, registeredReportDTO.EmployeeID, false);
                        UpdateValue(sheetName, "F", counter, registeredReportDTO.FeedbackTextNumber1, false);
                        UpdateValue(sheetName, "G", counter, registeredReportDTO.FeedbackTextNumber2, true);
                        UpdateValue(sheetName, "H", counter, registeredReportDTO.FeedbackTextNumber3, true);
                        counter++;
                    }
                    counter = 2;
                    foreach (NotAttendedReportDTO notAttendedReportDTO in reportDTO.NotAttendedReportDTOs)
                    {
                        sheetName = ConstantValues.NotParticipated;
                        UpdateValue(sheetName, "A", counter, notAttendedReportDTO.EventName, true);
                        UpdateValue(sheetName, "B", counter, notAttendedReportDTO.BeneficaryName, true);
                        UpdateValue(sheetName, "C", counter, notAttendedReportDTO.EventDate, true);
                        UpdateValue(sheetName, "D", counter, notAttendedReportDTO.Location, true);
                        UpdateValue(sheetName, "E", counter, notAttendedReportDTO.EmployeeID, false);
                        UpdateValue(sheetName, "F", counter, notAttendedReportDTO.FeedbackText, true);
                        counter++;
                    }
                    counter = 2;
                    foreach (UnregisteredReportDTO unregisteredReportDTO in reportDTO.UnregisteredReportDTOs)
                    {
                        sheetName = ConstantValues.UnRegistered;
                        UpdateValue(sheetName, "A", counter, unregisteredReportDTO.EventName, true);
                        UpdateValue(sheetName, "B", counter, unregisteredReportDTO.BeneficaryName, true);
                        UpdateValue(sheetName, "C", counter, unregisteredReportDTO.EventDate, true);
                        UpdateValue(sheetName, "D", counter, unregisteredReportDTO.Location, true);
                        UpdateValue(sheetName, "E", counter, unregisteredReportDTO.EmployeeID, false);
                        UpdateValue(sheetName, "F", counter, unregisteredReportDTO.FeedbackText, true);
                        counter++;
                    }
                    document.Close();
                }
                UrlPath = (UrlPath + reportName);
                return UrlPath;
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "CreateExcel",
                    ActionrName = "WriteToExcel",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }            
        }
        private bool UpdateValue(string sheetName, string colName, uint rowIndex, string value, bool isString)
        {
            try
            {
                bool updated = false;
                Sheet CurrentSheet = wbPart.Workbook.Descendants<Sheet>().Where(
                     (s) => s.Name == sheetName).FirstOrDefault();
                if (CurrentSheet != null)
                {
                    WorksheetPart worksheetPart = wbPart.GetPartById(CurrentSheet.Id.Value) as WorksheetPart;                    
                    Cell cell = InsertCellInWorksheet(colName, rowIndex, worksheetPart);
                    if (isString)
                    {
                        int stringIndex = InsertSharedStringItem(wbPart, value);
                        cell.DataType= new EnumValue<CellValues>(CellValues.SharedString);
                        cell.CellValue = new CellValue(stringIndex.ToString());
                        cell.CellValue.Text = stringIndex.ToString();
                    }
                    else
                    {
                        cell.CellValue = new CellValue(value);
                        cell.DataType = CellValues.Number;
                    }
                    worksheetPart.Worksheet.Save();
                    updated = true;
                }
                return updated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            try
            {
                Worksheet worksheet = worksheetPart.Worksheet;
                SheetData sheetData = worksheet.GetFirstChild<SheetData>();
                string cellReference = columnName + rowIndex;
                Row row;
                if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
                {
                    row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
                }
                else
                {
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);
                }
                if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
                {
                    return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
                }
                else
                {
                    Cell refCell = null;
                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        if (cell.CellReference.Value.Length == cellReference.Length)
                        {
                            if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                            {
                                refCell = cell;
                                break;
                            }
                        }
                    }
                    Cell newCell = new Cell() { CellReference = cellReference };
                    row.InsertBefore(newCell, refCell);

                    worksheet.Save();
                    return newCell;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }        
        private int InsertSharedStringItem(WorkbookPart wbPart, string value)
        {
            try
            {
                int index = 0;
                bool found = false;
                var stringTablePart = wbPart
                    .GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                if (stringTablePart == null)
                {
                    stringTablePart = wbPart.AddNewPart<SharedStringTablePart>();
                }
                var stringTable = stringTablePart.SharedStringTable;
                if (stringTable == null)
                {
                    stringTable = new SharedStringTable();
                }
                foreach (SharedStringItem item in stringTable.Elements<SharedStringItem>())
                {
                    if (item.InnerText == value)
                    {
                        found = true;
                        break;
                    }
                    index += 1;
                }
                if (!found)
                {
                    stringTable.AppendChild(new SharedStringItem(new Text(value)));
                    stringTable.Save();
                }
                return index;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }   
    }
}