namespace EnviroSense.Domain.Exceptions;

public class MeasurementsForThisDayNotFoundException : Exception
{
    public MeasurementsForThisDayNotFoundException() : base("The measurements for this day are not found.") { }
}
