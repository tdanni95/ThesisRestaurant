﻿namespace ThesisRestaurant.Application.Common.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
