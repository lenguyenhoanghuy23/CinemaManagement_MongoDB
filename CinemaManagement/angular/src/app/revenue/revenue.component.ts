import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import Chart from 'chart.js/auto';


@Component({
  selector: 'app-revenue',
  templateUrl: './revenue.component.html',
  styleUrls: ['./revenue.component.scss']
})
export class RevenueComponent implements AfterViewInit {
  @ViewChild('acquisitionsCanvas') acquisitionsCanvas: ElementRef;
  @ViewChild('myChartCanvas') myChartCanvas: ElementRef;

  ngAfterViewInit() {
    
    const data1 = {
      labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4', 'Label 5', 'Label 6', 'Label 7'],
      datasets: [{
        label: 'My First Dataset',
        data: [65, 59, 80, 81, 56, 55, 40],
        backgroundColor: [
          'rgba(255, 99, 132, 0.2)',
          'rgba(255, 159, 64, 0.2)',
          'rgba(255, 205, 86, 0.2)',
          'rgba(75, 192, 192, 0.2)',
          'rgba(54, 162, 235, 0.2)',
          'rgba(153, 102, 255, 0.2)',
          'rgba(201, 203, 207, 0.2)'
        ],
        borderColor: [
          'rgb(255, 99, 132)',
          'rgb(255, 159, 64)',
          'rgb(255, 205, 86)',
          'rgb(75, 192, 192)',
          'rgb(54, 162, 235)',
          'rgb(153, 102, 255)',
          'rgb(201, 203, 207)'
        ],
        borderWidth: 1
      }]
    };

    // Create the first chart on the 'acquisitionsCanvas'
    const acquisitionsCanvas = this.acquisitionsCanvas.nativeElement;
    new Chart(acquisitionsCanvas, {
      type: 'bar',
      data: data1,
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      },
    });

    // Data for the second chart
    const data2 = {
      labels: [
        'Red',
        'Blue',
        'Yellow'
      ],
      datasets: [{
        label: 'Acquisitions by year',
        data: [300, 50, 100],
        backgroundColor: [
          'rgb(255, 99, 132)',
          'rgb(54, 162, 235)',
          'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
      }]
    };

    // Create the second chart on the 'myChartCanvas'
    const myChartCanvas = this.myChartCanvas.nativeElement;
    new Chart(myChartCanvas, {
      type: 'doughnut',
      data: data2,
      
      options: {
        scales: {
          y: {
            
            beginAtZero: true,
          },
        },
      },
    });
  }
}
