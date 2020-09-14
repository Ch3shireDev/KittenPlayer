// Source code from CodeProject:
// www.codeproject.com/Articles/9188/Embedding-Controls-in-a-ListView
// Original author: mav.northwind (www.codeproject.com/Members/mav-northwind)
// Code is distributed under The Code Project Open Licence (CPOL)

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KittenPlayer
{
    public class ListViewEx : ListView
    {
        private  ArrayList _embeddedControls { get; } = new ArrayList();

        [DefaultValue(View.LargeIcon)]
        public new View View
        {
            get => base.View;
            set
            {
                foreach (EmbeddedControl ec in _embeddedControls)
                    ec.Control.Visible = value == View.Details;
                base.View = value;
            }
        }

        protected int[] GetColumnOrder()
        {
            var lPar = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * Columns.Count);

            var res = SendMessage(Handle, LVM_GETCOLUMNORDERARRAY, new IntPtr(Columns.Count), lPar);
            if (res.ToInt32() == 0)
            {
                Marshal.FreeHGlobal(lPar);
                return null;
            }

            var order = new int[Columns.Count];
            Marshal.Copy(lPar, order, 0, Columns.Count);

            Marshal.FreeHGlobal(lPar);

            return order;
        }

        protected Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
        {
            var subItemRect = Rectangle.Empty;

            if (Item == null)
                throw new ArgumentNullException("Item");

            var order = GetColumnOrder();
            if (order == null) return subItemRect;

            if (SubItem >= order.Length)
                throw new IndexOutOfRangeException("SubItem " + SubItem + " out of range");
            var lviBounds = Item.GetBounds(ItemBoundsPortion.Entire);
            var subItemX = lviBounds.Left;
            ColumnHeader col;
            int i;
            for (i = 0; i < order.Length; i++)
            {
                col = Columns[order[i]];
                if (col.Index == SubItem) break;
                subItemX += col.Width;
            }

            subItemRect = new Rectangle(subItemX, lviBounds.Top, Columns[order[i]].Width, lviBounds.Height);

            return subItemRect;
        }

        public void AddEmbeddedControl(Control c, int col, int row)
        {
            AddEmbeddedControl(c, col, row, DockStyle.Fill);
        }

        public void AddEmbeddedControl(Control c, int col, int row, DockStyle dock)
        {
            if (col >= Columns.Count || row >= Items.Count)
                throw new ArgumentOutOfRangeException();

            EmbeddedControl ec;
            ec.Control = c ?? throw new ArgumentNullException();
            ec.Column = col;
            ec.Row = row;
            ec.Dock = dock;
            ec.Item = Items[row];

            _embeddedControls.Add(ec);

            c.Click += _embeddedControl_Click;

            Controls.Add(c);
        }

        public void RemoveEmbeddedControl(Control c)
        {
            if (c == null) throw new ArgumentNullException();

            for (var i = 0; i < _embeddedControls.Count; i++)
            {
                var ec = (EmbeddedControl)_embeddedControls[i];
                if (ec.Control == c)
                {
                    c.Click -= _embeddedControl_Click;
                    Controls.Remove(c);
                    _embeddedControls.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Control not found!");
        }

        public Control GetEmbeddedControl(int col, int row)
        {
            foreach (EmbeddedControl ec in _embeddedControls)
                if (ec.Row == row && ec.Column == col)
                    return ec.Control;
            return null;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_PAINT:
                    if (View != View.Details)
                        break;
                    foreach (EmbeddedControl ec in _embeddedControls)
                    {
                        var rc = GetSubItemBounds(ec.Item, ec.Column);
                        if (HeaderStyle != ColumnHeaderStyle.None &&
                            rc.Top < Font.Height)
                        {
                            ec.Control.Visible = false;
                            continue;
                        }
                        ec.Control.Visible = true;

                        switch (ec.Dock)
                        {
                            case DockStyle.Fill:
                                break;

                            case DockStyle.Top:
                                rc.Height = ec.Control.Height;
                                break;

                            case DockStyle.Left:
                                rc.Width = ec.Control.Width;
                                break;

                            case DockStyle.Bottom:
                                rc.Offset(0, rc.Height - ec.Control.Height);
                                rc.Height = ec.Control.Height;
                                break;

                            case DockStyle.Right:
                                rc.Offset(rc.Width - ec.Control.Width, 0);
                                rc.Width = ec.Control.Width;
                                break;

                            case DockStyle.None:
                                rc.Size = ec.Control.Size;
                                break;
                        }
                        ec.Control.Bounds = rc;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void _embeddedControl_Click(object sender, EventArgs e)
        {
            foreach (EmbeddedControl ec in _embeddedControls)
                if (ec.Control == (Control)sender)
                {
                    SelectedItems.Clear();
                    ec.Item.Selected = true;
                }
        }

        private struct EmbeddedControl
        {
            public Control Control;
            public int Column;
            public int Row;
            public DockStyle Dock;
            public ListViewItem Item;
        }


        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wPar, IntPtr lPar);

        // ListView messages
        private const int LVM_FIRST = 0x1000;

        private const int LVM_GETCOLUMNORDERARRAY = LVM_FIRST + 59;

        // Windows Messages
        private const int WM_PAINT = 0x000F;

    }
}