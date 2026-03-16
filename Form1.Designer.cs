namespace GigaPrime2D
{
    partial class Form1
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
            this.tableLayoutPanel1 = new GigaPrime2D.SplitTablePanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SignalAxis5 = new System.Windows.Forms.Label();
            this.SignalAxis4 = new System.Windows.Forms.Label();
            this.SignalAxis3 = new System.Windows.Forms.Label();
            this.SignalAxis2 = new System.Windows.Forms.Label();
            this.SignalAxis1 = new System.Windows.Forms.Label();
            this.panel0 = new System.Windows.Forms.Panel();
            this.helpButton = new System.Windows.Forms.Button();
            this.highlightAxis5 = new System.Windows.Forms.CheckBox();
            this.highlightAxis4 = new System.Windows.Forms.CheckBox();
            this.highlightAxis3 = new System.Windows.Forms.CheckBox();
            this.highlightAxis2 = new System.Windows.Forms.CheckBox();
            this.highlightAxis1 = new System.Windows.Forms.CheckBox();
            this.HighLightAxeslabel = new System.Windows.Forms.Label();
            this.ZoomXlabel = new System.Windows.Forms.Label();
            this.hideAxes = new System.Windows.Forms.CheckBox();
            this.showLegend = new System.Windows.Forms.CheckBox();
            this.combineAxes = new System.Windows.Forms.CheckBox();
            this.timerControl = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Pesgo1 = new Gigasoft.ProEssentials.Pesgo();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel0.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel0, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1790, 1049);
            this.tableLayoutPanel1.SplitterSize = 6;
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.SignalAxis5);
            this.panel1.Controls.Add(this.SignalAxis4);
            this.panel1.Controls.Add(this.SignalAxis3);
            this.panel1.Controls.Add(this.SignalAxis2);
            this.panel1.Controls.Add(this.SignalAxis1);
            this.panel1.Location = new System.Drawing.Point(629, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1158, 212);
            this.panel1.TabIndex = 3;
            // 
            // SignalAxis5
            // 
            this.SignalAxis5.AutoSize = true;
            this.SignalAxis5.Location = new System.Drawing.Point(731, 14);
            this.SignalAxis5.Name = "SignalAxis5";
            this.SignalAxis5.Size = new System.Drawing.Size(104, 13);
            this.SignalAxis5.TabIndex = 4;
            this.SignalAxis5.Text = "Signal 5 Scale / Pos";
            // 
            // SignalAxis4
            // 
            this.SignalAxis4.AutoSize = true;
            this.SignalAxis4.Location = new System.Drawing.Point(550, 14);
            this.SignalAxis4.Name = "SignalAxis4";
            this.SignalAxis4.Size = new System.Drawing.Size(104, 13);
            this.SignalAxis4.TabIndex = 3;
            this.SignalAxis4.Text = "Signal 4 Scale / Pos";
            // 
            // SignalAxis3
            // 
            this.SignalAxis3.AutoSize = true;
            this.SignalAxis3.Location = new System.Drawing.Point(368, 14);
            this.SignalAxis3.Name = "SignalAxis3";
            this.SignalAxis3.Size = new System.Drawing.Size(104, 13);
            this.SignalAxis3.TabIndex = 2;
            this.SignalAxis3.Text = "Signal 3 Scale / Pos";
            // 
            // SignalAxis2
            // 
            this.SignalAxis2.AutoSize = true;
            this.SignalAxis2.Location = new System.Drawing.Point(190, 14);
            this.SignalAxis2.Name = "SignalAxis2";
            this.SignalAxis2.Size = new System.Drawing.Size(104, 13);
            this.SignalAxis2.TabIndex = 1;
            this.SignalAxis2.Text = "Signal 2 Scale / Pos";
            // 
            // SignalAxis1
            // 
            this.SignalAxis1.AutoSize = true;
            this.SignalAxis1.Location = new System.Drawing.Point(16, 14);
            this.SignalAxis1.Name = "SignalAxis1";
            this.SignalAxis1.Size = new System.Drawing.Size(104, 13);
            this.SignalAxis1.TabIndex = 0;
            this.SignalAxis1.Text = "Signal 1 Scale / Pos";
            // 
            // panel0
            // 
            this.panel0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel0.AutoScroll = true;
            this.panel0.Controls.Add(this.helpButton);
            this.panel0.Controls.Add(this.highlightAxis5);
            this.panel0.Controls.Add(this.highlightAxis4);
            this.panel0.Controls.Add(this.highlightAxis3);
            this.panel0.Controls.Add(this.highlightAxis2);
            this.panel0.Controls.Add(this.highlightAxis1);
            this.panel0.Controls.Add(this.HighLightAxeslabel);
            this.panel0.Controls.Add(this.ZoomXlabel);
            this.panel0.Controls.Add(this.hideAxes);
            this.panel0.Controls.Add(this.showLegend);
            this.panel0.Controls.Add(this.combineAxes);
            this.panel0.Controls.Add(this.timerControl);
            this.panel0.Location = new System.Drawing.Point(3, 4);
            this.panel0.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(620, 212);
            this.panel0.TabIndex = 4;
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(470, 14);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 24);
            this.helpButton.TabIndex = 17;
            this.helpButton.Text = "Help ?";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // highlightAxis5
            // 
            this.highlightAxis5.AutoSize = true;
            this.highlightAxis5.Location = new System.Drawing.Point(370, 132);
            this.highlightAxis5.Name = "highlightAxis5";
            this.highlightAxis5.Size = new System.Drawing.Size(32, 17);
            this.highlightAxis5.TabIndex = 10;
            this.highlightAxis5.Text = "5";
            this.highlightAxis5.UseVisualStyleBackColor = true;
            // 
            // highlightAxis4
            // 
            this.highlightAxis4.AutoSize = true;
            this.highlightAxis4.Location = new System.Drawing.Point(313, 132);
            this.highlightAxis4.Name = "highlightAxis4";
            this.highlightAxis4.Size = new System.Drawing.Size(32, 17);
            this.highlightAxis4.TabIndex = 9;
            this.highlightAxis4.Text = "4";
            this.highlightAxis4.UseVisualStyleBackColor = true;
            // 
            // highlightAxis3
            // 
            this.highlightAxis3.AutoSize = true;
            this.highlightAxis3.Location = new System.Drawing.Point(255, 132);
            this.highlightAxis3.Name = "highlightAxis3";
            this.highlightAxis3.Size = new System.Drawing.Size(32, 17);
            this.highlightAxis3.TabIndex = 8;
            this.highlightAxis3.Text = "3";
            this.highlightAxis3.UseVisualStyleBackColor = true;
            // 
            // highlightAxis2
            // 
            this.highlightAxis2.AutoSize = true;
            this.highlightAxis2.Location = new System.Drawing.Point(202, 132);
            this.highlightAxis2.Name = "highlightAxis2";
            this.highlightAxis2.Size = new System.Drawing.Size(32, 17);
            this.highlightAxis2.TabIndex = 7;
            this.highlightAxis2.Text = "2";
            this.highlightAxis2.UseVisualStyleBackColor = true;
            // 
            // highlightAxis1
            // 
            this.highlightAxis1.AutoSize = true;
            this.highlightAxis1.Location = new System.Drawing.Point(145, 132);
            this.highlightAxis1.Name = "highlightAxis1";
            this.highlightAxis1.Size = new System.Drawing.Size(32, 17);
            this.highlightAxis1.TabIndex = 6;
            this.highlightAxis1.Text = "1";
            this.highlightAxis1.UseVisualStyleBackColor = true;
            // 
            // HighLightAxeslabel
            // 
            this.HighLightAxeslabel.AutoSize = true;
            this.HighLightAxeslabel.Location = new System.Drawing.Point(13, 134);
            this.HighLightAxeslabel.Name = "HighLightAxeslabel";
            this.HighLightAxeslabel.Size = new System.Drawing.Size(87, 13);
            this.HighLightAxeslabel.TabIndex = 5;
            this.HighLightAxeslabel.Text = "High Light Signal";
            // 
            // ZoomXlabel
            // 
            this.ZoomXlabel.AutoSize = true;
            this.ZoomXlabel.Location = new System.Drawing.Point(13, 101);
            this.ZoomXlabel.Name = "ZoomXlabel";
            this.ZoomXlabel.Size = new System.Drawing.Size(70, 13);
            this.ZoomXlabel.TabIndex = 4;
            this.ZoomXlabel.Text = "Zoom X Axes";
            // 
            // hideAxes
            // 
            this.hideAxes.AutoSize = true;
            this.hideAxes.Location = new System.Drawing.Point(188, 41);
            this.hideAxes.Name = "hideAxes";
            this.hideAxes.Size = new System.Drawing.Size(132, 17);
            this.hideAxes.TabIndex = 2;
            this.hideAxes.Text = "Hide Overlapped Axes";
            this.hideAxes.UseVisualStyleBackColor = true;
            // 
            // showLegend
            // 
            this.showLegend.AutoSize = true;
            this.showLegend.Location = new System.Drawing.Point(10, 68);
            this.showLegend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showLegend.Name = "showLegend";
            this.showLegend.Size = new System.Drawing.Size(92, 17);
            this.showLegend.TabIndex = 3;
            this.showLegend.Text = "Show Legend";
            this.showLegend.UseVisualStyleBackColor = true;
            // 
            // combineAxes
            // 
            this.combineAxes.AutoSize = true;
            this.combineAxes.Location = new System.Drawing.Point(10, 42);
            this.combineAxes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.combineAxes.Name = "combineAxes";
            this.combineAxes.Size = new System.Drawing.Size(93, 17);
            this.combineAxes.TabIndex = 1;
            this.combineAxes.Text = "Combine Axes";
            this.combineAxes.UseVisualStyleBackColor = true;
            // 
            // timerControl
            // 
            this.timerControl.AutoSize = true;
            this.timerControl.Location = new System.Drawing.Point(10, 14);
            this.timerControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timerControl.Name = "timerControl";
            this.timerControl.Size = new System.Drawing.Size(104, 17);
            this.timerControl.TabIndex = 0;
            this.timerControl.Text = "Start/Stop Timer";
            this.timerControl.UseVisualStyleBackColor = true;
            this.timerControl.CheckedChanged += new System.EventHandler(this.timerControl_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.Pesgo1);
            this.panel2.Location = new System.Drawing.Point(3, 223);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1784, 823);
            this.panel2.TabIndex = 5;
            // 
            // Pesgo1
            // 
            this.Pesgo1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pesgo1.Location = new System.Drawing.Point(291, 212);
            this.Pesgo1.Name = "Pesgo1";
            this.Pesgo1.Size = new System.Drawing.Size(916, 165);
            this.Pesgo1.TabIndex = 0;
            this.Pesgo1.Text = "pesgo1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1790, 1049);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(446, 484);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel0.ResumeLayout(false);
            this.panel0.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public SplitTablePanel tableLayoutPanel1;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel0;
        private System.Windows.Forms.CheckBox timerControl;
        private System.Windows.Forms.CheckBox showLegend;
        private System.Windows.Forms.CheckBox combineAxes;
        private System.Windows.Forms.Panel panel2;
        private Gigasoft.ProEssentials.Pesgo Pesgo1;
        private System.Windows.Forms.CheckBox hideAxes;
        private System.Windows.Forms.Label ZoomXlabel;
        private System.Windows.Forms.CheckBox highlightAxis1;
        private System.Windows.Forms.Label HighLightAxeslabel;
        private System.Windows.Forms.CheckBox highlightAxis5;
        private System.Windows.Forms.CheckBox highlightAxis4;
        private System.Windows.Forms.CheckBox highlightAxis3;
        private System.Windows.Forms.CheckBox highlightAxis2;
        private System.Windows.Forms.Label SignalAxis3;
        private System.Windows.Forms.Label SignalAxis2;
        private System.Windows.Forms.Label SignalAxis1;
        private System.Windows.Forms.Label SignalAxis4;
        private System.Windows.Forms.Label SignalAxis5;
        private System.Windows.Forms.Button helpButton;
    }
}

