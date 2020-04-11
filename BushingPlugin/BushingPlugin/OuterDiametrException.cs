using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    /// <summary>
    /// Исключение, выбрасываемое, если параметр "Внешний диаметр" задан некорректно
    /// </summary>
    public class OuterDiametrException : ApplicationException
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"></param>
        public OuterDiametrException(string message) : base(message)
        {
        }
    }
}
