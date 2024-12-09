Feature: Admin Api Key Security

A/4/1
=====
Any HTTP request to an Admin Module endpoint must include
the secret Admin API key
as an "X-Api-Key" request header.

    @HappyPath
    Scenario: Admin API key authenticated and authorized
        Given I am a client using the Admin API key
        When I create the following country
        """
        {
            "countryCode": "GB",
            "name": "United Kingdom"
        }
        """
        Then the response status code should be "Ok"

    @SadPath
    Scenario: Public API key authenticated but not authorized
        Given I am a client using the Public API key
        When I create the following country
        """
        {
            "countryCode": "GB",
            "name": "United Kingdom"
        }
        """
        Then the response status code should be "Forbidden"

    @SadPath
    Scenario: Unrecognized API key not authenticated
        Given I am a client using an unrecognized API key
        When I create the following country
        """
        {
            "countryCode": "GB",
            "name": "United Kingdom"
        }
        """
        Then the response status code should be "Unauthorized"

    @SadPath
    Scenario: Missing API key not authenticated
        Given I am a client using no API key
        When I create the following country
        """
        {
            "countryCode": "GB",
            "name": "United Kingdom"
        }
        """
        Then the response status code should be "Unauthorized"
