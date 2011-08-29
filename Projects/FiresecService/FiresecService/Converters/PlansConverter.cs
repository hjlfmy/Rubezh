﻿using System;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiresecAPI.Models;

namespace FiresecService.Converters
{
    public static class PlansConverter
    {
        public static PlansConfiguration Convert(Firesec.Plans.surfaces innerPlans)
        {
            var plansConfiguration = new PlansConfiguration();
            if (innerPlans.surface != null)
            {
                plansConfiguration.Plans = new List<Plan>();
                foreach (var _planInner in innerPlans.surface)
                {
                    Plan planInner = new Plan();
                    planInner.Caption = _planInner.caption;
                    planInner.Height = Double.Parse(_planInner.height);
                    planInner.Width = Double.Parse(_planInner.width);
                    int index = 0;
                    foreach (var _elementInner in _planInner.layer)
                    {
                        if (_elementInner.name == "План") // Графические примитивы
                        {
                            ;
                        }
                        if ((_elementInner.name == "Зоны") || (_elementInner.name == "Несвязанные зоны") || (_elementInner.name == "Пожарные зоны") || (_elementInner.name == "Охранные зоны"))
                        {
                            
                            ElementZone zoneInner = null;
                            if (_elementInner.elements != null)
                            {
                                if (planInner.ElementZones == null) planInner.ElementZones = new List<ElementZone>();
                                foreach (var _zoneInner in _elementInner.elements)
                                {
                                    zoneInner = new ElementZone();
                                    string _idTempS = _zoneInner.id;
                                    long _idTempL = long.Parse(_idTempS);
                                    int _idTempI = (int)_idTempL;
                                    foreach (var _index in FiresecManager.DeviceConfiguration.Zones)
                                    {

                                        if (_index.ShapeId == _idTempL.ToString())
                                        {
                                            zoneInner.ZoneNo = _index.No;
                                        }
                                        else
                                            if (_index.ShapeId == _idTempI.ToString())
                                            {
                                                zoneInner.ZoneNo = _index.No;
                                            }
                                    }
                                    PolygonPoint polygonPointsInner = null;
                                    zoneInner.PolygonPoints = new List<PolygonPoint>();
                                    switch (_zoneInner.@class)
                                    {
                                        case "TFS_PolyZoneShape":
                                            if (_zoneInner.points != null)
                                            {
                                                foreach (var _pointInner in _zoneInner.points)
                                                {
                                                    polygonPointsInner = new PolygonPoint();
                                                    polygonPointsInner.X = ValidationDouble(_pointInner.x);
                                                    polygonPointsInner.Y = ValidationDouble(_pointInner.y);
                                                    zoneInner.PolygonPoints.Add(polygonPointsInner);
                                                }
                                            }; break;
                                        case "TFS_ZoneShape":
                                            foreach (var _rectInner in _zoneInner.rect)
                                            {
                                                polygonPointsInner = new PolygonPoint();
                                                polygonPointsInner.X = ValidationDouble(_rectInner.left);
                                                polygonPointsInner.Y = ValidationDouble(_rectInner.top);
                                                zoneInner.PolygonPoints.Add(polygonPointsInner);
                                                polygonPointsInner = new PolygonPoint();
                                                polygonPointsInner.X = ValidationDouble(_rectInner.right);
                                                polygonPointsInner.Y = ValidationDouble(_rectInner.top);
                                                zoneInner.PolygonPoints.Add(polygonPointsInner);
                                                polygonPointsInner = new PolygonPoint();
                                                polygonPointsInner.X = ValidationDouble(_rectInner.right);
                                                polygonPointsInner.Y = ValidationDouble(_rectInner.bottom);
                                                zoneInner.PolygonPoints.Add(polygonPointsInner);
                                                polygonPointsInner = new PolygonPoint();
                                                polygonPointsInner.X = ValidationDouble(_rectInner.left);
                                                polygonPointsInner.Y = ValidationDouble(_rectInner.bottom);
                                                zoneInner.PolygonPoints.Add(polygonPointsInner);
                                            };
                                            break;
                                    }

                                    planInner.ElementZones.Add(zoneInner);
                                }
                            }
                        };
                        if (_elementInner.name == "Устройства")
                        {
                            ElementDevice deviceInner = null;
                            if (_elementInner.elements != null)
                            {
                                if (planInner.ElementDevices == null) planInner.ElementDevices = new List<ElementDevice>();
                                foreach (var _deviceInner in _elementInner.elements)
                                {
                                    deviceInner = new ElementDevice();
                                    
/* Нету ShapeId в девайсах
                                    string _idTempS = _deviceInner.id;
                                    long _idTempL = long.Parse(_idTempS);
                                    int _idTempI = (int)_idTempL;
                                    //List<Zone> temp=FiresecManager.DeviceConfiguration.Zones;
                                    foreach (var _index in FiresecManager.DeviceConfiguration.Devices)
                                    {
                                        if (_index.ShapeId == _idTempL.ToString())
                                        {
                                            deviceInner.ZoneNo = _index.No;
                                        }
                                        else
                                            if (_index.ShapeId == _idTempI.ToString())
                                            {
                                                deviceInner.ZoneNo = _index.No;
                                            }
                                    }
                                    */
                                    deviceInner.Id = "NULL";
                                    if (_deviceInner.rect != null)
                                    {
                                        foreach (var _rectInner in _deviceInner.rect)
                                        {
                                            deviceInner.Left = ValidationDouble(_rectInner.left);
                                            deviceInner.Top = ValidationDouble(_rectInner.top);
                                            deviceInner.Width = ValidationDouble(_rectInner.right) - deviceInner.Left;
                                            deviceInner.Height = ValidationDouble(_rectInner.bottom) - deviceInner.Top;
                                        }
                                        planInner.ElementDevices.Add(deviceInner);
                                    }
                                }
                            }
                        }
                        index++;
                    }
                    plansConfiguration.Plans.Add(planInner);
                }
            }
            return plansConfiguration;
        }
        static Double ValidationDouble(string str)
        {
            Double result;
            try
            {
                str = str.Replace(".", ",");
                result = Double.Parse(str);
                return result;
            }
            catch
            {
                str = str.Replace(",", ".");
                result = Double.Parse(str);
                return result;
            }
        }
    }
}