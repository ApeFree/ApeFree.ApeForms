using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// 通过表格内数据生成List<Dictionary<string, object>>格式的表数据
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToTable(this DataGridView dgv)
        {
            List<string> colNames = new List<string>();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                colNames.Add(col.HeaderText);
            }

            List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                Dictionary<string, object> rows = new Dictionary<string, object>();
                for (int i = 0; i < colNames.Count; ++i)
                {
                    rows.Add(colNames[i] , row.Cells[i].Value);
                }
                table.Add(rows);
            }
            return table;
        }

        /// <summary>
        /// 将表格内容转换为符合Excel表格格式的字符串
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static string ToExcelString(this DataGridView dgv)
        {
            StringBuilder sb = new StringBuilder();
            List<string> colNames = new List<string>();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                colNames.Add(col.HeaderText);
            }
            sb.Append(string.Join("\t",colNames.ToArray()));
            sb.Append("\r\n");

            foreach (DataGridViewRow row in dgv.Rows)
            {
                List<string> rowValues = new List<string>();
                foreach(DataGridViewCell cell in row.Cells)
                {
                    rowValues.Add($"{(cell.Value is string ? "'" : "")}{cell.Value}");
                }
                sb.Append(string.Join("\t", rowValues.ToArray()));
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 用Json数组为DataGridView填充数据
        /// </summary>
        /// <param name="jarr">JArray格式数据</param>
        //public static void LoadDataFromJArray(this DataGridView dgv, JArray jarr)
        //{
        //    // 检查数据是否为空
        //    if (jarr == null || jarr.Count == 0) return;

        //    // 清空
        //    dgv.Columns.Clear();

        //    // 获取模板数据
        //    JObject jobj = jarr[0].ToObject<JObject>();

        //    // 创建列
        //    List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

        //    // 根据模板数据添加列
        //    foreach (var kv in jobj)
        //    {
        //        cols.Add(new DataGridViewTextBoxColumn() { HeaderText = kv.Key });
        //    }
        //    dgv.Columns.AddRange(cols.ToArray());

        //    // 添加行
        //    foreach (var jtkn in jarr)
        //    {
        //        JObject obj = jtkn.ToObject<JObject>();
        //        List<object> values = new List<object>();
        //        foreach (var kv in obj)
        //        {
        //            values.Add(kv.Value);
        //        }
        //        dgv.Rows.Add(values.ToArray());
        //    }
        //}

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="dgv"></param>
        public static void ShowLineNumbers(this DataGridView dgv)
        {
            dgv.RowStateChanged += Dgv_RowStateChanged;
        }

        /// <summary>
        /// 隐藏行号
        /// </summary>
        /// <param name="dgv"></param>
        public static void HideLineNumbers(this DataGridView dgv)
        {
            dgv.RowStateChanged -= Dgv_RowStateChanged;
            foreach(DataGridViewRow row in dgv.Rows)
            {
                row.HeaderCell.Value = "";
            }
        }

        private static void Dgv_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }

        /// <summary>
        /// 获取表格所有数据
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static List<List<object>> GetTable(this DataGridView dataGridView)
        {
            List<List<object>> table = new List<List<object>>();
            foreach (DataGridViewRow item in dataGridView.Rows)
            {
                List<object> row = new List<object>();
                foreach (DataGridViewCell cell in item.Cells)
                {
                    row.Add(cell.Value);
                }
                table.Add(row);
            }
            return table;
        }
    }
}
