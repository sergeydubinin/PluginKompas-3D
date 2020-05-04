using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushingParametrs
{
    /// <summary>
    /// Класс, содержащий параметры втулки
    /// </summary>
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
        private string _engravingText;
        private readonly bool _presenceEngraving = false;

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
                    _listError.Add(ParametersType.TotalLength, "Значение параметра введено некорректно: " +
                        "длина всей втулки не может быть менее 20 мм.");
                }
                else if (value > 100)
                {
                    _listError.Add(ParametersType.TotalLength, "Значение параметра введено некорректно: " +
                        "длина всей втулки не может быть более 100 мм.");
                }
                _totalLength = value;
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
                    _listError.Add(ParametersType.TopLength, "Значение параметра введено некорректно: " +
                        "длина верхней части втулки не может быть менее 5 мм.");
                }
                else if (value > ((TotalLength) / 2))
                {
                    _listError.Add(ParametersType.TopLength, "Значение параметра введено некорректно: " +
                        "длина верхней части втулки не может быть более 1/2 длины всей втулки мм.");
                }
                _topLength = value;
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
                    _listError.Add(ParametersType.TopDiametr, "Значение параметра введено некорректно: " +
                        "диамтер верхней части втулки не может быть менее 55 мм.");
                }
                else if (value > 120)
                {
                    _listError.Add(ParametersType.TopDiametr, "Значение параметра введено некорректно: " +
                        "диамтер верхней части втулки не может быть более 120 мм.");
                }
                _topDiametr = value;
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
                    _listError.Add(ParametersType.OuterDiametr, "Значение параметра введено некорректно: " +
                        "внешний диамтер втулки не может быть менее 35 мм.");
                }
                else if (value > ((2 * (TopDiametr)) / 3))
                {
                    _listError.Add(ParametersType.OuterDiametr, "Значение параметра введено некорректно: " +
                        "внешний диамтер втулки не может быть более 2/3 диаметра верхней части втулки мм.");
                }
                _outerDiametr = value;
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
                    _listError.Add(ParametersType.InnerDiametr, "Значение параметра введено некорректно: " +
                        "внутренний диамтер втулки не может быть менее 20 мм.");
                }
                else if (value > ((2 * (OuterDiametr)) / 3))
                {
                    _listError.Add(ParametersType.InnerDiametr, "Значение параметра введено некорректно: " +
                        "внутренний диамтер втулки не может быть более 2/3 внешнего диаметра втулки мм.");
                }
                _innerDiametr = value;
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
                    _listError.Add(ParametersType.NumberHoles, "Значение параметра введено некорректно: " +
                        "количество отверстий не может быть менее 2 шт.");
                }
                else if (value > 6)
                {
                    _listError.Add(ParametersType.NumberHoles, "Значение параметра введено некорректно: " +
                        "количество отверстий не может быть более 6 шт.");
                }
                _numberHoles = value;
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
                    _listError.Add(ParametersType.HolesDiametr, "Значение параметра введено некорректно: " +
                        "диамтер отверстий не может быть менее 4 мм.");
                }
                else if (value > ((InnerDiametr) / 4))
                {
                    _listError.Add(ParametersType.HolesDiametr, "Значение параметра введено некорректно: " +
                        "диамтер отверстий не может быть более 1/4 внутреннего диаметра втулки мм.");
                }
                _holesDiametr = value;
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
                if (value < ((3 * (TopDiametr)) / 4))
                {
                    _listError.Add(ParametersType.LocationDiametr, "Значение параметра введено некорректно: " +
                        "диамтер расположения отверстий не может быть менее 3/4 диаметра верхней части втулки мм.");
                }
                else if (value > ((4 * (TopDiametr)) / 5))
                {
                    _listError.Add(ParametersType.LocationDiametr, "Значение параметра введено некорректно: " +
                        "диамтер расположения отверстий не может быть более 4/5 диаметра верхней части втулки мм.");
                }
                _locationDiametr = value;
            }
        }

        /// <summary>
        /// Текст гравировки
        /// </summary>
        public string EngravingText
        {
            get
            {
                return _engravingText;
            }

            set
            {
                if (value.Length > 15)
                {
                    _listError.Add(ParametersType.EngravingText, "Значение параметра введено некорректно: " +
                        "текст гравировки не может быть более 15 символов");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    _listError.Add(ParametersType.EngravingText, "Значение параметра введено некорректно: " +
                        "не введен текст гравировки");
                }
                _engravingText = value;
            }
        }

        /// <summary>
        /// Наличие гравировки на втулке
        /// </summary>
        public bool PresenceEngraving
        {
            get => _presenceEngraving;
        }

        /// <summary>
        /// Конструктор без гравировки
        /// </summary>
        /// <param name="totalLength"></param>
        /// <param name="topLength"></param>
        /// <param name="topDiametr"></param>
        /// <param name="outerDiametr"></param>
        /// <param name="innerDiametr"></param>
        /// <param name="numberHoles"></param>
        /// <param name="holesDiametr"></param>
        /// <param name="locationDiametr"></param>
        public Bushing(double totalLength, double topLength, double topDiametr, double outerDiametr, double innerDiametr,
            int numberHoles, double holesDiametr, double locationDiametr)
        {
            _listError = new Dictionary<ParametersType, string>();
            _listError.Clear();

            TotalLength = totalLength;
            TopLength = topLength;
            TopDiametr = topDiametr;
            OuterDiametr = outerDiametr;
            InnerDiametr = innerDiametr;
            NumberHoles = numberHoles;
            HolesDiametr = holesDiametr;
            LocationDiametr = locationDiametr;
            _presenceEngraving = false;
        }

        /// <summary>
        /// Конструктор с гравировкой
        /// </summary>
        /// <param name="totalLength"></param>
        /// <param name="topLength"></param>
        /// <param name="topDiametr"></param>
        /// <param name="outerDiametr"></param>
        /// <param name="innerDiametr"></param>
        /// <param name="numberHoles"></param>
        /// <param name="holesDiametr"></param>
        /// <param name="locationDiametr"></param>
        /// <param name="engravingText"></param>
        public Bushing(double totalLength, double topLength, double topDiametr, double outerDiametr, double innerDiametr,
            int numberHoles, double holesDiametr, double locationDiametr, string engravingText)
        {
            _listError = new Dictionary<ParametersType, string>();
            _listError.Clear();

            TotalLength = totalLength;
            TopLength = topLength;
            TopDiametr = topDiametr;
            OuterDiametr = outerDiametr;
            InnerDiametr = innerDiametr;
            NumberHoles = numberHoles;
            HolesDiametr = holesDiametr;
            LocationDiametr = locationDiametr;
            EngravingText = engravingText;
            _presenceEngraving = true;
        }

        /// <summary>
        /// Словарь, связывающий параметр втулки и возникшую
        /// ошибку при вводе данного параметра
        /// </summary>
        public Dictionary<ParametersType, string> _listError;
    }
}
