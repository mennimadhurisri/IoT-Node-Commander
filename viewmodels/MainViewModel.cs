using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iotcommender.models;
using iotcommender.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
namespace IotCommander.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly Hardwareparserservice parser = new();

    private double temperature;
    private bool isConnected;

    public double Temperature
    {
        get => temperature;
        set
        {
            if (temperature != value)
            {
                temperature = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsConnected
    {
        get => isConnected;
        set
        {
            if (isConnected != value)
            {
                isConnected = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<Sensorreading> Readings { get; } = new();

    public ICommand ConnectCommand { get; }

    public MainViewModel()
    {
        ConnectCommand = new Command(ConnectAsync);
    }

    private async void ConnectAsync()
    {
        IsConnected = true;

        try
        {
            for (int i = 0; i < 10; i++)
            {
                string rawData = "0xAA 0x02 0x00 0xFA";

                Sensorreading? reading = parser.Parse(rawData);

                if (reading != null)
                {
                    Temperature = reading.Temperature;
                    Readings.Add(reading);
                }

                await Task.Delay(2000);
            }
        }
        finally
        {
            IsConnected = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}

public class Command : ICommand
{
    private readonly Action execute;

    public Command(Action execute)
    {
        this.execute = execute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { }
        remove { }
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        execute();
    }
}
