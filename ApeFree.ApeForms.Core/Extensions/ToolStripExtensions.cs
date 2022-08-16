using ApeFree.ApeForms.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public static class ToolStripExtensions
    {
        /// <summary>
        /// 获取ToolStrip的属性表
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static ToolStripProperties GetProperties(this ToolStrip menu)
        {
            ToolStripProperties msp = new ToolStripProperties();

            foreach (ToolStripItem tsmi in menu.Items)
            {
                GetProperties(tsmi, msp);
            }

            return msp;
        }
        /// <summary>
        /// 设置ToolStrip的主要属性
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="msp"></param>
        public static void SetProperties(this ToolStrip menu, ToolStripProperties msp, PropertiesFliter fliter = null)
        {
            fliter = fliter ?? PropertiesFliter.DefaultToolStripPropertiesFliter;

            foreach (ToolStripItem tsmi in menu.Items)
            {
                SetProperties(tsmi, msp, fliter);
            }
        }

        /// <summary>
        /// 获取MenuStrip属性的字典
        /// </summary>
        /// <param name="item">菜单下的子菜单项</param>
        /// <param name="msp">菜单的属性</param>
        private static void GetProperties(ToolStripItem item, ToolStripProperties msp)
        {
            if (string.IsNullOrWhiteSpace(item.Name)) return;

            var properties = new ToolItemProperties
            {
                Text = item.Text,
                Enable = item.Enabled,
            };

            msp.Add(item.Name, properties);

            if (item.GetType() == typeof(ToolStripMenuItem))
            {
                //当前的项是否被选中
                properties.Checked = ((ToolStripMenuItem)item).Checked;
                foreach (ToolStripItem child in ((ToolStripMenuItem)item).DropDownItems)
                {
                    GetProperties(child, msp);
                }
            }
            else if (item.GetType() == typeof(ToolStripComboBox))
            {
                properties.ComboBoxItems = new List<object>();
                foreach (var obj in ((ToolStripComboBox)item).Items)
                {
                    properties.ComboBoxItems.Add(obj);
                }
            }
            else
            {

            }
        }
        /// <summary>
        /// 菜单的多选框和选中状态
        /// </summary>
        /// <param name="item">菜单下的子菜单项</param>
        /// <param name="msp">菜单的属性</param>
        private static void SetProperties(ToolStripItem item, ToolStripProperties msp, PropertiesFliter fliter)
        {
            fliter = fliter ?? PropertiesFliter.DefaultToolStripPropertiesFliter;

            if (!msp.ContainsKey(item.Name)) return;

            ToolItemProperties properties = msp[item.Name];
            if (fliter.AllowText) item.Text = properties.Text;
            if (fliter.AllowEnable) item.Enabled = properties.Enable;

            if (item.GetType() == typeof(ToolStripMenuItem))
            {

                if (fliter.AllowChecked) ((ToolStripMenuItem)item).Checked = properties.Checked;

                foreach (ToolStripItem child in ((ToolStripMenuItem)item).DropDownItems)
                {
                    SetProperties(child, msp, fliter);
                }
            }
            else if (item.GetType() == typeof(ToolStripComboBox))
            {
                if (fliter.AllowComboBoxItems && properties.ComboBoxItems != null && properties.ComboBoxItems.Count > 0)
                {
                    ((ToolStripComboBox)item).Items.Clear();
                    ((ToolStripComboBox)item).Items.AddRange(properties.ComboBoxItems.ToArray());
                }
            }
            else
            {

            }
        }
    }
}

namespace ApeFree.ApeForms.Core.Extensions
{
    /// <summary>
    /// 属性字典集合
    /// key:item.Name
    /// value:item properties
    /// </summary>
    public class ToolStripProperties : Dictionary<string, ToolItemProperties> { }

    /// <summary>
    /// ToolItem属性信息
    /// </summary>
    public class ToolItemProperties
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
        public bool Enable { get; set; }
        public List<object> ComboBoxItems { get; set; }
    }

    /// <summary>
    /// 属性过滤器
    /// </summary>
    public class PropertiesFliter
    {
        public readonly static PropertiesFliter DefaultToolStripPropertiesFliter = new PropertiesFliter();
        public bool AllowText { get; set; } = true;
        public bool AllowChecked { get; set; } = true;
        public bool AllowEnable { get; set; } = true;
        public bool AllowComboBoxItems { get; set; } = true;
    }
}