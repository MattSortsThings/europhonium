Feature: Public Api Key Security

A request to a Public API* endpoint must include either the Public API key or the secret Admin API key as an "X-Api-Key" request header value.

    @HappyPath
    Scenario: Client using Admin API key is authenticated and authorized
        Given I am a client using the Admin API key
        When I request 10 mod 3
        Then the response status code should be "Ok"

    @SadPath
    Scenario: Client using Public API key is authenticated and authorized
        Given I am a client using the Public API key
        When I request 10 mod 3
        Then the response status code should be "Ok"

    @SadPath
    Scenario: Client using unrecognized API key is unauthenticated
        Given I am a client using an unrecognized API key
        When I request 10 mod 3
        Then the response status code should be "Unauthorized"

    @SadPath
    Scenario: Client using no API key is unauthenticated
        Given I am a client using no API key
        When I request 10 mod 3
        Then the response status code should be "Unauthorized"
