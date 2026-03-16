using Gigasoft.ProEssentials.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GigaPrime2D
{
    public partial class Form1 : Form
    {
        internal bool bLoaded = false;

        public static float[] wavedata = new float[10000];
        public static float[] wavedata2 = new float[10000];
        public static float[] wavedata3 = new float[10000];
        public static float[] wavedata4 = new float[10000];
        public static float[] wavedata5 = new float[10000];

        public static float[] fYDataPool = new float[120010000];      // One time use buffer where we prepare demo data 
        public static float[] fXData = new float[20000000];           // All subsets share this x data

        public static float[] fYDataToChart = new float[100000000];   // Buffer completely refilled randomly from fYDataPool each timer tick
        // This app passes the pointer of this array to the chart, no need to pass data to chart,
        // chart will forward this data to the gpu so the gpu compute shaders can process. 

        public static Random Rand_Num = new Random(unchecked((int)DateTime.Now.Ticks));

        private int _frameCount = 0;
        private DateTime _lastFpsTime = DateTime.Now;

        internal static NoFocusTrackBar signal1ScaleBar;
        internal static NoFocusTrackBar signal1PositionBar;
        internal static NoFocusTrackBar signal2ScaleBar;
        internal static NoFocusTrackBar signal2PositionBar;
        internal static NoFocusTrackBar signal3ScaleBar;
        internal static NoFocusTrackBar signal3PositionBar;
        internal static NoFocusTrackBar signal4ScaleBar;
        internal static NoFocusTrackBar signal4PositionBar;
        internal static NoFocusTrackBar signal5ScaleBar;
        internal static NoFocusTrackBar signal5PositionBar;
        internal static NoFocusTrackBar sliderSampleView;
        internal static System.Windows.Forms.Timer Timer1;

        public Form1()
        {
            InitializeComponent();

            Pesgo1.PeZoomIn += Pesgo1_PeZoomIn;
            Pesgo1.PeZoomOut += Pesgo1_PeZoomOut;

            combineAxes.CheckedChanged += CombineAxes_CheckedChanged;
            hideAxes.CheckedChanged += HideAxes_CheckedChanged;
            showLegend.CheckedChanged += ShowLegend_CheckedChanged;

            highlightAxis1.CheckedChanged += HighlightAxis1_CheckedChanged;
            highlightAxis2.CheckedChanged += HighlightAxis2_CheckedChanged;
            highlightAxis3.CheckedChanged += HighlightAxis3_CheckedChanged;
            highlightAxis4.CheckedChanged += HighlightAxis4_CheckedChanged;
            highlightAxis5.CheckedChanged += HighlightAxis5_CheckedChanged;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Text = "Thank you";

            Pesgo1.Left = 0;
            Pesgo1.Top = 0;
            Pesgo1.Width = panel2.Width;
            Pesgo1.Height = panel2.Height;
            Pesgo1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer1 = new Timer(); 
            Timer1.Tick += new System.EventHandler(Timer1_Tick);

            int nShift, j;
            for (j = 0; j < 20000000; j++)  // simple x data count //
                fXData[j] = (j + 1);

            for (j = 0; j <= 9999; j++)  // various waves
            {
                wavedata[j] = (((float)Math.Sin(3.1415 * 0.0002F * j)) * 10.0F) + 10.0F;
                wavedata2[j] = (((float)Math.Sin(3.1415 * 0.0001F * j)) * 20.0F);
                wavedata3[j] = (((float)Math.Sin(3.1415 * 0.0006F * j)) * 10.0F) + 10.0F;
                wavedata4[j] = ((float)Math.Sin(3.1415 * 0.00005F * j)) * 20.0F; 
                wavedata5[j] = ((float)Math.Sin(2.5415 * 0.0004F * j) * (float)Math.Sin(3.1415 * 0.0001F * j) * 10.0F) + 10.0F;
            }

            // Fill large buffers repeating above waveform data //

            nShift = (int)((float)(Rand_Num.NextDouble()) * 9000.0F);
            for (j = 0; j < 24000000; j += 10000)
                Array.Copy(wavedata, 0, fYDataPool, j, 10000);

            nShift = (int)((float)(Rand_Num.NextDouble()) * 9000.0F);
            Array.Copy(wavedata2, nShift, fYDataPool, 24000000, 10000 - nShift);
            for (j = nShift; j < 24000000; j += 10000)
                Array.Copy(wavedata2, 0, fYDataPool, j + 24000000, 10000);

            nShift = (int)((float)(Rand_Num.NextDouble()) * 9000.0F);
            Array.Copy(wavedata3, nShift, fYDataPool, 48000000, 10000 - nShift);
            for (j = nShift; j < 24000000; j += 10000)
                Array.Copy(wavedata3, 0, fYDataPool, j + 48000000, 10000);

            nShift = (int)((float)(Rand_Num.NextDouble()) * 9000.0F);
            Array.Copy(wavedata4, nShift, fYDataPool, 72000000, 10000 - nShift);
            for (j = nShift; j < 24000000; j += 10000)
                Array.Copy(wavedata4, 0, fYDataPool, j + 72000000, 10000);

            nShift = (int)((float)(Rand_Num.NextDouble()) * 9000.0F);
            Array.Copy(wavedata5, nShift, fYDataPool, 96000000, 10000 - nShift);
            for (j = nShift; j < 24000000; j += 10000)
                Array.Copy(wavedata5, 0, fYDataPool, j + 96000000, 10000);

            // Initialize ProEssentials Pesgo Chart //

            Pesgo1.PeFont.SizeGlobalCntl = 1.05F;  // tweak font sizes a bit larger 

            Pesgo1.PeData.Subsets = 5;
            Pesgo1.PeData.Points = 20000000;  // 20M points x 5 subsets =  100M continuously new data points per update 

            Pesgo1.PeGrid.MultiAxesSubsets[0] = 1; // define 5 axes, 1 subset per axis 
            Pesgo1.PeGrid.MultiAxesSubsets[1] = 1;
            Pesgo1.PeGrid.MultiAxesSubsets[2] = 1;
            Pesgo1.PeGrid.MultiAxesSubsets[3] = 1;
            Pesgo1.PeGrid.MultiAxesSubsets[4] = 1;

            // x axis parameters 
            Pesgo1.PeGrid.Configure.ManualScaleControlX = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinX = 0;
            Pesgo1.PeGrid.Configure.ManualMaxX = 20000000;
            Pesgo1.PeString.XAxisLabel = "Sample";

            // y axis parameters per axis index (WorkingAxis)
            Pesgo1.PeGrid.WorkingAxis = 0;
            Pesgo1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinY = 0;
            Pesgo1.PeGrid.Configure.ManualMaxY = 21;
            Pesgo1.PeString.YAxisLabel = "uV";

            Pesgo1.PeGrid.WorkingAxis = 1;
            Pesgo1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinY = 0;
            Pesgo1.PeGrid.Configure.ManualMaxY = 21;
            Pesgo1.PeString.YAxisLabel = "uV";

            Pesgo1.PeGrid.WorkingAxis = 2;
            Pesgo1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinY = 0;
            Pesgo1.PeGrid.Configure.ManualMaxY = 21;
            Pesgo1.PeString.YAxisLabel = "mV";

            Pesgo1.PeGrid.WorkingAxis = 3;
            Pesgo1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinY = 0;
            Pesgo1.PeGrid.Configure.ManualMaxY = 21;
            Pesgo1.PeString.YAxisLabel = "mV";

            Pesgo1.PeGrid.WorkingAxis = 4;
            Pesgo1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax;
            Pesgo1.PeGrid.Configure.ManualMinY = 0;
            Pesgo1.PeGrid.Configure.ManualMaxY = 21;
            Pesgo1.PeString.YAxisLabel = "uV";
            Pesgo1.PeGrid.WorkingAxis = 0;

            // Reset default 4 X data points and reset default 4 y data points //
            Pesgo1.PeData.Y[0, 0] = 0; Pesgo1.PeData.Y[0, 1] = 0; Pesgo1.PeData.Y[0, 2] = 0; Pesgo1.PeData.Y[0, 3] = 0;
            Pesgo1.PeData.X[0, 0] = 1.0F; Pesgo1.PeData.X[0, 1] = 2.0F;  Pesgo1.PeData.X[0, 2] = 3.0F; Pesgo1.PeData.X[0, 3] = 4.0F;

            Pesgo1.PeData.NullDataValue = -9999999;  //  default null is zero, this allows zero data 
            Pesgo1.PeData.NullDataValueX = -9999999;

            //Set various properties //
            Pesgo1.PeString.MainTitle = "";
            Pesgo1.PeString.SubTitle = "";

            // Disable much of the built in user interface //
            Pesgo1.PeUserInterface.Allow.FocalRect = false;
            Pesgo1.PeUserInterface.Dialog.PlotCustomization = false;
            Pesgo1.PeUserInterface.Dialog.Page2 = true;
            Pesgo1.PeUserInterface.Dialog.Axis = false;
            Pesgo1.PeUserInterface.Dialog.Subsets = false;
            Pesgo1.PeUserInterface.Dialog.RandomPointsToExport = false;
            Pesgo1.PeUserInterface.Allow.Customization = false;
            Pesgo1.PeUserInterface.Allow.Maximization = false;
            Pesgo1.PeUserInterface.Allow.Popup = true;
            Pesgo1.PeUserInterface.Menu.BorderType = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.BitmapGradient = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.QuickStyle = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.ViewingStyle = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.ShowLegend = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.PlotMethod = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.MarkDataPoints = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.CustomizeDialog = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.DataShadow = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.DataPrecision = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.LegendLocation = MenuControl.Hide;  
            Pesgo1.PeUserInterface.Menu.ShowAnnotations = MenuControl.Hide;
            Pesgo1.PeUserInterface.Menu.AnnotationControl = false;
            Pesgo1.PeUserInterface.Dialog.AllowEmfExport = false;
            Pesgo1.PeUserInterface.Dialog.AllowSvgExport = false;
            Pesgo1.PeUserInterface.Dialog.AllowWmfExport = false;
            Pesgo1.PeUserInterface.Allow.TextExport = false;
            Pesgo1.PeUserInterface.Dialog.HideExportImageDpi = true;
            Pesgo1.PeUserInterface.Dialog.HidePrintDpi = true;

            // Set zooming and scrollbar settings  
            Pesgo1.PeUserInterface.Scrollbar.ScrollingHorzZoom = true;
            Pesgo1.PeUserInterface.Scrollbar.MouseWheelFunction = MouseWheelFunction.HorizontalZoom;
            Pesgo1.PeUserInterface.Scrollbar.MouseWheelZoomFactor = 1.05F;
            Pesgo1.PeUserInterface.Scrollbar.MouseWheelZoomEvents = true;
            Pesgo1.PeUserInterface.Allow.Zooming = AllowZooming.None; // rely on MouseWheelFunction or external UI Slider 

            Pesgo1.PeString.SubsetLabels[0] = "Signal 1";
            Pesgo1.PeString.SubsetLabels[1] = "Signal 2";
            Pesgo1.PeString.SubsetLabels[2] = "Signal 3";
            Pesgo1.PeString.SubsetLabels[3] = "Signal 4";
            Pesgo1.PeString.SubsetLabels[4] = "Signal 5";

            Pesgo1.PeColor.SubsetColors[0] = Color.FromArgb(255, 255, 0, 69);
            Pesgo1.PeColor.SubsetColors[1] = Color.FromArgb(255, 63, 255, 0);
            Pesgo1.PeColor.SubsetColors[2] = Color.FromArgb(255, 255, 168, 0);
            Pesgo1.PeColor.SubsetColors[3] = Color.FromArgb(255, 255, 20, 255);
            Pesgo1.PeColor.SubsetColors[4] = Color.FromArgb(255, 26, 255, 255);
            Pesgo1.PeColor.SubsetShades[0] = Color.FromArgb(255, 80, 80, 80);
            Pesgo1.PeColor.SubsetShades[1] = Color.FromArgb(255, 100, 100, 100);
            Pesgo1.PeColor.SubsetShades[2] = Color.FromArgb(255, 60, 60, 60);
            Pesgo1.PeColor.SubsetShades[3] = Color.FromArgb(255, 120, 120, 120);
            Pesgo1.PeColor.SubsetShades[4] = Color.FromArgb(255, 50, 50, 50);

            Pesgo1.PePlot.DataShadows = DataShadows.None;
            Pesgo1.PePlot.SubsetLineTypes[0] = LineType.ThinSolid;
            Pesgo1.PePlot.SubsetLineTypes[1] = LineType.ThinSolid;
            Pesgo1.PePlot.SubsetLineTypes[2] = LineType.ThinSolid;
            Pesgo1.PePlot.SubsetLineTypes[3] = LineType.ThinSolid;
            Pesgo1.PePlot.SubsetLineTypes[4] = LineType.ThinSolid;

            // Set various export defaults //
            Pesgo1.PeSpecial.DpiX = 600;
            Pesgo1.PeSpecial.DpiY = 600;

            Pesgo1.PeUserInterface.Cursor.HourGlassThreshold = 2000000000; // disable hourglass, rendering Direct3D via compute shader
            Pesgo1.PeFont.FontSize = FontSize.Large;
            Pesgo1.PeFont.Fixed = true; // almost always the best choice 
            Pesgo1.PeLegend.Show = false;

            Pesgo1.PeConfigure.ImageAdjustTop = 100; // add a bit of extra space outside edge of chart, 100 for 100% of a character 
            Pesgo1.PeConfigure.ImageAdjustLeft = 100;

            Pesgo1.PeSpecial.AutoImageReset = false;  // important for D3D rendering, need to call Reinitialize or ReinitializeResetImage explicitly  

            // Composite2D3D // Only one D2D layer in back
            Pesgo1.PeConfigure.Composite2D3D = Composite2D3D.Background;

            // Non data Color settings //
            Pesgo1.PeColor.BitmapGradientMode = false;
            Pesgo1.PeConfigure.BorderTypes = TABorder.NoBorder;
            Pesgo1.PeColor.GraphBmpStyle = BitmapStyle.NoBmp;
            Pesgo1.PeColor.GraphBackground = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);
            Pesgo1.PeColor.Desk = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);
            Pesgo1.PeColor.GraphForeground = Color.White;
            Pesgo1.PeColor.Text = Color.White;
            Pesgo1.PeGrid.GridBands = false;
            Pesgo1.PeColor.GridBold = false;

            Pesgo1.PeConfigure.CacheBmp = true;
            Pesgo1.PeConfigure.PrepareImages = true;

            Pesgo1.PeConfigure.RenderEngine = RenderEngine.Direct3D;

            Pesgo1.PeData.DuplicateDataX = DuplicateData.PointIncrement;  // Instructs chart to share x data for all subsets/signals

            //// Provides chart with pointers to XData and YData.  Avoids chart from having to hold its own copy of data. 
            Pesgo1.PeData.X.UseDataAtLocation(fXData, 20000000); // 20 Million XData 
            Pesgo1.PeData.Y.UseDataAtLocation(fYDataToChart, 100000000); // 100 Million 

            //// v10's new feature, enabling computer shader settings for gpu side chart construction // 
            Pesgo1.PeData.ComputeShader = true;   // For now, only set for PlottingMethod = Line, or ContourColor (2D contours)
            Pesgo1.PeData.Filter2D3D = true;      // For Now, only set if ComputeShader = True, and only for PlottingMethod = Line
            Pesgo1.PeData.StagingBufferY = true;  // Always set for ComputeShader
            Pesgo1.PeData.StagingBufferX = true;  // Always set for ComputeShader 

            // Direct3D Reset image //
            Pesgo1.PeFunction.Force3dxNewColors = true;
            Pesgo1.PeFunction.Force3dxVerticeRebuild = true;
            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();
            //Pesgo1.Refresh(); // when you want to force a paint, Invalidate is usually all that is needed

            // Create and initialize horizontal slider to control x axis scale //

            sliderSampleView = new NoFocusTrackBar();
            sliderSampleView.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);
            sliderSampleView.Parent = panel0;
            sliderSampleView.Location = new System.Drawing.Point(ZoomXlabel.Width + 10, ZoomXlabel.Top - 5);
            sliderSampleView.Size = new System.Drawing.Size(260, 30);
            sliderSampleView.Scroll += new System.EventHandler(sliderSampleView_Scroll);
            sliderSampleView.Maximum = 1000;
            sliderSampleView.Minimum = 1;
            sliderSampleView.TickFrequency = 100;
            sliderSampleView.LargeChange = 10;
            sliderSampleView.SmallChange = 1;
            sliderSampleView.Value = 175; // initialize with some level of x axis zoom 
            SampleViewToZoomAmount(175);
            panel0.Controls.Add( sliderSampleView );

            // Create and initialize vertical sliders to control axis scale and position //

            signal1ScaleBar = new NoFocusTrackBar();
            signal1ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal1ScaleBar });
            signal1ScaleBar.Orientation = Orientation.Vertical;
            signal1ScaleBar.Location = new System.Drawing.Point(SignalAxis1.Left + 15, SignalAxis1.Bottom + 2);
            signal1ScaleBar.Size = new System.Drawing.Size(45, 180);
            signal1ScaleBar.Scroll += new System.EventHandler(Signal1ScaleBar_Scroll);
            signal1ScaleBar.Maximum = 90;
            signal1ScaleBar.Minimum = 10;
            signal1ScaleBar.TickFrequency = 8;
            signal1ScaleBar.LargeChange = 1;
            signal1ScaleBar.SmallChange = 1;
            signal1ScaleBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal1PositionBar = new NoFocusTrackBar();
            signal1ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal1PositionBar });
            signal1PositionBar.Orientation = Orientation.Vertical;
            signal1PositionBar.Location = new System.Drawing.Point(SignalAxis1.Right - 30, SignalAxis1.Bottom + 2);
            signal1PositionBar.Size = new System.Drawing.Size(45, 180);
            signal1PositionBar.Scroll += new System.EventHandler(Signal1PositionBar_Scroll);
            signal1PositionBar.Maximum = 100;
            signal1PositionBar.Minimum = 0;
            signal1PositionBar.TickFrequency = 10;
            signal1PositionBar.LargeChange = 1;
            signal1PositionBar.SmallChange = 1;
            signal1PositionBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal2ScaleBar = new NoFocusTrackBar();
            signal2ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal2ScaleBar });
            signal2ScaleBar.Orientation = Orientation.Vertical;
            signal2ScaleBar.Location = new System.Drawing.Point(SignalAxis2.Left + 15, SignalAxis1.Bottom + 2);
            signal2ScaleBar.Size = new System.Drawing.Size(45, 180);
            signal2ScaleBar.Scroll += new System.EventHandler(Signal2ScaleBar_Scroll);
            signal2ScaleBar.Maximum = 90;
            signal2ScaleBar.Minimum = 10;
            signal2ScaleBar.TickFrequency = 8;
            signal2ScaleBar.LargeChange = 1;
            signal2ScaleBar.SmallChange = 1;
            signal2ScaleBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal2PositionBar = new NoFocusTrackBar();
            signal2ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal2PositionBar });
            signal2PositionBar.Orientation = Orientation.Vertical;
            signal2PositionBar.Location = new System.Drawing.Point(SignalAxis2.Right - 30, SignalAxis1.Bottom + 2);
            signal2PositionBar.Size = new System.Drawing.Size(45, 180);
            signal2PositionBar.Scroll += new System.EventHandler(Signal2PositionBar_Scroll);
            signal2PositionBar.Maximum = 100;
            signal2PositionBar.Minimum = 0;
            signal2PositionBar.TickFrequency = 10;
            signal2PositionBar.LargeChange = 1;
            signal2PositionBar.SmallChange = 1;
            signal2PositionBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal3ScaleBar = new NoFocusTrackBar();
            signal3ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal3ScaleBar });
            signal3ScaleBar.Orientation = Orientation.Vertical;
            signal3ScaleBar.Location = new System.Drawing.Point(SignalAxis3.Left + 15, SignalAxis1.Bottom + 2);
            signal3ScaleBar.Size = new System.Drawing.Size(45, 180);
            signal3ScaleBar.Scroll += new System.EventHandler(Signal3ScaleBar_Scroll);
            signal3ScaleBar.Maximum = 90;
            signal3ScaleBar.Minimum = 10;
            signal3ScaleBar.TickFrequency = 8;
            signal3ScaleBar.LargeChange = 1;
            signal3ScaleBar.SmallChange = 1;
            signal3ScaleBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal3PositionBar = new NoFocusTrackBar();
            signal3ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal3PositionBar });
            signal3PositionBar.Orientation = Orientation.Vertical;
            signal3PositionBar.Location = new System.Drawing.Point(SignalAxis3.Right - 30, SignalAxis1.Bottom + 2);
            signal3PositionBar.Size = new System.Drawing.Size(45, 180);
            signal3PositionBar.Scroll += new System.EventHandler(Signal3PositionBar_Scroll);
            signal3PositionBar.Maximum = 100;
            signal3PositionBar.Minimum = 0;
            signal3PositionBar.TickFrequency = 10;
            signal3PositionBar.LargeChange = 1;
            signal3PositionBar.SmallChange = 1;
            signal3PositionBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal4ScaleBar = new NoFocusTrackBar();
            signal4ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal4ScaleBar });
            signal4ScaleBar.Orientation = Orientation.Vertical;
            signal4ScaleBar.Location = new System.Drawing.Point(SignalAxis4.Left + 15, SignalAxis1.Bottom + 2);
            signal4ScaleBar.Size = new System.Drawing.Size(45, 180);
            signal4ScaleBar.Scroll += new System.EventHandler(Signal4ScaleBar_Scroll);
            signal4ScaleBar.Maximum = 90;
            signal4ScaleBar.Minimum = 10;
            signal4ScaleBar.TickFrequency = 8;
            signal4ScaleBar.LargeChange = 1;
            signal4ScaleBar.SmallChange = 1;
            signal4ScaleBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal4PositionBar = new NoFocusTrackBar();
            signal4ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal4PositionBar });
            signal4PositionBar.Orientation = Orientation.Vertical;
            signal4PositionBar.Location = new System.Drawing.Point(SignalAxis4.Right - 30, SignalAxis1.Bottom + 2);
            signal4PositionBar.Size = new System.Drawing.Size(45, 180);
            signal4PositionBar.Scroll += new System.EventHandler(Signal4PositionBar_Scroll);
            signal4PositionBar.Maximum = 100;
            signal4PositionBar.Minimum = 0;
            signal4PositionBar.TickFrequency = 10;
            signal4PositionBar.LargeChange = 1;
            signal4PositionBar.SmallChange = 1;
            signal4PositionBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal5ScaleBar = new NoFocusTrackBar();
            signal5ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal5ScaleBar });
            signal5ScaleBar.Orientation = Orientation.Vertical;
            signal5ScaleBar.Location = new System.Drawing.Point(SignalAxis5.Left + 15, SignalAxis1.Bottom + 2);
            signal5ScaleBar.Size = new System.Drawing.Size(45, 180);
            signal5ScaleBar.Scroll += new System.EventHandler(Signal5ScaleBar_Scroll);
            signal5ScaleBar.Maximum = 90;
            signal5ScaleBar.Minimum = 10;
            signal5ScaleBar.TickFrequency = 8;
            signal5ScaleBar.LargeChange = 1;
            signal5ScaleBar.SmallChange = 1;
            signal5ScaleBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            signal5PositionBar = new NoFocusTrackBar();
            signal5ScaleBar.Parent = panel1;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[] { signal5PositionBar });
            signal5PositionBar.Orientation = Orientation.Vertical;
            signal5PositionBar.Location = new System.Drawing.Point(SignalAxis5.Right - 30, SignalAxis1.Bottom + 2);
            signal5PositionBar.Size = new System.Drawing.Size(45, 180);
            signal5PositionBar.Scroll += new System.EventHandler(Signal5PositionBar_Scroll);
            signal5PositionBar.Maximum = 100;
            signal5PositionBar.Minimum = 0;
            signal5PositionBar.TickFrequency = 10;
            signal5PositionBar.LargeChange = 1;
            signal5PositionBar.SmallChange = 1;
            signal5PositionBar.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            BackColor = Color.Black;  // table layout sizing bars black  

            panel0.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);
            panel1.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);
            panel2.BackColor = Color.FromArgb(0xff, 0x00, 0x2B, 0x35);

            // Set UI ForeColor settings 

            timerControl.ForeColor = Color.White;
            combineAxes.ForeColor = Color.White;
            hideAxes.Enabled = false;
            hideAxes.ForeColor = Color.White;   

            showLegend.ForeColor = Color.White;
            ZoomXlabel.ForeColor = Color.White;

            HighLightAxeslabel.ForeColor = Color.White; 
            highlightAxis1.ForeColor = Pesgo1.PeColor.SubsetColors[0];
            highlightAxis2.ForeColor = Pesgo1.PeColor.SubsetColors[1];
            highlightAxis3.ForeColor = Pesgo1.PeColor.SubsetColors[2];
            highlightAxis4.ForeColor = Pesgo1.PeColor.SubsetColors[3];
            highlightAxis5.ForeColor = Pesgo1.PeColor.SubsetColors[4];

            SignalAxis1.ForeColor = Pesgo1.PeColor.SubsetColors[0];
            SignalAxis2.ForeColor = Pesgo1.PeColor.SubsetColors[1];
            SignalAxis3.ForeColor = Pesgo1.PeColor.SubsetColors[2];
            SignalAxis4.ForeColor = Pesgo1.PeColor.SubsetColors[3];
            SignalAxis5.ForeColor = Pesgo1.PeColor.SubsetColors[4];

            // Sets the axis colors to match SubsetColors of chart  
            for (int i = 0; i < 5; i++) { Pesgo1.PeGrid.WorkingAxis = i; Pesgo1.PeColor.YAxis = Pesgo1.PeColor.SubsetColors[i]; }
        }

        private void Timer1_Tick(object sender, System.EventArgs e)
        {
            Timer1.Stop();

            // FPS counter
            _frameCount++;
            var elapsed = (DateTime.Now - _lastFpsTime).TotalSeconds;
            if (elapsed >= 1.0)
            {
                this.Text = $"GigaPrime2D — 100M Points — {_frameCount} FPS";
                _frameCount = 0;
                _lastFpsTime = DateTime.Now;
            }

            Random rn = new Random();
            int iRandomOffset = rn.Next(600000); // pick a new random start of waveform data to produce variation

            // pass 100 Million Data points from larger prepared array to charted array fYDataToChart, size 100M
            // ProEssentials is set up to use the pointer of fYDataToChart as source of chart data,
            // Changing the data inside fYDataToChart is all that is needed to change the chart. 
            Array.Copy(fYDataPool, iRandomOffset, fYDataToChart, 0, 20000000);
            Array.Copy(fYDataPool, iRandomOffset + 24000000, fYDataToChart, 20000000, 20000000);  // DataPool holds 24M samples per signal,but copies 20M per signal to plot  
            Array.Copy(fYDataPool, iRandomOffset + 48000000, fYDataToChart, 40000000, 20000000);  // 48M is offset to DataPool 3rd Signal, 40M is offset to YDataToChart for 3rd subset 
            Array.Copy(fYDataPool, iRandomOffset + 72000000, fYDataToChart, 60000000, 20000000);
            Array.Copy(fYDataPool, iRandomOffset + 96000000, fYDataToChart, 80000000, 20000000);

            Pesgo1.PeData.ReuseDataX = true; // tells chart x data is not changing, reuse current xdata buffer 

            Pesgo1.PeFunction.Force3dxVerticeRebuild = true; // lets chart know it needs to process new data stored in YDataToChart 
            Pesgo1.Invalidate();
            Timer1.Start();
        }

        private void timerControl_CheckedChanged(object sender, EventArgs e)
        {
            if (timerControl.Checked == true)
            {
                Form1.Timer1.Interval = 10;
                Form1.Timer1.Start();
            }
            else
            {
                Form1.Timer1.Stop();
            }
        }

        private void sliderSampleView_Scroll(object sender, System.EventArgs e)
        {
            SampleViewToZoomAmount(sliderSampleView.Value);
            Pesgo1.PeGrid.Zoom.Mode = true;
            Pesgo1.PeFunction.Reinitialize(); // this is needed if a scrollbar is visible and needs updating  
            Pesgo1.Invalidate();
        }

        private void SampleViewToZoomAmount(int nSliderValue)
        {
            double dValue = nSliderValue;
            dValue = dValue / 1000.0F;

            double dZoomRange = 20000000.0F * dValue;
            double dHalfRange = dZoomRange / 2.0F;

            // establish the programmatic zoom state as we will be setting ZoomMode = true //
            Pesgo1.PeGrid.Zoom.MinX = 10000000.0F - dHalfRange;
            Pesgo1.PeGrid.Zoom.MaxX = 10000000.0F + dHalfRange;

            for (int i = 0; i < 5; i++) // iterate axes and transfer non zoom axis extents to zoom extents 
            {
                Pesgo1.PeGrid.WorkingAxis = i;
                Pesgo1.PeGrid.Zoom.MinY = Pesgo1.PeGrid.Configure.ManualMinY;
                Pesgo1.PeGrid.Zoom.MaxY = Pesgo1.PeGrid.Configure.ManualMaxY;
            }
            Pesgo1.PeGrid.WorkingAxis = 0; // reset WorkingAxis = 0 as a good practise 
            Pesgo1.PeGrid.Zoom.Mode = true;
        }

        private void Pesgo1_PeZoomIn(object sender, EventArgs e)
        {
            if (Pesgo1.PeGrid.Zoom.Mode)
            {
                // this code simply centers zoom, one could add logic to reflect a current zoom location
                double dZoom = Pesgo1.PeGrid.Zoom.MaxX - Pesgo1.PeGrid.Zoom.MinX; // range
                double dZoomPercent = (dZoom / 20000000.0F) * 100;  // range div by totalrange 20000000
                int nNewSliderValue = (int)dZoomPercent * 10;      // slider is 1 to 1000, 10x
                if (nNewSliderValue < 1) { nNewSliderValue = 1; }   // assure new value 
                if (nNewSliderValue > 1000) { nNewSliderValue = 1000; }
                sliderSampleView.Value = nNewSliderValue;
            }
            else
                sliderSampleView.Value = 1000; // if not zoomed, slider to full range
        }

        private void Pesgo1_PeZoomOut(object sender, EventArgs e)
        {
            sliderSampleView.Value = 1000;  // update slider to show full range 
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.Timer1 != null)
                Timer1.Stop();
        }


        private void Signal1ScaleBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal1ScaleBar.Value / 10F);
            double dPosition = (double)signal1PositionBar.Value / 100.0F;
            SignalScalePositionHelper(0, dZoom, dPosition);
        }
        private void Signal1PositionBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal1ScaleBar.Value / 10F);
            double dPosition = (double)signal1PositionBar.Value / 100.0F;
            SignalScalePositionHelper(0, dZoom, dPosition);
        }
        private void Signal2ScaleBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal2ScaleBar.Value / 10F);
            double dPosition = (double)signal2PositionBar.Value / 100.0F;
            SignalScalePositionHelper(1, dZoom, dPosition);
        }
        private void Signal2PositionBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal2ScaleBar.Value / 10F);
            double dPosition = (double)signal2PositionBar.Value / 100.0F;
            SignalScalePositionHelper(1, dZoom, dPosition);
        }
        private void Signal3ScaleBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal3ScaleBar.Value / 10F);
            double dPosition = (double)signal3PositionBar.Value / 100.0F;
            SignalScalePositionHelper(2, dZoom, dPosition);
        }
        private void Signal3PositionBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal3ScaleBar.Value / 10F);
            double dPosition = (double)signal3PositionBar.Value / 100.0F;
            SignalScalePositionHelper(2, dZoom, dPosition);
        }
        private void Signal4ScaleBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal4ScaleBar.Value / 10F);
            double dPosition = (double)signal4PositionBar.Value / 100.0F;
            SignalScalePositionHelper(3, dZoom, dPosition);
        }
        private void Signal4PositionBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal4ScaleBar.Value / 10F);
            double dPosition = (double)signal4PositionBar.Value / 100.0F;
            SignalScalePositionHelper(3, dZoom, dPosition);
        }
        private void Signal5ScaleBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal5ScaleBar.Value / 10F);
            double dPosition = (double)signal5PositionBar.Value / 100.0F;
            SignalScalePositionHelper(4, dZoom, dPosition);
        }
        private void Signal5PositionBar_Scroll(object sender, EventArgs e)
        {
            double dZoom = 21.0F / ((double)signal5ScaleBar.Value / 10F);
            double dPosition = (double)signal5PositionBar.Value / 100.0F;
            SignalScalePositionHelper(4, dZoom, dPosition);
        }

        private void SignalScalePositionHelper(int nAxis, double dZoom, double dPosition)
        {
            double dRemainder = 21.0F - dZoom;
            double dOffset = dPosition * dRemainder;
            double dMin = 0.0F + dOffset;
            double dMax = dMin + dZoom;
            Pesgo1.PeGrid.WorkingAxis = nAxis;
            Pesgo1.PeGrid.Configure.ManualMinY = dMin;
            Pesgo1.PeGrid.Configure.ManualMaxY = dMax;
            Pesgo1.PeGrid.Zoom.MinY = dMin;
            Pesgo1.PeGrid.Zoom.MaxY = dMax;
            Pesgo1.Invalidate();
        }

        private void HighlightAxis1_CheckedChanged(object sender, EventArgs e)
        {
            if (highlightAxis1.Checked)
            {
                highlightAxis2.Checked = false;
                highlightAxis3.Checked = false;
                highlightAxis4.Checked = false;
                highlightAxis5.Checked = false;
                Pesgo1.PeGrid.MultiAxesProportions[0] = .80F;
                Pesgo1.PeGrid.MultiAxesProportions[1] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[2] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[3] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[4] = .05F;
            }
            else
            {
                Pesgo1.PeGrid.MultiAxesProportions.Clear();
            }
            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();
        }
        private void HighlightAxis2_CheckedChanged(object sender, EventArgs e)
        {
            if (highlightAxis2.Checked)
            {
                highlightAxis1.Checked = false;
                highlightAxis3.Checked = false;
                highlightAxis4.Checked = false;
                highlightAxis5.Checked = false;
                Pesgo1.PeGrid.MultiAxesProportions[0] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[1] = .80F;
                Pesgo1.PeGrid.MultiAxesProportions[2] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[3] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[4] = .05F;
            }
            else
            {
                Pesgo1.PeGrid.MultiAxesProportions.Clear();
            }
            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();

        }
        private void HighlightAxis3_CheckedChanged(object sender, EventArgs e)
        {
            if (highlightAxis3.Checked)
            {
                highlightAxis1.Checked = false;
                highlightAxis2.Checked = false;
                highlightAxis4.Checked = false;
                highlightAxis5.Checked = false;
                Pesgo1.PeGrid.MultiAxesProportions[0] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[1] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[2] = .80F;
                Pesgo1.PeGrid.MultiAxesProportions[3] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[4] = .05F;
            }
            else
            {
                Pesgo1.PeGrid.MultiAxesProportions.Clear();
            }
            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();

        }
        private void HighlightAxis4_CheckedChanged(object sender, EventArgs e)
        {
            if (highlightAxis4.Checked)
            {
                highlightAxis1.Checked = false;
                highlightAxis2.Checked = false;
                highlightAxis3.Checked = false;
                highlightAxis5.Checked = false;
                Pesgo1.PeGrid.MultiAxesProportions[0] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[1] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[2] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[3] = .80F;
                Pesgo1.PeGrid.MultiAxesProportions[4] = .05F;
            }
            else
            {
                Pesgo1.PeGrid.MultiAxesProportions.Clear();
            }
            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();

        }
        private void HighlightAxis5_CheckedChanged(object sender, EventArgs e)
        {
            if (highlightAxis5.Checked)
            {
                highlightAxis1.Checked = false;
                highlightAxis2.Checked = false;
                highlightAxis3.Checked = false;
                highlightAxis4.Checked = false;
                Pesgo1.PeGrid.MultiAxesProportions[0] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[1] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[2] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[3] = .05F;
                Pesgo1.PeGrid.MultiAxesProportions[4] = .80F;
            }
            else
            {
                Pesgo1.PeGrid.MultiAxesProportions.Clear();
            }
            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();

        }

        private void ShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            if (showLegend.Checked)
            {
                Pesgo1.PeLegend.Show = true;
                Pesgo1.PeLegend.Style = LegendStyle.OneLineTopOfAxis;
            }
            else
            {
                Pesgo1.PeLegend.Show = false;
                Pesgo1.PeLegend.Style = LegendStyle.TwoLine;
            }

            Pesgo1.PeLegend.SimpleLine = true;

            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();
        }

        private void HideAxes_CheckedChanged(object sender, EventArgs e)
        {
            if (hideAxes.Checked)
                Hide4Axes();
            else
                Show4Axes();

            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();
        }

        private void Hide4Axes()
        {
            for (int i = 1; i < 5; i++) // iterate 1-4 axes 
            {
                Pesgo1.PeGrid.WorkingAxis = i;
                Pesgo1.PeGrid.Option.ShowYAxis = ShowAxis.Empty;  // hide the Y axes 1-4
            }
            Pesgo1.PeGrid.WorkingAxis = 0;
            Pesgo1.PeString.YAxisLabel = "Combined Axes";
        }

        private void Show4Axes()
        {
            for (int i = 1; i < 5; i++) // iterate 1-4 axes 
            {
                Pesgo1.PeGrid.WorkingAxis = i;
                Pesgo1.PeGrid.Option.ShowYAxis = ShowAxis.All; // show the Y axes 1-4
            }
            Pesgo1.PeGrid.WorkingAxis = 0;
            Pesgo1.PeString.YAxisLabel = "uV";
        }

        private void CombineAxes_CheckedChanged(object sender, EventArgs e)
        {
            if (combineAxes.Checked)
            {
                Pesgo1.PeGrid.OverlapMultiAxes[0] = 5; // create one overlap multi axes holding all 5 multi axes  
                hideAxes.Enabled = true;

                highlightAxis1.Checked = false;
                highlightAxis2.Checked = false;
                highlightAxis3.Checked = false;
                highlightAxis4.Checked = false;
                highlightAxis5.Checked = false;

                if (hideAxes.Checked)
                    Hide4Axes();
                else
                    Show4Axes();
            }
            else
            {
                Pesgo1.PeGrid.OverlapMultiAxes.Clear();  // clear overlap multi axes settings 
                hideAxes.Enabled = false;

                // if not combining, set all axes shown 
                Show4Axes();
            }
            Pesgo1.PeFunction.ReinitializeResetImage();
            Pesgo1.Invalidate();
        }


        private void helpButton_Click(object sender, EventArgs e)
        {
            string hs = "GigaPrime2D WinForms — 100 Million Point Demo\n\n";
            hs += "This demo demonstrates ProEssentials v10 GPU compute shader rendering — ";
            hs += "100 million data points completely re-passed and rendered per timer tick.\n\n";
            hs += "The title bar displays live FPS. GPU compute shader render time is ~15ms. ";
            hs += "End-to-end frame rate including 100M point data transfer is typically 18-22 FPS ";
            hs += "on a modern development workstation with dedicated GPU.\n\n";
            hs += "Controls:\n";
            hs += "1. Mouse Wheel — zooms X axis range.\n";
            hs += "2. Right-click — shows popup menu.\n";
            hs += "3. Right-click → Undo Zoom — resets chart zoom.\n";
            hs += "4. Zoom X Axes slider — programmatic zoom control.\n";
            hs += "5. Highlight Signal checkboxes — expand individual axis to 80% height.\n";
            hs += "6. Signal Scale/Position sliders — per-channel amplitude and offset control.\n";
            hs += "7. Combine Axes — overlaps all 5 signals into one shared graph area.\n\n";
            hs += "WinForms vs WPF Performance:\n";
            hs += "WinForms has a slight performance edge as Direct3D is directly coupled to the ";
            hs += "window device context, avoiding the texture compositing step that WPF requires. ";
            hs += "Both versions use identical GPU compute shaders and achieve comparable frame rates. ";
            hs += "For a WPF implementation see the WPF version.\n\n";
            hs += "Performance is dependent on CPU/GPU. Systems without a dedicated GPU or with ";
            hs += "poor integrated graphics may see reduced frame rates. ";
            hs += "ProEssentials renders this data faster than any other known .NET charting library.";
            MessageBox.Show(hs, "GigaPrime2D WinForms Help");

        }
    }
}
