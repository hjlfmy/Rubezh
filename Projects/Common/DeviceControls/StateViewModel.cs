﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeviceLibrary;
using System.Windows.Controls;
using System.Windows.Threading;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Frame = DeviceLibrary.Frame;

namespace DeviceControls
{
    public class StateViewModel
    {
        readonly DispatcherTimer _timer = new DispatcherTimer();
        int _tick;
        readonly List<DeviceLibrary.Frame> _frames;
        Canvas _canvas;
        readonly ObservableCollection<Canvas> _stateCanvases;

        public StateViewModel(State state, ObservableCollection<Canvas> stateCanvases)
        {
            _stateCanvases = stateCanvases;
            _frames = state.Frames;

            if (state.Frames.Count > 1)
            {
                _timer = new DispatcherTimer();
                _timer.Tick += TimerTick;
                _timer.IsEnabled = true;
            }
            else
            {
                Draw(_frames[0]);
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var frame = _frames[_tick];
            _timer.Interval = new TimeSpan(0, 0, 0, 0, frame.Duration);
            Draw(frame);
            _tick = (_tick + 1) % _frames.Count;
        }

        void Draw(Frame frame)
        {
            _stateCanvases.Remove(_canvas);
            _canvas = SvgToCanvas(frame.Image);
            Panel.SetZIndex(_canvas, frame.Layer);
            _stateCanvases.Add(_canvas);
        }

        static Canvas SvgToCanvas(string svg)
        {
            var frameImage = SvgConverter.Svg2Xaml(svg, ResourceHelper.svg2xaml_xsl);
            var stringReader = new StringReader(frameImage);
            var xmlReader = XmlReader.Create(stringReader);
            var canvas = (Canvas)XamlReader.Load(xmlReader);
            return canvas;
        }
    }
}
