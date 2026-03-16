# GigaPrime2D — 100 Million Point WinForms Chart

A ProEssentials v10 demonstration of GPU compute shader rendering — 
100 million data points completely re-passed and re-rendered per timer 
tick using .NET Framework 4.72 WinForms.

![GigaPrime2D 100 Million Points WinForms Chart](https://gigasoft.com/files/GigaPrime2D-100MPoints-Winform.png)

---

## What This Demonstrates

GigaPrime2D demonstrates v10's GPU compute shader feature — rendering 
large amounts of data on the GPU. On each timer tick, 100 million 
data points are completely re-passed and rendered.

- **5 subsets × 20,000,000 points = 100M data points per update**
- **GPU compute shaders** process all data in parallel on the GPU
- **WinForms Direct3D** updates at ~15ms — faster than WPF's ~25ms 
  due to Direct3D being directly coupled to the WinForms window 
  device context
- **WPF** uses compute shaders similarly but renders to texture, 
  resulting in slightly higher latency
- **Zero memory copy** — the chart receives a pointer to 
  `fYDataToChart` directly; changing the array contents is all 
  that is needed to update the chart

---

## How It Works

### Data Architecture
```csharp
// 120M point pool — prepared once at startup
float[] fYDataPool = new float[120010000];

// 100M point buffer — pointer passed directly to chart
// Chart forwards this to GPU compute shaders each render
float[] fYDataToChart = new float[100000000];
```

On each timer tick, `Array.Copy` moves 100M points from a random 
offset in `fYDataPool` into `fYDataToChart` producing variation. 
The chart renders the new data immediately via GPU compute shaders.

### Five Independent Axes

Each of the 5 signal channels gets its own Y axis lane via 
`MultiAxesSubsets`. The UI lets you combine, hide, highlight, 
and resize axes interactively.

---

## Interactive Controls

- **Start/Stop Timer** — enables 100M point continuous re-rendering
- **Mouse wheel** — zooms X axis range
- **Scale/Position sliders** — per-channel amplitude and offset control
- **Sample View slider** — controls visible X range
- **Combine Axes** — overlaps all 5 axes into one shared graph area
- **Hide Axes** — collapses to single combined Y axis label
- **Highlight Axis 1-5** — expands selected channel to 80% of height
- **Show Legend** — toggles legend display
- **Right-click** — full built-in context menu including zoom reset

---

## Prerequisites

- Visual Studio 2022 or 2019
- .NET Framework 4.72
- Internet connection for NuGet restore

> **Designer Support:** Visual Studio designer requires the full 
> ProEssentials installation. The project builds and runs correctly 
> via NuGet without a full installation.

> **Note:** This project references a specific NuGet version due to 
> the native DLL post-build copy rule. If you update the NuGet 
> package, update the version number in the post-build event in 
> `GigaPrime2D.csproj` to match.

---

## How to Run
```
1. Clone this repository
2. Open GigaPrime2D.sln in Visual Studio
3. Build → Rebuild Solution (restores NuGet package automatically)
4. Press F5
5. Click Start Timer to begin 100M point rendering
```

---

## Performance Notes

WinForms is the better interface for true real-time charting as 
Direct3D is directly coupled to the window device context. 
WPF renders to texture and updates max at approximately 25ms vs 
WinForms' 15ms.

Performance is dependent on CPU/GPU. Systems without a dedicated GPU 
or with poor integrated graphics may struggle with this data volume. 
ProEssentials plots this data faster than any other known .NET 
charting library.

---

## NuGet Package

This project references 
[`ProEssentials.Chart.Net.x64.Winforms`](https://www.nuget.org/packages/ProEssentials.Chart.Net.x64.Winforms) 
from nuget.org. Package restore happens automatically on build.

---

## Related

- [Plot 100 Million Points — 5-Library Comparison](https://gigasoft.com/blog/plot-100-million-points-wpf-comparison)
- [Performance — GPU Architecture Comparison](https://gigasoft.com/why-proessentials/performance)
- [No-hassle evaluation download](https://gigasoft.com/net-chart-component-wpf-winforms-download)
- [gigasoft.com](https://gigasoft.com)

---

## License

Example code is MIT licensed. ProEssentials requires a commercial 
license for continued use.