﻿/*
#region

using FluentAssertions;
using NUnit.Framework;
using TheHub.Application.Common.Exceptions;

#endregion

namespace TheHub.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        List<ValidationFailure> failures = new() { new ValidationFailure("Age", "must be over 18") };

        IDictionary<string, string[]> actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo("Age");
        actual["Age"].Should().BeEquivalentTo("must be over 18");
    }

    [Test]
    public void
        MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        List<ValidationFailure> failures = new List<ValidationFailure>
        {
            new("Age", "must be 18 or older"),
            new("Age", "must be 25 or younger"),
            new("Password", "must contain at least 8 characters"),
            new("Password", "must contain a digit"),
            new("Password", "must contain upper case letter"),
            new("Password", "must contain lower case letter")
        };

        IDictionary<string, string[]> actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo("Password", "Age");

        actual["Age"].Should().BeEquivalentTo("must be 25 or younger", "must be 18 or older");

        actual["Password"].Should().BeEquivalentTo("must contain lower case letter", "must contain upper case letter",
            "must contain at least 8 characters", "must contain a digit");
    }
}
*/
