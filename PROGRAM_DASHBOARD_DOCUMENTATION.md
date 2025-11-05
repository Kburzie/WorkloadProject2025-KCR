# Program Data Dashboard - Complete Documentation

This document provides a comprehensive, line-by-line explanation of the Program Data Dashboard implementation.

---

## 1️⃣. Prototype Layout (Concept)

```
-------------------------------------------------------
| PROGRAM DATA DASHBOARD                             |
|-----------------------------------------------------|
| [ Search Bar ]   [ Filter by Department ▼ ]        |
|                                                     |
| 📊 Bar Chart Area                                   |
| --------------------------------------------------- |
|  Computer Science   ████████████                    |
|  Business Admin     █████████                       |
|  Nursing            ███████████████████             |
|  IT Support         ████████                        |
| --------------------------------------------------- |
| When user clicks "Computer Science" → Right panel   |
| shows details:                                      |
| ----------------------------------------------------|
| Program Details                                     |
| ----------------------------------------------------|
| Name: Computer Science                              |
| Duration: 2 years                                   |
| Department: Technology                              |
| School: Engineering School                          |
| Instructor: Mr. John Doe                            |
| Workload: 20 hrs/week                               |
| Tuition: $8,000.00/year                             |
| Number of Courses: 12                               |
| Courses:                                            |
|   • Programming Fundamentals (3 hours)              |
|   • Database Management (4 hours)                   |
|   • Computer Networks (3 hours)                     |
| ----------------------------------------------------|
-------------------------------------------------------
```

---

## 2️⃣. Data Model (C#)

### ProgramOfStudy Class

The core data model that represents a program of study with all its properties:

```csharp
namespace WorkloadProject2025.Data.Models
{
    public class ProgramOfStudy
    {
        // Primary identifier
        public int Id { get; set; }
        
        // Program name (e.g., "Computer Science", "Business Administration")
        public string Name { get; set; }

        // Foreign key to Department
        public int DepartmentId { get; set; }
        
        // Navigation property to Department (includes School info)
        public Department Department { get; set; }
        
        // List of all courses in this program
        public List<Course> Courses { get; set; }
        
        // Duration of the program in years
        public int DurationYears { get; set; }
        
        // Lead instructor or coordinator for the program
        public string? Instructor { get; set; }
        
        // Expected weekly workload for students (in hours)
        public int WorkloadHours { get; set; }
        
        // Annual tuition cost
        public decimal Tuition { get; set; }
    }
}
```

### Example Data Usage

```csharp
// Example: Creating a sample list of programs
var programs = new List<ProgramOfStudy>
{
    new ProgramOfStudy
    {
        Id = 1,
        Name = "Computer Science",
        DurationYears = 2,
        Instructor = "Mr. John Doe",
        WorkloadHours = 20,
        Tuition = 8000.00m,
        DepartmentId = 1,
        Courses = new List<Course>
        {
            new Course { Name = "Programming Fundamentals", Hours = 3 },
            new Course { Name = "Database Management", Hours = 4 },
            new Course { Name = "Computer Networks", Hours = 3 }
        }
    },
    new ProgramOfStudy
    {
        Id = 2,
        Name = "Business Administration",
        DurationYears = 2,
        Instructor = "Dr. Jane Smith",
        WorkloadHours = 18,
        Tuition = 7500.00m,
        DepartmentId = 2,
        Courses = new List<Course>
        {
            new Course { Name = "Business Strategy", Hours = 3 },
            new Course { Name = "Marketing", Hours = 3 },
            new Course { Name = "Finance", Hours = 4 }
        }
    },
    new ProgramOfStudy
    {
        Id = 3,
        Name = "Nursing",
        DurationYears = 3,
        Instructor = "Prof. Sarah Johnson",
        WorkloadHours = 25,
        Tuition = 12000.00m,
        DepartmentId = 3,
        Courses = new List<Course>
        {
            new Course { Name = "Anatomy", Hours = 5 },
            new Course { Name = "Clinical Practice", Hours = 6 },
            new Course { Name = "Patient Care", Hours = 4 }
        }
    }
};
```

---

## 3️⃣. Bar Chart Implementation (JavaScript)

### Dashboard JavaScript (`wwwroot/dashboard.js`)

Custom canvas-based bar chart implementation without external dependencies:

```javascript
// Chart.js-like functionality for the program dashboard
let programChartInstance = null;

window.renderProgramChart = function (labels, data, onBarClick) {
    const canvas = document.getElementById('programChart');
    if (!canvas) {
        console.error('Canvas element not found');
        return;
    }

    const ctx = canvas.getContext('2d');
    const width = canvas.width;
    const height = canvas.height;
    const barWidth = Math.min(80, (width - 100) / labels.length - 20);
    const maxValue = Math.max(...data, 1);
    const barHeightRatio = (height - 80) / maxValue;

    // Clear canvas
    ctx.clearRect(0, 0, width, height);

    // Draw bars
    labels.forEach((label, index) => {
        const barHeight = data[index] * barHeightRatio;
        const x = 60 + index * (barWidth + 20);
        const y = height - barHeight - 40;

        // Draw bar with gradient (purple theme)
        const gradient = ctx.createLinearGradient(x, y, x, y + barHeight);
        gradient.addColorStop(0, '#594ae2');
        gradient.addColorStop(1, '#7b5beb');
        ctx.fillStyle = gradient;
        ctx.fillRect(x, y, barWidth, barHeight);

        // Add bar border
        ctx.strokeStyle = '#4a3bb0';
        ctx.lineWidth = 2;
        ctx.strokeRect(x, y, barWidth, barHeight);

        // Draw label (rotated for better readability)
        ctx.fillStyle = '#333';
        ctx.font = '12px Arial';
        ctx.textAlign = 'center';
        ctx.save();
        ctx.translate(x + barWidth / 2, height - 10);
        ctx.rotate(-Math.PI / 6);
        ctx.fillText(label.length > 15 ? label.substring(0, 15) + '...' : label, 0, 0);
        ctx.restore();

        // Draw value on top of bar
        ctx.fillStyle = '#333';
        ctx.font = 'bold 14px Arial';
        ctx.textAlign = 'center';
        ctx.fillText(data[index].toString(), x + barWidth / 2, y - 5);
    });

    // Add click event listener for bar interaction
    canvas.onclick = function (event) {
        const rect = canvas.getBoundingClientRect();
        const clickX = event.clientX - rect.left;
        const clickY = event.clientY - rect.top;

        labels.forEach((label, index) => {
            const barHeight = data[index] * barHeightRatio;
            const x = 60 + index * (barWidth + 20);
            const y = height - barHeight - 40;

            if (clickX >= x && clickX <= x + barWidth && clickY >= y && clickY <= y + barHeight) {
                if (onBarClick) {
                    onBarClick.invokeMethodAsync('OnBarClickedJS', label);
                }
            }
        });
    };

    // Draw Y-axis
    ctx.strokeStyle = '#666';
    ctx.lineWidth = 2;
    ctx.beginPath();
    ctx.moveTo(50, 20);
    ctx.lineTo(50, height - 40);
    ctx.stroke();

    // Draw X-axis
    ctx.beginPath();
    ctx.moveTo(50, height - 40);
    ctx.lineTo(width - 20, height - 40);
    ctx.stroke();
};
```

**Key Features:**
- **Dynamic Scaling**: Automatically adjusts bar heights based on data values
- **Interactive Bars**: Click detection for user interaction
- **Visual Appeal**: Gradient fills and borders for modern look
- **Responsive Labels**: Rotated text labels for better space utilization
- **Data Display**: Values shown on top of each bar

---

## 4️⃣. Blazor Component Implementation

### ProgramDashboardPage.razor

The main Blazor component implementing the dashboard:

```razor
@page "/program-dashboard"
@using WorkloadProject2025.Data.Models
@using WorkloadProject2025.Services
@inject IProgramsOfStudyService _programService
@inject IDepartmentService _departmentService

<PageTitle>Program Dashboard</PageTitle>

<MudContainer MaxWidth="MaxWidth.ExtraLarge" Class="mt-4">
    <MudText Typo="Typo.h3" Align="Align.Center" GutterBottom="true">
        📊 PROGRAM DATA DASHBOARD
    </MudText>

    <!-- Search and Filter Section -->
    <MudGrid Class="mb-4">
        <MudItem xs="12" sm="8">
            <MudTextField @bind-Value="searchTerm" Label="Search Program" 
                          Variant="Variant.Outlined" 
                          Adornment="Adornment.Start" 
                          AdornmentIcon="@Icons.Material.Filled.Search"
                          OnKeyUp="@(() => FilterAndRender())" />
        </MudItem>
        <MudItem xs="12" sm="4">
            <MudSelect Value="@selectedDepartmentId" 
                       Label="Filter by Department" 
                       Variant="Variant.Outlined"
                       T="int" ValueChanged="@OnDepartmentChanged">
                <MudSelectItem Value="0">All Departments</MudSelectItem>
                @foreach (var dept in departments)
                {
                    <MudSelectItem Value="@dept.Id">@dept.Name</MudSelectItem>
                }
            </MudSelect>
        </MudItem>
    </MudGrid>

    <!-- Bar Chart Area -->
    <MudPaper Elevation="3" Class="pa-4 mb-4">
        <canvas id="programChart" width="1000" height="400" 
                style="width: 100%; max-width: 1000px;"></canvas>
    </MudPaper>

    <!-- Selected Program Details Panel -->
    @if (selectedProgram != null)
    {
        <MudPaper Elevation="3" Class="pa-4">
            <MudText Typo="Typo.h5" GutterBottom="true">Program Details</MudText>
            <MudDivider Class="mb-3" />
            
            <MudGrid>
                <MudItem xs="12" sm="6">
                    <MudText><strong>Name:</strong> @selectedProgram.Name</MudText>
                </MudItem>
                <MudItem xs="12" sm="6">
                    <MudText><strong>Duration:</strong> @selectedProgram.DurationYears year(s)</MudText>
                </MudItem>
                <MudItem xs="12" sm="6">
                    <MudText><strong>Department:</strong> @(selectedProgram.Department?.Name ?? "N/A")</MudText>
                </MudItem>
                <MudItem xs="12" sm="6">
                    <MudText><strong>School:</strong> @(selectedProgram.Department?.School?.Name ?? "N/A")</MudText>
                </MudItem>
                <MudItem xs="12" sm="6">
                    <MudText><strong>Instructor:</strong> @(selectedProgram.Instructor ?? "Not assigned")</MudText>
                </MudItem>
                <MudItem xs="12" sm="6">
                    <MudText><strong>Workload:</strong> @selectedProgram.WorkloadHours hrs/week</MudText>
                </MudItem>
                <MudItem xs="12" sm="6">
                    <MudText><strong>Tuition:</strong> $@selectedProgram.Tuition.ToString("N2")/year</MudText>
                </MudItem>
                <MudItem xs="12" sm="6">
                    <MudText><strong>Number of Courses:</strong> @(selectedProgram.Courses?.Count ?? 0)</MudText>
                </MudItem>
                <MudItem xs="12">
                    <MudText><strong>Courses:</strong></MudText>
                    @if (selectedProgram.Courses != null && selectedProgram.Courses.Any())
                    {
                        <MudList T="string" Dense="true">
                            @foreach (var course in selectedProgram.Courses)
                            {
                                <MudListItem T="string">
                                    <MudText>• @course.Name (@course.Hours hours)</MudText>
                                </MudListItem>
                            }
                        </MudList>
                    }
                    else
                    {
                        <MudText Color="Color.Secondary">No courses assigned yet</MudText>
                    }
                </MudItem>
            </MudGrid>
        </MudPaper>
    }
    else
    {
        <MudAlert Severity="Severity.Info">
            Click on a bar in the chart to view program details.
        </MudAlert>
    }
</MudContainer>
```

### Component Code-Behind

```csharp
@code {
    private List<ProgramOfStudy> programs = new();
    private List<Department> departments = new();
    private ProgramOfStudy? selectedProgram;
    private string searchTerm = "";
    private int selectedDepartmentId = 0;
    private DotNetObjectReference<ProgramDashboardPage>? objRef;

    // Initialize data on component load
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    // Setup JavaScript interop after first render
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            objRef = DotNetObjectReference.Create(this);
            await FilterAndRender();
        }
    }

    // Load programs and departments from services
    private async Task LoadData()
    {
        programs = await _programService.GetAllWithDetailsAsync();
        departments = await _departmentService.GetAllAsync();
        StateHasChanged();
    }

    // Handle department filter change
    private async Task OnDepartmentChanged(int departmentId)
    {
        selectedDepartmentId = departmentId;
        await FilterAndRender();
    }

    // Filter data and re-render chart
    private async Task FilterAndRender()
    {
        var filtered = programs
            .Where(p => (string.IsNullOrEmpty(searchTerm) || 
                        p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                     && (selectedDepartmentId == 0 || p.DepartmentId == selectedDepartmentId))
            .ToList();

        var labels = filtered.Select(p => p.Name).ToArray();
        var data = filtered.Select(p => p.Courses?.Count ?? 0).ToArray();

        await JS.InvokeVoidAsync("renderProgramChart", labels, data, objRef);
    }

    // JavaScript callback when user clicks a bar
    [JSInvokable]
    public async Task OnBarClickedJS(string programName)
    {
        selectedProgram = programs.FirstOrDefault(p => p.Name == programName);
        StateHasChanged();
        await Task.CompletedTask;
    }

    // Cleanup
    public void Dispose()
    {
        objRef?.Dispose();
    }

    [Inject]
    public IJSRuntime JS { get; set; } = default!;
}
```

---

## 5️⃣. How It All Works Together

### Data Flow Diagram

```
┌─────────────────────────────────────────────────────┐
│                  User Interface                     │
│  ┌────────────┐         ┌───────────────────────┐  │
│  │ Search Bar │         │ Department Dropdown   │  │
│  └────────────┘         └───────────────────────┘  │
│              ▼                    ▼                 │
│         ┌──────────────────────────────┐           │
│         │   FilterAndRender Method     │           │
│         └──────────────────────────────┘           │
│                       ▼                             │
│         ┌──────────────────────────────┐           │
│         │  JavaScript Chart Rendering  │           │
│         │    (renderProgramChart)      │           │
│         └──────────────────────────────┘           │
│                       ▼                             │
│         ┌──────────────────────────────┐           │
│         │    Interactive Bar Chart     │           │
│         │  (Canvas with Click Events)  │           │
│         └──────────────────────────────┘           │
│                       ▼                             │
│              User Clicks Bar                        │
│                       ▼                             │
│         ┌──────────────────────────────┐           │
│         │  OnBarClickedJS (C# Method)  │           │
│         └──────────────────────────────┘           │
│                       ▼                             │
│         ┌──────────────────────────────┐           │
│         │    Program Details Panel     │           │
│         │  (Shows all program info)    │           │
│         └──────────────────────────────┘           │
└─────────────────────────────────────────────────────┘
```

### Step-by-Step Execution Flow

1. **Page Load**
   - Component initializes and calls `OnInitializedAsync()`
   - Loads all programs and departments from database via services
   - First render completes

2. **Chart Rendering**
   - `OnAfterRenderAsync()` creates .NET object reference for JavaScript interop
   - Calls `FilterAndRender()` to display initial chart
   - JavaScript `renderProgramChart()` draws bars on canvas

3. **User Interaction - Search**
   - User types in search box
   - `OnKeyUp` event triggers `FilterAndRender()`
   - Chart updates with filtered programs

4. **User Interaction - Filter**
   - User selects department from dropdown
   - `OnDepartmentChanged()` method called
   - Chart re-renders with programs from selected department

5. **User Interaction - Bar Click**
   - User clicks on a bar in the chart
   - JavaScript detects click coordinates
   - Calls C# method `OnBarClickedJS()` with program name
   - Component finds matching program
   - Details panel appears with full program information

---

## 6️⃣. Technology Stack

- **Frontend Framework**: Blazor Server (.NET 9.0)
- **UI Library**: MudBlazor 8.x
- **Chart Rendering**: HTML5 Canvas + JavaScript
- **Backend**: Entity Framework Core
- **Database**: SQL Server
- **Language**: C# 12

---

## 7️⃣. Key Features

✅ **Real-time Search**: Instant filtering as you type
✅ **Department Filtering**: View programs by department
✅ **Interactive Visualization**: Click bars to see details
✅ **Responsive Design**: Works on desktop and mobile
✅ **Rich Details**: Complete program information display
✅ **Custom Charts**: No external chart library dependencies

---

## 8️⃣. Future Enhancements

Potential improvements that could be added:

- Export data to PDF/Excel
- Compare multiple programs side-by-side
- Add enrollment statistics
- Include student feedback/ratings
- Add program completion rates
- Integration with calendar for intake dates
- Email notifications for program updates

---

*End of Documentation*
