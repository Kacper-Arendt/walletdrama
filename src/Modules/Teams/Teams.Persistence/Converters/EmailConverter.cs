using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Abstractions.ValueObjects;

namespace Teams.Persistence.Converters;

public class EmailConverter() : ValueConverter<Email, string>(
    modelValue => modelValue.Value,
    dbValue => new Email(dbValue)
);