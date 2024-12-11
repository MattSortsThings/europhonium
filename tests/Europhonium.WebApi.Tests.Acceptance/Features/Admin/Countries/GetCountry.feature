Feature: Get Country

A/1/2
=====
As the Admin,
I want to retrieve a single country from the system,
identified by supplying its ID,
so that I can see the contests and broadcasts in which it is involved.

    @HappyPath
    Scenario: Country requested
        Given the following countries exist
          | CountryCode | Name           |
          | GB          | United Kingdom |
        And I am a client using the Admin API key
        When I request the country with the country code "GB" by its ID
        Then the response status code should be "Ok"
        And the response content should match expectations

    @SadPath
    Scenario: Country not found
        Given I am a client using the Admin API key
        When I request a country that does not exist
        Then the response status code should be "NotFound"
        And the response content should match expectations
