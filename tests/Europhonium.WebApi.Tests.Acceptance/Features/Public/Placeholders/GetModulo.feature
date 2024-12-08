Feature: Get Modulo

    Scenario: Modulo calculated
        Given I am a client using the Public API key
        When I request 10 mod 3
        Then the response status code should be "Ok"
