using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PhoneLogic.Core.Areas.ReportCriteria;

namespace PhoneLogic.Core.Helpers
{

    public static class DataGridExtensions
    {
        public static void Export(this DataGrid dg, string filename)
        {
            ExportDataGrid(dg, filename);
        }

        public static void ExportDataGrid(DataGrid dGrid, string filename)
        {
            var objSFD = new SaveFileDialog()
            {
                DefaultExt = "csv",
                Filter = "CSV Files (*.csv)|*.csv|Excel XML (*.xml)|*.xml|All files (*.*)|*.*",
                FilterIndex = 1,
                 DefaultFileName = filename + ".xml"
                
            };
            if (objSFD.ShowDialog() == true)
            {
                string strFormat = objSFD.SafeFileName.Substring(objSFD.SafeFileName.LastIndexOf('.') + 1).ToUpper();
                var strBuilder = new StringBuilder();
                if (dGrid.ItemsSource == null) return;
                var lstFields = new List<string>();
                if (dGrid.HeadersVisibility == DataGridHeadersVisibility.Column ||
                    dGrid.HeadersVisibility == DataGridHeadersVisibility.All)
                {
                    lstFields.AddRange(dGrid.Columns.Select(dgcol => FormatField(dgcol.Header.ToString(), strFormat)));
                    BuildStringOfRow(strBuilder, lstFields, strFormat);
                }
                foreach (object data in dGrid.ItemsSource)
                {
                    lstFields.Clear();
                    foreach (DataGridColumn col in dGrid.Columns)
                    {
                        string strValue = "";
                        Binding objBinding = null;
                        if (col is DataGridBoundColumn)
                            objBinding = (col as DataGridBoundColumn).Binding;
                        if (col is DataGridTemplateColumn)
                        {
                            //This is a template column... let us see the underlying dependency object
                            DependencyObject objDO = (col as DataGridTemplateColumn).CellTemplate.LoadContent();
                            var oFE = (FrameworkElement) objDO;
                            var oFI = oFE.GetType().GetField("TextProperty");
                            if (oFI != null)
                            {
                                if (oFI.GetValue(null) != null)
                                {
                                    if (oFE.GetBindingExpression((DependencyProperty) oFI.GetValue(null)) != null)
                                        objBinding =
                                            oFE.GetBindingExpression((DependencyProperty) oFI.GetValue(null))
                                                .ParentBinding;
                                }
                            }
                        }
                        if (objBinding != null)
                        {
                            if (objBinding.Path.Path != "")
                            {
                                PropertyInfo pi = data.GetType().GetProperty(objBinding.Path.Path);
                                if (pi != null) strValue = pi.GetValue(data, null).ToString();
                            }
                            if (objBinding.Converter != null)
                            {
                                if (strValue != "")
                                    strValue =
                                        objBinding.Converter.Convert(strValue, typeof (string),
                                            objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                                else
                                    strValue =
                                        objBinding.Converter.Convert(data, typeof (string),
                                            objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                            }
                        }
                        lstFields.Add(FormatField(strValue, strFormat));
                    }
                    BuildStringOfRow(strBuilder, lstFields, strFormat);
                }
                var sw = new StreamWriter(objSFD.OpenFile());
                if (strFormat == "XML")
                {
                    //Let us write the headers for the Excel XML
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
                    sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                    sw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
                    sw.WriteLine("<Author>Your name here</Author>");
                    sw.WriteLine("<Created>" + DateTime.Now.ToLocalTime().ToLongDateString() + "</Created>");
                    sw.WriteLine("<LastSaved>" + DateTime.Now.ToLocalTime().ToLongDateString() + "</LastSaved>");
                    sw.WriteLine("<Company>Reckner</Company>");
                    sw.WriteLine("<Version>1</Version>");
                    sw.WriteLine("</DocumentProperties>");
                    sw.WriteLine(
                        "<Worksheet ss:Name=\"Silverlight Export\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                    sw.WriteLine("<Table>");
                }
                sw.Write(strBuilder.ToString());
                if (strFormat == "XML")
                {
                    sw.WriteLine("</Table>");
                    sw.WriteLine("</Worksheet>");
                    sw.WriteLine("</Workbook>");
                }
                sw.Close();
            }
        }

        private static void BuildStringOfRow(StringBuilder strBuilder, List<string> lstFields, string strFormat)
        {
            switch (strFormat)
            {
                case "XML":
                    strBuilder.AppendLine("<Row>");
                    strBuilder.AppendLine(String.Join("\r\n", lstFields.ToArray()));
                    strBuilder.AppendLine("</Row>");
                    break;
                case "CSV":
                    strBuilder.AppendLine(String.Join(",", lstFields.ToArray()));
                    break;
            }
        }

        //private static string FormatField(string data, string format)
        //{
        //    switch (format)
        //    {
        //        case "XML":
        //            return String.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", data);
        //        case "CSV":
        //            return String.Format("\"{0}\"", data.Replace("\"", "\"\"\"").Replace("\n", "").Replace("\r", ""));
        //    }
        //    return data;
        //}

      // replaced to format numbers
        private static string FormatField(string data, string format)
        {
            double Num;
            bool isNum = double.TryParse(data, out Num);

            switch (format)
            {
                case "XML":
                    return String.Format(isNum ? "<Cell><Data ss:Type=\"Number\">{0}</Data></Cell>" : "<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", data);
                case "CSV":
                    return String.Format("\"{0}\"", data.Replace("\"", "\"\"\"").Replace("\n", "").Replace("\r", ""));
            }
            return data;
        }
    }
}