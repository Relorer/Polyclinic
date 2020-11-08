using POLYCLINIC.BLL.Infrastructure;
using POLYCLINIC.BLL.Models;
using POLYCLINIC.Client.Infrastructure.Structures;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace POLYCLINIC.Client.Infrastructure.Converters
{
    class AppointmentDayDataMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new AppointmentDayData()
            {
                DataGrid = values[0] as DataGrid,
                Week = values[1] as Week
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            AppointmentDayData data = value as AppointmentDayData;
            return new object[] { data.DataGrid, data.Week };
        }
    }
}
