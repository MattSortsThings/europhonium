using ErrorOr;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Europhonium.Shared.Domain.Tests.Unit.Utils;

internal sealed class ErrorAssertions : ReferenceTypeAssertions<Error, ErrorAssertions>
{
    public ErrorAssertions(Error subject) : base(subject)
    {
        Identifier = "error";
    }

    protected override string Identifier { get; }

    internal AndConstraint<ErrorAssertions> HaveType(ErrorType expected)
    {
        Execute.Assertion
            .Given(() => Subject.Type)
            .ForCondition(type => type == expected)
            .FailWith("Expected Error.Type to be {0}, but found {1}.", expected, Subject.Type);

        return new AndConstraint<ErrorAssertions>(this);
    }

    internal AndConstraint<ErrorAssertions> HaveCode(string expected)
    {
        Execute.Assertion
            .Given(() => Subject.Code)
            .ForCondition(type => type.Equals(expected, StringComparison.Ordinal))
            .FailWith("Expected Error.Code to be {0}, but found {1}.", expected, Subject.Code);

        return new AndConstraint<ErrorAssertions>(this);
    }

    internal AndConstraint<ErrorAssertions> ContainMetadataKeyValuePair(string key, object value)
    {
        Execute.Assertion
            .Given(() => Subject.Metadata)
            .ForCondition(metadata => metadata is not null)
            .FailWith("Expected Error.Metadata to contain the key-value pair {0}: {1}, but Metadata is null.", key, value)
            .Then
            .ForCondition(metadata => metadata!.ContainsKey(key))
            .FailWith("Expected Error.Metadata to contain the key-value pair {0}: {1}, but key is not present.", key, value)
            .Then
            .ForCondition(metadata => metadata![key] == value)
            .FailWith("Expected Error.Metadata to contain the key-value pair {0}: {1}, but value is different.", key, value);

        return new AndConstraint<ErrorAssertions>(this);
    }
}
