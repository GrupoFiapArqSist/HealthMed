﻿using HealthMed.CrossCutting.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace User.API.Filters;

public class NotificationFilter : IAsyncResultFilter
{
    private readonly NotificationContext _notificationContext;

    public NotificationFilter(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var notifications = JsonSerializer.Serialize(_notificationContext.Notifications);
            await context.HttpContext.Response.WriteAsync(notifications);

            return;
        }

        await next();
    }
}
