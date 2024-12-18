# Testing Strategy

This document outlines the testing strategy adopted during development of the *Europhonium* project.

- [Testing Strategy](#testing-strategy)
  - [Acceptance tests](#acceptance-tests)
  - [Integration tests](#integration-tests)
  - [Unit tests](#unit-tests)
  - [Architecture tests](#architecture-tests)

## Acceptance tests

The system is developed using **acceptance test driven development**.

When a feature is selected for development, first of all a set of acceptance tests are created. The feature is implemented using integration and unit tests. The feature is completed when all the acceptance tests pass.

An acceptance test is written using Gherkin syntax, for example:

```gherkindotnet
Given I am a client using the Admin API Key
And I have created the following countries
| CountryCode | Name           |
| GB          | United Kingdom |
| HR          | Croatia        |
When I request the country "GB" by its ID
Then the response status code should be "Ok"
And the response content should match expectations
```

Acceptance tests use a web application running on an in-memory test server, with a test database running in a Docker container.

The following rules apply to the acceptance tests:

- All acceptance tests are run sequentially in a single test collection.
- After every acceptance test, the test database is reset to its original empty state.
- Acceptance test methods (including setup and verification) can only interact with the system using HTTP requests and responses.
- The response status code is confirmed.
- The response content is snapshot-tested using Verify.

## Integration tests

Each module has its own integration test project. As with acceptance tests, the integration tests use a web application running on an in-memory test server, with a test database running in a Docker container.

Integration tests for a feature are run against its `ExecuteAsync` method.

For each integration test project:

- All integration tests in the test project are run sequentially in a single test collection.
- The *Admin API* module's integration tests use a web app fixture that resets its test database to its original empty state after each test.
- The *Public API* module's integration tests use a web app fixture that has a test database seeded with sample queryable data.

## Unit tests

The `Europhonium.Shared.Domain` assembly has its own unit test project to verify the behaviour of the domain objects. A large number of parametrized test cases are run.

## Architecture tests

An architecture test project enforces design rules across all the source code assemblies, including internal types.
