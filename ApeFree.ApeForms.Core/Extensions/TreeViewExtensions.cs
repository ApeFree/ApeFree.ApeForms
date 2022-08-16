using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public static class TreeViewExtensions
    {
        /// <summary>
        /// 向TreeView中添加Json数据
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="jc">Json Object或Json Array类型的数据</param>
        /// <param name="isClear">是否清空数据</param>
        public static void LoadJsonDataToTreeViewNode(this TreeView treeView, JContainer jc, bool isClear = true)
        {
            treeView.SuspendLayout();
            treeView.Nodes.InputJsonDataToTreeViewNode(jc, isClear);
            treeView.ResumeLayout();
        }

        /// <summary>
        /// 向TreeNode中添加Json数据
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="jc">Json Object或Json Array类型的数据</param>
        /// <param name="isClear">是否清空数据</param>
        public static void InputJsonDataToTreeViewNode(this TreeNodeCollection nodes, JContainer jc, bool isClear = true)
        {
            // 清空节点
            if (isClear) nodes.Clear();

            if (jc is JObject)
            {
                JObject jo = (JObject)jc;
                foreach (var j in jo)
                {
                    if (j.Value.Type == JTokenType.Object)
                    {
                        var node = nodes.Add(j.Key);
                        InputJsonDataToTreeViewNode(node.Nodes, (JObject)j.Value, false);
                    }
                    else if (j.Value.Type == JTokenType.Array)
                    {
                        var node = nodes.Add(j.Key);
                        InputJsonDataToTreeViewNode(node.Nodes, (JArray)j.Value, false);
                    }
                    else
                    {
                        nodes.Add($"{j.Key}: {j.Value}");
                    }
                }
            }
            else if (jc is JArray)
            {
                JArray ja = (JArray)jc;
                int i = 0;
                foreach (var j in ja)
                {
                    i++;
                    if (j.Type == JTokenType.Object)
                    {
                        var node = nodes.Add(i.ToString());
                        InputJsonDataToTreeViewNode(node.Nodes, (JObject)j, false);
                    }
                    else if (j.Type == JTokenType.Array)
                    {
                        var node = nodes.Add(i.ToString());
                        InputJsonDataToTreeViewNode(node.Nodes, (JArray)j, false);
                    }
                    else
                    {
                        nodes.Add(j.ToString());
                    }
                }
            }
            else
            {
                nodes.Add(jc.ToString());
            }
        }
    }
}
