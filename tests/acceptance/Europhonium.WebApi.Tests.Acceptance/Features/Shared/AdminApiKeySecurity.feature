Feature: Admin Api Key Security

A request to an Admin API endpoint must include the secret Admin API key as an "X-Api-Key" request header value.

    @HappyPath
    Scenario: Client using Admin API key is authenticated and authorized
        Given I am a client using the Admin API key
        When I request 5 greetings
        Then the response status code should be "Ok"

    @SadPath
    Scenario: Client using Public API key is authenticated but unauthorized
        Given I am a client using the Public API key
        When I request 5 greetings
        Then the response status code should be "Forbidden"

    @SadPath
    Scenario: Client using unrecognized API key is unauthenticated
        Given I am a client using an unrecognized API key
        When I request 5 greetings
        Then the response status code should be "Unauthorized"

    @SadPath
    Scenario: Client using no API key is unauthenticated
        Given I am a client using no API key
        When I request 5 greetings
        Then the response status code should be "Unauthorized"
