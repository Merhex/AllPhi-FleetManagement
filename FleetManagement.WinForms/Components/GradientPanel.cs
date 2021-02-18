using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManagement.Blazor.Components
{
    public class GradientPanel : Panel
    {
        public Color FadeFromColor { get; set; }
        public Color FadeToColor { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            var linearGradientBrush = new LinearGradientBrush(ClientRectangle, FadeFromColor, FadeToColor, 90F);
            var graphics = e.Graphics;

            graphics.FillRectangle(linearGradientBrush, ClientRectangle);

            base.OnPaint(e);
        }
    }
}
