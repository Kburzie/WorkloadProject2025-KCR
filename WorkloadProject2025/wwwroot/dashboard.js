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

        // Draw bar with gradient
        const gradient = ctx.createLinearGradient(x, y, x, y + barHeight);
        gradient.addColorStop(0, '#594ae2');
        gradient.addColorStop(1, '#7b5beb');
        ctx.fillStyle = gradient;
        ctx.fillRect(x, y, barWidth, barHeight);

        // Add bar border
        ctx.strokeStyle = '#4a3bb0';
        ctx.lineWidth = 2;
        ctx.strokeRect(x, y, barWidth, barHeight);

        // Draw label
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

    // Add click event listener
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

// Function to download a file
window.downloadFile = function (filename, content) {
    const blob = new Blob([content], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    
    link.setAttribute('href', url);
    link.setAttribute('download', filename);
    link.style.visibility = 'hidden';
    
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    
    URL.revokeObjectURL(url);
};
