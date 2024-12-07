# Ways of working

This document is an active list of the design and coding decisions taken during the *Europhonium* project.

- [Ways of working](#ways-of-working)
  - [Testing framework](#testing-framework)
  - [Version control](#version-control)

## Testing framework

The system is developed using **Acceptance Test Driven Development**.

There are four levels of testing:

- **Acceptance tests** verify the behaviour of the system as a whole against feature files written in Gherkin syntax. Tests use a real web API running on an in-memory test server, with a test database running in a Docker container. Tests can only interact with the system using HTTP requests.
- **Integration tests** verify the behaviour each endpoint in each module. The system under test is the `ExecuteAsync` method in the source code feature file. Tests use a real web API running on an in-memory test server, with a test database running in a Docker container.
- **Unit tests** verify the behaviour of the domain entities in isolation.
- **Architecture tests** enforce coding standards across all source code assemblies.

## Version control

Git commit messages adhere to the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) standard.

Features are tagged as follows:

```
Feature: A/1/1
```
