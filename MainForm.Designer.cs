namespace TempReaderWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TemperaturePlot = new OxyPlot.WindowsForms.PlotView();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.info2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LogLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.HumidityPlot = new OxyPlot.WindowsForms.PlotView();
            this.SerialDataLogger = new System.Windows.Forms.RichTextBox();
            this.SmoothedTemperaturePlot = new OxyPlot.WindowsForms.PlotView();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TemperaturePlot
            // 
            this.TemperaturePlot.Location = new System.Drawing.Point(12, 12);
            this.TemperaturePlot.Name = "TemperaturePlot";
            this.TemperaturePlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.TemperaturePlot.Size = new System.Drawing.Size(300, 300);
            this.TemperaturePlot.TabIndex = 0;
            this.TemperaturePlot.Text = "plotView1";
            this.TemperaturePlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.TemperaturePlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.TemperaturePlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 2000;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.label1,
            this.MessageLabel,
            this.info2,
            this.LogLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 630);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(938, 22);
            this.MainStatusStrip.TabIndex = 2;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.Text = "Error:";
            // 
            // MessageLabel
            // 
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(23, 17);
            this.MessageLabel.Text = "OK";
            // 
            // info2
            // 
            this.info2.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.info2.Name = "info2";
            this.info2.Size = new System.Drawing.Size(30, 17);
            this.info2.Text = "Log:";
            // 
            // LogLabel
            // 
            this.LogLabel.Name = "LogLabel";
            this.LogLabel.Size = new System.Drawing.Size(29, 17);
            this.LogLabel.Text = "N/A";
            // 
            // HumidityPlot
            // 
            this.HumidityPlot.Location = new System.Drawing.Point(318, 12);
            this.HumidityPlot.Name = "HumidityPlot";
            this.HumidityPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.HumidityPlot.Size = new System.Drawing.Size(300, 300);
            this.HumidityPlot.TabIndex = 3;
            this.HumidityPlot.Text = "plotView1";
            this.HumidityPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.HumidityPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.HumidityPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // SerialDataLogger
            // 
            this.SerialDataLogger.Location = new System.Drawing.Point(624, 12);
            this.SerialDataLogger.Name = "SerialDataLogger";
            this.SerialDataLogger.Size = new System.Drawing.Size(300, 606);
            this.SerialDataLogger.TabIndex = 4;
            this.SerialDataLogger.Text = "";
            // 
            // SmoothedTemperaturePlot
            // 
            this.SmoothedTemperaturePlot.Location = new System.Drawing.Point(12, 318);
            this.SmoothedTemperaturePlot.Name = "SmoothedTemperaturePlot";
            this.SmoothedTemperaturePlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.SmoothedTemperaturePlot.Size = new System.Drawing.Size(300, 300);
            this.SmoothedTemperaturePlot.TabIndex = 5;
            this.SmoothedTemperaturePlot.Text = "plotView1";
            this.SmoothedTemperaturePlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.SmoothedTemperaturePlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.SmoothedTemperaturePlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 652);
            this.Controls.Add(this.SmoothedTemperaturePlot);
            this.Controls.Add(this.SerialDataLogger);
            this.Controls.Add(this.HumidityPlot);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.TemperaturePlot);
            this.Name = "MainForm";
            this.Text = "Temperature reader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView TemperaturePlot;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel label1;
        private System.Windows.Forms.ToolStripStatusLabel MessageLabel;
        private System.Windows.Forms.ToolStripStatusLabel info2;
        private System.Windows.Forms.ToolStripStatusLabel LogLabel;
        private OxyPlot.WindowsForms.PlotView HumidityPlot;
        private System.Windows.Forms.RichTextBox SerialDataLogger;
        private OxyPlot.WindowsForms.PlotView SmoothedTemperaturePlot;
    }
}

