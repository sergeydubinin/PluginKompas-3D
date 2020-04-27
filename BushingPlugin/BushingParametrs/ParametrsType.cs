using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingParametrs
{
    /// <summary>
    /// Перечисление, содержащее
    /// список параметров втулки
    /// </summary>
    public enum ParametersType
    {
        /// <summary>
        /// Длина всей втулки
        /// </summary>
        TotalLength,

        /// <summary>
        /// Длина верхней части втулки
        /// </summary>
        TopLength,

        /// <summary>
        /// Диаметр верхней части втулки
        /// </summary>
        TopDiametr,

        /// <summary>
        /// Внешний диаметр втулки
        /// </summary>
        OuterDiametr,

        /// <summary>
        /// Внутренний диаметр втулки
        /// </summary>
        InnerDiametr,

        /// <summary>
        /// Количество отверстий
        /// </summary>
        NumberHoles,

        /// <summary>
        /// Даметр отверстий
        /// </summary>
        HolesDiametr,

        /// <summary>
        /// Диаметр расположения отверстий
        /// </summary>
        LocationDiametr
    }
}
