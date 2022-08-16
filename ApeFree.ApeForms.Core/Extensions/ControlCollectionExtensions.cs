using System.Collections.Generic;
using static System.Windows.Forms.Control;

namespace System.Windows.Forms
{
    public static class ControlCollectionExtensions
    {
        public static List<T> ToList<T>(this ControlCollection collection) where T : Control
        {
            List<T> list = new List<T>();
            foreach (T ctrl in collection)
                list.Add(ctrl);
            return list;
        }
        public static T[] ToArray<T>(this ControlCollection collection) where T : Control => collection.ToList<T>().ToArray();
        public static List<Control> ToList(this ControlCollection collection) => collection.ToList<Control>();
        public static Control[] ToArray(this ControlCollection collection) => collection.ToList<Control>().ToArray();
    }
}
