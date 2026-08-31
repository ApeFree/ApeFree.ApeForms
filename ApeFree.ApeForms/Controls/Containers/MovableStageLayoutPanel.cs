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
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Controls.Containers
{
    /// <summary>
    /// 舞台布局面板（支持运动动画）
    /// </summary>
    public class MovableStageLayoutPanel : StageLayoutPanel
    {
        /// <summary>
        /// 移动速率等级
        /// </summary>
        public byte MovingRateLevel { get; set; } = 2;

        /// <inheritdoc/>
        protected override void ControlLocationChangeHandler(Control control, Point point)
        {
            control.LocationGradualChange(point, MovingRateLevel);
        }

        /// <inheritdoc/>
        protected override void ControlSizeChangeHandler(Control control, Size size)
        {
            control.SizeGradualChange(size, MovingRateLevel);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MovableStageLayoutPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "MovableStageLayoutPanel";
            this.Size = new System.Drawing.Size(423, 167);
            this.ResumeLayout(false);

        }
    }
}
