﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Reviso.TimeTracker.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            // Web API routes
            config.MapHttpAttributeRoutes();

        }
    }
}
