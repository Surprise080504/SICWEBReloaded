import { Component } from "react";
import Chart from 'react-apexcharts'
import {
  Card,
  CardContent,
  Typography,
  useTheme
} from '@material-ui/core';
export default class LineChart extends Component {
  
  constructor(props) {
    super(props);
  }
  render() {
    return (
      <Card>
        <CardContent>
          <Typography
            variant="h4"
            color="textPrimary"
          >
            Tráfico Web
          </Typography>
          <Chart options={
            {
              chart: {
                background: '#ffffff',
                id: 'apexchart-example'
              },
              colors: ['#1f87e6', '#ff5c7c'],
              dataLabels: {
                enabled: false
              },
              grid: {
                borderColor: "rgba(0, 0, 0, 0.12)",
                yaxis: {
                  lines: {
                    show: false
                  }
                }
              },
              legend: {
                show: true,
                position: 'top',
                horizontalAlign: 'right',
                labels: {
                  colors: "#546e7a"
                }
              },
              markers: {
                size: 4,
                strokeColors: ['#1f87e6', '#27c6db'],
                strokeWidth: 0,
                shape: 'circle',
                radius: 2,
                hover: {
                  size: undefined,
                  sizeOffset: 2
                }
              },
              stroke: {
                width: 3,
                curve: 'smooth',
                lineCap: 'butt',
                dashArray: [0, 3]
              },
              theme: {
                mode: "light"          
              },
              tooltip: {
                theme: "light"
              },
              xaxis: {
                axisBorder: {
                  color: "rgba(0, 0, 0, 0.12)"
                },
                axisTicks: {
                  show: true,
                  color: "rgba(0, 0, 0, 0.12)"
                },
                categories: ['01 Jan', '02 Jan', '03 Jan', '04 Jan', '05 Jan', '06 Jan', '07 Jan', '08 Jan', '09 Jan', '10 Jan', '11 Jan', '12 Jan'],
                labels: {
                  style: {
                    colors: "#546e7a"
                  }
                }
              },
              yaxis: [
                {
                  axisBorder: {
                    show: true,
                    color: "rgba(0, 0, 0, 0.12)"
                  },
                  axisTicks: {
                    show: true,
                    color: "rgba(0, 0, 0, 0.12)"
                  },
                  labels: {
                    style: {
                      colors: "#546e7a"
                    }
                  }
                },
                {
                  axisTicks: {
                    show: true,
                    color: "rgba(0, 0, 0, 0.12)"
                  },
                  axisBorder: {
                    show: true,
                    color: "rgba(0, 0, 0, 0.12)"
                  },
                  labels: {
                    style: {
                      colors: "#546e7a"
                    }
                  },
                  opposite: true
                }
              ]
            }
          } series={[
            {
              name: 'Vistas de página',
              data: [3350, 1840, 2254, 5780, 9349, 5241, 2770, 2051, 3764, 2385, 5912, 8323]
            },
            {
              name: 'Duración de la sesión',
              data: [35, 41, 62, 42, 13, 18, 29, 37, 36, 51, 32, 35]
            }
          ]} type="line" height="300" />
        </CardContent>
      </Card>
      
    )
  }
}