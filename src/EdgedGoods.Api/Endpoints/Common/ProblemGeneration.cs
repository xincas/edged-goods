﻿using ErrorOr;

namespace EdgedGoods.Api.Endpoints.Common;

public static class ProblemGeneration
{
    public static IResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return TypedResults.Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }
    
    private static IResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return TypedResults.Problem(statusCode: statusCode, detail: error.Description);
    }

    private static IResult ValidationProblem(List<Error> errors)
    {
        Dictionary<string, string[]> validationErrorsDictionary = [];
        
        foreach (var error in errors)
        {
            validationErrorsDictionary.TryAdd(
                error.Code,
                [error.Description]);
        }

        return TypedResults.ValidationProblem(validationErrorsDictionary);
    }
}