using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

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
        /// Запуск Компас-3D, если он не запущен
        /// </summary>
        public void StartKompas()
        {
            try
            {
                if (_kompas == null)
                {
                    Type kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance(kompasType);
                }

                if (_kompas != null)
                {
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                }
            }
            catch
            {
                throw new ArgumentException("Не удается открыть Компас-3D");
            }
        }


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
