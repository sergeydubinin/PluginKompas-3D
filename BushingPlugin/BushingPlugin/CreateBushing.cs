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
    public class CreateBushing
    {
        /// <summary>
        /// Объект Компас API
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        /// <param name="kompas">Исходный объект</param>
        public CreateBushing(KompasObject kompas)
        {
            this._kompas = kompas;

            var doc = (ksDocument3D)kompas.Document3D();
            doc.Create();
        }

        public void CreateDetail(Bushing bushing)
        {
            CreateHole(bushing);
            CreateLeg(bushing);
        }

        private void CreateHole(Bushing bushing)
        {
            double radius = bushing.TopDiametr / 2;
            double depth = bushing.TopLength;
            var doc = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);
            if (part != null)
            {
                // Создаем новый эскиз
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {
                        // получим интерфейс базовой плоскости
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                        sketchDef.SetPlane(basePlane);	// установим плоскость базовой для эскиза
                        entitySketch.Create();			// создадим эскиз1

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();


                        // отверстие
                        sketchEdit.ksCircle(0, 0, radius, 1);
                        sketchDef.EndEdit();	// завершение редактирования эскиза

                        ksEntity entityExtr = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
                        if (entityExtr != null)
                        {
                            // интерфейс свойств базовой операции выдавливания
                            ksBaseExtrusionDefinition extrusionDef =
                                 (ksBaseExtrusionDefinition)entityExtr.GetDefinition();
                            // интерфейс базовой операции выдавливания
                            if (extrusionDef != null)
                            {
                                extrusionDef.directionType = (short)Direction_Type.dtNormal;
                                // направление выдавливания
                                extrusionDef.SetSideParam(true, // прямое направление
                                                          (short)End_Type.etBlind, // строго на глубину
                                                          depth); // Расстояние выдавливания
                                extrusionDef.SetSketch(entitySketch); // эскиз операции выдавливания

                                entityExtr.Create(); // создать операцию
                                sketchDef.EndEdit(); // завершение редактирования эскиза
                            }
                        }

                    }
                }
            }
        }

        private void CreateLeg(Bushing bushing)
        {
            double radius = bushing.OuterDiametr / 2;
            double depth = bushing.TotalLength - bushing.TopLength;
            var doc = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);
            if (part != null)
            {
                // Создаем новый эскиз
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {
                        // получим интерфейс базовой плоскости
                        ksEntity basePlane;
                        basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                        sketchDef.SetPlane(basePlane);	// установим плоскость базовой для эскиза
                        entitySketch.Create();			// создадим эскиз1

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();


                        // отверстие
                        sketchEdit.ksCircle(0, 0, radius, 1);
                        sketchDef.EndEdit();	// завершение редактирования эскиза

                        ksEntity entityExtr = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
                        if (entityExtr != null)
                        {
                            // интерфейс свойств базовой операции выдавливания
                            ksBaseExtrusionDefinition extrusionDef =
                                 (ksBaseExtrusionDefinition)entityExtr.GetDefinition();
                            // интерфейс базовой операции выдавливания
                            if (extrusionDef != null)
                            {
                                extrusionDef.directionType = (short)Direction_Type.dtNormal;
                                // направление выдавливания
                                extrusionDef.SetSideParam(true, // прямое направление
                                                          (short)End_Type.etBlind, // строго на глубину
                                                          depth); // Расстояние выдавливания
                                extrusionDef.SetSketch(entitySketch); // эскиз операции выдавливания

                                entityExtr.Create(); // создать операцию
                                sketchDef.EndEdit(); // завершение редактирования эскиза
                            }
                        }

                    }
                }
            }
        }
        /*
        private void CreateCircle(double radius, double depth)
        {
            var doc = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);
            if (part != null)
            {
                // Создаем новый эскиз
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {
                        // получим интерфейс базовой плоскости
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                        sketchDef.SetPlane(basePlane);	// установим плоскость базовой для эскиза
                        entitySketch.Create();			// создадим эскиз1

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();


                        // отверстие
                        sketchEdit.ksCircle(0, 0, radius, 1);
                        sketchDef.EndEdit();	// завершение редактирования эскиза

                        ksEntity entityExtr = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
                        if (entityExtr != null)
                        {
                            // интерфейс свойств базовой операции выдавливания
                            ksBaseExtrusionDefinition extrusionDef =
                                 (ksBaseExtrusionDefinition)entityExtr.GetDefinition();
                            // интерфейс базовой операции выдавливания
                            if (extrusionDef != null)
                            {
                                extrusionDef.directionType = (short)Direction_Type.dtNormal;
                                // направление выдавливания
                                extrusionDef.SetSideParam(true, // прямое направление
                                                          (short)End_Type.etBlind, // строго на глубину
                                                          depth); // Расстояние выдавливания
                                extrusionDef.SetSketch(entitySketch); // эскиз операции выдавливания

                                entityExtr.Create(); // создать операцию
                                sketchDef.EndEdit(); // завершение редактирования эскиза
                            }
                        }

                    }
                }
            }
        }*/
    }
}
