Feature: Public Api Key Security

P/6/1
=====
Any HTTP request to a Public Module endpoint must include
EITHER the Public API key
OR the secret Admin API key
as an "X-Api-Key" request header.

    @HappyPath
    Scenario: Public API key authenticated and authorized
        Given I am a client using the Public API key
        When I request all the queryable countries
        Then the response status code should be "Ok"

    @HappyPath
    Scenario: Admin API key authenticated and authorized
        Given I am a client using the Admin API key
        When I request all the queryable countries
        Then the response status code should be "Ok"

    @SadPath
    Scenario: Unrecognized API key not authenticated
        Given I am a client using an unrecognized API key
        When I request all the queryable countries
        Then the response status code should be "Unauthorized"

    @SadPath
    Scenario: Missing API key not authenticated
        Given I am a client using no API key
        When I request all the queryable countries
        Then the response status code should be "Unauthorized"
