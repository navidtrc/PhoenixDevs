﻿using FluentValidation.Results;

namespace Phoenix.Application.Behavior;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>(StringComparer.Ordinal);
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage, StringComparer.Ordinal)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray(), StringComparer.Ordinal);
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public IDictionary<string, string[]>? Errors { get; }
}
