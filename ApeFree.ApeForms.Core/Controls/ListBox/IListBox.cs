using ApeFree.CodePlus.Algorithm.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
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
