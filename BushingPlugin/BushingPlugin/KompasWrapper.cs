using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BushingParametrs;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using System.Runtime.InteropServices;

namespace BushingPlugin
{
    /// <summary>
    /// Класс, предназначенный для взаимодействия с САПР Компас-3D
    /// </summary>
    public class KompasWrapper
    {
        /// <summary>
        /// Объект Компас API
        /// </summary>
        private KompasObject _kompas = null;

        /// <summary>
        /// Запуск Компас-3D
        /// </summary>
        public void StartKompas()
        {
            try
            {
                if (_kompas != null)
                {
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                }

                if (_kompas == null)
                {
                    Type kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance(kompasType);

                    StartKompas();

                    if (_kompas == null)
                    {
                        throw new Exception("Не удается открыть Koмпас-3D");
                    }
                }
            }
            catch (COMException)
            {
                _kompas = null;
                StartKompas();
            }
        }


        /// <summary>
        /// Построение втулки
        /// </summary>
        /// <param name="bushing"></param>
        public void BuildBushing(Bushing bushing)
        {
            try
            {
                CreateBushing detail = new CreateBushing(_kompas);
                detail.CreateDetail(bushing);
            }
            catch
            {
                throw new ArgumentException("Не удается построить деталь");
            }
        }
    }
}
