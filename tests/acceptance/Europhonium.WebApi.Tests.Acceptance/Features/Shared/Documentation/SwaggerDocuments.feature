Feature: Swagger Documents

The system will serve two Swagger documents in development and production:
one for the Admin API
and one for the Public API

    @HappyPath
    Scenario: Swagger endpoint
        Given I am a client using no API key
        When I request the Swagger page
        Then the response status code should be "Ok"

    @HappyPath
    Scenario: Admin API Swagger document
        Given I am a client using no API key
        When I request the "admin-v1" Swagger document
        Then the response status code should be "Ok"

    @HappyPath
    Scenario: Public API Swagger document
        Given I am a client using no API key
        When I request the "public-v1" Swagger document
        Then the response status code should be "Ok"
