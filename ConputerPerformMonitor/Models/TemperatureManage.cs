using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConputerPerformMonitor.ViewModels;
using ConputerPerformMonitor.Models;
using LibreHardwareMonitor.Hardware;
using System.Collections.Concurrent;

namespace ConputerPerformMonitor.Models
{

    public class Log{
        public string date;
        public string time;
        public string cpu;
        public string memory;
        public string gpu;
    }
    public class TemperatureManage
    {
        public static event Action<TemperatureData>? UpdateTemp;
        public TemperatureManage()
        {
            _ = UpdateTemperature();
        }
        public async Task UpdateTemperature()
        {
            Trace.WriteLine("UpdateTemperature");
            Computer computer = new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsStorageEnabled = true,
                IsBatteryEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsPsuEnabled = true
            };
            try
            {
                computer.Open();

                while (true)
                {
                    foreach (var hardware in computer.Hardware)
                    {
                        hardware.Update();
                        foreach (var subHardware in hardware.SubHardware)
                        {
                            subHardware.Update();
                        }
                        TemperatureData temperatureData = new();
                        foreach (var sensor in hardware.Sensors)
                        {
                            
                            if (sensor.SensorType == SensorType.Load)
                            {
                                //Trace.WriteLine($"传感器名称: {sensor.Name}, 值: {sensor.Value}%");
                                Trace.WriteLine($"传感器名称: {sensor.Name}, 值: {sensor.Value}%");
                                temperatureData.temperatures.Add(new Temperature
                                {
                                    Name = sensor.Name,
                                    Value = sensor.Value
                                });
                            }
                            
                        }
                        if (temperatureData.temperatures.Count > 0)
                        {
                            UpdateTemp?.Invoke(temperatureData);
                        }
                        temperatureData.temperatures.Clear();
                        
                    }
                    await Task.Delay(500);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            finally
            {
                //computer.Close();
            }
        }
    }
    public class TemperatureData
    {
        public List<Temperature> temperatures = new();
    }
    public class Temperature
    {
        public string Name { get; set; }
        public float? Value { get; set; }

    }
}
