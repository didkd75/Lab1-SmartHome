using System;

namespace Lab_1_Roshko
{
    public class SmartDevice
    {
        // Приватні поля
        private string _serialNumber; // Формат: 6 цифр
        private string _deviceName;   // Довжина 3-15 символів, тільки букви та пробіли
        private DeviceType _type;     // Enum
        private double _powerUsage;   // Від 0.0 до 500.0 Вт
        private bool _isActive;       // Стан пристрою

        // Публічні властивості (getters / setters)
        public string SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = value;
        }

        public string DeviceName
        {
            get => _deviceName;
            set => _deviceName = value;
        }

        public DeviceType Type
        {
            get => _type;
            set => _type = value;
        }

        public double PowerUsage
        {
            get => _powerUsage;
            set => _powerUsage = value;
        }

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public SmartDevice() { }

        public SmartDevice(string serialNumber, string deviceName, DeviceType type, double powerUsage, bool isActive)
        {
            _serialNumber = serialNumber;
            _deviceName = deviceName;
            _type = type;
            _powerUsage = powerUsage;
            _isActive = isActive;
        }

        // Публічні методи поведінки пристрою
        public void TogglePower()
        {
            _isActive = !_isActive;
            Console.WriteLine($"[Поведінка] Стан пристрою '{_deviceName}' змінено на: {(_isActive ? "Увімкнено" : "Вимкнено")}.");
        }

        public void CalibratePower(double newUsage)
        {
            _powerUsage = newUsage;
            Console.WriteLine($"[Поведінка] Потужність пристрою '{_deviceName}' відкалібровано до {_powerUsage} Вт.");
        }

        public string GetDiagnosticsReport()
        {
            return $"[Діагностика] Пристрій: {_deviceName} (SN: {_serialNumber}) | Стан: {(_isActive ? "Active" : "Standby")} | Споживання: {_powerUsage}W";
        }
    }
}
