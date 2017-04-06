using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public static class TempleteExcelhelper
    {
            /// <summary>
            /// 利用模板，DataTable导出到Excel（单个类别）
            /// </summary>
            /// <param name="dtSource">DataTable</param>
            /// <param name="strFileName">生成的文件路径、名称</param>
            /// <param name="strTemplateFileName">模板的文件路径、名称</param>
            /// <param name="flg">文件标识（1：经营贸易情况/2：生产经营情况/3：项目投资情况/4：房产销售情况/其他：总表）</param>
            /// <param name="titleName">表头名称</param>
            //public static void ExportExcelForDtByNPOI(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName)
            //{
            //    // 利用模板，DataTable导出到Excel（单个类别）
            //    using (MemoryStream ms = ExportExcelForDtByNPOI(dtSource, strTemplateFileName, flg, titleName))
            //    {
            //        byte[] data = ms.ToArray();

            //        #region 客户端保存
            //        HttpResponse response = System.Web.HttpContext.Current.Response;
            //        response.Clear();
            //        response.Charset = "UTF-8";
            //        response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
            //        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
            //        System.Web.HttpContext.Current.Response.BinaryWrite(data);
            //        #endregion
            //    }
            //}

            /// <summary>
            /// 利用模板，DataTable导出到Excel（单个类别）
            /// </summary>
            /// <param name="dtSource">DataTable</param>
            /// <param name="strTemplateFileName">模板的文件路径、名称</param>
            /// <param name="flg">文件标识--sheet名（1：经营贸易情况/2：生产经营情况/3：项目投资情况/4：房产销售情况/其他：总表）</param>
            /// <param name="titleName">表头名称</param>
            /// <returns></returns>
            //private static MemoryStream ExportExcelForDtByNPOI(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
            //{
            //    #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            //    #endregion
            //    int rowIndex = 2;       // 起始行
            //    int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            //    FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            //    HSSFWorkbook workbook = new HSSFWorkbook(file);
            //    string sheetName = "";
            //    switch (flg)
            //    {
            //        case 1:
            //            sheetName = "abc";
            //            break;
            //    }
            //    HSSFSheet sheet = workbook.GetSheet(sheetName);

            //    #region 右击文件 属性信息
            //    //{
            //    //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    //    dsi.Company = "农发集团";
            //    //    workbook.DocumentSummaryInformation = dsi;

            //    //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    //    si.Author = "农发集团"; //填加xls文件作者信息
            //    //    si.ApplicationName = "瞬时达"; //填加xls文件创建程序信息
            //    //    si.LastAuthor = "瞬时达"; //填加xls文件最后保存者信息
            //    //    si.Comments = "瞬时达"; //填加xls文件作者信息
            //    //    si.Title = "农发集团报表"; //填加xls文件标题信息
            //    //    si.Subject = "农发集团报表";//填加文件主题信息
            //    //    si.CreateDateTime = DateTime.Now;
            //    //    workbook.SummaryInformation = si;
            //    //}
            //    #endregion

            //    #region 表头
            //    HSSFRow headerRow = sheet.GetRow(0);
            //    HSSFCell headerCell = headerRow.GetCell(0);
            //    headerCell.SetCellValue(titleName);
            //    #endregion


            //    foreach (DataRow row in dtSource.Rows)
            //    {
            //        #region 填充内容
            //        HSSFRow dataRow = sheet.GetRow(rowIndex);
            //        if (dataRow == null)
            //        {
            //            dataRow = sheet.CreateRow(rowIndex);
            //        }
            //        int columnIndex = 0;        // 开始列（0为标题列，从1开始）
            //        foreach (DataColumn column in dtSource.Columns)
            //        {
            //            // 列序号赋值
            //            if (columnIndex >= dtSource.Columns.Count)
            //                break;
            //            HSSFCell newCell = dataRow.GetCell(columnIndex);
            //            if (newCell == null)
            //            {
            //                newCell = dataRow.CreateCell(columnIndex);
            //            }

            //            string drValue = row[column].ToString();

            //            switch (column.DataType.ToString())
            //            {
            //                case "System.String"://字符串类型
            //                    newCell.SetCellValue(drValue);
            //                    break;
            //                case "System.DateTime"://日期类型
            //                    DateTime dateV;
            //                    DateTime.TryParse(drValue, out dateV);
            //                    newCell.SetCellValue(dateV);

            //                    break;
            //                case "System.Boolean"://布尔型
            //                    bool boolV = false;
            //                    bool.TryParse(drValue, out boolV);
            //                    newCell.SetCellValue(boolV);
            //                    break;
            //                case "System.Int16"://整型
            //                case "System.Int32":
            //                case "System.Int64":
            //                case "System.Byte":
            //                    int intV = 0;
            //                    int.TryParse(drValue, out intV);
            //                    newCell.SetCellValue(intV);
            //                    break;
            //                case "System.Decimal"://浮点型
            //                case "System.Double":
            //                    double doubV = 0;
            //                    double.TryParse(drValue, out doubV);
            //                    newCell.SetCellValue(doubV);
            //                    break;
            //                case "System.DBNull"://空值处理
            //                    newCell.SetCellValue("");
            //                    break;
            //                default:
            //                    newCell.SetCellValue("");
            //                    break;
            //            }
            //            columnIndex++;
            //        }
            //        #endregion

            //        rowIndex++;
            //    }

            //    // 格式化当前sheet，用于数据total计算
            //    sheet.ForceFormulaRecalculation = true;

            //    #region Clear "0"
            //    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //    int cellCount = headerRow.LastCellNum;

            //    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //    {
            //        HSSFRow row = sheet.GetRow(i);
            //        if (row != null)
            //        {
            //            for (int j = row.FirstCellNum; j < cellCount; j++)
            //            {
            //                HSSFCell c = row.GetCell(j);
            //                if (c != null)
            //                {
            //                    switch (c.CellType)
            //                    {
            //                        case HSSFCellType.NUMERIC:
            //                            if (c.NumericCellValue == 0)
            //                            {
            //                                c.SetCellType(HSSFCellType.STRING);
            //                                c.SetCellValue(string.Empty);
            //                            }
            //                            break;
            //                        case HSSFCellType.BLANK:

            //                        case HSSFCellType.STRING:
            //                            if (c.StringCellValue == "0")
            //                            { c.SetCellValue(string.Empty); }
            //                            break;

            //                    }
            //                }
            //            }

            //        }

            //    }
            //    #endregion

            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        workbook.Write(ms);
            //        ms.Flush();
            //        ms.Position = 0;
            //        sheet = null;
            //        workbook = null;
            //        return ms;
            //    }
            //}
    }
}
