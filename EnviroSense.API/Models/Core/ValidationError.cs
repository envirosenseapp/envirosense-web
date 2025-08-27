using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EnviroSense.API.Models.Core;

public class ValidationError : BaseError
{

    public ValidationError(ModelStateDictionary modelStateDictionary) : base(
        "validation_error",
        "validation failed"
    )
    {
        foreach (var state in modelStateDictionary)
        {
            foreach (var error in state.Value.Errors)
            {

                Context.Add(new Entry(
                    state.Key,
                    error.ErrorMessage
                ));
            }
        }
    }

    public ValidationError(params Entry[] entries) : base(
        "validation_error",
        "validation failed"
    )
    {
        foreach (var entry in entries)
        {
            Context.Add(entry);
        }
    }
}
