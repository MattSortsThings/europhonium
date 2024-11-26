Feature: Get Modulo

    Scenario: Requesting modulo calculation
        Given I am a client using no API key
        When I request 10 mod 4
        Then the response status code should be "Ok"
