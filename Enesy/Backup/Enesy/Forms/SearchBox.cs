using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
            
            this.ContextMenuStrip = this.mnusColumn;
            this.mnusColumn.ItemClicked += 
                new ToolStripItemClickedEventHandler(mnusColumn_ItemClicked);
        }

        #region Properties & Field

        /// <summary>
        /// Contextmenustrip to show & select column
        /// </summary>
        private ContextMenuStrip mnusColumn = new ContextMenuStrip();

        /// <summary>
        /// DataSource for filter
        /// </summary>
        private object dataSource = null;

        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Category("Data")]
        [DefaultValue(null)]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (this.dataSource != value)
                {
                    this.dataSource = value;
                    OnDataSourceChanged();
                }
            }
        }

        protected virtual void OnDataSourceChanged()
        {
            this.txtFilter.DataSource = this.dataSource;
            if (this.dataSource != null)
            {
                DataTable dt = this.dataSource as DataTable;
                DataColumnCollection dcc = dt.Columns;
                if (dcc.Count == 0) return;

                mnusColumn.Items.Clear();
                foreach (DataColumn dc in dcc)
                {
                    mnusColumn.Items.Add(dc.ColumnName);
                }
            }
        }

        #endregion

        private void butColumn_Click(object sender, EventArgs e)
        {
            mnusColumn.Show(Cursor.Position);            
        }

        void mnusColumn_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.txtFilter.DisplayMember = e.ClickedItem.Text;
        }
    }
}