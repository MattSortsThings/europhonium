Feature: Create Country

A/1/1
As the Admin,
I want to create a new country in the system,
so that I can reference it in contests and broadcasts that I will go on to create.

    @HappyPath
    Scenario: Country created
        Given I am a client using the Admin API key
        When I create the following country
        """
        {
            "countryCode": "GB",
            "name": "United Kingdom"
        }
        """
        Then the response status code should be "Ok"
        And the response content should match expectations

    @SadPath
    Scenario: Invalid country code
        Given I am a client using the Admin API key
        When I create the following country
        """
        {
          "countryCode": "INVALID_COUNTRY_CODE",
          "name": "United Kingdom"
        }
        """
        Then the response status code should be "BadRequest"
        And the response content should match expectations

    @SadPath
    Scenario: Country code conflict
        Given the following countries exist
          | CountryCode | Name           |
          | GB          | United Kingdom |
        And I am a client using the Admin API key
        When I create the following country
        """
        {
            "countryCode": "GB",
            "name": "Royaume-Uni"
        }
        """
        Then the response status code should be "Conflict"
        And the response content should match expectations
