﻿using FluentValidation.Results;

namespace HealthMed.CrossCutting.Notifications;

public class NotificationContext
{
    private readonly List<Notification> _notifications;
    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public bool HasNotifications => _notifications.Any();

    public NotificationContext()
    {
        _notifications = new List<Notification>();
    }

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IReadOnlyCollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(IList<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(ICollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(IEnumerable<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(List<ValidationFailure> notifications)
    {
        _notifications.AddRange(notifications.Select(t => new Notification(t.ErrorCode, t.ErrorMessage)));
    }

    public void AddNotifications(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddNotification(error.ErrorCode, error.ErrorMessage);
        }
    }
}
