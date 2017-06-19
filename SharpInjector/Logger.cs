using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpInjector
{
    class Logger
    {
        public enum Level
        {
            Debug,
            Info,
            Warning,
            Error,
            Critical,
            All,
        }

        private readonly string _logFilename;

        private readonly bool _append;

        private StreamWriter _logFile;

        private readonly uint _levels;

        public uint Levels { get; set; }


        public Logger
        (string filename, bool append, uint logLevels)
        {
            _logFilename = filename;
            _append = append;
            _levels = logLevels;

            Init();
        }


        private bool Init()
        {
            lock (this)
            {
                // Fail if logfile is void
                if (string.IsNullOrEmpty(_logFilename))
                    return false;

                // Delete log file if it exists
                if (!_append)
                {
                    try
                    {
                        File.Delete(_logFilename);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                // Open file for writing
                if (!File.Exists(_logFilename))
                {
                    try
                    {
                        _logFile = File.CreateText(_logFilename);
                    }
                    catch (Exception)
                    {
                        _logFile = null;
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        _logFile = File.AppendText(_logFilename);
                    }
                    catch (Exception)
                    {
                        _logFile = null;
                        return false;
                    }
                }
                _logFile.AutoFlush = true;

                return true;
            }
        }

        public bool Stop()
        {
            lock (this)
            {
                // Stop logging
                try
                {
                    _logFile.Close();
                    _logFile = null;
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public bool Debug(string msg)
        {
            return WriteLog(Level.Debug, msg);
        }

        public bool Info(string msg)
        {
            return WriteLog(Level.Info, msg);
        }

        public bool Warning(string msg)
        {
            return WriteLog(Level.Warning, msg);
        }

        public bool Error(string msg)
        {
            return WriteLog(Level.Error, msg);
        }

        public bool Fatal(string msg)
        {
            return WriteLog(Level.Critical, msg);
        }

        private bool WriteLog(Level level, string msg)
        {
            lock (this)
            {
                // Ignore message logging is paused or it doesn't pass the filter
                if ((_levels & (uint)level) != (uint)level)
                    return true;

                // Write log message
                DateTime tmNow = DateTime.Now;
                string logMsg = $"{tmNow.ToShortDateString()} {tmNow.ToLongTimeString()}  {level.ToString().Substring(0, 1)}: {msg}";
                _logFile.WriteLine(logMsg);
                return true;
            }
        }

    }
}
