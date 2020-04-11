using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    /// <summary>
    /// Исключение, выбрасываемое, если параметр "Диаметр расположения отверстий" задан некорректно
    /// </summary>
    public class LocationDiametrException : ApplicationException
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"></param>
        public LocationDiametrException(string message) : base(message)
        {
        }
    }
}
