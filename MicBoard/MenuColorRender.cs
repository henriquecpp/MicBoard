using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicBoard
{
    class MenuColorRender : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return Color.FromArgb(36, 36, 36);
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return Color.FromArgb(36, 36, 36);
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return Color.FromArgb(36, 36, 36);
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return Color.FromArgb(36, 36, 36);
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color MenuItemBorder
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return Color.FromArgb(64, 64, 64);
            }
        }

        public override Color MenuStripGradientBegin
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color MenuStripGradientEnd
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return Color.FromArgb(64, 64, 64);
            }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return Color.FromArgb(64, 64, 64);
            }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return Color.Black;
            }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return Color.Black;
            }
        }
    }
}
