using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Domain.ValueObjects;

namespace Teams.Persistence.Converters;

public class EmailConverter() : ValueConverter<Email, string>(
    modelValue => modelValue.Value,
    dbValue => new Email(dbValue)
);