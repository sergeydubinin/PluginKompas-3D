using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    /// <summary>
    /// Исключение, выбрасываемое, если параметр "Внутренний диаметр втулки" задан некорректно
    /// </summary>
    public class InnerDiametrException : ApplicationException
    {
        /// <summary>
        /// Конструктор исключения
        /// </summary>
        /// <param name="message"></param>
        public InnerDiametrException(string message) : base(message)
        {
        }
    }
}
