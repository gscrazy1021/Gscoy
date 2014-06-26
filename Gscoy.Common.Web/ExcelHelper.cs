using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using Microsoft.VisualBasic;
using NPOI.HSSF.UserModel;
using System.IO;

namespace Gscoy.Common.Web
{
    public class ExcelHelper
    {
        /// <summary>
        /// 将DataTable导出到Excel表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="FileName"></param>
        public static void ToExcel(DataTable dt, string FileName)
        {
            //EDIT BY LILT   改成附件下载
            if (!FileName.EndsWith(".xls"))
            {
                FileName += ".xls";
            }
            HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("gb2312");
            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName);
            }
            else
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            }
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            int i = 0, j;
            for (i = 0; i < dt.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write(dt.Columns[i].ToString() + Constants.vbTab);
            }
            HttpContext.Current.Response.Write(Constants.vbCrLf);
            foreach (DataRow dr in dt.Rows)
            {
                for (j = 0; j < dr.ItemArray.Length; j++)
                    HttpContext.Current.Response.Write(dr.ItemArray[j].ToString().Trim() + Constants.vbTab);
                HttpContext.Current.Response.Write(Constants.vbCrLf);
            }
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 不规则列DataTable输出
        /// </summary>
        /// <param name="head">列标题的Html格式</param>
        /// <param name="dt"></param>
        /// <param name="FileName"></param>
        public static void ToExcel(string head, DataTable dt, string FileName)
        {
            if (!FileName.EndsWith(".xls"))
            {
                FileName += ".xls";
            }
            // 当前对话
            HttpContext curContext = HttpContext.Current;
            // IO用于导出并返回excel文件
            System.IO.StringWriter strWriter = null;

            //设置样式
            curContext.Response.ClearContent();
            curContext.Response.Buffer = true;
            if (curContext.Request.UserAgent.ToLower().IndexOf("msie") > -1)
            {
                FileName = HttpUtility.UrlPathEncode(FileName);
            }

            if (curContext.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                curContext.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName);
            }
            else
            {
                curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            }
            //EDIT BY LILT   改成附件下载
            curContext.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 


            if (dt.Rows.Count > 0)
                curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            else
                curContext.Response.ContentEncoding = System.Text.Encoding.Default;
            curContext.Response.Charset = "GB2312";
            //设置输出流
            int i, j;
            i = dt.Columns.Count;
            strWriter = new System.IO.StringWriter();
            strWriter.Write("<table border=1><tr><td colspan=" + i.ToString() + ">");
            strWriter.Write(head);
            strWriter.Write("</td></tr>");
            foreach (DataRow dr in dt.Rows)
            {
                strWriter.Write("<tr>");
                for (j = 0; j < dr.ItemArray.Length; j++)
                    strWriter.Write("<td>" + dr.ItemArray[j].ToString().Trim() + Constants.vbTab + "</td>");
                strWriter.Write("</tr>" + Constants.vbCrLf);

            }
            strWriter.Write("</table>");

            curContext.Response.Write(strWriter.ToString());
            curContext.Response.Flush();
            curContext.Response.End();
        }

        public static void ExportToExcel(string head, DataTable dt, string FileName)
        {
            if (!FileName.EndsWith(".xls"))
            {
                FileName += ".xls";
            }
            HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("gb2312");
            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("msie") > -1)
            {
                FileName = HttpUtility.UrlPathEncode(FileName);
            }

            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName);
            }
            else
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlDecode(FileName, System.Text.Encoding.UTF8));
            }
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 

            int j = 0;
            int i = dt.Columns.Count;

            HttpContext.Current.Response.Write(head);

            HttpContext.Current.Response.Write(Constants.vbCrLf);
            foreach (DataRow dr in dt.Rows)
            {
                for (j = 0; j < dr.ItemArray.Length; j++)
                    HttpContext.Current.Response.Write(dr.ItemArray[j].ToString().Trim() + Constants.vbTab);
                HttpContext.Current.Response.Write(Constants.vbCrLf);
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 输出Excel报表
        /// </summary>
        /// <param name="dtDataSourse">报表数据源</param>
        /// <param name="FileName">输出文件名</param>
        /// <param name="titles">Excel表头标题名</param>
        /// <param name="fields">Excel绑定字段名</param>
        public static void ToExcel(DataTable dtDataSourse, string FileName, string[] titles, string[] fields)
        {
            //判断绑定字段
            if (fields == null || fields.Length == 0)
            {
                throw new ArgumentNullException("绑定字段不能为空");
            }
            if (titles == null || titles.Length != fields.Length)
            {
                throw new ArgumentException("titles.Length != fields.Length", "fields");
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            if (!FileName.EndsWith(".xls"))
                FileName += ".xls";
            HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("gb2312");
            if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName);
            }
            else
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            }
            //输出Excel表头
            foreach (string title in titles)
            {
                HttpContext.Current.Response.Write(title + "\t");
            }
            HttpContext.Current.Response.Write("\n");
            //输出Excel内容 
            foreach (DataRow dr in dtDataSourse.Rows)
            {
                foreach (string field in fields)
                {
                    HttpContext.Current.Response.Write(dr[field].ToString().Trim() + "\t");
                }
                HttpContext.Current.Response.Write("\n");
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="fileName">文件名</param>
        public static void ToExcelNew(DataTable dt, string fileName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFCellStyle headerStyle = CreateHeaderStyle(workbook);//设置标题拦单元格样式
            HSSFCellStyle conentStyle = CreateConentStyle(workbook);//设置内容部分单元格样式
            HSSFSheet sheet = workbook.CreateSheet("Sheet1");
            HSSFRow headerRow = sheet.CreateRow(0);//创建一行为标头
            sheet.DefaultColumnWidth = 30; //设置列的默认宽度
            string dateIntStr = DateTime.Now.ToString("yyyyMMdd");
            int cellIndex = 0;
            int rowIndex = 1;
            int sheetIndex = 1;
            //添加表头
            foreach (DataColumn item in dt.Columns)
            {
                HSSFCell hcell = headerRow.CreateCell(cellIndex);
                hcell.CellStyle = headerStyle;
                hcell.SetCellValue(item.ColumnName);
                cellIndex++;
            }

            //开始写数据
            foreach (DataRow item in dt.Rows)
            {
                HSSFRow row = sheet.CreateRow(rowIndex);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string cellValue = item[dt.Columns[i].ColumnName] == null ? "" : item[dt.Columns[i].ColumnName].ToString();
                    HSSFCell cell = row.CreateCell(i);
                    cell.CellStyle = conentStyle;
                    cell.SetCellValue(cellValue);
                }
                rowIndex++;
                if (rowIndex > 65530)
                {
                    sheet = (HSSFSheet)workbook.CreateSheet("sheet_" + (++sheetIndex));
                    headerRow = (HSSFRow)sheet.CreateRow(0);
                    cellIndex = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        HSSFCell hcell = headerRow.CreateCell(cellIndex);
                        hcell.CellStyle = headerStyle;
                        hcell.SetCellValue(dc.ColumnName);
                        cellIndex++;
                    }
                    rowIndex = 1;
                }
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.Charset = "gb2312";
            if (!fileName.EndsWith(".xls"))
                fileName += ".xls";
            HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("gb2312");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/x-msdownload;Charset=gb2312";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="fileName">文件名</param>
        /// <param name="titles">Excel表头标题名</param>
        /// <param name="fields">Excel绑定字段名</param>
        public static void ToExcelNew(DataTable dt, string fileName, string[] titles, string[] fields)
        {
            //判断绑定字段
            if (fields == null || fields.Length == 0)
            {
                throw new ArgumentNullException("绑定字段不能为空");
            }
            if (titles == null || titles.Length != fields.Length)
            {
                throw new ArgumentException("titles.Length != fields.Length", "fields");
            }
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFCellStyle headerStyle = CreateHeaderStyle(workbook);//设置标题拦单元格样式
            HSSFCellStyle conentStyle = CreateConentStyle(workbook);//设置内容部分单元格样式
            HSSFSheet sheet = workbook.CreateSheet("Sheet1");
            HSSFRow headerRow = sheet.CreateRow(0);//创建一行为标头
            sheet.DefaultColumnWidth = 30; //设置列的默认宽度
            string dateIntStr = DateTime.Now.ToString("yyyyMMdd");
            int cellIndex = 0;
            int rowIndex = 1;
            ////添加表头
            //foreach (DataColumn item in dt.Columns)
            //{
            //    HSSFCell hcell = headerRow.CreateCell(cellIndex);
            //    hcell.CellStyle = headerStyle;
            //    hcell.SetCellValue(item.ColumnName);
            //    cellIndex++;
            //}
            //输出Excel表头
            foreach (string title in titles)
            {
                //HttpContext.Current.Response.Write(title + "\t");
                HSSFCell hcell = headerRow.CreateCell(cellIndex);
                hcell.CellStyle = headerStyle;
                hcell.SetCellValue(title);
                cellIndex++;
            }
            //HttpContext.Current.Response.Write("\n");
            ////输出Excel内容 
            //foreach (DataRow dr in dtDataSourse.Rows)
            //{
            //    foreach (string field in fields)
            //    {
            //        HttpContext.Current.Response.Write(dr[field].ToString().Trim() + "\t");
            //    }
            //    HttpContext.Current.Response.Write("\n");
            //}
            //开始写数据
            foreach (DataRow item in dt.Rows)
            {
                HSSFRow row = sheet.CreateRow(rowIndex);
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    string cellValue = item[dt.Columns[i].ColumnName] == null ? "" : item[dt.Columns[i].ColumnName].ToString();
                //    HSSFCell cell = row.CreateCell(i);
                //    cell.CellStyle = conentStyle;
                //    cell.SetCellValue(cellValue);
                //}
                var i = 0;
                foreach (string field in fields)
                {
                    string cellValue = item[field] == null ? "" : item[field].ToString();
                    HSSFCell cell = row.CreateCell(i);
                    cell.CellStyle = conentStyle;
                    cell.SetCellValue(cellValue);
                    i++;
                }
                rowIndex++;
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.Charset = "gb2312";
            if (!fileName.EndsWith(".xls"))
                fileName += ".xls";
            HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("gb2312");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/x-msdownload;Charset=gb2312";
        }
        public static void ToExecl<T>(IEnumerable<T> DataSourceT, List<string> headList, string fileName) where T : class
        {
            System.Reflection.PropertyInfo[] infoArray = typeof(T).GetProperties();

            HSSFWorkbook workbook = new HSSFWorkbook();
            //HSSFCellStyle headerStyle = CreateHeaderStyle(workbook);//设置标题拦单元格样式
            //HSSFCellStyle conentStyle = CreateConentStyle(workbook);//设置内容部分单元格样式
            int sheetIndex = 1;
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("Sheet" + sheetIndex);
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);//创建一行为标头
            sheet.DefaultColumnWidth = 30; //设置列的默认宽度
            string dateIntStr = DateTime.Now.ToString("yyyyMMdd");
            int cellIndex = 0;
            int rowIndex = 1;
            //添加表头
            foreach (string item in headList)
            {
                HSSFCell hcell = (HSSFCell)headerRow.CreateCell(cellIndex);
                //hcell.CellStyle = headerStyle;
                hcell.SetCellValue(item);
                cellIndex++;
            }

            //开始写数据
            foreach (T item in DataSourceT)
            {
                //HSSFCellStyle h1 = CreateHeaderStyle(workbook);
                HSSFRow row = (HSSFRow)sheet.CreateRow(rowIndex);

                for (int i = 0; i < infoArray.Length; i++)
                {

                    string cellValue = infoArray[i].Name == null ? "" : infoArray[i].GetValue(item, null).ToString();
                    HSSFCell cell = (HSSFCell)row.CreateCell(i);
                    cell.SetCellValue(cellValue);
                    //cell.CellStyle = conentStyle;
                }
                rowIndex++;
                if (rowIndex > 65530)
                {
                    sheet = (HSSFSheet)workbook.CreateSheet("sheet_" + (++sheetIndex));
                    headerRow = (HSSFRow)sheet.CreateRow(0);
                    cellIndex = 0;
                    foreach (string colname in headList)
                    {
                        HSSFCell hcell = (HSSFCell)headerRow.CreateCell(cellIndex);
                        //hcell.CellStyle = headerStyle;
                        hcell.SetCellValue(colname);
                        cellIndex++;
                    }
                    rowIndex = 1;
                }
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.Charset = "gb2312";
            string fName = fileName + ".xls";
            fileName = HttpUtility.UrlEncode(fName);//用于处理中文文件名
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/x-msdownload;Charset=utf-8";
        }

        /// <summary>
        /// 将DataTable数据转换成Excel的内存流
        /// </summary>
        /// <param name="dt">待转换的数据源DataTable</param>
        /// <returns></returns>
        public static MemoryStream ToExcelMemoryStream(DataTable dt)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFCellStyle headerStyle = CreateHeaderStyle(workbook);//设置标题拦单元格样式
            HSSFCellStyle conentStyle = CreateConentStyle(workbook);//设置内容部分单元格样式
            HSSFSheet sheet = workbook.CreateSheet("Sheet1");
            HSSFRow headerRow = sheet.CreateRow(0);//创建一行为标头
            sheet.DefaultColumnWidth = 30; //设置列的默认宽度
            string dateIntStr = DateTime.Now.ToString("yyyyMMdd");
            int cellIndex = 0;
            int rowIndex = 1;
            int sheetIndex = 1;
            //添加表头
            foreach (DataColumn item in dt.Columns)
            {
                HSSFCell hcell = headerRow.CreateCell(cellIndex);
                hcell.CellStyle = headerStyle;
                hcell.SetCellValue(item.ColumnName);
                cellIndex++;
            }

            //开始写数据
            foreach (DataRow item in dt.Rows)
            {
                HSSFRow row = sheet.CreateRow(rowIndex);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var srcCell = item[dt.Columns[i].ColumnName];
                    var srcType = dt.Columns[i].DataType;

                    
                    HSSFCell cell = row.CreateCell(i);
                    cell.CellStyle = conentStyle;
                    

                    // 设置数字类型的Cell格式
                    if (srcType == typeof(int) ||
                        srcType == typeof(long) ||
                        srcType == typeof(short) ||
                        srcType == typeof(float) ||
                        srcType == typeof(double) ||
                        srcType == typeof(decimal))
                    {
                        cell.SetCellType(HSSFCell.CELL_TYPE_NUMERIC);
                        cell.SetCellValue(double.Parse(srcCell.ToString()));
                    }
                    else
                    {
                        cell.SetCellValue((srcCell ?? "").ToString());
                    }
                }
                rowIndex++;
                if (rowIndex > 65530)
                {
                    sheet = (HSSFSheet)workbook.CreateSheet("sheet_" + (++sheetIndex));
                    headerRow = (HSSFRow)sheet.CreateRow(0);
                    cellIndex = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        HSSFCell hcell = headerRow.CreateCell(cellIndex);
                        hcell.CellStyle = headerStyle;
                        hcell.SetCellValue(dc.ColumnName);
                        cellIndex++;
                    }
                    rowIndex = 1;
                }
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);

            return ms;
        }

        static HSSFCellStyle CreateHeaderStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle headerStyle = workbook.CreateCellStyle();
            headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLUE.index;
            headerStyle.FillPattern = HSSFCellStyle.ALT_BARS;
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BLUE.index;
            headerStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
            headerStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
            HSSFFont headerFont = workbook.CreateFont();
            headerFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
            headerStyle.SetFont(headerFont);
            return headerStyle;
        }

        static HSSFCellStyle CreateConentStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle headerStyle = workbook.CreateCellStyle();
            headerStyle.VerticalAlignment = HSSFCellStyle.VERTICAL_CENTER;
            headerStyle.Alignment = HSSFCellStyle.ALIGN_CENTER;
            return headerStyle;

        }
    }
}
