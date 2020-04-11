using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    /// <summary>
    /// Исключение, выбрасываемое, если параметр "Диаметр отверстий" задан некорректно
    /// </summary>
    public class HolesDiametrException : ApplicationException
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"></param>
        public HolesDiametrException(string message) : base(message)
        {
        }
    }
}
