using BattleShipMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShipMVC.Models
{
    public class Logger : ILogger
    {
        IBattleShipRepository<LogInfo> _logReposiroty;
        public Logger(IBattleShipRepository<LogInfo> logReposiroty)
        {
            _logReposiroty = logReposiroty;
        }
        public void Log(string message)
        {
            LogInfo logInfo = new LogInfo(message, DateTime.Now);
            _logReposiroty.Create(logInfo);
        }
    }

}