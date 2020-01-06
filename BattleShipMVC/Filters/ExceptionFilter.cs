using BattleShipMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BattleShipMVC.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        ILogger _logger;
        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            _logger.Log(filterContext.Exception.Message);
        }
    }
}