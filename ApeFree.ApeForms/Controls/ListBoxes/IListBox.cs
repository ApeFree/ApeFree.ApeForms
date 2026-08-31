using ApeFree.ApeForms.Controls.Buttons;
using ApeFree.ApeForms.Controls.Cards;
using ApeFree.ApeForms.Controls.Containers;
using ApeFree.ApeForms.Controls.DatePickers;
using ApeFree.ApeForms.Controls.Grids;
using ApeFree.ApeForms.Controls.Labels;
using ApeFree.ApeForms.Controls.ListBoxes;
using ApeFree.ApeForms.Controls.TabBoxes;
using ApeFree.ApeForms.Controls.TextBoxes;
using ApeFree.ApeForms.Controls.TitleBars;
using ApeFree.ApeForms.Controls.Views;
using ApeFree.ApeForms.Utils;
using ApeFree.ApeForms.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Controls.ListBoxes
{
    public interface IListBox<TItem> where TItem : Control
    {
        EventableList<TItem> Items { get; }
        void AddItem(TItem item);
        void AddItems(IEnumerable<TItem> items);

        // 不需要实现这些方法，可直接调用Items属性实现
        //void Clear();
        //bool Remove(TItem item);
        //void RemoveAt(int index);
    }
}
