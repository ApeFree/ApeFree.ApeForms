﻿using STTech.CodePlus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public partial class GridView : UserControl
    {
        public GridView()
        {
            // 启用双缓冲
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            scrollBar = new VScrollBar();
            SuspendLayout();

            scrollBar.Dock = DockStyle.Right;
            scrollBar.LargeChange = 2;
            scrollBar.Maximum = 1;
            scrollBar.Size = new Size(17, 390);
            scrollBar.Scroll += new ScrollEventHandler(scrollBar_Scroll);

            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(scrollBar);
            Size = new Size(100, 100);

            ResumeLayout(false);
        }

        #region 内部类
        /// <summary>
        /// 表格列的设置
        /// </summary>
        public class ColumnSettings
        {
            /// <summary>
            /// 列标题
            /// </summary>
            [Description("列标题")]
            public string Text { get; set; }

            /// <summary>
            /// 列宽度（宽度为0时将自动调整宽度）
            /// </summary>
            [Description("列宽度（宽度为0时将自动调整宽度）")]
            public int Width { get; set; }

            /// <summary>
            /// 列实际宽度
            /// </summary>
            [Browsable(false)]
            public int ActualWidth { get; internal set; }
        }

        /// <summary>
        /// 单元格选中模式
        /// </summary>
        [Flags]
        public enum CellSelectionMode
        {
            /// <summary>
            /// 单个单元格
            /// </summary>
            SingleCell = 1,

            /// <summary>
            /// 整行选中
            /// </summary>
            EntireRow = 2,

            /// <summary>
            /// 整列选中
            /// </summary>
            EntireColumn = 4,

            /// <summary>
            /// 行列同时选中
            /// </summary>
            BothRowAndColumn = 7,
        }

        /// <summary>
        /// 单元格交互状态
        /// </summary>
        public enum CellInteractionState
        {
            None,
            Hovered,
            Selected,
        }

        /// <summary>
        /// 绘制单元格事件参数
        /// </summary>
        public class DrawCellEventArgs : EventArgs
        {
            public DrawCellEventArgs(Graphics graphics, Rectangle cellArea, object[] rowData, CellInteractionState cellInteractionState, Color cellForeColor, Color cellBackColor, int row, int col)
            {
                Graphics = graphics;
                CellArea = cellArea;
                RowData = rowData;
                CellInteractionState = cellInteractionState;
                CellForeColor = cellForeColor;
                CellBackColor = cellBackColor;
                RowIndex = row;
                ColumnIndex = col;
            }

            /// <summary>
            /// 绘图图面
            /// </summary>
            public Graphics Graphics { get; }

            /// <summary>
            /// 单元格区域
            /// </summary>
            public Rectangle CellArea { get; }

            /// <summary>
            /// 单元格数据
            /// </summary>
            public object CellData => RowData[ColumnIndex];

            /// <summary>
            /// 行数据
            /// </summary>
            public object[] RowData { get; }

            /// <summary>
            /// 单元格交互状态
            /// </summary>
            public CellInteractionState CellInteractionState { get; }

            /// <summary>
            /// 单元格前景色
            /// </summary>
            public Color CellForeColor { get; }

            /// <summary>
            /// 单元格背景色
            /// </summary>
            public Color CellBackColor { get; }
            /// <summary>
            /// 行序号
            /// </summary>
            public int RowIndex { get; }

            /// <summary>
            /// 列序号
            /// </summary>
            public int ColumnIndex { get; }

            /// <summary>
            /// 是否需要表格重绘
            /// </summary>
            public bool IsRedrawingNeeded { get; set; }
        }

        public class CellEventArgs : EventArgs
        {

            /// <summary>
            /// 单元格数据
            /// </summary>
            public object CellData
            {
                get
                {
                    return RowData[ColumnIndex];
                }
                set
                {
                    RowData[ColumnIndex] = value;
                }
            }

            /// <summary>
            /// 行数据
            /// </summary>
            public object[] RowData { get; }

            /// <summary>
            /// 行序号
            /// </summary>
            public int RowIndex { get; }

            /// <summary>
            /// 列序号
            /// </summary>
            public int ColumnIndex { get; }

            public CellEventArgs(object[] rowData, int rowIndex, int columnIndex)
            {
                RowData = rowData;
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
            }

        }
        #endregion

        #region 属性及字段
        private VScrollBar scrollBar;

        private List<object[]> dataSource = new List<object[]>();
        private Rectangle[][] CurrentCellsArea = new Rectangle[0][];

        /// <summary>当前鼠标移动到的单元格坐标</summary>
        protected Point? MouseMovingCellLocation { get; private set; }

        private int displayRow = 5;
        private Font headerFont;
        private int headerHeight = 25;
        private CellSelectionMode selectionMode = CellSelectionMode.EntireRow;
        private ColumnSettings[] columns = new ColumnSettings[0];
        private int ColumnCount => Columns.Length;
        private Color selectedBackColor = SystemColors.Highlight;
        private Color selectedForeColor = Color.White;
        private Color headerBackColor = Color.WhiteSmoke;
        private Color hoveredBackColor = Color.FromArgb(32, SystemColors.Highlight);

        // 单元格编辑回调方法字典
        private Dictionary<Type, EventHandler<CellEventArgs>> cellEditFuncDict = new Dictionary<Type, EventHandler<CellEventArgs>>();

        /// <summary>
        /// 绘制自定义数据类型单元格的回调方法
        /// </summary>
        public EventHandler<DrawCellEventArgs> DrawCell;

        /// <summary>
        /// 选中项发生变化事件
        /// </summary>
        [Description("选中项发生变化事件")]
        public event EventHandler SelectionChanged;

        /// <summary>
        /// 单元格选中模式
        /// </summary>
        [Description("单元格选中模式")]
        public CellSelectionMode SelectionMode { get { return selectionMode; } set { selectionMode = value; Invalidate(); } }

        /// <summary>
        /// 表格显示列数
        /// </summary>
        [Description("表格显示列数")]
        [Browsable(false)]
        [Obsolete("此属性无效，当前控件不支持水平滚动")]
        public int DisplayColumn { get; set; } = 6;

        /// <summary>
        /// 表格显示行数
        /// </summary>
        [Description("表格显示行数")]
        public int DisplayRow { get { return displayRow; } set { displayRow = value; Refresh(); } }

        /// <summary>
        /// 表头字体
        /// </summary>
        [Description("表头字体")]
        public Font HeaderFont { get { return headerFont; } set { headerFont = value; Invalidate(); } }

        /// <summary>
        /// 表头高度
        /// </summary>
        [Description("表头高度")]
        public int HeaderHeight { get { return headerHeight; } set { headerHeight = value; Refresh(); } }

        /// <summary>
        /// 表头单元格的背景颜色
        /// </summary>
        [Description("表头单元格的背景颜色")]
        public Color HeaderBackColor { get { return headerBackColor; } set { headerBackColor = value; Invalidate(); } }

        /// <summary>
        /// 数据源
        /// </summary>
        [Description("数据源")]
        public List<object[]> DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                SelectedRowIndex = -1;
                SelectedColumnIndex = -1;
                MouseMovingCellLocation = null;
                CurrentCellsArea = null;
                TopRowIndex = 0;

                Refresh();
            }
        }

        /// <summary>
        /// 表格列设置
        /// </summary>
        [Description("数据源")]
        public ColumnSettings[] Columns { get { return columns; } set { columns = value; Refresh(); } }

        /// <summary>
        /// 鼠标选中单元格的背景颜色
        /// </summary>
        [Description("鼠标选中单元格的背景颜色")]
        public Color SelectedBackColor { get { return selectedBackColor; } set { selectedBackColor = value; Invalidate(); } }

        /// <summary>
        /// 鼠标选中单元格的前景颜色
        /// </summary>
        [Description("鼠标选中单元格的前景颜色")]
        public Color SelectedForeColor { get { return selectedForeColor; } set { selectedForeColor = value; Invalidate(); } }

        /// <summary>
        /// 鼠标悬停单元格的背景颜色
        /// </summary>
        [Description("鼠标悬停单元格的背景颜色")]
        public Color HoveredBackColor { get { return hoveredBackColor; } set { hoveredBackColor = value; Invalidate(); } }

        /// <summary>
        /// 当前表格顶部行的序号
        /// </summary>
        [Browsable(false)]
        public int TopRowIndex
        {
            get { return scrollBar.Value; }
            set { scrollBar.Value = value; }
        }

        /// <summary>
        /// 当前选中数据的行索引
        /// </summary>
        [Browsable(false)]
        public int SelectedRowIndex { get; private set; } = -1;

        /// <summary>
        /// 当前选中数据的列索引
        /// </summary>
        [Browsable(false)]
        public int SelectedColumnIndex { get; private set; } = -1;

        /// <summary>
        /// 允许编辑
        /// </summary>
        public virtual bool AllowEdit { get; set; }

        #endregion

        #region 重写方法

        public override void Refresh()
        {
            if (DataSource != null)
            {
                scrollBar.Maximum = Math.Max(DataSource.Count - DisplayRow + 1, 1);
            }

            // 清空计算单元格区域
            CurrentCellsArea = null;

            base.Refresh();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            // 计算滚动后的首行行号
            var top = TopRowIndex - e.Delta / 120;

            if (top < 0)
            {
                // 行号不能小于0
                top = 0;
            }
            else if (top > scrollBar.Maximum - 1)
            {
                // 行号不能超出最大数据行数
                top = scrollBar.Maximum - 1;
            }
            else
            {
                // 直接使用
            }

            // 更新首行位置
            TopRowIndex = top;

            // 立即重绘
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // 获取当前移动中鼠标指针所属单元格的位置
            var movingCellLocation = GetCellLocation(e.Location);

            if (movingCellLocation.X == -1)
            {
                return;
            }

            if (DataSource == null)
            {
                return;
            }

            if (MouseMovingCellLocation != movingCellLocation)
            {
                MouseMovingCellLocation = movingCellLocation;

                if (DataSource.Count <= movingCellLocation.Y)
                {
                    return;
                }
                else if (DataSource[movingCellLocation.Y].Length <= movingCellLocation.X)
                {
                    return;
                }

                OnMouseCellHover(new CellEventArgs(DataSource[movingCellLocation.Y], movingCellLocation.Y, movingCellLocation.X));

                Invalidate();
            }
        }

        protected virtual void OnMouseCellHover(CellEventArgs e)
        {

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // 获取当前按压的鼠标
            var cellLocation = GetCellLocation(e.Location);

            // 当前鼠标按压单元格真实数据的行列序号
            var rowIndex = cellLocation.Y + TopRowIndex - 1;
            var columnIndex = cellLocation.X;

            // 行号超出选中区域则忽略（比如选中的是无数据空白区域）
            if (rowIndex < 0 || DataSource == null || rowIndex > DataSource.Count - 1)
            {
                return;
            }

            // 如果行列索引发生变化
            if (SelectedRowIndex != rowIndex || SelectedColumnIndex != columnIndex)
            {
                // 更新索引，重绘界面
                SelectedRowIndex = rowIndex;
                SelectedColumnIndex = columnIndex;
                Invalidate();

                // 触发事件
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            // 当鼠标离开控件区域时，清除鼠标的悬停位置信息
            MouseMovingCellLocation = null;

            // 立即重绘界面
            Invalidate();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            // 如果不允许编辑则退出
            if (!AllowEdit)
            {
                return;
            }

            // 如果不是鼠标左键点击则退出
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // 获取当前移动中鼠标指针所属单元格的位置
            var clickedCellPos = GetCellLocation(e.Location);

            // 检查点击位置是否有效
            if (clickedCellPos.X < 1 || clickedCellPos.Y < 1)
            {
                return;
            }

            // 获取单元格区域
            var clickedCell = CurrentCellsArea[clickedCellPos.Y][clickedCellPos.X];

            // 如果未点击有效单元格则
            if (clickedCellPos.X == -1 || clickedCellPos.Y == -1)
            {
                return;
            }

            var rowIndex = clickedCellPos.Y - 1 + TopRowIndex;

            // 获取行数据
            var row = DataSource[rowIndex];

            // 如果列超出有效数据的长度
            if (row.Length < clickedCellPos.X)
            {
                return;
            }

            // 获取单元格数据
            var data = row[clickedCellPos.X];

            // 如果数据为空则退出
            if (data == null)
            {
                return;
            }

            // 获取单元格数据的类型
            var type = data.GetType();

            // 获取数据类型对应的编辑回调
            EventHandler<CellEventArgs> func;
            if (cellEditFuncDict.TryGetValue(type, out func))
            {
                var args = new CellEventArgs(row, clickedCellPos.Y - 1, clickedCellPos.X);
                // 通过回调编辑数据
                func.DynamicInvoke(this, args);

                // 重绘单元格
                // TODO: 可只重绘被修改的单元格，不需要重绘全部
                Invalidate(clickedCell, false);
            }
            else
            {
                // 未注册同类型回调则退出方法
                return;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 无列配置，直接返回
            if (ColumnCount == 0)
            {
                return;
            }

            // 获取当前所有单元格区域
            if (CurrentCellsArea == null)
            {
                CurrentCellsArea = GetCellsRectangle();
            }

            // 绘制表头单元格
            DrawHeaderCells(e.Graphics);

            // 判断是否有数据
            if (DataSource == null || !DataSource.Any())
            {
                // 仅绘制表格即退出
                DrawGridLines(e.Graphics, CurrentCellsArea, 1, Color.LightGray);
                return;
            }

            // 绘制数据单元格
            for (int displayRow = 0; displayRow < DisplayRow; displayRow++)
            {
                // 获取当前行的单元格区域
                var rowCells = CurrentCellsArea[displayRow + 1];

                // 计算实际行数据的行号
                var row = TopRowIndex + displayRow;

                // 如果当前绘制的数据行超出数据源总行数，则跳出
                if (row >= DataSource.Count)
                {
                    break;
                }

                // 获取行数据数组
                var rowObjects = DataSource[row];

                // 绘制每一个单元格
                for (int col = 0; col < ColumnCount; col++)
                {
                    // 当前待绘制的单元格区域
                    var cell = rowCells[col];

                    // 当前待绘制的单元格数据
                    var data = rowObjects.Length > col ? rowObjects[col] : null;

                    // 预处理单元格数据
                    data = PreprocessCellData(data, new Point(row, col));

                    // 当前单元格交互状态
                    var interactionState = CellInteractionState.None;

                    // 根据单元格选中模式确定背景色的颜色
                    if (SelectionMode == CellSelectionMode.SingleCell && row == SelectedRowIndex && col == SelectedColumnIndex)
                    {
                        interactionState = CellInteractionState.Selected;
                    }
                    else if (SelectionMode == CellSelectionMode.EntireRow && row == SelectedRowIndex)
                    {
                        interactionState = CellInteractionState.Selected;
                    }
                    else if (SelectionMode == CellSelectionMode.EntireColumn && col == SelectedColumnIndex)
                    {
                        interactionState = CellInteractionState.Selected;
                    }
                    else if (SelectionMode == CellSelectionMode.BothRowAndColumn && (row == SelectedRowIndex || col == SelectedColumnIndex))
                    {
                        interactionState = CellInteractionState.Selected;
                    }
                    else if (MouseMovingCellLocation != null) // 判断当前是否鼠标悬停在单元格上
                    {
                        // 获取当前鼠标悬停单元格是行列序号
                        var hoverColumn = MouseMovingCellLocation.Value.X;
                        var hoverRow = MouseMovingCellLocation.Value.Y - 1 + TopRowIndex;

                        // 根据单元格选中模式确定背景色的颜色
                        if (SelectionMode == CellSelectionMode.SingleCell && row == hoverRow && col == hoverColumn)
                        {
                            interactionState = CellInteractionState.Hovered;
                        }
                        else if (SelectionMode == CellSelectionMode.EntireRow && row == hoverRow)
                        {
                            interactionState = CellInteractionState.Hovered;
                        }
                        else if (SelectionMode == CellSelectionMode.EntireColumn && col == hoverColumn)
                        {
                            interactionState = CellInteractionState.Hovered;
                        }
                        else if (SelectionMode == CellSelectionMode.BothRowAndColumn && (row == hoverRow || col == hoverColumn))
                        {
                            interactionState = CellInteractionState.Hovered;
                        }
                    }
                    else
                    {
                        // 无交互
                        interactionState = CellInteractionState.None;
                    }

                    // 单元格颜色
                    Color cellBackColor;
                    Color cellForeColor;

                    // 根据单元格交互状态设置背景颜色
                    switch (interactionState)
                    {
                        case CellInteractionState.Hovered:
                            cellBackColor = HoveredBackColor;
                            cellForeColor = ForeColor;
                            break;
                        case CellInteractionState.Selected:
                            cellBackColor = SelectedBackColor;
                            cellForeColor = SelectedForeColor;
                            break;
                        case CellInteractionState.None:
                        default:
                            cellBackColor = BackColor;
                            cellForeColor = ForeColor;
                            break;
                    }

                    if (data == null)                                                   // 无数据仅填充背景色
                    {
                        FillRectangleColor(e.Graphics, cellBackColor, cell);
                        continue;
                    }
                    else if (DrawCell != null)                                          // 其他数据类型通过自定义单元格绘制回调处理
                    {
                        var args = new DrawCellEventArgs(e.Graphics, cell, rowObjects, interactionState, cellForeColor, cellBackColor, row, col);
                        DrawCell.Invoke(this, args);
                        if (!args.IsRedrawingNeeded)
                        {
                            continue;
                        }
                    }

                    if (data is Image)                                                  // 数据类型为图像
                    {
                        DrawImage(e.Graphics, cell, (Image)data, cellBackColor);
                    }
                    else if (data is string)                                            // 数据类型为文本
                    {
                        DrawText(e.Graphics, cell, (string)data, Font, cellForeColor, cellBackColor);
                    }
                    else                                                                // 其他数据类型强转为文本
                    {
                        DrawText(e.Graphics, cell, data.ToString(), Font, cellForeColor, cellBackColor);
                    }
                }
            }

            // 绘制表格
            DrawGridLines(e.Graphics, CurrentCellsArea, 1, Color.LightGray);
        }

        /// <summary>
        /// 单元格数据预处理
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="location">数据位置</param>
        /// <returns>处理后的数据</returns>
        protected virtual object PreprocessCellData(object data, Point location) => data;
        #endregion

        #region 单元格区域计算
        /// <summary>
        /// 获取所有单元格的位置
        /// </summary>
        /// <returns>首行为表头单元格的区域，其余为数据单元格区域</returns>
        private Rectangle[][] GetCellsRectangle()
        {
            // 清空所有实际宽度值
            foreach (var x in Columns)
            {
                if (x.Width == 0)
                {
                    x.ActualWidth = 0;
                }
                else
                {
                    x.ActualWidth = x.Width;
                }
            }

            // 计算实际宽度
            var knownWidth = Columns.Where(x => x.Width != 0).Sum(x => x.Width) + 1;
            var unknownWidthCellCount = Columns.Where(x => x.Width == 0).Count();
            var unknownWidth = Width - knownWidth - (scrollBar.Visible ? scrollBar.Width : 0);
            var autoWidth = unknownWidth / (float)unknownWidthCellCount;
            var columnLeftArray = Enumerable.Range(0, unknownWidthCellCount + 1).Select(col => (int)(knownWidth + autoWidth * col)).ToArray();

            for (int i = 0; i < unknownWidthCellCount; i++)
            {
                var cs = Columns.FirstOrDefault(x => x.Width == 0 && x.ActualWidth == 0);
                if (cs == null)
                {
                    break;
                }
                else
                {
                    var width = columnLeftArray[i + 1] - columnLeftArray[i];
                    cs.ActualWidth = width;
                }
            }

            // 计算单元格行高
            var cellHeight = (Height - HeaderHeight) / (float)DisplayRow;
            var rowHeightArray = Enumerable.Range(0, DisplayRow + 1).Select(row => (int)(HeaderHeight + cellHeight * row)).ToArray();

            // 创建表格区域二维数组（总行数=显示行数+表头）
            Rectangle[][] grid = new Rectangle[DisplayRow + 1][];

            // 表头区域
            grid[0] = Columns.Select((c, r) =>
            {
                var location = new Point(Columns.Take(r).Sum(x => x.ActualWidth), 0);
                var rect = new Rectangle(location, new Size(Columns[r].ActualWidth, HeaderHeight));
                return rect;
            }).ToArray();

            // 数据区域
            for (int i = 1; i < grid.Length; i++)
            {
                var rowTop = rowHeightArray[i - 1];
                var rowHeight = rowHeightArray[i] - rowTop;

                grid[i] = Columns.Select((c, r) =>
                {
                    var location = new Point(Columns.Take(r).Sum(x => x.ActualWidth), rowTop);
                    var rect = new Rectangle(location, new Size(Columns[r].ActualWidth, rowHeight));
                    return rect;
                }).ToArray();
            }

            return grid;
        }

        /// <summary>
        /// 获取界面指定一点所在的单元格坐标
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private Point GetCellLocation(Point location)
        {
            if (CurrentCellsArea == null)
            {
                return new Point(-1, -1);
            }

            for (int row = 0; row < CurrentCellsArea.Length; row++)
            {
                for (int col = 0; col < CurrentCellsArea[row].Length; col++)
                {
                    // 当前行列的单元格区域
                    var cell = CurrentCellsArea[row][col];

                    // 坐标点是否位于该单元格内
                    var b = cell.Contains(location);

                    if (b)
                    {
                        return new Point(col, row);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return new Point(-1, -1);
        }

        /// <summary>
        /// 通过单元格位置获取单元格数据
        /// </summary>
        /// <param name="cellLocation"></param>
        /// <returns></returns>
        protected object GetDataByCellLocation(Point cellLocation)
        {
            // 当前鼠标按压单元格真实数据的行列序号
            var rowIndex = cellLocation.Y + TopRowIndex - 1;
            var columnIndex = cellLocation.X;

            // 行号超出选中区域则忽略（比如选中的是无数据空白区域）
            if (rowIndex < 0 || rowIndex > DataSource.Count - 1)
            {
                return null;
            }

            var row = DataSource[rowIndex];
            if (row == null || row.Length <= columnIndex)
            {
                return null;
            }

            return row[columnIndex];
        }
        #endregion

        #region 表格元素绘制
        /// <summary>
        /// 绘制表头单元格
        /// </summary>
        /// <param name="g"></param>
        private void DrawHeaderCells(Graphics g)
        {
            // 第0行为表头单元格区域
            var headerCells = CurrentCellsArea[0];

            // 表头字体、表头背景色
            var headerFont = HeaderFont ?? new Font(Font, FontStyle.Bold);
            var headerBackColor = HeaderBackColor;

            // 绘制表头
            for (int i = 0; i < ColumnCount; i++)
            {
                var cs = Columns[i];
                var cell = headerCells[i];
                DrawText(g, cell, cs.Text, headerFont, ForeColor, headerBackColor);
            }
        }

        /// <summary>
        /// 绘制网格线
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cells"></param>
        /// <param name="lineWidth"></param>
        /// <param name="color"></param>
        private void DrawGridLines(Graphics g, Rectangle[][] cells, int lineWidth, Color color)
        {
            var rowLines = cells.SelectMany(x => new int[] { x.First().Top, x.First().Top + x.First().Height }).Distinct().Select(y => new int[] { 0, y, Width, y }).ToArray();
            var colLines = cells.First().SelectMany(x => new int[] { x.Left, x.Left + x.Width }).Distinct().Select(x => new int[] { x, 0, x, Height }).ToArray();

            DrawLine(g, rowLines, lineWidth, color);
            DrawLine(g, colLines, lineWidth, color);
        }
        #endregion

        #region 基础绘制
        /// <summary>
        /// 在指定矩形区域内绘制文本（居中）
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        protected virtual void DrawText(Graphics g, Rectangle rect, string text, Font font, Color? foreColor, Color? backColor)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // 绘制背景颜色
            FillRectangleColor(g, backColor, rect);

            using (var brush = new SolidBrush(foreColor.Value))
            {
                // 绘制居中的文本
                g.DrawString(text, font, brush, rect, stringFormat);
            }
        }

        /// <summary>
        /// 在指定矩形区域中绘制图像（Zoom模式）
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="image"></param>
        /// <param name="backColor"></param>
        protected virtual void DrawImage(Graphics g, Rectangle rect, Image image, Color? backColor)
        {
            // 计算缩放比例
            float scaleX = (float)rect.Width / image.Width;
            float scaleY = (float)rect.Height / image.Height;
            float scale = Math.Min(scaleX, scaleY);

            // 计算绘制图像的位置和大小
            int drawWidth = (int)(image.Width * scale);
            int drawHeight = (int)(image.Height * scale);
            int drawX = rect.X + (rect.Width - drawWidth) / 2;
            int drawY = rect.Y + (rect.Height - drawHeight) / 2;

            // 绘制背景颜色
            FillRectangleColor(g, backColor, rect);

            // 确保图像完全居中
            if (drawX < rect.X)
            {
                drawX = rect.X;
            }
            else if (drawX + drawWidth > rect.X + rect.Width)
            {
                drawX = rect.X + rect.Width - drawWidth;
            }

            if (drawY < rect.Y)
            {
                drawY = rect.Y;
            }
            else if (drawY + drawHeight > rect.Y + rect.Height)
            {
                drawY = rect.Y + rect.Height - drawHeight;
            }

            // 绘制缩放后的图像
            g.DrawImage(image, new Rectangle(drawX, drawY, drawWidth, drawHeight));
        }

        /// <summary>
        /// 在矩形区域内填充颜色
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="cellArea"></param>
        private void FillRectangleColor(Graphics g, Color? backColor, Rectangle cellArea)
        {
            // 绘制背景颜色
            if (backColor != null)
            {
                using (var brush = new SolidBrush(backColor.Value))
                {
                    g.FillRectangle(brush, cellArea);
                }
            }
        }

        /// <summary>
        /// 绘制线段
        /// </summary>
        /// <param name="g"></param>
        /// <param name="lines"></param>
        /// <param name="lineWidth"></param>
        /// <param name="color"></param>
        private void DrawLine(Graphics g, int[][] lines, int lineWidth, Color color)
        {
            using (var pen = new Pen(color, lineWidth))
            {
                lines.ForEach(line =>
                {
                    g.DrawLine(pen, line[0], line[1], line[2], line[3]);
                });
            }
        }
        #endregion

        #region 滚动条
        /// <summary>
        /// 滚动条发生滑动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // 只要发生滑动立即重绘界面
            Invalidate();
        }
        #endregion

        #region 单元格编辑

        /// <summary>
        /// 注册指定类型的单元格编辑处理函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        public void RegisterTypeEditHandler<T>(EventHandler<CellEventArgs> handler) => RegisterTypeEditHandler(typeof(T), handler);

        /// <summary>
        /// 注册指定类型的单元格编辑处理函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handler"></param>
        public void RegisterTypeEditHandler(Type type, EventHandler<CellEventArgs> handler)
        {
            if (handler == null)
            {
                cellEditFuncDict.Remove(type);
            }
            else
            {
                cellEditFuncDict[type] = handler;
            }
        }
        #endregion
    }
}