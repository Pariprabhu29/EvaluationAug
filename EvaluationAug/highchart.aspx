<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="highchart.aspx.cs" Inherits="EvaluationAug.highchart" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Highcharts Example</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/pareto.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="containerForPie" style="width: 100%; height: 300px;"></div>
        <div id="containerForPareto" style="width: 100%; height: 500px;"></div>
    </form>
    <script>
        $(document).ready(function () {
            displayChart();
        });

        function displayChart() {
            $.ajax({
                url: "highchart.aspx/GetDataForDisplayInPiechart",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                success: function (response) {
                    buildChart(response.d);
                },
                error: function (xhr, status, error) {
                    console.error("AJAX Error: ", status, error);
                }
            });
        }


        function getCategories(data) {
            return data.map(item => item.name);
        }

        function secondsToHHMM(seconds) {
            let hours = Math.floor(seconds / 3600);
            let minutes = Math.floor((seconds % 3600) / 60);
            return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}`;
        }


        function getTime(data) {
            return data.map(item => item.y);
        }

        function buildChart(data) {
            Highcharts.chart('containerForPie', {
                chart: {
                    type: 'pie'
                },
                title: {
                    text: 'Downtime vs DownID'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>',
                    valueSuffix: '%',
                },
                credits: {
                    enabled: false,
                },
                subtitle: {
                    text: 'PIE Chart For Downtime vs DownID'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}: {point.y} [{point.percentage:.1f}%]',
                            style: {
                                fontSize: '10px'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Percentage',
                    colorByPoint: true,
                    data: data
                }]
            });

            Highcharts.chart('containerForPareto', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'DownTime vs DownID'
                },
                tooltip: {
                    shared: true
                },
                xAxis: {
                    categories: getCategories(data),
                    crosshair: true,
                },
                yAxis: [{
                    title: {
                        text: 'DowTime in HH:mm'
                    },
                    labels: {
                        formatter: function () {
                            return secondsToHHMM(this.value);
                        }
                    }
                }, {
                    opposite: true,
                    labels: {
                        format: '{value}%'
                    }
                }],
                plotOptions: {
                    column: {
                        dataLabels: {
                            enabled: true, // Enable data labels for all columns
                            formatter: function () {
                                return secondsToHHMM(this.y);
                            }
                        }
                    },
                    pareto: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                return Highcharts.numberFormat(this.y, 2) + '%';
                            },
                            verticalAlign: 'bottom',
                            align: 'left',
                            style: {
                                color: '#000',
                                textOutline: '1px contrast'
                            }
                        }
                    }
                },
                    series: [{
                        type: 'pareto',
                        name: 'Pareto',
                        yAxis: 1,
                        baseSeries: 1,
                        tooltip: {
                            valueDecimals: 2,
                            valueSuffix: '%'
                        }
                    }, {
                        name: 'DownTimeID',
                        type: 'column',
                        enabled: true,
                        colorByPoint: true,
                        data: getTime(data)
                    }]
                });
        }
    </script>
</body>
</html>
