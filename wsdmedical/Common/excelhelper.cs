using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Linq;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.Record;//NPOI.HSSF.Record.Formula.Eval改为了NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Eval;//同上
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.SS;
using NPOI.DDF;
using NPOI.SS.Util;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;//2007
using NPOI.XSSF.Util;
using System.Collections;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Xml.Linq;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace Common
{
    /// <summary>
    /// 这个类是主要解决
    /// </summary>
    public class NpoiMemoryStream : MemoryStream
    {
        public NpoiMemoryStream()
        {
            AllowClose = true;
        }

        public bool AllowClose { get; set; }

        public override void Close()
        {
            if (AllowClose)
                base.Close();
        }
    }
    public class ExcelHelper
    {
        public static bool isNumeric(String message, out double result)
        {
            Regex rex = new Regex(@"^[-]?\d+[.]?\d*$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = double.Parse(message);
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 导出
        /// <summary>
        /// [datatable导出问题]DataTable导出到Excel的MemoryStream--验证
        /// 读出memorystram后的代码Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));Response.BinaryWrite(ms.ToArray());
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream ExportJcDT(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();


            #region 右击文件 属性信息

            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "http://www.baidu.com/";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "宋东亚"; //填加xls文件作者信息
                si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
                si.LastAuthor = "宋东亚"; //填加xls文件最后保存者信息
                si.Comments = "说明信息"; //填加xls文件作者信息
                si.Title = "NPOI测试"; //填加xls文件标题信息
                si.Subject = "NPOI测试Demo"; //填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }

            #endregion

            HSSFSheet sheet = null;
            int sheetCount = 0;
            sheet = workbook.CreateSheet("Sheet" + sheetCount) as HSSFSheet;

            #region  设置样式
            HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
            HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            HSSFCellStyle mycellstyle = GetIRowStyle(workbook);//设置标题样式

            HSSFCellStyle mytableLinestyle = GetTableLineStyle(workbook);//设置表列样式

            HSSFCellStyle cellStyleContent = GetCellStyle(workbook, true, false, false, 0, 0, 0);//显示列
            #endregion

            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns) //不要设置列名称
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;//獲取要填充到單元格中內容的長度
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])   //记录列长度
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }

            int rowIndex = 0;

            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                //分頁
                if (rowIndex % 65536 == 0 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheetCount++;
                        sheet = workbook.CreateSheet("Sheet" + sheetCount) as HSSFSheet;
                        rowIndex = 0;
                    }
                    #region 表头及样式
                    if ("" != strHeaderText)
                    {
                        HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);
                        headerRow.GetCell(0).CellStyle = mycellstyle;
                        rowIndex++;
                    }
                    #endregion
                    #region 列头及样式
                    {
                        HSSFRow headerRow = sheet.CreateRow(rowIndex) as HSSFRow;

                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = mytableLinestyle;
                            if (arrColWidth[column.Ordinal] > 100)
                            {
                                sheet.SetColumnWidth(column.Ordinal, 100 * 256);
                            }
                            else
                            {
                                sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                            }
                            headerRow.Height = 30 * 18;
                        }
                    }
                    #endregion
                    rowIndex++;
                    SetCellRangeAddress(sheet, 0, 0, 0, 3);
                    //SetCellRangeAddress(sheet, 1, 2, 0, 0);
                    //SetCellRangeAddress(sheet, 1, 2, 1, 1);
                    //SetCellRangeAddress(sheet, 1, 2, 2, 2);
                    //SetCellRangeAddress(sheet, 1, 1, 3, 7);
                    //SetCellRangeAddress(sheet, 1, 1, 8, 12);
                    //SetCellRangeAddress(sheet, 1, 1, 13, 17);
                    //SetCellRangeAddress(sheet, 1, 1, 18, 19);
                    //SetCellRangeAddress(sheet, 1, 2, 20, 20);
                }
                #endregion

                #region 填充内容
                HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                dataRow.Height = 30 * 12;
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
                    string drValue = row[column].ToString();
                    newCell.CellStyle = cellStyleContent;
                    switch (column.DataType.ToString()) //根據數據類型填充單元格
                    {
                        case "System.String": //字符串类型
                            double result;
                            if (isNumeric(drValue, out result))
                            {
                                double.TryParse(drValue, out result);
                                newCell.SetCellValue(result);
                            }
                            else
                            {
                                newCell.SetCellValue(drValue);
                            }
                            break;
                        case "System.DateTime": //日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);
                            newCell.CellStyle = dateStyle; //格式化显示
                            break;
                        case "System.Boolean": //布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull": //空值处理
                            newCell.SetCellValue("");
                            break;
                        case "System.UInt16":
                        case "System.UInt32":
                        case "System.UInt64":
                            uint uintV = 0;
                            uint.TryParse(drValue, out uintV);
                            newCell.SetCellValue(uintV);
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }
                #endregion
                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }

        /// <summary>
        /// [汇出excel的多个sheet时用到]DataTable导出到Excel的MemoryStream--验证
        /// [读出后memorystream后,直接加上Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName); Response.BinaryWrite(ms.ToArray());]--验证
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">SheetName</param>
        public static MemoryStream ExportDTNoColumnsBySdy(List<DataTable> dtSource, List<string> SheetName, List<bool> ColumnsName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            for (int k = 0; k < dtSource.Count; k++)
            {
                HSSFSheet sheet = workbook.CreateSheet(SheetName[k].ToString()) as HSSFSheet;
                HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                //取得列宽
                int[] arrColWidth = new int[dtSource[k].Columns.Count];
                foreach (DataColumn item in dtSource[k].Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dtSource[k].Rows.Count; i++)
                {
                    for (int j = 0; j < dtSource[k].Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource[k].Rows[i][j].ToString()).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                int rowIndex = 0;

                foreach (DataRow row in dtSource[k].Rows)
                {
                    #region 新建表，填充表头，填充列头，样式
                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        if (rowIndex != 0)
                        {
                            sheet = workbook.CreateSheet() as HSSFSheet;
                        }
                        #region 列头及样式
                        if (ColumnsName[k])
                        {
                            HSSFRow headerRow = sheet.CreateRow(rowIndex) as HSSFRow;
                            HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                            HSSFFont font = workbook.CreateFont() as HSSFFont;
                            font.FontHeightInPoints = 10;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            foreach (DataColumn column in dtSource[k].Columns)
                            {
                                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

                                //设置列宽
                                if (arrColWidth[column.Ordinal] > 100)
                                {
                                    sheet.SetColumnWidth(column.Ordinal, 100 * 256);
                                }
                                else
                                {
                                    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                                }
                            };
                        }
                        #endregion
                        rowIndex++;
                    }
                    #endregion

                    #region 填充内容

                    HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                    foreach (DataColumn column in dtSource[k].Columns)
                    {
                        HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
                        string drValue = row[column].ToString();
                        switch (column.DataType.ToString())
                        {
                            case "System.String": //字符串类型
                                double result;
                                if (isNumeric(drValue, out result))
                                {
                                    double.TryParse(drValue, out result);
                                    newCell.SetCellValue(result);
                                }
                                else
                                {
                                    newCell.SetCellValue(drValue);
                                }
                                break;
                            case "System.DateTime": //日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle; //格式化显示
                                break;
                            case "System.Boolean": //布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16": //整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                            case "System.UInt32":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal": //浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull": //空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }
                    #endregion
                    rowIndex++;
                }
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }

        #endregion

        #region npoi 导出样式
        #region 設置樣式
        /// <summary>
        /// 設置樣式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="isBorder">是否有邊框</param>
        /// <param name="isBackColer">是否有背景顏色</param>
        /// <param name="setFont">設置字體</param>
        /// <param name="fintSize">字體大小</param>
        /// <param name="clor">顏色</param>
        /// <param name="newFontSize">新字體大小</param>
        /// <returns></returns>
        public static HSSFCellStyle GetCellStyle(HSSFWorkbook workbook, bool isBorder, bool isBackColer, bool setFont, short fintSize, short clor, short newFontSize)
        {
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontName = "微软雅黑";//設置字體
            font.FontHeightInPoints = fintSize;//設置字體大小
            font.Color = HSSFColor.BLACK.index;//字體顏色
            font.Boldweight = 18;//加粗
            if (newFontSize > 0)
            {
                font.FontHeightInPoints = newFontSize;
            }

            HSSFCellStyle cellStyle = (NPOI.HSSF.UserModel.HSSFCellStyle)workbook.CreateCellStyle();//創建Excel樣式
            cellStyle.WrapText = true;//是否自動換行
            cellStyle.VerticalAlignment = VerticalAlignment.CENTER;//垂直居中
            cellStyle.Alignment = HorizontalAlignment.CENTER;

            if (isBackColer)
            {
                cellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                cellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;//填充模式 
                cellStyle.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;//填充色彩
            }
            if (setFont)
            {
                cellStyle.SetFont(font);
            }
            else
            {
                if (clor > 0 || newFontSize > 0)
                {

                    HSSFFont fontN = (HSSFFont)workbook.CreateFont();

                    if (clor > 0)
                    {
                        fontN.Color = clor;//字體顏色
                    }
                    if (newFontSize > 0)
                    {
                        fontN.FontHeightInPoints = newFontSize;
                        fontN.Boldweight = 20;//加粗
                    }
                    cellStyle.SetFont(fontN);
                }

            }//添加字體樣式

            if (isBorder)
            {
                //設置邊框樣式
                cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                //边框颜色  
                cellStyle.BottomBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;
                cellStyle.TopBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;
                cellStyle.LeftBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;
                cellStyle.RightBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;
            }
            return cellStyle;

        }
        #endregion

        /// <summary>
        /// 合并单元格--add by sdy --2016-10-31
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        #region 表头样式
        public static HSSFCellStyle GetIRowStyle(HSSFWorkbook hssfworkbook)
        {
            HSSFCellStyle headStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;

            HSSFFont font = hssfworkbook.CreateFont() as HSSFFont;
            font.FontHeightInPoints = 20;
            font.Boldweight = 700;
            headStyle.SetFont(font);
            return headStyle;
        }
        #endregion

        #region 表列样式
        public static HSSFCellStyle GetTableLineStyle(HSSFWorkbook hssfworkbook)
        {
            HSSFCellStyle headStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            headStyle.VerticalAlignment = VerticalAlignment.CENTER;//垂直居中
            //設置邊框樣式
            headStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            headStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            headStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            headStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //边框颜色  
            headStyle.BottomBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;
            headStyle.TopBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;
            headStyle.LeftBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;
            headStyle.RightBorderColor = HSSFColor.OLIVE_GREEN.BLACK.index;

            //设置背景颜色
            headStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            headStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;//填充模式 
            headStyle.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;//填充色彩

            HSSFFont font = hssfworkbook.CreateFont() as HSSFFont;
            font.FontHeightInPoints = 10;
            font.Boldweight = 700;
            headStyle.SetFont(font);
            return headStyle;
        }
        #endregion

        #endregion

        /// <summary>
        /// [把datatable直接汇出到某个位置]DataTable导出到Excel文件--验证
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void ExportDTtoExcel2(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = ExportJcDT(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }


        /// <summary>
        /// 通过模板导出--验证
        /// </summary>
        public static void TemplateExportDTtoExcel(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName)
        {
            if (strTemplateFileName.Trim().ToLower().EndsWith(".xls"))
            {
                using (MemoryStream ms = ExportExcelForDtByNPOI2003(dtSource, strTemplateFileName, flg, titleName))
                {
                    byte[] data = ms.ToArray();

                    #region 客户端保存
                    HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.Clear();
                    response.Charset = "UTF-8";
                    response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                    System.Web.HttpContext.Current.Response.BinaryWrite(data);
                    #endregion
                }
            }
            else if (strTemplateFileName.Trim().ToLower().EndsWith(".xlsx"))
            {
                using (MemoryStream ms = ExportExcelForDtByNPOI2007(dtSource, strTemplateFileName, flg, titleName))
                {
                    byte[] data = ms.ToArray();

                    #region 客户端保存
                    HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.Clear();
                    response.Charset = "UTF-8";
                    response.ContentType = "application/vnd.ms-excel";//"application/vnd-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                    System.Web.HttpContext.Current.Response.BinaryWrite(data);
                    #endregion
                }
            }
           
        }

        public static MemoryStream ExportExcelForDtByNPOI2003(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {
            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "abc";
                    break;
            }
            HSSFSheet sheet = workbook.GetSheet(sheetName) as HSSFSheet;

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "http://www.baidu.com/";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "宋东亚"; //填加xls文件作者信息
                si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
                si.LastAuthor = "宋东亚"; //填加xls文件最后保存者信息
                si.Comments = "说明信息"; //填加xls文件作者信息
                si.Title = "NPOI测试"; //填加xls文件标题信息
                si.Subject = "NPOI测试Demo"; //填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            #region 表头
            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
            HSSFCell headerCell = headerRow.GetCell(0) as HSSFCell;
            headerCell.SetCellValue(titleName);
            #endregion


            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                HSSFRow dataRow = sheet.GetRow(rowIndex) as HSSFRow;
                if (dataRow == null)
                {
                    dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                }
                int columnIndex = 0;        // 开始列（0为标题列，从1开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count)
                        break;
                    HSSFCell newCell = dataRow.GetCell(columnIndex) as HSSFCell;
                    if (newCell == null)
                    {
                        newCell = dataRow.CreateCell(columnIndex) as HSSFCell;
                    }

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion
                rowIndex++;
            }

            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear 
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            int cellCount = headerRow.LastCellNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        HSSFCell c = row.GetCell(j) as HSSFCell;

                        switch (c.CellType)
                        {

                            case CellType.NUMERIC:
                                if (c.NumericCellValue == 0)
                                {
                                    c.SetCellType(CellType.STRING);
                                    c.SetCellValue(string.Empty);
                                }
                                break;
                            case CellType.BLANK:

                            case CellType.STRING:
                                if (c.StringCellValue == "0")
                                { c.SetCellValue(string.Empty); }
                                break;
                        }
                    }
                }

            }
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;
                return ms;
            }
        }

        public static MemoryStream ExportExcelForDtByNPOI2007(DataTable dtSource, string strTemplateFileName, int flg, string titleName)
        {
            #region 处理DataTable,处理明细表中没有而需要额外读取汇总值的两列

            #endregion
            int rowIndex = 2;       // 起始行
            int dtRowIndex = dtSource.Rows.Count;       // DataTable的数据行数

            FileStream file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            XSSFWorkbook workbook = new XSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "abc";
                    break;
            }
            XSSFSheet sheet = workbook.GetSheet(sheetName) as XSSFSheet;

            #region 表头
            XSSFRow headerRow = sheet.GetRow(0) as XSSFRow;
            XSSFCell headerCell = headerRow.GetCell(0) as XSSFCell;
            headerCell.SetCellValue(titleName);
            #endregion


            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                XSSFRow dataRow = sheet.GetRow(rowIndex) as XSSFRow;
                if (dataRow == null)
                {
                    dataRow = sheet.CreateRow(rowIndex) as XSSFRow;
                }
                int columnIndex = 0;        // 开始列（0为标题列，从1开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count)
                        break;
                    XSSFCell newCell = dataRow.GetCell(columnIndex) as XSSFCell;
                    if (newCell == null)
                    {
                        newCell = dataRow.CreateCell(columnIndex) as XSSFCell;
                    }

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion
                rowIndex++;
            }

            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region Clear
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            int cellCount = headerRow.LastCellNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                XSSFRow row = sheet.GetRow(i) as XSSFRow;
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        XSSFCell c = row.GetCell(j) as XSSFCell;

                        switch (c.CellType)
                        {

                            case CellType.NUMERIC:
                                if (c.NumericCellValue == 0)
                                {
                                    c.SetCellType(CellType.STRING);
                                    c.SetCellValue(string.Empty);
                                }
                                break;
                            case CellType.BLANK:

                            case CellType.STRING:
                                if (c.StringCellValue == "0")
                                { c.SetCellValue(string.Empty); }
                                break;
                        }
                    }
                }

            }
            #endregion
            //用来解决报错的问题
            using (NpoiMemoryStream ms = new NpoiMemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                //ms.Position = 0;
                sheet = null;
                workbook = null;
                return ms;
            }
        }
        #region 从excel2003中将数据导出到datatable
        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ImportExcel2003toDt(string strFileName)
        {
            DataTable dt = new DataTable();
            IWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = hssfworkbook.GetSheetAt(0) as HSSFSheet;
            dt = ImportExcel2003InDt(sheet, 0, true);
            return dt;
        }


        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExcel2003toDt(string strFileName, string SheetName, int HeaderRowIndex)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheet(SheetName) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportExcel2003InDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExcel2007toDt(string strFileName, string SheetName, int HeaderRowIndex)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheet(SheetName) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportExcel2007InDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExcel2003toDt(string strFileName, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheetAt(SheetIndex) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportExcel2003InDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExcel2007toDt(string strFileName, int SheetIndex, int HeaderRowIndex)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheetAt(SheetIndex) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportExcel2007InDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExcel2003toDt(string strFileName, string SheetName, int HeaderRowIndex, bool needHeader)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheet(SheetName) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportExcel2003InDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExcel2003toDt(string strFileName, int SheetIndex, int HeaderRowIndex, bool needHeader)
        {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = workbook.GetSheetAt(SheetIndex) as HSSFSheet;
            DataTable table = new DataTable();
            table = ImportExcel2003InDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        static DataTable ImportExcel2003InDt(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable table = new DataTable();
            HSSFRow headerRow;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0) as HSSFRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex) as HSSFRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                DataColumn column = new DataColumn(Convert.ToString(i));
                                table.Columns.Add(column);
                            }

                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        HSSFRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i) as HSSFRow;
                        }
                        else
                        {
                            row = sheet.GetRow(i) as HSSFRow;
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.STRING:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.NUMERIC:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.BOOLEAN:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.ERROR:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.FORMULA:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.STRING:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.NUMERIC:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.BOOLEAN:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.ERROR:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                //wl.WriteLogs(exception.ToString());
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception exception)
                    {
                        //wl.WriteLogs(exception.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                //wl.WriteLogs(exception.ToString());
            }
            return table;
        }

        /// <summary>
        /// 将制定sheet中的数据导出到datatable中
        /// </summary>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        static DataTable ImportExcel2007InDt(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable table = new DataTable();
            NPOI.XSSF.UserModel.XSSFRow headerRow;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0) as NPOI.XSSF.UserModel.XSSFRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex) as NPOI.XSSF.UserModel.XSSFRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                DataColumn column = new DataColumn(Convert.ToString(i));
                                table.Columns.Add(column);
                            }

                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        NPOI.XSSF.UserModel.XSSFRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i) as NPOI.XSSF.UserModel.XSSFRow;
                        }
                        else
                        {
                            row = sheet.GetRow(i) as NPOI.XSSF.UserModel.XSSFRow;
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.STRING:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.NUMERIC:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.BOOLEAN:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.ERROR:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.FORMULA:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.STRING:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.NUMERIC:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.BOOLEAN:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.ERROR:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                //wl.WriteLogs(exception.ToString());
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception exception)
                    {
                        //wl.WriteLogs(exception.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                //wl.WriteLogs(exception.ToString());
            }
            return table;
        }
        #endregion

        /// <summary>
        /// [读取excel的多个sheet时用到]根据路径读取excel的多个sheet--验证
        /// </summary>
        /// <param name="Path">excel的绝对路径</param>
        /// <returns></returns>
        public static DataSet GetExcelModel(string Path)
        {

            DataSet ExcelData = new DataSet();
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                IWorkbook workbook = WorkbookFactory.Create(fs);
                int num = workbook.NumberOfSheets;//獲取Excel裡面的表數量
                for (int a = 0; a < num; a++)
                {
                    ISheet sheet = workbook.GetSheetAt(a);//通過索引獲取循環中的表

                    DataTable table = new DataTable();
                    //获取sheet的首行
                    IRow headerRow = sheet.GetRow(0);
                    if (headerRow != null)
                    {
                        //一行最后一个方格的编号 即总的列数
                        int cellCount = headerRow.LastCellNum;

                        ///用户生成表头
                        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                            table.Columns.Add(column);
                        }

                        //最后一列的标号  即总的行数
                        //int rowCount = sheet.LastRowNum;
                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                        {
                            IRow row = sheet.GetRow(i);
                            DataRow dataRow = table.NewRow();

                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                    dataRow[j] = row.GetCell(j).ToString();
                            }
                            table.Rows.Add(dataRow);
                        }
                        table.TableName = workbook.GetSheetName(a);
                        ExcelData.Tables.Add(table);
                    }
                }
            }
            return ExcelData;
        }
    }
}