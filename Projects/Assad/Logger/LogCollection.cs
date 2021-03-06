﻿using System;
using System.Collections.Generic;

namespace Logger
{
    [Serializable]
    public class LogCollection
    {
        public List<LogEntry> logEnties { get; set; }

        public LogCollection()
        {
            logEnties = new List<LogEntry>();
        }

        public void AddEntry(LogEntry logEntry)
        {
            logEnties.Add(logEntry);
        }
    }
}