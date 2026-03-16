using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GigaPrime2D
{

    internal class NoFocusTrackBar : System.Windows.Forms.TrackBar
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        private static int MakeParam(int loWord, int hiWord)
        {
            return (hiWord << 16) | (loWord & 0xffff);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            SendMessage(this.Handle, 0x0128, MakeParam(1, 0x1), 0);
        }
    }



    public partial class SplitTablePanel : TableLayoutPanel
    {

        public void BeginUpdate()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        public void EndUpdate()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            Parent.Invalidate(true);
        }


        public int SplitterSize { get; set; }

        int sizingRow = -1;
        int currentRow = -1;
        Point mdown = Point.Empty;
        int oldHeight = -1;
        bool isNormalRow = false;
        List<RectangleF> tlpRows = new List<RectangleF>();
        int[] rowHeights = new int[0];

        int sizingCol = -1;
        int currentCol = -1;
        int oldWidth = -1;
        bool isNormalCol = false;
        List<RectangleF> tlpCols = new List<RectangleF>();
        int[] colWidths = new int[0];

        public SplitTablePanel()
        {
            //InitializeComponent();
            this.MouseDown += SplitTablePanel_MouseDown;
            this.MouseMove += SplitTablePanel_MouseMove;
            this.MouseUp += SplitTablePanel_MouseUp;
            this.MouseLeave += SplitTablePanel_MouseLeave;
            this.Resize += SplitTablePanel_Resize;
            SplitterSize = 6;
        }

        void SplitTablePanel_Resize(object sender, EventArgs e)
        {
            getRowRectangles(SplitterSize);
            getColRectangles(SplitterSize);
        }
        void SplitTablePanel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }


        void SplitTablePanel_MouseUp(object sender, MouseEventArgs e)
        {
            getRowRectangles(SplitterSize);
            getColRectangles(SplitterSize);
        }

        public void JumpStartToPreventFlicker()
        {
            // Not sure why this is needed, I tried a bit to step through the included
            // logic and seems maybe the way the child controls are aligned or listbox
            // with integral height, but something is causing an initial flicker when app 
            // launches and mouse first moves over splitter. 
            MouseEventArgs ev;
            ev = new MouseEventArgs(System.Windows.Forms.MouseButtons.Right, 0, 30, 30, 0);
            bool r = rowMove(this, ev);
            bool c = colMove(this, ev);
            r = rowMove(this, ev); // not sure why two calls are needed, but the listbox integral height adjusts on second call 
            c = colMove(this, ev);
        }

        void SplitTablePanel_MouseMove(object sender, MouseEventArgs e)
        {
            bool r = rowMove(sender, e);
            bool c = colMove(sender, e);    

            if (r && !c)
                Cursor = Cursors.SizeNS;
            else if (!r && c)
                Cursor = Cursors.SizeWE;
            else if (r && c)
                Cursor = Cursors.SizeAll;
            else
                Cursor = Cursors.Default;
        }

        bool rowMove(object sender, MouseEventArgs e)
        {
            bool isMove = false;
            if (!isNormalRow) nomalizeRowStyles();
            if (tlpRows.Count <= 0) getRowRectangles(SplitterSize);
            if (rowHeights.Length <= 0) rowHeights = GetRowHeights();

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (sizingRow < 0) return false;
                int newHeight = oldHeight + e.Y - mdown.Y;

                sizeRow(sizingRow, newHeight);
                isMove = true;
            }
            else
            {
                currentRow = -1;
                for (int i = 0; i < tlpRows.Count; i++)
                if (tlpRows[i].Contains(e.Location)) 
                { 
                    currentRow = i;
                    isMove = true;
                    break;
                }
            }
            return isMove;
        }

        bool colMove(object sender, MouseEventArgs e)
        {
            bool isMove = false;
            if (!isNormalCol) nomalizeColStyles();
            if (tlpCols.Count <= 0) getColRectangles(SplitterSize);
            if (colWidths.Length <= 0) colWidths = GetColumnWidths();

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (sizingCol < 0) return false;
                int newWidth = oldWidth + e.X - mdown.X;
                sizeCol(sizingCol, newWidth);
                isMove = true;
            }
            else
            {
                currentCol = -1;
                for (int i = 0; i < tlpCols.Count; i++)
                if (tlpCols[i].Contains(e.Location)) 
                {
                    currentCol = i; 
                    isMove = true;
                    break;
                }
            }
            return isMove;
        }

        void SplitTablePanel_MouseDown(object sender, MouseEventArgs e)
        {
            mdown = Point.Empty;
            rowDown();   
            colDown();
            mdown = e.Location;
        }

        void rowDown()
        {
            sizingRow = -1;
            if (currentRow < 0) return;
            sizingRow = currentRow;
            oldHeight = rowHeights[sizingRow];
        }

        void colDown()
        {
            sizingCol = -1;
            if (currentCol < 0) return;
            sizingCol = currentCol;
            oldWidth = colWidths[sizingCol];
        }


        void getRowRectangles(float size)
        {   // get a list of mouse sensitive rectangles
            float sz = size / 2f;
            float y = 0f;
            int w = ClientSize.Width;
            int[] rw = GetRowHeights();

            if (rw.Length < 2)
                return;

            if (rw[1] < 175)  // code specific to 2 rows 
            {
                int hTotal = rw[0] + rw[1];
                rw[1] = 175;
                rw[0] = hTotal - 175;
                if (rw[0] < 0) { rw[0] = 0; }
                for (int i = 0; i < rw.Length - 1; i++)
                {
                    RowStyles[i] = new RowStyle(SizeType.Absolute, rw[i]);
                }
                rowHeights = GetRowHeights();
            }

            tlpRows.Clear();
            for (int i = 0; i < rw.Length - 1; i++)
            {
                y += rw[i];
                tlpRows.Add(new RectangleF(0, y - sz, w, size));
            }

        }

        void getColRectangles(float size)
        {   // get a list of mouse sensitive rectangles
            float sz = size / 2f;
            float x = 0f;
            int h = ClientSize.Height;
            int[] rw = GetColumnWidths();

            tlpCols.Clear();
            for (int i = 0; i < rw.Length - 1; i++)
            {
                x += rw[i];
                tlpCols.Add(new RectangleF(x - sz, 0, size, h));
            }

        }

        void sizeRow(int row, int newHeight)
        {   // change the height of one row
            if (newHeight == 0) return;
            if (sizingRow < 0) return;

            SuspendLayout();
            rowHeights = GetRowHeights();
            if (sizingRow >= rowHeights.Length) return;

            if (newHeight > 0)
            {
                if (row != 0) // code specific to 2 rows 
                {
                    int nH = newHeight;
                    if (nH <= 175) { nH = 175; }
                    if (this.Height - nH > 175)
                    {
                        RowStyles[sizingRow] = new RowStyle(SizeType.Absolute, nH);
                    }
                }
                else
                {
                    int nH = newHeight;
                    if (nH <= 26) { nH = 26; }
                    if (this.Height - nH > 26)
                    {
                        RowStyles[sizingRow] = new RowStyle(SizeType.Absolute, nH);
                    }
                }
            }

            ResumeLayout();
            rowHeights = GetRowHeights();
            getRowRectangles(SplitterSize);
        }

        void sizeCol(int col, int newWidth)
        {   // change the height of one row
            if (newWidth == 0) return;
            if (sizingCol < 0) return;
            SuspendLayout();
            colWidths = GetColumnWidths();
            if (sizingCol >= colWidths.Length) return;

            if (newWidth > 0) 
                ColumnStyles[sizingCol] = new ColumnStyle(SizeType.Absolute, newWidth);
            ResumeLayout();
            colWidths = GetColumnWidths();
            getColRectangles(SplitterSize);
        }

        void nomalizeRowStyles()
        {   // set all rows to absolute and the last one to percent=100!
            if (rowHeights.Length <= 0) return;
            rowHeights = GetRowHeights();
            RowStyles.Clear();
            for (int i = 0; i < RowCount - 1; i++)
            {
                RowStyle cs = new RowStyle(SizeType.Absolute, rowHeights[i]);
                RowStyles.Add(cs);
            }
            RowStyles.Add ( new RowStyle(SizeType.Percent, 100) );
            isNormalRow = true;
        }

        void nomalizeColStyles()
        {   // set all rows to absolute and the last one to percent=100!
            if (colWidths.Length <= 0) return;
            colWidths = GetColumnWidths();
            ColumnStyles.Clear();
            for (int i = 0; i < ColumnCount - 1; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Absolute, colWidths[i]);
                ColumnStyles.Add(cs);
            }
            ColumnStyles.Add ( new ColumnStyle(SizeType.Percent, 100) );
            isNormalCol = true;
        }
    }

   public static class NativeMethods
   {
       public static int WM_SETREDRAW = 0x000B; //uint WM_SETREDRAW
       public static int WS_EX_COMPOSITED = 0x02000000;

       [DllImport("user32.dll", CharSet = CharSet.Auto)]
       public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam); //UInt32 Msg
   }

}
