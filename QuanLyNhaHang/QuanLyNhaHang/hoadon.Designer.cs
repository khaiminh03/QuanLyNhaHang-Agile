namespace QuanLyNhaHang
{
    partial class hoadon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.quanLyNhaHangDataSet = new QuanLyNhaHang.QuanLyNhaHangDataSet();
            this.uSPxuatHDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.uSP_xuatHDTableAdapter = new QuanLyNhaHang.QuanLyNhaHangDataSetTableAdapters.USP_xuatHDTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.quanLyNhaHangDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uSPxuatHDBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "HoaDon";
            reportDataSource1.Value = this.uSPxuatHDBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyNhaHang.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // quanLyNhaHangDataSet
            // 
            this.quanLyNhaHangDataSet.DataSetName = "QuanLyNhaHangDataSet";
            this.quanLyNhaHangDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uSPxuatHDBindingSource
            // 
            this.uSPxuatHDBindingSource.DataMember = "USP_xuatHD";
            this.uSPxuatHDBindingSource.DataSource = this.quanLyNhaHangDataSet;
            // 
            // uSP_xuatHDTableAdapter
            // 
            this.uSP_xuatHDTableAdapter.ClearBeforeFill = true;
            // 
            // hoadon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "hoadon";
            this.Text = "hoadon";
            this.Load += new System.EventHandler(this.hoadon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.quanLyNhaHangDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uSPxuatHDBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource uSPxuatHDBindingSource;
        private QuanLyNhaHangDataSet quanLyNhaHangDataSet;
        private QuanLyNhaHangDataSetTableAdapters.USP_xuatHDTableAdapter uSP_xuatHDTableAdapter;
    }
}