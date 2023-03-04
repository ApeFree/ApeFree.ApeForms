using System;
using System.Collections;
using System.Collections.Generic;

namespace ApeFree.ApeForms.Core.Utils
{
    public class EventableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        public event EventHandler<ListItemsChangedEventArgs<T>> ItemAdded;
        public event EventHandler<ListItemsChangedEventArgs<T>> ItemRemoved;
        public event EventHandler<ListItemsChangedEventArgs<T>> ItemInserted;
        public event EventHandler ItemsCleared;

        private readonly List<T> innerList;
        public int Count => innerList.Count;
        public bool IsReadOnly => false;
        public T this[int index] { get => innerList[index]; set => innerList[index] = value; }

        public EventableList()
        {
            innerList = new List<T>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            int index = Count;
            innerList.Add(item);
            ItemAdded?.Invoke(this, new ListItemsChangedEventArgs<T>(item, index));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection)
        {
            int index = Count;
            innerList.AddRange(collection);
            foreach (T item in collection)
            {
                ItemAdded?.Invoke(this, new ListItemsChangedEventArgs<T>(item, index++));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Clear()
        {
            innerList.Clear();
            ItemsCleared?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            bool b = innerList.Remove(item);
            if (b)
            {
                ItemRemoved?.Invoke(this, new ListItemsChangedEventArgs<T>(item, -1));
            }
            return b;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            T item = this[index];
            innerList.RemoveAt(index);
            ItemRemoved?.Invoke(this, new ListItemsChangedEventArgs<T>(item, index));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            innerList.Insert(index, item);
            ItemInserted?.Invoke(this, new ListItemsChangedEventArgs<T>(item, index));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            innerList.InsertRange(index, collection);
            foreach (T item in collection)
            {
                ItemInserted?.Invoke(this, new ListItemsChangedEventArgs<T>(item, index++));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return innerList.IndexOf(item);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return innerList.Contains(item);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            innerList?.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return innerList?.GetEnumerator();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// 列表项发生变更时的事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListItemsChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 变更项
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// 变更的位置
        /// </summary>
        public int Position { get; set; }

        public ListItemsChangedEventArgs(T item, int position)
        {
            Item = item;
            Position = position;
        }
    }
}
