using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mu.Api.Infrastructure;

public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter() : base(
        v => v.ToUniversalTime(),
        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
    )
    { }
}