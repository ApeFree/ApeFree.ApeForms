using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls.Container
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
