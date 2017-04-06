using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Web;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Net;
using System.Drawing;

namespace Common
{
    public class NPOI4ExcelHelper
    {
        private IWorkbook hssfworkbook = null;
        private string filePath;
        public NPOI4ExcelHelper(string filePath)
        {
            this.filePath = filePath;
            Init();
        }

        private void Init()
        {
            #region//初始化信息
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (filePath.Trim().ToLower().EndsWith(".xls"))
                    {
                        hssfworkbook = new HSSFWorkbook(file);
                    }
                    else if (filePath.Trim().ToLower().EndsWith(".xlsx"))
                    {
                        hssfworkbook = new XSSFWorkbook(file);
                    }
                    else if (filePath.Trim().ToLower().EndsWith(".csv"))
                    {
                        hssfworkbook = new XSSFWorkbook(file);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion
        }

        public ArrayList SheetNameForExcel()
        {
            if (hssfworkbook == null) { return null; };

            ArrayList names = new ArrayList();
            int i = hssfworkbook.NumberOfSheets;
            for (int j = 0; j < i; j++)
            {
                string sheet = hssfworkbook.GetSheetName(j);
                if (!names.Contains(sheet))
                {
                    names.Add(sheet);
                }
            }
            return names;
        }

        public DataTable SheetData(int sheetIndex = 0)
        {
            if (filePath.Trim().ToLower().EndsWith(".xls"))
            {
                return ExcelToTableForXLS(sheetIndex);
            }
            else if (filePath.Trim().ToLower().EndsWith(".xlsx"))
            {
                return ExcelToTableForXLSX(sheetIndex);
            }
            else if (filePath.Trim().ToLower().EndsWith(".csv"))
            {
                return ExcelToTableForXLSX(sheetIndex);
            }

            return null;
        }

        /// <summary>
        /// 讀取excel文件第一個sheet的資料  07
        /// </summary>
        /// <param name="filePath">文件路徑</param>
        /// <returns></returns>
        private DataTable ExcelToTableForXLSX(int sheetIndex = 0)
        {
            if (hssfworkbook == null) { return null; };

            ISheet sheet = hssfworkbook.GetSheetAt(sheetIndex);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            //第一行添加未table的列名
            //一行最后一个方格的编号 即总的列数
            rows.MoveNext();
            IRow headerRow = (XSSFRow)rows.Current;
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(headerRow.GetCell(j) != null ? headerRow.GetCell(j).StringCellValue.Trim() : "");
            }

            while (rows.MoveNext())
            {
                IRow row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < (row.LastCellNum <= dt.Columns.Count ? row.LastCellNum : dt.Columns.Count); i++)
                {
                    ICell cell = row.GetCell(i);

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        switch (cell.CellType)
                        {
                            case CellType.NUMERIC:
                                DateTime value = new DateTime();
                                if (DateTime.TryParse(cell.ToString(), out value))
                                    dr[i] = cell.DateCellValue.ToString("yyyy/MM/dd hh:mm:ss");
                                else
                                    dr[i] = cell.NumericCellValue.ToString();
                                break;
                            default:
                                dr[i] = cell.ToString().Trim();
                                break;
                        }

                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        //add by yafeng0715j 2015-09-11 PM
        /// <summary>
        /// 讀取excel文件第一個sheet的資料  07
        /// </summary>
        /// <param name="filePath">文件路徑</param>
        /// <returns></returns>
        public DataTable ExcelToTableForXLSX()
        {
            if (hssfworkbook == null) { return null; };

            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            //第一行添加未table的列名
            //一行最后一个方格的编号 即总的列数
            rows.MoveNext();
            IRow headerRow = (XSSFRow)rows.Current;
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(headerRow.GetCell(j) != null ? headerRow.GetCell(j).StringCellValue.Trim() : "");
            }

            while (rows.MoveNext())
            {
                IRow row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < (row.LastCellNum <= dt.Columns.Count ? row.LastCellNum : dt.Columns.Count); i++)
                {
                    ICell cell = row.GetCell(i);

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString().Trim();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 讀取excel文件第一個sheet的資料  03
        /// </summary>
        /// <param name="filePath">文件路徑</param>
        /// <returns></returns>
        private DataTable ExcelToTableForXLS(int sheetIndex = 0)
        {
            if (hssfworkbook == null) { return null; };

            ISheet sheet = hssfworkbook.GetSheetAt(sheetIndex);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            //第一行添加未table的列名
            //一行最后一个方格的编号 即总的列数
            rows.MoveNext();
            IRow headerRow = (HSSFRow)rows.Current;
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(headerRow.GetCell(j) != null ? headerRow.GetCell(j).StringCellValue.Trim() : "");
            }

            while (rows.MoveNext())
            {
                IRow row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < (row.LastCellNum <= dt.Columns.Count ? row.LastCellNum : dt.Columns.Count); i++)
                {
                    ICell cell = row.GetCell(i);

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        switch (cell.CellType)
                        {
                            case CellType.NUMERIC:
                                DateTime value = new DateTime();
                                if (DateTime.TryParseExact(cell.ToString(), "M/d/y H:m", null, System.Globalization.DateTimeStyles.None, out value)
                                    || DateTime.TryParse(cell.ToString(), out value))
                                    dr[i] = cell.DateCellValue.ToString("yyyy/MM/dd HH:mm:ss");//將Excel單元格中的時間轉為24小時制   jinfeng 
                                else
                                    dr[i] = cell.ToString();
                                break;
                            default:
                                dr[i] = cell.ToString().Trim();
                                break;
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
