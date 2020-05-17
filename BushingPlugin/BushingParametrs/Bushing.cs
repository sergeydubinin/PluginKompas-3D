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
        /// <summary>
        /// Длина всей втулки
        /// </summary>
        private double _totalLength;

        /// <summary>
        /// Длина верхней части втулки
        /// </summary>
        private double _topLength;

        /// <summary>
        /// Диаметр верхней части втулки
        /// </summary>
        private double _topDiametr;

        /// <summary>
        /// Внешний диаметр втулки
        /// </summary>
        private double _outerDiametr;

        /// <summary>
        /// Внутренний диаметр втулки
        /// </summary>
        private double _innerDiametr;

        /// <summary>
        /// Количество отверстий
        /// </summary>
        private int _numberHoles;

        /// <summary>
        /// Диамтер отверстий
        /// </summary>
        private double _holesDiametr;

        /// <summary>
        /// Диамтер расположения отверстий
        /// </summary>
        private double _locationDiametr;

        /// <summary>
        /// Текст гравировки
        /// </summary>
        private string _engravingText;

        /// <summary>
        /// Наличие гравировки
        /// </summary>
        private readonly bool _presenceEngraving = false;

        /// <summary>
        /// Возвращает и устанавливает значение длины всей втулки
        /// </summary>
        public double TotalLength
        {
            get
            {
                return _totalLength;
            }

            set
            {
                const int minTotalLength = 20;
                const int maxTotalLength = 100;
                string messageMin = "длина всей втулки не может быть " +
                    "менее 20 мм.";
                string messageMax = "длина всей втулки не может быть " +
                    "более 100 мм.";
                ValidationParameter(value, minTotalLength, maxTotalLength, 
                    ParametersType.TotalLength, messageMin, messageMax);
                _totalLength = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение длины верхней части втулки
        /// </summary>
        public double TopLength
        {
            get
            {
                return _topLength;
            }

            set
            {
                const int minTopLength = 5;
                double maxTopLength = ((TotalLength) / 2);
                string messageMin = "длина верхней части втулки не может " +
                    "быть менее 5 мм.";
                string messageMax = "длина верхней части втулки не может " +
                    "быть более 1/2 длины всей втулки мм.";
                ValidationParameter(value, minTopLength, maxTopLength, 
                    ParametersType.TopLength, messageMin, messageMax);
                _topLength = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение диаметра верхей части втулки
        /// </summary>
        public double TopDiametr
        {
            get
            {
                return _topDiametr;
            }

            set
            {
                const int minTopDiametr = 55;
                const int maxTopDiametr = 120;
                string messageMin = "диаметр верхней части втулки не может " +
                    "быть менее 55 мм.";
                string messageMax = "диаметр верхней части втулки не может " +
                    "быть более 120 мм.";
                ValidationParameter(value, minTopDiametr, maxTopDiametr, 
                    ParametersType.TopDiametr, messageMin, messageMax);
                _topDiametr = value;
            }
        }


        /// <summary>
        /// Возвращает и устанавливает значение внешнего диаметра втулки
        /// </summary>
        public double OuterDiametr
        {
            get
            {
                return _outerDiametr;
            }

            set
            {
                const int minOuterDiametr = 35;
                double maxOuterDiametr = ((2 * (TopDiametr)) / 3);
                string messageMin = "внешний диаметр втулки не может быть " +
                    "менее 35 мм.";
                string messageMax = "внешний диаметр втулки не может быть " +
                    "более 2/3 диаметра верхней части втулки мм.";
                ValidationParameter(value, minOuterDiametr, maxOuterDiametr, 
                    ParametersType.OuterDiametr, messageMin, messageMax);
                _outerDiametr = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение внутреннего диаметра втулки
        /// </summary>
        public double InnerDiametr
        {
            get
            {
                return _innerDiametr;
            }

            set
            {
                const int minInnerDiametr = 20;
                double maxInnerDiametr = ((2 * (OuterDiametr)) / 3);
                string messageMin = "внутренний диаметр втулки не может " +
                    "быть менее 20 мм.";
                string messageMax = "внутренний диаметр втулки не может " +
                    "быть более 2/3 внешнего диаметра втулки мм.";
                ValidationParameter(value, minInnerDiametr, maxInnerDiametr, 
                    ParametersType.InnerDiametr, messageMin, messageMax);
                _innerDiametr = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает количество отверстий
        /// </summary>
        public int NumberHoles
        {
            get
            {
                return _numberHoles;
            }

            set
            {
                const int minNumberHoles = 2;
                const int maxNumberHoles = 6;
                string messageMin = "количество отверстий не может быть " +
                    "менее 2 шт.";
                string messageMax = "количество отверстий не может быть " +
                    "более 6 шт.";
                ValidationParameter(value, minNumberHoles, maxNumberHoles, 
                    ParametersType.NumberHoles, messageMin, messageMax);
                _numberHoles = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает диаметр отверстий
        /// </summary>
        public double HolesDiametr
        {
            get
            {
                return _holesDiametr;
            }

            set
            {
                const int minHolesDiametr = 4;
                double maxHolesDiametr = ((InnerDiametr) / 4);
                string messageMin = "диаметр отверстий не может быть менее " +
                    "4 мм.";
                string messageMax = "диаметр отверстий не может быть более " +
                    "1/4 внутреннего диаметра втулки мм.";
                ValidationParameter(value, minHolesDiametr, maxHolesDiametr, 
                    ParametersType.HolesDiametr, messageMin, messageMax);
                _holesDiametr = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает диаметр расположения отверстий
        /// </summary>
        public double LocationDiametr
        {
            get
            {
                return _locationDiametr;
            }

            set
            {
                double minLocationDiametr = ((3 * (TopDiametr)) / 4);
                double maxLocationDiametr = ((4 * (TopDiametr)) / 5);
                string messageMin = "диаметр расположения отверстий не " +
                    "может быть менее 3/4 диаметра верхней части втулки мм.";
                string messageMax = "диаметр расположения отверстий не " +
                    "может быть более 4/5 диаметра верхней части втулки мм.";
                ValidationParameter(value, minLocationDiametr,
                    maxLocationDiametr, ParametersType.LocationDiametr, 
                    messageMin, messageMax);
                _locationDiametr = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает текст гравировки
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
                    _listError.Add(ParametersType.EngravingText, 
                        "текст гравировки не может быть более 15 символов");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    _listError.Add(ParametersType.EngravingText, 
                        "не введен текст гравировки");
                }
                _engravingText = value;
            }
        }

        /// <summary>
        /// Возвращает наличие гравировки на втулке
        /// </summary>
        public bool PresenceEngraving
        {
            get => _presenceEngraving;
        }

        /// <summary>
        /// Конструктор втулки без гравировки
        /// </summary>
        /// <param name="totalLength">Длина всей втулки</param>
        /// <param name="topLength">Длина верхней части втулки</param>
        /// <param name="topDiametr">Диаметр верхней части втулки</param>
        /// <param name="outerDiametr">Внешний диаметр втулки</param>
        /// <param name="innerDiametr">Внутренний диаметр втулки</param>
        /// <param name="numberHoles">Количество отверстий</param>
        /// <param name="holesDiametr">Диаметр отверстий</param>
        /// <param name="locationDiametr">Диаметр расположения</param>
        public Bushing(double totalLength, double topLength,
            double topDiametr, double outerDiametr, double innerDiametr,
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
        /// Конструктор втулки с гравировкой
        /// </summary>
        /// <param name="totalLength">Длина всей втулки</param>
        /// <param name="topLength">Длина верхней части втулки</param>
        /// <param name="topDiametr">Диаметр верхней части втулки</param>
        /// <param name="outerDiametr">Внешний диаметр втулки</param>
        /// <param name="innerDiametr">Внутренний диаметр втулки</param>
        /// <param name="numberHoles">Количество отверстий</param>
        /// <param name="holesDiametr">Диаметр отверстий</param>
        /// <param name="locationDiametr">Диаметр расположения</param>
        /// <param name="engravingText">Текст гравировки</param>
        public Bushing(double totalLength, double topLength, 
            double topDiametr, double outerDiametr, double innerDiametr,
            int numberHoles, double holesDiametr, double locationDiametr,
            string engravingText)
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

        /// <summary>
        /// Метод, предназначенный для валидации параметров втулки
        /// </summary>
        /// <param name="value">Введеное значение параметра</param>
        /// <param name="minValue">Минимальное возможное значение</param>
        /// <param name="maxValue">Максимальное возможное значение</param>
        /// <param name="parametersType">Параметр</param>
        /// <param name="messageMin">Сообщение при выходе за рамки min</param>
        /// <param name="messageMax">Сообщение при выходе за рамки max</param>
        private void ValidationParameter(double value, double minValue, 
            double maxValue, ParametersType parametersType, 
            string messageMin, string messageMax)
        {
            if (value < minValue)
            {
                _listError.Add(parametersType, messageMin);
            }
            else if (value > maxValue)
            {
                _listError.Add(parametersType, messageMax);
            }
        }
    }
}
