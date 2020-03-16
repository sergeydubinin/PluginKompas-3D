using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingPlugin
{
    public class Bushing
    {
        private double _totalLength;
        private double _topLength;
        private double _topDiametr;
        private double _outerDiametr;
        private double _innerDiametr;
        private int _numberHoles;
        private double _holesDiametr;
        private double _locationDiametr;

        /// <summary>
        /// Длина всей втулки
        /// </summary>
        public double TotalLength
        {
            get
            {
                return _totalLength;
            }

            set
            {
                if (value < 20)
                {
                    throw new ArgumentException("Длина всей втулки не может быть менее 20 мм.");
                }

                else if (value > 100)
                {
                    throw new ArgumentException("Длина всей втулки не может быть более 100 мм.");
                }

                else
                {
                    _totalLength = value;
                }
            }
        }

        /// <summary>
        /// Длина верхней части втулки
        /// </summary>
        public double TopLength
        {
            get
            {
                return _topLength;
            }

            set
            {
                if (value < 5)
                {
                    throw new ArgumentException("Длина верхней части втулки не может быть менее 5 мм.");
                }

                else if (value > (TotalLength / 2))
                {
                    throw new ArgumentException("Длина верхней части втулки не может быть более 1/2 длины всей втулки мм.");
                }

                else
                {
                    _topLength = value;
                }
            }
        }

        /// <summary>
        /// Диаметр верхней части втулки
        /// </summary>
        public double TopDiametr
        {
            get
            {
                return _topDiametr;
            }

            set
            {
                if (value < 55)
                {
                    throw new ArgumentException("Диамтер верхней части втулки не может быть менее 55 мм.");
                }

                else if (value > 120)
                {
                    throw new ArgumentException("Диамтер верхней части втулки не может быть более 120 мм.");
                }
                
                else
                {
                    _topDiametr = value;
                }
            }
        }

        /// <summary>
        /// Внешний диаметр втулки
        /// </summary>
        public double OuterDiametr
        {
            get
            {
                return _outerDiametr;
            }

            set
            {
                if (value < 35)
                {
                    throw new ArgumentException("Наружный диамтер втулки не может быть менее 35 мм.");
                }

                else if (value < ((2/3)*(TopDiametr)))
                {
                    throw new ArgumentException("Наружный диамтер втулки не может быть более 2/3 диаметра верхней части втулки мм.");
                }

                else
                {
                    _outerDiametr = value;
                }
            }
        }

        /// <summary>
        /// Внутренний диаметр втулки
        /// </summary>
        public double InnerDiametr
        {
            get
            {
                return _innerDiametr;
            }

            set
            {
                if (value < 20)
                {
                    throw new ArgumentException("Внутренний диамтер втулки не может быть менее 20 мм.");
                }

                else if (value > ((2 / 3) * (OuterDiametr)))
                {
                    throw new ArgumentException("Внутренний диамтер втулки не может быть более 2/3 наружного диаметра втулки мм.");
                }

                else
                {
                    _innerDiametr = value;
                }
            }
        }

        /// <summary>
        /// Количество отверстий
        /// </summary>
        public int NumberHoles
        {
            get
            {
                return _numberHoles;
            }

            set
            {
                if (value < 2)
                {
                    throw new ArgumentException("Количество отверстий не может быть менее 2 шт.");
                }

                else if (value > 6)
                {
                    throw new ArgumentException("Количество отверстий не может быть более 6 шт.");
                }

                else
                {
                    _numberHoles = value;
                }
            }
        }

        /// <summary>
        /// Диаметр отверстий
        /// </summary>
        public double HolesDiametr
        {
            get
            {
                return _holesDiametr;
            }

            set
            {
                if (value < 4)
                {
                    throw new ArgumentException("Диамтер отверстий не может быть менее 4 мм.");
                }

                else if (value > ((1 / 5) * (InnerDiametr)))
                {
                    throw new ArgumentException("Диамтер отверстий не может быть более 1/5 внутреннего диаметра втулки мм.");
                }

                else
                {
                    _holesDiametr = value;
                }
            }
        }

        /// <summary>
        /// Диаметр расположения отверстий
        /// </summary>
        public double LocationDiametr
        {
            get
            {
                return _locationDiametr;
            }

            set
            {
                if (value < ((1 / 2) * (TopDiametr)))
                {
                    throw new ArgumentException("Диамтер расположения отверстий не может быть менее 1/2 диаметра верхней части втулки мм.");
                }

                else if (value > ((4 / 5) * (TopDiametr)))
                {
                    throw new ArgumentException("Диамтер расположения отверстий не может быть более 4/5 диаметра верхней части втулки мм.");
                }

                else
                {
                    _locationDiametr = value;
                }
            }
        }
    }
}
