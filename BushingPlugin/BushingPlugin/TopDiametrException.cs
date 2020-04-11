using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    /// <summary>
    /// Исключение, выбрасываемое, если параметр "Диаметр верхней части втулки" задан некорректно
    /// </summary>
    public class TopDiametrException : ApplicationException
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"></param>
        public TopDiametrException(string message) : base(message)
        {
        }
    }
}
