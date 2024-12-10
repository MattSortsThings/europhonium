Feature: Get Queryable Countries

P/1/3
=====
As a Euro-Fan,
I want to retrieve a list of all the countries in the system,
ordered by country code,
so that I can plan my queries.

    @HappyPath
    Scenario: Countries requested
        Given the following countries exist
          | CountryCode | Name           |
          | XX          | Rest of World  |
          | AT          | Austria        |
          | BE          | Belgium        |
          | CZ          | Czechia        |
          | DE          | Germany        |
          | EE          | Estonia        |
          | FI          | Finland        |
          | GB          | United Kingdom |
          | HR          | Croatia        |
        And I am a client using the Public API key
        When I request all the queryable countries
        Then the response status code should be "Ok"
        And the response content should match expectations
